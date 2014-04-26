using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enfield.ShopManager.Services
{
    public class PasswordGenerator
    {
        private static string[] words = { 
            "ford", 
            "chevy", 
            "dodge", 
            "honda", 
            "toyota", 
            "lexus", 
            "buick", 
            "merc", 
            "alabama", 
            "alaska", 
            "arizona", 
            "florida", 
            "georgia", 
            "idaho", 
            "iowa", 
            "kansas", 
            "maine", 
            "montana", 
            "nevada", 
            "ohio", 
            "oregon", 
            "texas", 
            "utah", 
            "vermont", 
            "wyoming", 
            "black", 
            "blue", 
            "brick", 
            "brown", 
            "cadet", 
            "copper", 
            "peach", 
            "forest", 
            "gold", 
            "gray", 
            "green", 
            "lemon", 
            "maroon", 
            "melon", 
            "navy", 
            "olive", 
            "orange", 
            "pine", 
            "plum", 
            "umber", 
            "red", 
            "salmon", 
            "sea", 
            "sepia", 
            "sky", 
            "silver", 
            "spring", 
            "tan", 
            "violet", 
            "white", 
            "yellow" 
        };

        [ThreadStatic()]
        private static Random random = new Random();

        public static string GeneratePassword()
        {
            if (random == null) random = new Random();
            return words[random.Next(0, words.Length - 1)] + random.Next(0, 999).ToString("000");
        }
    }

}