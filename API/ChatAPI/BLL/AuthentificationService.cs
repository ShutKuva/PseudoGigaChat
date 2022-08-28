using BLL.Abstractions;
using Core;
using Core.DTOs;
using Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL
{
    public class AuthentificationService : IAuthenticationService
    {
        private readonly AuthOptions _authOptions;
        private readonly ICRUDService<User> _crudService;

        public AuthentificationService(IOptions<AuthOptions> authOptions, ICRUDService<User> crudService)
        {
            _authOptions = authOptions.Value;
            _crudService = crudService;
        }

        public async Task<string> Authenticate(UserDTOFromUI user)
        {
            User actualUser;

            try
            {
                IEnumerable<User> usersThatFitsCondition = await _crudService.GetByCondition(dbuser => dbuser.Name == user.Name && dbuser.Password == user.Password);
                actualUser = usersThatFitsCondition.First();
            }
            catch
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", actualUser.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey)), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddHours(3)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
