using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OTL_API.Services
{
    public class AccessAPI
    {
        private string _apiURL;
        private string _apiToken;
        private HttpClient _client;

        public AccessAPI(string controllerName)
        {
            _apiURL = controllerName;
            _client = new HttpClient();
        }

        //Token Generator
        private void GenerateToken(String userName)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.Name, userName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("03fb1760-a45f-4473-bed4-aab1e8d7e87a"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44307",
                audience: "http://localhost:50282",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            _apiToken = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void AssignAuth(String userName)
        {
            GenerateToken(userName);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
        }

        public async Task<string> GetRequest()
        {
            try
            {
                var request = await _client.GetAsync(_apiURL);
                if (request.IsSuccessStatusCode)
                {
                    var result = request.Content.ReadAsStringAsync().Result;
                    return result;
                }

                return null;
            }
            catch
            {
                return null;
            }

        }
    }
}
