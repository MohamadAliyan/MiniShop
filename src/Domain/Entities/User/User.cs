

using Microsoft.AspNetCore.Identity;

namespace EShop.Domain;
public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


public class Role : IdentityRole<int>
{

}