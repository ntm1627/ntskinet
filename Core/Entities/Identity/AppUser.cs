using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser :IdentityUser  //this is why the Identity.Stores is installed in core.csproj
    {
        public string DisplayName { get; set; } 
        public Address Address { get; set; }   //the r/p b/n the AppUser and Address is considered to be 1-1
        
    }
}