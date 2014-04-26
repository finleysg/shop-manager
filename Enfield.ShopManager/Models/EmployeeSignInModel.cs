using System;

namespace Enfield.ShopManager.Models
{
    public class EmployeeSignInModel
    {
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime SignInDate { get; set; }
        public DateTime SignOutDate { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FullName)) return Name;
                else return FullName;
            }
        }

        public string FormattedSignInDate
        {
            get
            {
                return SignInDate.ToShortTimeString();
            }
        }

        public string FormattedSignOutDate
        {
            get
            {
                if (SignOutDate == DateTime.MinValue) return "missing";
                return SignOutDate.ToShortTimeString();
            }
        }

        public string Hours
        {
            get
            {
                if (SignOutDate == DateTime.MinValue) return "unknown";
                var duration = SignOutDate.Subtract(SignInDate);
                return string.Format("{0} hrs, {1} min", duration.Hours, duration.Minutes);
            }
        }
    }
}