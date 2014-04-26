namespace Enfield.ShopManager.Models
{
    public class PublicMenuModel : MenuModel
    {
        private string page;

        public PublicMenuModel(string page)
        {
            this.page = page;
        }

        protected override void BuildSubMenus()
        {
            SubMenu.Add(new MenuItem("home-menuitem", "HOME")
            {
                HelpText = "Enfield's Detail Home Page",
                Controller = "Public",
                Action = "Index",
                IsSelected = (page == "Home")
            });
            SubMenu.Add(new MenuItem("services-menuitem", "SERVICES")
            {
                HelpText = "Enfield's Detail Services Page",
                Controller = "Public",
                Action = "Services",
                IsSelected = (page == "Services")
            });
            SubMenu.Add(new MenuItem("gallery-menuitem", "GALLERY")
            {
                HelpText = "Enfield's Detail Gallery Page",
                Controller = "Public",
                Action = "Gallery",
                IsSelected = (page == "Gallery")
            });
            SubMenu.Add(new MenuItem("contact-menuitem", "CONTACT")
            {
                HelpText = "Enfield's Detail Contact Page",
                Controller = "Public",
                Action = "Contact",
                IsSelected = (page == "Contact")
            });
        }
    }

}