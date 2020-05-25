using System;
using System.Collections.Generic;
using System.Linq;
using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Helpers;
using Sep3Tier3WithAuth.Models;

namespace Sep3Tier3WithAuth.Services
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;

        public UserService(AuthContext context)
        {
            _context = context;
        }
        public User Authenticate(string username, string password)
        {
            // Checking if the username or password is not empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            //Getting user from the database
            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // Checking if user exists
            if (user == null)
                return null;
            // Check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            // authentication was successful, return user obj
            return user;
        }

        public IEnumerable<Fisher> GetAll()
        {
            return _context.Fishers;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required!");

            if(_context.Users.Any(x => x.Username == user.Username) || _context.Administrators.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken!");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public Administrator CreateAdmin(Administrator admin, string password)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required!");

            if (_context.Administrators.Any(x => x.Username == admin.Username) || _context.Fishers.Any(x => x.Username == admin.Username))
                throw new AppException("Username \"" + admin.Username + "\" is already taken!");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _context.Administrators.Add(admin);
            _context.SaveChanges();

            return admin;
        }

        public void Update(Fisher userParam, string password = null)
        {
            var fisher = _context.Fishers.Find(userParam.Id);

            if (fisher == null)
                throw new AppException("User not found.");

            //// update username if it has been changed
            //if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != fisher.Username)
            //{
            //    // throw an error if username is already taken
            //    if (_context.Users.Any(x => x.Username == userParam.Username))
            //    {
            //        throw new AppException("Username: " + userParam.Username + " is already taken.");
            //    }

            //    fisher.Username = userParam.Username;
            //}

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Description))
                fisher.Description = userParam.Description;

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.SexPref))
                fisher.SexPref = userParam.SexPref;

            if (!string.IsNullOrWhiteSpace(userParam.PicRef))
                fisher.PicRef = userParam.PicRef;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password , out var passwordHash, out var passwordSalt);

                fisher.PasswordHash = passwordHash;
                fisher.PasswordSalt = passwordSalt;
            }

            _context.Fishers.Update(fisher);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("Password is empty");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) 
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            { 
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
