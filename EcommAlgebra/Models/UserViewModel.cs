namespace EcommAlgebra.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }  
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string UserName { get; set; }    
        public IEnumerable<string> Roles { get; set; }  
    }
}
