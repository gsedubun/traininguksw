using System;
using System.Collections.Generic;

namespace smo_data.models
{
    public partial class TrUser
    {
        public TrUser()
        {
            TtUserRole = new HashSet<TtUserRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public ICollection<TtUserRole> TtUserRole { get; set; }
    }
}
