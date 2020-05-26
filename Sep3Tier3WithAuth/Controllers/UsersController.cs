using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore.Migrations;

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
                Subject = new ClaimsIdentity(new Claim[]
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
        [Authorize(Roles = Roles.admin)]
        [HttpPost("registeradmin")]
        public IActionResult Register([FromBody] AddAdminModel model)
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

        [Authorize(Roles = Roles.admin)]
        [HttpGet]
        public IActionResult GetAllFishers()
        {
            var fishers = _userService.GetAll();
            var model = _mapper.Map<IList<FisherModel>>(fishers);
            return Ok(model);
        }

        [Authorize(Roles = Roles.fisher)]
        [HttpGet("getuser/{id}")]
        public IActionResult GetByID(int id)
        {
            FisherModel model;
            var fisher = _userService.GetById(id);
            try
            {
                model = _mapper.Map<FisherModel>(fisher);
            }
            catch (AutoMapperMappingException e)
            {
                return BadRequest(new { message = "User does not exists in our database or it is not a fisher" });
            }

            return Ok(model);
        }

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
