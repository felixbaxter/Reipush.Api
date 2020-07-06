using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reipush.Api.Entities;
using Reipush.Api.Services;
using Reipush.Api.ViewModels;


namespace Reipush.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ReipushContext _context;

        public UsersController(ReipushContext context)
        {
            _context = context;
        }

        // GET: api/Users/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {

            Services.UserService _UsersService = new UserService(_context);

            // This is utilizing Store Procedures
            return await _UsersService.GetAllUsers();
           
            // This is using the Entity Framework
            //return await _context.User.ToListAsync();
        }

        // GET: api/Users/GetById
        [HttpPost("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<User> GetUser(viUserId iuser)
        {
            Services.UserService _UsersService = new UserService(_context);

            var user =  _UsersService.GetUserById(iuser);
            
            //var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // GET: api/Users/GetById
        [HttpPost("GetFullNameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<voUser> GetUserFN(viUserId iuser)
        {
            Services.UserService _UsersService = new UserService(_context);

            var user = _UsersService.GetUserCombineNameById(iuser);

            //var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddUser")]
        public  ActionResult<User> PostUser(User iuser)
        {
            //_context.User.Add(user);
            //await _context.SaveChangesAsync();

            Services.UserService _UsersService = new UserService(_context);

            var user = _UsersService.CreateUser(iuser);

            if (user == null)
            {
                return NotFound();
            }

            return user;

            //  return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }


        // GET: api/Users/emailaddressexist
        [HttpPost("emailaddressexist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<User> EmailExist(viEmail iEmail)
        {
            Services.UserService _UsersService = new UserService(_context);
            Entities.User user;
            try
            {
                user = _UsersService.GetUserByEmail(iEmail.Email);


                if (user == null)
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return user;
        }



        // GET: api/Users/createaccount
        [HttpPost("createaccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> CreateAccount(viEmailPwd iCred)
        {
            Services.UserService _UsersService = new UserService(_context);
            Entities.User user;
            try
            {
                // Verify the email address does not exist.
                user = _UsersService.GetUserByEmail(iCred.Email);                           
                if (user != null) {
                    return BadRequest("This email address already exist in our system.");
                }

                 user = _UsersService.CreateAccount(iCred);

            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return user;
        }

    }
}
