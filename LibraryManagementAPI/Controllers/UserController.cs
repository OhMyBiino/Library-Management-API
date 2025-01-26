using LibraryManagementAPI.Models.UserRepository;
using LibraryManagementModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetUsers()
        {
            try
            {
                var users = (await _userRepository.GetUsersAsync()).ToList();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving users from the database.");
            }
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<UserModel>> GetUserById(Guid Id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(Id);

                if (user == null)
                {
                    return NotFound($"User with ID:{Id} could not be found.");
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving the user from the database.");
            }
        }

        [HttpGet("{search}/{Query}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Search(string Query)
        {
            try
            {
                if (Query == null) 
                {
                    return NotFound($"No results.");
                }

                var results = await _userRepository.SearchAsync(Query);

                if (results.Any()) 
                {
                    return Ok(results);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving users from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel newUser)
        {
            try
            {
                var existingUserId = await _userRepository.GetUserByIdAsync(newUser.UserId);

                if (existingUserId != null) 
                {
                    return BadRequest("User is already existing in the database.");
                }

                //custom validation for email
                var existingEmail = await _userRepository.GetUserByEmail(newUser.Email);

                if (existingEmail != null) 
                {
                    return BadRequest("The email is already used.");
                }

                //custom validation for username
                var existingUsername = await _userRepository.GetUserByUsername(newUser.UserName);

                if (existingUsername != null) 
                {
                    return BadRequest("The username is already used.");
                }

                var addedUser = await _userRepository.CreateUserAsync(newUser);

                return CreatedAtAction(nameof(GetUserById), new { Id = newUser.UserId }, newUser);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding the user from the database.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUser(Guid Id, UserModel updatedUser)
        {
            try
            {

                if (Id != updatedUser.UserId) 
                {
                    return BadRequest("Id mismatch.");
                }

                var userToUpdate = await _userRepository.GetUserByIdAsync(Id);

                if(userToUpdate == null)
                {
                    return BadRequest($"User with Id:{Id} could not be found.");    
                }

                var latestUser = await _userRepository.UpdateUserAsync(updatedUser);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating the user from the database.");
            }
        }


        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<UserModel>> DeleteUser(Guid Id) 
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(Id);

                if (existingUser == null) 
                {
                    return NotFound($"User with ID: {Id} could not be found.");
                }

                var deletedUser = _userRepository.DeleteUserAsync(Id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting the user from the database.");
            }
        }
    }
}
