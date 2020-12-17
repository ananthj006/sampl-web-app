using Sample.DTO;
using System.Collections.Generic;

namespace Sample.Domain
{
    public interface IUserDomain
    {
        IEnumerable<User> GetUsers(string search, string sort);
        int AddUser(User user);
        int UpdateUser(int userId, User user);
        int DeleteUser(int userId);
    }
}
