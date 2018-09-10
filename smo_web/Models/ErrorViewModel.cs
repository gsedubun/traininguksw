using System;

namespace smo_web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class UserViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string  Password { get; set; }
    }
}