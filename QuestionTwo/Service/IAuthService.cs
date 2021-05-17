using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuestionTwo.Service
{
    public interface IAuthService
    {
        public (bool,string) Validate(IHeaderDictionary header);
    }

    public class AuthService : IAuthService
    {
        private bool HeaderContain(string key, IHeaderDictionary dict)
        {
            return dict.ContainsKey(key);
        }
        public (bool,string) Validate(IHeaderDictionary header)
        {
            var ContainsAuth = HeaderContain("authorization", header);
            if (!ContainsAuth)
                return (false, "autheorization key missing");

            var ContainsTimeStamp = HeaderContain("timestamp", header);
            if(!ContainsTimeStamp)
                return (false, "timestamp missing");
            var containsHashedKey = HeaderContain("hashed", header);

            if(!containsHashedKey)
                return (false, "hashed key missing");

            var containsAppkey = HeaderContain("appKey", header);

            if (!containsAppkey)
                return (false, "appKey missing");

            var hashed_value = ComputeHash(header["appKey"].ToString() + header["timestamp"].ToString());

            Console.WriteLine(hashed_value);
            Debug.WriteLine(hashed_value);
            if (hashed_value != header["hashed"])
                return (false, "invalid authorization key");

            var authorizationKey = header["authorization"].ToString();

            // if(authorizationKey != "3line"+hashed_value)
            // {
            //     return (false, "invalid authorization key");
            // }


            return (true, "authorization key valid");
        }


        private string ComputeHash(string key)
        {
            var data = Encoding.UTF8.GetBytes(key.ToCharArray());
            byte[] hashed_value = default; 
            using(var hsh = new SHA512Managed())
            {
                hashed_value = hsh.ComputeHash(data);
            }
            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashed_value)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
            
        }
    }
}
