using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRira.Core.ModelViews;

namespace TestRira.Core.interfaces
{
    public interface IUsersService
    {
        byte[] addUser(byte[] user);
        byte[] updateUsers(byte[] user);
        byte[] deleteUser(byte[] user);
        byte[] listOfUsers();
        byte[] getByUser(byte[] user);
    }
}
