using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace event_train
{

    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";

        const string KEY = "i_wont_tell_you_the_key123123123123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
