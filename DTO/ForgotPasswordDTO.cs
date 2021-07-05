using System;


namespace AuthorizationMS.DTO
{
    public class ForgotPasswordDTO
    {
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
