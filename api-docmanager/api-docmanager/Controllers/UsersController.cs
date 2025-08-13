using api_docmanager.Dtos.Documents;
using api_docmanager.Dtos.Users;
using api_docmanager.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api_docmanager.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_docmanager.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly DocManagerContext _context;
    private readonly IMapper _mapper;

    public UsersController(DocManagerContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> PostUser([FromForm]CreateUserDto newUser)
    {
        try
        {
            var tmpUser = this._mapper.Map<UserAccount>(newUser);
            tmpUser.Code = SecurityFunctions.GenerateRandomId();
            tmpUser.Password = SecurityFunctions.HashPassword(tmpUser.Password);
            if (tmpUser.UsrRole == null)
            {
                tmpUser.UsrRole = 2;
            }
            
            this._context.Add(tmpUser);
            await this._context.SaveChangesAsync();

            return Created("New user created", new
            {
                message = "User created successfully.",
                status = 201,
                user_code = tmpUser.Code,
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        try
        {
            var listUsers = await this._context.UserAccounts
                .Where(usr => usr.Deleted == false)
                .Include(usr => usr.UnitBelongNavigation)
                .ToListAsync();

            return Ok(new
            {
                message = "Users retrieved successfully.",
                status = 200,
                users = this._mapper.Map<List<UserDto>>(listUsers)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }

    [HttpGet("{code:int}", Name = "GetUserByCode")]
    public async Task<ActionResult<UserDto>> GetUserByCode(int? code)
    {
        try
        {
            if (code == null)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Invalid code"
                });
            }

            var tmpUser = await this._context.UserAccounts
                .Where(usr => usr.Deleted == false && usr.Code.Equals(code))
                .Include(usr => usr.UnitBelongNavigation)
                .FirstOrDefaultAsync();

            if (tmpUser == null)
            {
                return NotFound(new
                {
                    message = "User not found with the given code",
                    status = 404
                });
            }

            return Ok(new
            {
                message = "User retrieved successfully.",
                status = 200,
                user = this._mapper.Map<UserDto>(tmpUser)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }

    [HttpGet("{code:int}/docs", Name = "GetUserDocs")]
    public async Task<ActionResult<List<DocDto>>> GetUserDocuments(int code)
    {
        try
        {
            var usrDocs = await _context.Documents
                .Where(doc => doc.Deleted == false && doc.GenByUsr == code)
                .Include(doc => doc.UnitBelongNavigation)
                .Include(doc => doc.GenByUsrNavigation)
                .OrderByDescending(doc => doc.DateCreate)
                .ToListAsync();

            return Ok(new
            {
                message = "User documents retrieved successfully",
                status = 200,
                docs = _mapper.Map<List<DocDto>>(usrDocs)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }
    
    [HttpDelete("{code:int}", Name = "DeleteUser")]
    public async Task<ActionResult> DeleteUser(int? code)
    {
        try
        {
            var tmpUser = await this._context.UserAccounts
                .Where(usr => usr.Deleted == false && usr.Code.Equals(code))
                .FirstOrDefaultAsync();

            if (tmpUser == null)
            {
                return NotFound(new
                {
                    message = "User not found with the given code",
                    status = 404
                });
            }
            
            tmpUser.Deleted = true;
            await this._context.SaveChangesAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }

    [HttpPut("{code:int}", Name = "UpdateUser")]
    public async Task<ActionResult> PutUser(int code, [FromForm] UpdateUserDto updateUser)
    {
        try
        {
            var tmpUser = await this._context.UserAccounts
                .Where(u => u.Deleted == false && u.Code.Equals(code))
                .FirstOrDefaultAsync();

            if (tmpUser == null)
            {
                return NotFound(new
                {
                    message = "User not found with the given code",
                    status = 404
                });
            }
            
            this._mapper.Map(source: updateUser, destination:tmpUser);
            await this._context.SaveChangesAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }
    
    [HttpPatch("{code:int}", Name = "PutPassword")]
    public async Task<ActionResult> PutPassword(int code, [FromForm] UpdatePasswordDto updatePassword)
    {
        try
        {
            var tmpUser = await this._context.UserAccounts
                .FirstOrDefaultAsync(u => u.Deleted == false && u.Code.Equals(code));

            if (tmpUser == null)
            {
                return NotFound(new
                {
                    message = "User not found with the given code",
                    status = 404
                });
            }
            
            //We first validate if the current password is correct
            if (SecurityFunctions.HashPassword(updatePassword.CurrentPassword) != tmpUser.Password)
            {
                return Unauthorized(new
                {
                    message = "The current password sent is not the same as the current password",
                    status = 401
                });
            }
            
            tmpUser.Password = SecurityFunctions.HashPassword(updatePassword.Password);
            await this._context.SaveChangesAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = e.Message,
                status = 500
            });
        }
    }
    
}