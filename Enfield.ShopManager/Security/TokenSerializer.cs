using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Enfield.ShopManager.Security
{
    public class TokenSerializer
    {
        private const char delimiter = '-';

        public static string Serialize(Token token)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(token.CreateDate.Ticks.ToString()).Append(delimiter);
            sb.Append(Convert.ToBase64String(token.Hash)).Append(delimiter);
            sb.Append(token.LocationId.ToString()).Append(delimiter);
            sb.Append(token.UserId.ToString()).Append(delimiter);
            sb.Append(token.Role.ToString());

            return sb.ToString();
        }

        public static Token Deserialize(string token)
        {
            Token result = new Token();

            var tokens = token.Split(delimiter);
            result.CreateDate = new DateTime(long.Parse(tokens[0]));
            result.Hash = Convert.FromBase64String(tokens[1]);
            result.LocationId = int.Parse(tokens[2]);
            result.UserId = int.Parse(tokens[3]);
            result.Role = int.Parse(tokens[4]);

            return result;
        }

        public static HttpCookie GetCookieFromToken(Token token)
        {
            HttpCookie auth = new HttpCookie("auth");
            auth.Value = Serialize(token);
            //auth.Domain = "enfieldsdetail.com";
            auth.Domain = System.Web.HttpContext.Current.Request.Url.Host;
            auth.Expires = DateTime.Today.AddDays(1);
            auth.Secure = true;
            return auth;
        }

        public static Token GetTokenFromCookie(HttpCookie auth)
        {
            return Deserialize(auth.Value);
        }
    }
}