using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.User.Common.ViewModels
{
    public class UserAdminViewModels
    {
        public int userID { get; set; }
        public string userName { get; set; } = string.Empty;
        public int employeeID { get; set; }
        public string password_User { get; set; } = string.Empty;
        public int RoleID { get; set; }
    }
}
