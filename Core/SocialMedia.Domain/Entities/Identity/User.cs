using Microsoft.AspNetCore.Identity;


namespace SocialMedia.Domain.Entities.Identity
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
