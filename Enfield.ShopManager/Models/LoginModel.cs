using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using System.Web.Mvc;

namespace Enfield.ShopManager.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            DowngradeRole = true;
        }

        private string password;

        [Display(Name = "Login Name")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(password))
                    return "*empty*";
                return password;
            }
            set { password = value; }
        }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Display(Name = "This a shared shop computer")]
        public bool DowngradeRole { get; set; }

        public string ReturnUrl { get; set; }
    }
}