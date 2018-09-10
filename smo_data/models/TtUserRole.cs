using System;
using System.Collections.Generic;

namespace smo_data.models
{
    public partial class TtUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public TrRole Role { get; set; }
        public TrUser User { get; set; }
    }
}
