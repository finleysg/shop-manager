using System.Linq;
using System.Web.Mvc;
using Castle.Core.Logging;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Security;
using Enfield.ShopManager.Services;
using Enfield.ShopManager.Filters;
using System;
using Enfield.ShopManager.Helpers;

namespace Enfield.ShopManager.Controllers
{
    [BuildMenu()]
    [ValidateToken("Employee,Manager,Administrator")]
    public class AccountsController : RestrictedControllerBase
    {

        public ActionResult Index()
        {
            return RedirectToAction("AccountListing");
        }

        public ActionResult AccountListing(AccountFilterModel filter)
        {
            var listing = AccountServices.GetAccountListing(filter);

            ViewBag.SizeOptions = LookupServices.GetSizeOptions(filter.Size);
            ViewBag.AccountTypeOptions = AccountTypeServices.GetAccountTypes(true);

            if (listing.AccountList.Count == 0)
                return View("AccountListingNoData", listing);
            else
                return View("AccountListing", listing);
        }

        public ActionResult EditAccount(int id, AccountFilterModel filter)
        {
            var account = AccountServices.GetAccount(id);

            var model = new AccountDetailModel()
            {
                Action = "EditAccount",
                Account = account,
                Filter = filter,
            };

            ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false, account.AccountTypeId);
            ViewBag.StateCodes = LookupServices.GetStateOptions(account.StateCode);

            return View("AccountDetail", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAccount(int id, AccountFilterModel filter, FormCollection collection)
        {
            var account = AccountServices.GetAccount(id);

            try
            {
                UpdateModel(account);
                AccountServices.UpdateAccount(account);

                return RedirectToAction("AccountListing", filter.GenerateAccountListingRoute());
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
                var model = new AccountDetailModel()
                {
                    Action = "EditAccount",
                    Account = account,
                    Filter = filter
                };

                ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false, account.AccountTypeId);
                ViewBag.StateCodes = LookupServices.GetStateOptions(account.StateCode);

                return View("AccountDetail", model);
            }
        }

        public ActionResult NewAccount(AccountFilterModel filter)
        {
            var model = new AccountDetailModel()
            {
                Action = "NewAccount",
                Account = new AccountModel(),
                Filter = filter,
            };

            ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false);
            ViewBag.StateCodes = LookupServices.GetStateOptions("TN");

            return View("AccountDetail", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewAccount(AccountFilterModel filter, FormCollection collection)
        {
            var account = new AccountModel();
            try
            {
                UpdateModel(account);
                AccountServices.CreateAccount(account);

                return RedirectToAction("AccountListing", filter.GenerateAccountListingRoute());
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
                var model = new AccountDetailModel()
                {
                    Action = "NewAccount",
                    Account = account,
                    Filter = filter
                };

                ViewBag.AccountTypes = AccountTypeServices.GetAccountTypes(false, account.AccountTypeId);
                ViewBag.StateCodes = LookupServices.GetStateOptions(account.StateCode);

                return View("AccountDetail", model);
            }
        }

        public ActionResult EditContact(int id, int contactId, AccountFilterModel filter)
        {
            var account = AccountServices.GetAccount(id);

            var model = new ContactDetailModel()
            {
                Action = "EditContact",
                Contact = account.ContactList.Where(c => c.Id == contactId).FirstOrDefault(),
                Filter = filter,
            };

            ViewBag.ContactTypes = AccountServices.GetContactTypes(model.Contact.ContactTypeId);

            return View("ContactDetail", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditContact(int id, int contactId, AccountFilterModel filter, FormCollection collection)
        {
            var account = AccountServices.GetAccount(id);
            var contact = account.ContactList.Where(c => c.Id == contactId).FirstOrDefault();

            try
            {
                // Need to specify properties because the id (account) overwrites Contact.Id
                UpdateModel(contact, new string[] { "ContactTypeId", "ContactDetail", "FirstName", "LastName" });
                AccountServices.UpdateContact(account, contact);

                return RedirectToAction("EditAccount", filter.GenerateAccountDetailRoute(id));
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);

                var model = new ContactDetailModel()
                {
                    Action = "EditContact",
                    Contact = contact,
                    Filter = filter,
                };

                ViewBag.ContactTypes = AccountServices.GetContactTypes(contact.ContactTypeId);

                return View("ContactDetail", model);
            }
        }

        public ActionResult NewContact(int id, AccountFilterModel filter)
        {
            var account = AccountServices.GetAccount(id);

            var model = new ContactDetailModel()
            {
                Action = "NewContact",
                Contact = new ContactModel() { AccountId = id },
                Filter = filter,
            };

            ViewBag.ContactTypes = AccountServices.GetContactTypes(model.Contact.ContactTypeId);

            return View("ContactDetail", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewContact(int id, AccountFilterModel filter, FormCollection collection)
        {
            var account = AccountServices.GetAccount(id);
            var contact = new ContactModel() { AccountId = id };

            try
            {
                // Need to specify properties because the id (account) overwrites Contact.Id
                UpdateModel(contact, new string[] { "ContactTypeId", "ContactDetail", "FirstName", "LastName" });
                AccountServices.AddContact(account, contact);

                return RedirectToAction("EditAccount", filter.GenerateAccountDetailRoute(id));
            }
            catch (Exception ex)
            {
                // Invalid - redisplay with errors
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);

                var model = new ContactDetailModel()
                {
                    Action = "NewContact",
                    Contact = contact,
                    Filter = filter,
                };

                ViewBag.ContactTypes = AccountServices.GetContactTypes(contact.ContactTypeId);

                return View("ContactDetail", model);
            }
        }

        public ActionResult GetAccounts(string startsWith, int limit)
        {
            var accounts = AccountServices.SearchAccounts(startsWith);
            var result = accounts.Take<AccountSearchModel>(limit).ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountType(int accountId)
        {
            var account = AccountServices.GetAccount(accountId);
            var id = (account == null) ? 0 : account.AccountTypeId;
            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}
