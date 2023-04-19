using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace hitachiv1.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration){
            _configuration = configuration;
            _context = context;
        }

        public async Task<Response<string>> Login(string email, string password)
        {
            var response = new Response<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            if(user is null){
                response.Success = false;
                response.Message = "User Not Found!";
            }else if(!user.IsActive){
                response.Success = false;
                response.Message = "User has been deactivated!";
            }else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                user.LockoutCount++;
                
                //edit for max lockout count
                if(user.LockoutCount >= 3){
                    user.LockoutEnable = true;
                    response.Success = false;
                    response.Message = "You’ve reached the maximum logon attempts.";
                }else{
                    response.Success = false;
                    response.Message = "Wrong password!";
                }
                await _context.SaveChangesAsync();
            }else{

                if(user.LockoutEnable){
                    response.Success = false;
                    response.Message = "You’ve reached the maximum logon attempts!";
                }else{
                    user.LockoutCount = 0;
                    user.LastLoginDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    
                    response.Data = CreateToken(user);
                }
            }
            return response;
        }

        public async Task<Response<int>> Register(User user, string password)
        {
            var response = new Response<int>();

            if(user.Email == string.Empty || password == string.Empty ){
                response.Success = false;
                response.Message = "String empty.";
                return response;
            }

            if(await UserExist(user.Email)){
                response.Success = false;
                response.Message = "User already exist.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            response.Data = user.Id;
            return response;
        }

        public Response<string> Test()
        {
            var response = new Response<string>();
            //var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value!;
            //if(appSettingsToken is null) throw new Exception("AppSetting Token is null!");
            response.Data =  "2023-04-17 12:02";
            return response;
        }

        public async Task<bool> UserExist(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower())){
                return true;
            }
            return false;
        }


    //*** ############################## ~ UTILITIES ~ ############################## ***//
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
                //new Claim(ClaimTypes.Role, "Admin")
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if(appSettingsToken is null) throw new Exception("AppSetting Token is null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));
                
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}