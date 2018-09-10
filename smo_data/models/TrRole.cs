using System;
using System.Collections.Generic;

namespace smo_data.models
{
    public partial class TrRole
    {
        public TrRole()
        {
            TtUserRole = new HashSet<TtUserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<TtUserRole> TtUserRole { get; set; }
    }
}
