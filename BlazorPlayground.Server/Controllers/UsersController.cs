using BlazorPlayground.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BlazorPlayground.Server.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [Route("api/users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "Joe Doe", Departments = new List<Department> { Department.DepartmentA, Department.DepartmentB } },
                new User { Id = Guid.NewGuid(), Name = "Jane Doe", Departments = new List<Department> { Department.DepartmentB, Department.DepartmentC, Department.DepartmentD } }
            });
        }

        [Route("api/users/{id}")]
        [HttpGet]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(new User 
            { 
                Id = id, 
                Name = "Jane Doe", 
                Departments = new List<Department> 
                { 
                    Department.DepartmentB, 
                    Department.DepartmentC, 
                    Department.DepartmentD 
                } 
            });
        }

        [Route("api/users")]
        [HttpPut]
        public IActionResult CreateOrUpdateUser(User user)
        {
            return Ok(user);
        }

        [Route("api/users/{userId}/departments")]
        [HttpGet]
        public IActionResult GetUserDepartmentById(Guid userId)
        {
            return Ok(new List<Department>
            {
                Department.DepartmentB,
                Department.DepartmentC,
                Department.DepartmentD
            });
        }

        [Route("api/users/{userId}/departments")]
        [HttpPost]
        public IActionResult AddDepartmentsToUser(Guid userId, List<Department> departments)
        {
            return Ok(departments);
        }

        [Route("api/users/{userId}/departments/{department}")]
        [HttpDelete]
        public IActionResult DeleteUserDepartment(Guid userId, Department department)
        {
            return NoContent();
        }
    }
}