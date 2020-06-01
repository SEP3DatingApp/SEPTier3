using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep3Tier3WithAuth.Controllers;
using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Helpers;

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

        public Fisher GetById(int id)
        {
            return _context.Fishers.Find(id);
        }
        public IEnumerable<Fisher> GetAllFishersAccordingToTheirPref(int userId,string gender,int sexPref)
        {
            IQueryable<Fisher> fishers = Enumerable.Empty<Fisher>().AsQueryable();
            //Mans not hot
            if (gender.Equals("M") && sexPref == 1)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => (b.Gender == "F" && b.PersonSexualityId == 1) ||
                                    (b.Gender == "F" && b.PersonSexualityId == 3) && b.IsActive && b.Id != userId)
                    select fisher;
            }
            else if (gender.Equals("M") && sexPref == 2)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => (b.Gender == "M" && b.PersonSexualityId == 2) || (b.Gender == "M" && b.PersonSexualityId == 3) && b.IsActive && b.Id != userId)
                    select fisher;
            }
            else if (gender.Equals("M") && sexPref == 3)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => b.Gender == "M" && b.PersonSexualityId == 2 || b.Gender == "M" && b.PersonSexualityId == 3 || b.Gender == "F" && b.PersonSexualityId == 1 || b.Gender == "F" && b.PersonSexualityId == 3 && b.IsActive && b.Id != userId)
                    select fisher;
            }
            // Females
            else if (gender.Equals("F") && sexPref == 1)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => (b.Gender == "M" && b.PersonSexualityId == 1) || (b.Gender == "M" && b.PersonSexualityId == 3) && b.IsActive && b.Id != userId)
                    select fisher;
            }
            else if (gender.Equals("F") && sexPref == 2)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => (b.Gender == "F" && b.PersonSexualityId == 2) || (b.Gender == "F" && b.PersonSexualityId == 3) && b.IsActive && b.Id != userId)
                    select fisher;
            }
            else if (gender.Equals("F") && sexPref == 3)
            {
                fishers = from fisher in _context.Fishers
                        .Where(b => (b.Gender == "M" && b.PersonSexualityId == 1) || (b.Gender == "M" && b.PersonSexualityId == 3) || (b.Gender == "F" && b.PersonSexualityId == 2) || (b.Gender == "F" && b.PersonSexualityId == 3) && b.IsActive && b.Id != userId)
                    select fisher; 
            }
            return fishers;
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

        public void LikePerson(int userId,LikeReject lr)
        {
            lr.Fisher1Id = userId;
            //Validation
            if (lr.Fisher1Id <= 0 || lr.Fisher2Id <= 0)
                throw new AppException("Some of the user id's are empty!");

            _context.LikeReject.Add(lr);
            _context.SaveChanges();
        }

        public void RejectPerson(int userId, LikeReject lr)
        {
            lr.Fisher1Id = userId;
            //Validation
            if (lr.Fisher1Id <= 0 || lr.Fisher2Id <= 0)
                throw new AppException("Some of the user id's are empty!");

            _context.LikeReject.Add(lr);
            _context.SaveChanges();
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
            if (!string.IsNullOrWhiteSpace(userParam.Email))
                fisher.Email = userParam.Email;

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Description))
                fisher.Description = userParam.Description;

            // update user properties if provided
            if (userParam.PersonSexualityId != fisher.PersonSexualityId)
                fisher.PersonSexualityId = userParam.PersonSexualityId;

            if (!string.IsNullOrWhiteSpace(userParam.PicRef))
                fisher.PicRef = userParam.PicRef;

            if(userParam.IsActive != fisher.IsActive)
                fisher.IsActive = userParam.IsActive;

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
                throw new AppException("Password is empty");
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new AppException("password");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new AppException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) 
                throw new AppException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new AppException("Invalid length of password salt (128 bytes expected).", "passwordHash");

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
