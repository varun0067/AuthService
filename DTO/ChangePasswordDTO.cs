
namespace AuthorizationMS.DTO
{
    public class ChangePasswordDTO
    {
        public string CustomerId { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
