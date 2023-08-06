using Microsoft.AspNetCore.Identity;

namespace EcommAlgebra.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }  
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string Role { get; set; }
        public string? SelectedRole { get; set; }
        public List<IdentityRole>? AvailableRoles { get; set; }
    }
}
