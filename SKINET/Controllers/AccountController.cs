using AutoMapper;
using Core.Entities.Identity;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SKINET.Dtos;
using SKINET.Errors;
using System.Security.Claims;

namespace SKINET.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;

        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager, IMapper mapper, ITokenService token)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _token = token;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return NotFound("Email isn't exsit");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            var userDto = _mapper.Map<AppUser, UserDto>(user);
            userDto.Token = _token.CreateToken(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // check email had exsit
            var useEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (useEmail != null) return BadRequest("Email had been register");
            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            var email = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            return Ok(user.Address);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            return new UserDto
            {
                Email = user.Email,
                Token = _token.CreateToken(user),
                DisplayName = user.DisplayName

            };
        }

    }
}
