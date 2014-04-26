using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Enfield.ShopManager.Data.Graph;

namespace Enfield.ShopManager.Helpers
{
    public class Formatter
    {
        public static string WhoAmI()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Name={0}", HttpContext.Current.User.Identity.Name);
            if (HttpContext.Current.User.IsInRole("Dealer"))
                sb.Append("|Dealer");
            if (HttpContext.Current.User.IsInRole("Employee"))
                sb.Append("|Employee");
            if (HttpContext.Current.User.IsInRole("Manager"))
                sb.Append("|Manager");
            if (HttpContext.Current.User.IsInRole("Administrator"))
                sb.Append("|Administrator");
            return sb.ToString();
        }

        public static string GetTime(int minutes)
        {
            if (minutes < 60) return string.Format("{0}m", minutes);
            else
            {
                return string.Format("{0}h, {1}m", minutes / 60, minutes % 60);
            }
        }

        public static string GetTime(DateTime startDate, DateTime? endDate)
        {
            if (endDate.HasValue)
            {
                int min = (int)endDate.Value.Subtract(startDate).TotalMinutes;
                return string.Format("{0}h, {1}m", min / 60, min % 60);
            }
            else
            {
                return "0";
            }
        }

        public static string GetVehicleColor(string vehicleColor)
        {
            if (string.IsNullOrEmpty(vehicleColor))
                return "Transparent";

            string bg;
            switch (vehicleColor.ToUpper())
            {
                case "CHARCOAL":
                    bg = "DarkSlateGray";
                    break;
                case "CHESTNUT":
                    bg = "Moccasin";
                    break;
                case "LAVA":
                    bg = "SlateGray";
                    break;
                case "SILVERSTONE":
                    bg = "Silver";
                    break;
                case "RUST":
                    bg = "DarkRed";
                    break;
                case "STEEL BLUE":
                    bg = "SteelBlue";
                    break;
                case "ROSE":
                    bg = "RosyBrown";
                    break;
                case "PLUM":
                    bg = "Plum";
                    break;
                case "COPPER":
                    bg = "DarkOrchid";
                    break;
                case "AMBER":
                    bg = "Goldenrod";
                    break;
                case "CRANBERRY":
                    bg = "Crimson";
                    break;
                case "VAPOR":
                    bg = "WhiteSmoke";
                    break;
                case "MERLOT":
                    bg = "DarkRed";
                    break;
                case "CHAMPAGNE":
                    bg = "BlanchedAlmond";
                    break;
                case "PEARL":
                    bg = "Gainsboro";
                    break;
                case "CREAM":
                    bg = "Beige";
                    break;
                case "WINE":
                    bg = "Maroon";
                    break;
                case "KHAKI":
                    bg = "Khaki";
                    break;
                case "BRONZE":
                    bg = "Chocolate";
                    break;
                case "ALMOND":
                    bg = "BlanchedAlmond";
                    break;
                case "SHALE":
                    bg = "Gray";
                    break;
                case "SAGE":
                    bg = "PaleGreen";
                    break;
                case "TEAL":
                    bg = "Teal";
                    break;
                case "GARNET":
                    bg = "Magenta";
                    break;
                case "BURGANDY":
                    bg = "DarkRed";
                    break;
                case "BURG":
                    bg = "DarkRed";
                    break;
                case "ORANGE":
                    bg = "Orange";
                    break;
                case "SAND":
                    bg = "SandyBrown";
                    break;
                case "GRAPHITE":
                    bg = "LightSlateGray";
                    break;
                case "PURPLE":
                    bg = "Purple";
                    break;
                case "STEEL":
                    bg = "SteelBlue";
                    break;
                case "SIENNA":
                    bg = "Sienna";
                    break;
                case "BROWN":
                    bg = "Brown";
                    break;
                case "TAUPE":
                    bg = "Beige";
                    break;
                case "YELLOW":
                    bg = "Yellow";
                    break;
                case "PEWTER":
                    bg = "SlateGray";
                    break;
                case "TAN":
                    bg = "Tan";
                    break;
                case "GREY":
                    bg = "Gray";
                    break;
                case "MAROON":
                    bg = "Maroon";
                    break;
                case "GOLD":
                    bg = "Gold";
                    break;
                case "GRAY":
                    bg = "Gray";
                    break;
                case "BEIGE":
                    bg = "Beige";
                    break;
                case "GREEN":
                    bg = "MediumSeaGreen";
                    break;
                case "RED":
                    bg = "Red";
                    break;
                case "BLUE":
                    bg = "Blue";
                    break;
                case "WHITE":
                    bg = "GhostWhite";
                    break;
                case "WHT":
                    bg = "GhostWhite";
                    break;
                case "SILVER":
                    bg = "Silver";
                    break;
                case "BLACK":
                    bg = "Black";
                    break;
                case "BLK":
                    bg = "Black";
                    break;
                default:
                    bg = "Transparent";
                    break;
            }

            return bg;
        }

        public static string GetVehicleTextColor(string vehicleColor)
        {
            if (string.IsNullOrEmpty(vehicleColor))
                return "Black";

            switch (vehicleColor.ToUpper())
            {
                case "CHARCOAL":
                case "RUST":
                case "MERLOT":
                case "WINE":
                case "BURGANDY":
                case "BURG":
                case "PURPLE":
                case "MAROON":
                case "BLUE":
                case "BLACK":
                case "BLK":
                    return "White";
                default:
                    return "Black";
            }
        }

        public static string GetAmountDue(Invoice invoice)
        {
            decimal due = (from s in invoice.ServiceList
                           select s.Rate).Sum();
            return due.ToString("C2");
        }

        public static string GetStatementTotal(IEnumerable<Invoice> invoiceList)
        {
            decimal total = (from i in invoiceList
                             select (from s in i.ServiceList
                                     select s.Rate).Sum()).Sum();
            return total.ToString("C2");
        }

        public static string GetLaborTotal(IEnumerable<Labor> laborList)
        {
            decimal total = (from l in laborList select l.ActualRate).Sum();
            return total.ToString("C2");
        }

        //public static string GetServiceTotal(IEnumerable<ServiceTotal> serviceList)
        //{
        //    decimal total = (from s in serviceList select s.Total).Sum();
        //    return total.ToString("C2");
        //}

        //public static string GetServiceCount(IEnumerable<ServiceTotal> serviceList)
        //{
        //    int total = (from s in serviceList select s.Cars).Sum();
        //    return total.ToString();
        //}

        public static string GetInvoiceId(Labor l)
        {
            return l.Invoice.Id.ToString();
        }
    }
}