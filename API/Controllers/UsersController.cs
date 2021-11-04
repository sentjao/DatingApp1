using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly DataContext _dataContext;
        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

       
        [AllowAnonymous]
        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        [Authorize]
        [HttpGet("find/{id}")]
        public async Task<ActionResult<AppUser>> Find(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }
    }
}