using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Helpers;
using Sep3Tier3WithAuth.Models;
using Sep3Tier3WithAuth.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Sep3Tier3WithAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //  ******************************GET METHODS****************************  \\
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAllFishers()
        {
            var fishers = _userService.GetAll();
            var model = _mapper.Map<IList<FisherModel>>(fishers);
            return Ok(model);
        }
        [Authorize(Roles = Roles.Fisher)]
        [HttpGet("GetFishersPref/{gender};{sexPref}")]
        public IActionResult GetAllFishersAccordingToTheirPref(string gender, int sexPref)
        {
            var userId = User.GetUserId();
            var fishers = _userService.GetAllFishersAccordingToTheirPref(userId,gender, sexPref);
            var model = _mapper.Map<IList<FisherInfoForMatches>>(fishers);
            return Ok(model);
        }

        [Authorize(Roles = Roles.Fisher)]
        [HttpGet("GetUser/{id}")]
        public IActionResult GetById(int id)
        {
            FisherModel model;
            var fisher = _userService.GetById(id);
            try
            {
                model = _mapper.Map<FisherModel>(fisher);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "User does not exists in our database or it is not a fisher" });
            }

            return Ok(model);
        }
        //  ******************************GET METHODS ENDS****************************  \\
        //  ******************************POST METHODS****************************  \\
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Discriminator)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                userID = user.Id,
                username = user.Username,
                Role = user.Discriminator,
                Token = tokenString
            });
        }


        // Just for fun, letting to add admins :)
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("RegisterAdmin")]
        public IActionResult RegisterAdmin([FromBody] AddAdminModel model)
        {
            // Map model to entity
            var admin = _mapper.Map<Administrator>(model);

            try
            {
                //create user
                _userService.CreateAdmin(admin, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error if something went wrong with the registration
                return BadRequest(new { message = ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // Map model to entity
            var fisher = _mapper.Map<Fisher>(model);

            try
            {
                //create user
                _userService.Create(fisher, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error if something went wrong with the registration
                return BadRequest(new {message = ex.Message});
            }
        }
        [Authorize(Roles = Roles.Fisher)]
        [HttpPost("Like")]
        public IActionResult LikePerson([FromBody] LikeModel model)
        {
            // Map model to entity
            var likeReject = _mapper.Map<LikeReject>(model);
            var userId = int.Parse(User.GetUserId());
            try
            {
                //like someone
                _userService.LikePerson(userId,likeReject);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error if something went wrong with the registration
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Roles.Fisher)]
        [HttpPost("Reject")]
        public IActionResult RejectPerson([FromBody] RejectModel model)
        {
            // Map model to entity
            var likeReject = _mapper.Map<LikeReject>(model);
            var userId = int.Parse(User.GetUserId());

            try
            {
                //like someone
                _userService.RejectPerson(userId,likeReject);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error if something went wrong with the registration
                return BadRequest(new { message = ex.Message });
            }
        }

        //  ******************************POST METHODS ENDS****************************  \\
        [Authorize(Roles = Roles.Fisher)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModel model)
        {
            // map model to entity and set id
            var fisher = _mapper.Map<Fisher>(model);
            fisher.Id = id;
            try
            {
                //Update user
                _userService.Update(fisher, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
