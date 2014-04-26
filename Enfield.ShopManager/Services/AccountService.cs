using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Models;
using AutoMapper;
using PagedList;
using System.Web.Mvc;

namespace Enfield.ShopManager.Services
{
    public class AccountService : DomainServiceBase
    {
        public List<SelectListItem> GetContactTypes(int? selected = null)
        {
            var contactTypes = AccountRepository.GetContactTypes();

            List<SelectListItem> lookup =
                contactTypes.Select(l => new SelectListItem() { Value = l.Id.ToString(), Text = l.Description }).ToList();

            if (selected.HasValue)
            {
                var selectedItem = lookup.Where(l => l.Value == selected.Value.ToString()).FirstOrDefault();
                if (selectedItem != null) selectedItem.Selected = true;
            }

            return lookup;
        }
        
        public AccountListingModel GetAccountListing(AccountFilterModel filter)
        {
            if (filter == null) filter = new AccountFilterModel();

            var query = Mapper.Map<AccountFilterModel, Data.Query.AccountQuery>(filter);
            var accounts = AccountRepository.GetAccounts(query).OrderBy(o => o.Name).ToList();
            var accountList = Mapper.Map<IList<Data.Graph.Account>, IList<AccountModel>>(accounts);

            AccountListingModel model = new AccountListingModel();
            model.Filter = filter;
            model.AccountList = accountList.ToPagedList(filter.Page, filter.Size);

            return model;
        }

        public SelectList GetAccountLookupWithBalanceDue(string accountType)
        {
            var accounts = ReportRepository.GetAccountsWithOpenBalances(accountType)
                .Select(d => new { Id = d.AccountId, Name = string.Format("{0} ({1})", d.AccountName, d.BalanceDue.ToString("C2")) });
            return new SelectList(accounts, "Id", "Name");
        }

        public AccountModel GetAccount(int id)
        {
            var account = AccountRepository.GetAccount(id);
            if (account == null) return null;

            return Mapper.Map<Data.Graph.Account, AccountModel>(account);
        }

        public AccountModel GetAccount(string name)
        {
            var account = AccountRepository.GetAccount(name);
            if (account == null) return null;

            return Mapper.Map<Data.Graph.Account, AccountModel>(account);
        }

        public AccountModel CreateAccount(AccountModel account)
        {
            var graph = new Data.Graph.Account();
            graph.AccountType = AccountTypeRepository.GetAccountType(account.AccountTypeId);
            graph.AddressLine1 = account.AddressLine1;
            graph.AddressLine2 = account.AddressLine2;
            graph.City = account.City;
            graph.Name = account.Name;
            graph.Notes = account.Notes;
            graph.PostalCode = account.PostalCode;
            graph.StateCode = account.StateCode;

            AccountRepository.SaveAccount(graph);

            return Mapper.Map<Data.Graph.Account, AccountModel>(graph);
        }

        public AccountModel UpdateAccount(AccountModel account)
        {
            var graph = AccountRepository.GetAccount(account.Id);
            graph.AccountType = AccountTypeRepository.GetAccountType(account.AccountTypeId);
            graph.AddressLine1 = account.AddressLine1;
            graph.AddressLine2 = account.AddressLine2;
            graph.City = account.City;
            graph.Name = account.Name;
            graph.Notes = account.Notes;
            graph.PostalCode = account.PostalCode;
            graph.StateCode = account.StateCode;

            AccountRepository.SaveAccount(graph);

            return Mapper.Map<Data.Graph.Account, AccountModel>(graph);
        }

        public AccountModel UpdateContact(AccountModel account, ContactModel contact)
        {
            var accountGraph = AccountRepository.GetAccount(account.Id);
            var contactGraph = accountGraph.ContactList.Where(c => c.Id == contact.Id).First();

            contactGraph.ContactDetail = contact.ContactDetail;
            contactGraph.ContactType = AccountRepository.GetContactTypes().Where(t => t.Id == contact.ContactTypeId).First();
            contactGraph.FirstName = contact.FirstName;
            contactGraph.LastName = contact.LastName;

            AccountRepository.SaveAccount(accountGraph);

            return Mapper.Map<Data.Graph.Account, AccountModel>(accountGraph);
        }

        public AccountModel AddContact(AccountModel account, ContactModel contact)
        {
            var accountGraph = AccountRepository.GetAccount(account.Id);
            accountGraph.AddContact(new Data.Graph.Contact() 
            {
                ContactDetail = contact.ContactDetail,
                ContactType = AccountRepository.GetContactTypes().Where(t => t.Id == contact.ContactTypeId).First(),
                FirstName = contact.FirstName,
                LastName = contact.LastName
            });

            AccountRepository.SaveAccount(accountGraph);

            return Mapper.Map<Data.Graph.Account, AccountModel>(accountGraph);
        }

        public List<AccountSearchModel> SearchAccounts(string query)
        {

            var accounts = (AccountSearchModel[])HttpRuntime.Cache["accounts"];
            if (accounts == null)
            {
                var allAccounts = AccountRepository.GetAccounts();
                accounts = Mapper.Map<IList<Data.Graph.Account>, AccountSearchModel[]>(allAccounts);
                HttpRuntime.Cache["accounts"] = accounts;
            }

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToUpper().Replace(" ", "");
                return accounts.Where(a => a.Name.ToUpper().Replace(" ", "").StartsWith(query)).ToList();
            }
            return accounts.ToList();
        }
    }
}