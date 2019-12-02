using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EletronicShippingSuplies.Data_Objects
{
    public class UserManagement
    {

        public static User GetUserDetails(DB_OSSEntities oss, string sEmail)
        {
            var users = from u in oss.User
                        where u.EMAIL.ToUpper().Equals(sEmail.ToUpper())
                        select u;

            if (users.Count() == 0)
            {
                return null;
            }
            else
            {
                return users.First();
            }
        }

        //public static IQueryable<User> GetUsersLike(DB_OSSEntities OSS, string sUsername, string sName, string sActive)
        //{
        //    // ############ NAO APAGAR ESTES COMENTARIOS ##############

        //    // string toSearch = "blabla";
        //    //var stringProperties = typeof(USER).GetProperties().Where(prop =>
        //    //prop.PropertyType == toSearch.GetType());

        //    //cpEntities.USER.Where(customer =>
        //    //        stringProperties.Any(prop =>
        //    //            prop.GetValue(customer, null).ToString() == toSearch));

        //    //cpEntities.USER.Where(customer =>
        //    //        stringProperties.Any(prop => 
        //    //        ((prop.GetValue(customer, null) == null) ? "" : prop.GetValue(customer, null).ToString().ToLower()) == toSearch));

        //    //from o in dc.Organization
        //    //join oh in dc.OrganizationsHierarchy on o.Id equals oh.OrganizationsId
        //    //where oh.Hierarchy.Contains(@"/12/")
        //    //select new { o.Id, o.Name }

        //    // https://docs.microsoft.com/en-us/dotnet/csharp/linq/handle-null-values-in-query-expressions

        //    IQueryable<User> users = null;

        //    if (!String.IsNullOrEmpty(sUsername))
        //    {
        //        if (users == null)
        //        {
        //            users = (OSS.USER
        //                     .Where(u => u.USERNAME.ToUpper().StartsWith(sUsername.ToUpper()))).Take(CPTools.Constants.MaxDBNrRows);
        //        }
        //        else
        //        {
        //            users = (users
        //                     .Where(u => u.USERNAME.ToUpper().StartsWith(sUsername.ToUpper()))).Take(CPTools.Constants.MaxDBNrRows);
        //        }
        //    }


        //    return users;
        //}

        #region Password management
        public static string ComputePassword(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            // If salt is not specified, generate it.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
            new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
            saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyPassword(string plainText, string hashAlgorithm, string hashValue)
        {

            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = ComputePassword(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        public static bool AuthenticateUser(DB_OSSEntities cpEntities, string sEmail, string PlainPassword, out User user)
        {
            bool isAuthenticated = false;

            user = GetUserDetails(cpEntities, sEmail);

            if (user != null)
            {
                if (VerifyPassword(PlainPassword, "SHA512", user.PASSWORD))
                {
                    isAuthenticated = true;
                }

            }
            return isAuthenticated;
        }
        #endregion Password management

        //public static void UpdateLastLogin(DB_OSSEntities cpEntities, string Username)
        //{
        //    USER user = UserManagement.GetUserDetails(cpEntities, Username);
        //    user.LAST_LOGON = DateTime.Now;
        //    cpEntities.USER.Attach(user);
        //    cpEntities.Entry(user).State = EntityState.Modified;
        //    cpEntities.SaveChanges();
        //}



        public class LoggedUser
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public String Email { get; set; }
            public String Account { get; set; }
            public List<LoggedUserAddress> Addresses { get; set; }
            public bool isAdmin { get; set; }
        }

        public class LoggedUserAddress
        {
            public String Street { get; set; }
            public String Number { get; set; }
            public String ZipCode { get; set; }
            public String City { get; set; }
            public bool IsDefault { get; set; }
            public int ID { get; set; }
        }
    }
}