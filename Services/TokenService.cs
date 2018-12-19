using HomeSecurityAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeSecurityAPI.Services
{
    public class TokenService
    {
            private readonly string _jwtKey;
            private readonly double _jwtExpireDays;
            private readonly string _jwtIssuer;

            public TokenService(string jwtKey, double jwtExpireDays, string jwtIssuer)
            {
                _jwtKey = jwtKey;
                _jwtExpireDays = jwtExpireDays;
                _jwtIssuer = jwtIssuer;
            }

            public string GenerateJwtToken(User user)
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.Username),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(_jwtExpireDays);

                var token = new JwtSecurityToken(
                    _jwtIssuer,
                    _jwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

