using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Enfield.ShopManager.Security
{
    public class TokenHasher
    {
        [ThreadStatic()]
        private static byte[] buffer;
        private static Guid secret = new Guid("CB4B9658-7744-4177-877A-706140827F96");

        public static bool IsValid(Token token)
        {
            byte[] h1 = token.Hash;
            if (h1 == null || h1.Length == 0) return false;

            byte[] h2 = GenerateHash(token);
            if (h1.Length != h2.Length) return false;

            for(int i = 0; i < h1.Length; i++)
            {
                if (h1[i] != h2[i]) return false;
            }

            return true;
        }

        // tokens expire at midnight
        public static bool IsExpired(Token token)
        {
            return (token.CreateDate.Day != DateTime.Today.Day);
        }

        public static void Hash(Token token)
        {
            token.Hash = GenerateHash(token);
        }

        public static byte[] GenerateHash(Token token)
        {
            if (buffer == null)
            {
                // 8 bytes of time
                // 4 bytes of user id
                // 4 bytes of location id
                // 4 bytes of role
                // 16 bytes of id address
                // 16 bytes of secret
                // = 64 bytes
                buffer = Array.CreateInstance(typeof(byte), 52) as byte[];

                // copy the secret to the end of the array, this never changes
                Array.Copy(secret.ToByteArray(), 0, buffer, 36, 16);
            }

            // copy token creation time to the first 8 bytes
            long time = token.CreateDate.Ticks;
            buffer[0] = (byte)(time & 0xFF);
            buffer[1] = (byte)((time >> 0x08) & 0xFF);
            buffer[2] = (byte)((time >> 0x10) & 0xFF);
            buffer[3] = (byte)((time >> 0x18) & 0xFF);
            buffer[4] = (byte)((time >> 0x20) & 0xFF);
            buffer[5] = (byte)((time >> 0x28) & 0xFF);
            buffer[6] = (byte)((time >> 0x30) & 0xFF);
            buffer[7] = (byte)((time >> 0x38) & 0xFF);
    
            // copy the user id to the next 4 bytes
            BitConverter.GetBytes(token.UserId).CopyTo(buffer, 8);

            // copy the location id to the next 4 bytes
            BitConverter.GetBytes(token.LocationId).CopyTo(buffer, 12);

            // copy the role to the next 4 bytes
            BitConverter.GetBytes(token.Role).CopyTo(buffer, 16);
            //var role = new String(' ', 16);
            //role = ((string.IsNullOrEmpty(token.Role)) ? "employee" : token.Role.ToLower()).PadRight(16);
            //System.Text.Encoding.ASCII.GetBytes(role).CopyTo(buffer, 16);

            // copy the ip address to the next 16 bytes
            var ip = new String(' ', 16);
            ip = token.IpAddress.PadRight(16);
            System.Text.Encoding.ASCII.GetBytes(ip).CopyTo(buffer, 20);

            using (SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider())
            {
                return provider.ComputeHash(buffer, 0, 52);
            }
        }
    }
}