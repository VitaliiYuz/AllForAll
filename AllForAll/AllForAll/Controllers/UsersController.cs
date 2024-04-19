
using BusinessLogic.Dto.User;
using BusinessLogic.Implementation;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AllForAll.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public UsersController(IPhotoService photoService, IUserService userService)
        {
            _photoService = photoService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequestDto userDto, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file != null && file.Length > 0)
            {
                var uploadResult = await _photoService.AddPhotoAsync(file);
                if (uploadResult.Error != null)
                {
                    return BadRequest("Failed to upload photo");
                }
                userDto.UserPhotoLink = uploadResult.SecureUrl.AbsoluteUri;
            }

            var userId = await _userService.CreateUserAsync(userDto, cancellationToken);

            if (userId == 0)
            {
                return BadRequest("Failed to create user");
            }

            return Ok(userId);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UserRequestDto user, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(id, user, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(id, cancellationToken);
            return NoContent();
        }
        [HttpPost("upload-photo/{userId}")]
        public async Task<IActionResult> UploadPhoto(int userId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is empty");
            }
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var uploadResult = await _photoService.AddPhotoAsync(file);
            if (uploadResult.Error != null)
            {
                return BadRequest("Failed to upload photo");
            }

            user.UserPhotoLink = uploadResult.SecureUrl.AbsoluteUri;

            await _userService.UpdateUserAsync(userId, new UserRequestDto { UserPhotoLink = user.UserPhotoLink });

            return Ok("Photo uploaded successfully");
        }


    }
}
