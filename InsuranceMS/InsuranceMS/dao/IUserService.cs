using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMS.dao
{
    internal interface IUserService
    {
        public bool RegisterUser(string username, string password, string role);
        public bool IsUsernameExists(string username);
        public bool IsLoginValid(string username, string password);
    }
}
