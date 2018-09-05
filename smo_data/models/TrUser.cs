using System;
using System.Collections.Generic;

namespace smo_data.models
{
    public partial class TrUser
    {
        public long Userid { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
    }
}
