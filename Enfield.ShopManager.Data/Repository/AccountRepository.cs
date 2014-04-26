using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enfield.ShopManager.Data.Graph;
using NHibernate.Criterion;
using Enfield.ShopManager.Data.Query;

namespace Enfield.ShopManager.Data.Repository
{
    public class AccountRepository : RepositoryBase
    {
        public Account GetAccount(int accountId)
        {
            var account = Session.QueryOver<Account>()
                .Where(a => a.Id == accountId).SingleOrDefault();
            return account;
        }

        public Account GetAccount(string name)
        {
            var account = Session.QueryOver<Account>()
                .Where(a => a.Name == name).List().FirstOrDefault();
            return account;
        }

        public IList<Account> GetAccounts()
        {
            var accounts = Session.QueryOver<Account>()
                .Where(a => a.Name != null)
                .OrderBy(o => o.Name).Asc
                .List();
            return accounts;
        }

        public IList<Account> GetAccounts(string filter)
        {
            var accounts = Session.QueryOver<Account>()
               .Where(Restrictions.On<Account>(x => x.Name).IsLike(filter + "%"))
               .OrderBy(o => o.Name).Asc
               .List();
            return accounts;
        }

        public IList<Account> GetAccounts(AccountQuery query)
        {
            var criteria = Session.CreateCriteria<Account>();

            if (!string.IsNullOrEmpty(query.AccountName)) criteria.Add(Expression.Like("Name", query.AccountName + "%"));
            if (query.AccountTypeId.HasValue) criteria.CreateAlias("AccountType", "a").Add(Expression.Eq("a.Id", query.AccountTypeId.Value));

            if (string.IsNullOrEmpty(query.SortBy))
                criteria.AddOrder(new Order("Id", true));
            else
                criteria.AddOrder(new Order(query.SortBy, (query.SortDirection != null && query.SortDirection.Equals("asc", StringComparison.InvariantCultureIgnoreCase))));

            return criteria.List<Account>();
        }

        public Account SaveAccount(Account account)
        {
            Session.SaveOrUpdate(account);
            return account;
        }

        public IList<ContactType> GetContactTypes()
        {
            return Session.QueryOver<ContactType>().List();
        }
    }
}
