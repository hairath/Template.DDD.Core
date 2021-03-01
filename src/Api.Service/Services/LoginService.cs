using System;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces;
using Api.Domain.Dtos;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private IConfiguration _configuration { get; }
        public SigningConfigurations _signingConfigurations;
        public TokenConfigurations _tokeConfigurations;


        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokeConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        public async Task<object> AutenticarUser(LoginDto user)
        {
            if (user == null && string.IsNullOrEmpty(user.Email))
            {
                return new
                {
                    authenticated = false,
                    message = "falha ao autenticar"
                };
            }

            var entity = await _repository.FindByLogin(user.Email);

            if (entity == null)
            {
                return new
                {
                    authenticated = false,
                    message = "falha ao autenticar"
                };
            }
            else
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(entity.Email),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, entity.Email)
                    });

                var createDate = DateTime.Now;
                var expirationDate = createDate + TimeSpan.FromSeconds(_tokeConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();

                string token = CreateToken(identity, createDate, expirationDate, handler);

                return RetornarAutenticacao(createDate, expirationDate, token, user);
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokeConfigurations.Issuer,
                Audience = _tokeConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

        private object RetornarAutenticacao(DateTime createDate, DateTime expirationDate, string token, LoginDto dto)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = dto.Email,
                message = "Usuário logado com sucesso"
            };
        }
    }
}
