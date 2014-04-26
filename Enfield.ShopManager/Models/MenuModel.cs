using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enfield.ShopManager.Controllers;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public abstract class MenuModel
    {
        protected const string ShopFloorController = "ShopFloor";
        protected const string AccountController = "Accounts";
        protected const string ReportController = "Reporting";
        protected const string AdminController = "Administration";

        public static MenuModel Create(object model, string controllerName, string actionName, string roleName)
        {
            MenuModel menu;

            switch (controllerName)
            {
                case ShopFloorController:
                    menu = new ShopFloorMenuModel();
                    break;
                case AccountController:
                    menu = new AccountsMenuModel();
                    break;
                case AdminController:
                    menu = new AdministrationMenuModel();
                    break;
                case ReportController:
                    menu = new ReportingMenuModel();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            menu.Model = model;
            menu.ControllerName = controllerName;
            menu.ActionName = actionName;
            menu.RoleName = roleName;

            return menu;
        }

        public List<MenuItem> MainMenu { get; set; }
        public List<MenuItem> SubMenu { get; set; }

        protected MenuModel()
        {
            MainMenu = new List<MenuItem>();
            SubMenu = new List<MenuItem>();
        }

        protected object Model { get; set; }
        protected string ControllerName { get; set; }
        protected string ActionName { get; set; }
        protected string RoleName { get; set; }

        public void BuildMenus()
        {
            BuildMainMenu();
            BuildSubMenus();
        }

        protected abstract void BuildSubMenus();

        protected void BuildMainMenu()
        {
            MainMenu.Add(new MenuItem("shop-floor-menu", "Shop Floor")
            {
                HelpText = "Log services and labor for vehicles in the shop",
                Controller = ShopFloorController,
                IsSelected = (ControllerName == ShopFloorController)
            });
            MainMenu.Add(new MenuItem("accounts-menu", "Accounts")
            {
                HelpText = "Add or edit dealer and private accounts",
                Controller = AccountController,
                IsSelected = (ControllerName == AccountController)
            });
            MainMenu.Add(new MenuItem("reports-menu", "Reporting")
            {
                HelpText = "View and print reports",
                Controller = ReportController,
                IsSelected = (ControllerName == ReportController),
                IsVisible = (RoleName == "Administrator" || RoleName == "Manager")
            });
            MainMenu.Add(new MenuItem("admin-menu", "Administration")
            {
                HelpText = "Perform administrative tasks for the shop manager system",
                Controller = AdminController,
                IsSelected = (ControllerName == AdminController),
                IsVisible = (RoleName == "Administrator")
            });
        }
    }

    public class ShopFloorMenuModel : MenuModel
    {
        private InvoiceModel CurrentInvoice { get; set; }

        protected override void BuildSubMenus()
        {
            var model = Model as ShopFloorModel;
            if (model != null) CurrentInvoice = model.Invoice;

            SubMenu.Add(new MenuItem("new-vehicle-menuitem", "New Vehicle")
            {
                HelpText = "Log a new vehicle into the current location",
                Controller = ShopFloorController,
                Action = "NewVehicle",
                IsSelected = (ActionName == "NewVehicle")
            });
            SubMenu.Add(new MenuItem("print-invoice-menuitem", "Print Invoice")
            {
                HelpText = "Open an invoice report for the current vehicle",
                Controller = ReportController,
                Action = "InvoiceReport",
                IsEnabled = (CurrentInvoice != null)
            });
            SubMenu.Add(new MenuItem("complete-vehicle-menuitem", "Complete Vehicle")
            {
                HelpText = "Flag the current vehicle as completed",
                Controller = ShopFloorController,
                Action = "CompleteVehicle",
                IsEnabled = (CurrentInvoice != null),
                IsVisible = (CurrentInvoice != null && !CurrentInvoice.IsComplete)
            });
            SubMenu.Add(new MenuItem("recall-vehicle-menuitem", "Recall Vehicle")
            {
                HelpText = "Recall the current vehicle back to the shop",
                Controller = ShopFloorController,
                Action = "RecallVehicle",
                IsEnabled = (CurrentInvoice != null),
                IsVisible = (CurrentInvoice != null && CurrentInvoice.IsComplete)
            });
            SubMenu.Add(new MenuItem("add-history-menuitem", "Add Note")
            {
                HelpText = "Add a note to the history of the current vehicle",
                Controller = ShopFloorController,
                Action = "AddHistory",
                IsEnabled = (CurrentInvoice != null && !string.IsNullOrEmpty(CurrentInvoice.StockNumber))
            });
            SubMenu.Add(new MenuItem("delete-invoice-menuitem", "Delete Invoice")
            {
                HelpText = "Permanently delete the current invoice",
                Controller = ShopFloorController,
                Action = "DeleteInvoice",
                IsEnabled = (CurrentInvoice != null),
                IsVisible = (RoleName == "Administrator")
            });
            SubMenu.Add(new MenuItem("find-invoice-menuitem", "Search")
            {
                HelpText = "Find a vehicle by invoice number or stock number",
                Controller = ShopFloorController,
                Action = "FindInvoice"
            });
            SubMenu.Add(new MenuItem("sign-in-menuitem", "Sign In")
            {
                HelpText = "Sign in to the shop at the current location",
                Controller = ShopFloorController,
                Action = "SignIn"
            });
            SubMenu.Add(new MenuItem("sign-out-menuitem", "Sign Out")
            {
                HelpText = "Sign out of the shop at the current location",
                Controller = ShopFloorController,
                Action = "SignOut"
            });
        }
    }

    public class AdministrationMenuModel : MenuModel
    {
        protected override void BuildSubMenus()
        {
            SubMenu.Add(new MenuItem("invoice-listing-menuitem", "Invoices")
            {
                HelpText = "View and administrate invoices and invoice detail",
                Controller = AdminController,
                Action = "InvoiceListing",
                IsSelected = (ActionName == "InvoiceListing")
            });
            SubMenu.Add(new MenuItem("user-listing-menuitem", "Employees")
            {
                HelpText = "Add new users or change something about a current user",
                Controller = AdminController,
                Action = "UserListing",
                IsSelected = (ActionName == "UserListing")
            });
            SubMenu.Add(new MenuItem("location-listing-menuitem", "Locations")
            {
                HelpText = "Add a new location or change an existing location",
                Controller = AdminController,
                Action = "LocationListing",
                IsSelected = (ActionName == "LocationListing")
            });
            SubMenu.Add(new MenuItem("services-and-labor-menuitem", "Services and Labor")
            {
                HelpText = "Add or edit service types and/or labor types",
                Controller = AdminController,
                Action = "ServicesAndLabor",
                IsSelected = (ActionName == "ServicesAndLabor")
            });
            SubMenu.Add(new MenuItem("security-log-menuitem", "Security Log")
            {
                HelpText = "View login attempts",
                Controller = AdminController,
                Action = "SecurityLog",
                IsSelected = (ActionName == "SecurityLog")
            });
            SubMenu.Add(new MenuItem("error-log-menuitem", "Error Log")
            {
                HelpText = "View the Elmah log",
                Controller = AdminController,
                Action = "ErrorLog",
                IsSelected = (ActionName == "ErrorLog")
            });
        }
    }

    public class AccountsMenuModel : MenuModel
    {
        protected override void BuildSubMenus()
        {
            SubMenu.Add(new MenuItem("account-listing-menuitem", "All Accounts")
            {
                HelpText = "Search existing accounts",
                Controller = AccountController,
                Action = "AccountListing",
                IsSelected = (ActionName == "AccountListing")
            });
            SubMenu.Add(new MenuItem("new-account-menuitem", "New Account")
            {
                HelpText = "Add a new account",
                Controller = AccountController,
                Action = "NewAccount",
                IsSelected = (ActionName == "NewAccount")
            });
        }
    }

    public class ReportingMenuModel : MenuModel
    {
        protected override void BuildSubMenus()
        {
            SubMenu.Add(new MenuItem("dealer-statements-menuitem", "Dealer Statements")
            {
                HelpText = "Print a dealer statements report",
                Controller = ReportController,
                Action = "DealerStatements",
                IsSelected = (ActionName == "DealerStatements")
            });
            SubMenu.Add(new MenuItem("private-statements-menuitem", "Private Statements")
            {
                HelpText = "Print a private statements report",
                Controller = ReportController,
                Action = "PrivateStatements",
                IsSelected = (ActionName == "PrivateStatements")
            });
            SubMenu.Add(new MenuItem("dealer-totals-menuitem", "Dealer Totals")
            {
                HelpText = "Print a dealer totals report",
                Controller = ReportController,
                Action = "DealerTotals",
                IsSelected = (ActionName == "DealerTotals")
            });
            SubMenu.Add(new MenuItem("payroll-menuitem", "Payroll")
            {
                HelpText = "Print a payroll report",
                Controller = ReportController,
                Action = "Payroll",
                IsSelected = (ActionName == "Payroll")
            });
            SubMenu.Add(new MenuItem("daily-log-menuitem", "Daily Log")
            {
                HelpText = "Print a daily log report",
                Controller = ReportController,
                Action = "DailyLog",
                IsSelected = (ActionName == "DailyLog")
            });
            SubMenu.Add(new MenuItem("invoice-report-menuitem", "Invoice")
            {
                HelpText = "Print or reprint an invoice",
                Controller = ReportController,
                Action = "Invoice",
                IsSelected = (ActionName == "Invoice")
            });
            SubMenu.Add(new MenuItem("employee-log-menuitem", "Employee Log")
            {
                HelpText = "Print an employee log report",
                Controller = ReportController,
                Action = "EmployeeLog",
                IsSelected = (ActionName == "EmployeeLog")
            });
        }
    }

    public class MenuItem
    {
        public MenuItem(string id, string text)
        {
            Id = id;
            DisplayText = text;
            Action = "Index";
            IsEnabled = true;
            IsVisible = true;
        }

        public string Id { get; set; }
        public string DisplayText { get; set; }
        public string HelpText { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Boolean IsSelected { get; set; }
        public Boolean IsEnabled { get; set; }
        public Boolean IsVisible { get; set; }
    }
}