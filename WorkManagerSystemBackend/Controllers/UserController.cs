using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.User;
using WorkManagerSystemBackend.Core.Entities;
using WorkManagerSystemBackend.Core.Helpers;

namespace WorkManagerSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }

        private JwtService _jwtService;

        public UserController(ApplicationDbContext context, IMapper mapper, JwtService jwtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterDto dto)
        {
            Users newUser = _mapper.Map<Users>(dto);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Generate JWT for the new user
            var jwt = _jwtService.Generate(newUser.Id);

            // Append the JWT as a cookie in the response
            Response.Cookies.Append("jwt", jwt, new CookieOptions { 
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new { message = "User Created Successfully" });
        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto dto)
        {
            var user = GetUserByEmail(dto.Email);

            if (user == null)
            {
                return BadRequest("Invalid email");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password,user.Password))
            {
                return BadRequest("Invalid password");
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });
           
            return Ok(new
            { 
                message = "success"
            });
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetUsers() 
        {
            var users = await _context.Users.ToListAsync();
            var convertedUsers = _mapper.Map<IEnumerable<UserGetDto>> (users);
            return Ok(convertedUsers);
        }

        [HttpGet]
        [Route("GetByJwt")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = GetUserById(userId);
                return Ok(user);
            } catch (Exception _) 
            {
                return Unauthorized();
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Delete("jwt", cookieOptions);

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet]
        [Route("Email")]
        public Users GetUserByEmail(string email) {
            return _context.Users.FirstOrDefault(x => x.Email == email);    
        }


        [HttpGet]
        [Route("Id")]
        public Users GetUserById(long id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut]
        [Route("UpdateName")]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserNameDto dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Ažuriranje korisnika koristeći AutoMapper
            _mapper.Map(dto, user);

            await _context.SaveChangesAsync();

            return Ok(new { message = "User name updated successfully" });
        }

        [HttpPut]
        [Route("UpdateEmail")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailDto dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Ažuriranje korisnika koristeći AutoMapper
            _mapper.Map(dto, user);

            await _context.SaveChangesAsync();

            return Ok(new { message = "User email updated successfully" });
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto dto)
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
            {
                return BadRequest("Current password is incorrect");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully" });
        }

        [HttpDelete("DeleteUserByPassword")]
        public async Task<IActionResult> DeleteUserByPassword([FromBody] DeleteUserDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if(user == null)
            {
                return NotFound("User not found");
            } 

            if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest("Password incorrect!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User with ID {dto.UserId} deleted successfully.");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(long userId)
        {
            try
            {
                var userToDelete = await _context.Users.FindAsync(userId);

                if (userToDelete == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();

                return Ok($"User with ID {userId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete user: {ex.Message}");
            }
        }


    }
}
