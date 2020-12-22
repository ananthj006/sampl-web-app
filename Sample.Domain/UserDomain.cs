using Microsoft.Extensions.Logging;
using Sample.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly ILogger _logger;
        private static IEnumerable<User> _usersList = new List<User>();

        public UserDomain(ILogger<UserDomain> logger)
        {
            _logger = logger;
        }
        public IEnumerable<User> GetUsers(string search, string sort)
        {
            var records = _usersList;

            if (!string.IsNullOrEmpty(search))
            {
                records = _usersList.Where(x => x.Email.Equals(search, StringComparison.OrdinalIgnoreCase)
                || x.Phone.Equals(search, StringComparison.OrdinalIgnoreCase));
            }

            if (string.IsNullOrEmpty(sort))
            {
                _logger.LogInformation("Sort not applied");
                return records;
            }

            var propertyInfo = typeof(User).GetProperty(sort);
            if (propertyInfo != null)
                records = records.OrderBy(x => propertyInfo.GetValue(x, null));
            else
                _logger.LogWarning("Sort property not found in the user object");

            return records;
        }
        public int AddUser(User user)
        {
            var maxUserId = _usersList.Count() > 0 ? _usersList.Max(x => x.UserId) : 0;

            var users = _usersList.ToList();
            users.Add(new User
            {
                FullName = user.FullName,
                UserId = maxUserId + 1,
                Email = user.Email,
                Phone = user.Phone,
                Age = user.Age
            });

            _usersList = users;

            return maxUserId + 1;
        }

        public int UpdateUser(int userId, User user)
        {
            if (userId != user.UserId)
            {
                _logger.LogError("UserId doesnt match. Bad Request");
                return 0;
            }

            var userInfo = _usersList.FirstOrDefault(x => x.UserId == userId);
            if (userInfo == null)
            {
                _logger.LogError("User details not exists");
                return 0;
            }

            userInfo.FullName = user.FullName;
            userInfo.Email = user.Email;
            userInfo.Age = user.Age;

            return userInfo.UserId;
        }

        public int DeleteUser(int userId)
        {
            if (userId == 0 || !_usersList.Any(x => x.UserId == userId))
            {
                _logger.LogError("User not found");
                return 0;
            }

            _usersList = _usersList.Where(x => x.UserId != userId);
            return userId;
        }
    }
}
