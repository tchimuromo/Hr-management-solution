using Microsoft.AspNetCore.Mvc;
using HrManagementApi.Models;

namespace HrManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        // In-memory list for data storage
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@company.com",
                Phone = "+1234567890",
                Department = "IT",
                Position = "Software Engineer",
                HireDate = new DateTime(2020, 1, 15),
                Salary = 75000
            },
            new Employee
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@company.com",
                Phone = "+1234567891",
                Department = "HR",
                Position = "HR Manager",
                HireDate = new DateTime(2019, 3, 20),
                Salary = 85000
            },
            new Employee
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@company.com",
                Phone = "+1234567892",
                Department = "Finance",
                Position = "Financial Analyst",
                HireDate = new DateTime(2021, 6, 10),
                Salary = 70000
            }
        };
        
        private static int _nextId = 4;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        // GET: api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                _logger.LogInformation("Getting all employees");
                return Ok(_employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employees");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            try
            {
                _logger.LogInformation($"Getting employee with id: {id}");
                var employee = _employees.FirstOrDefault(e => e.Id == id);
                
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with id {id} not found");
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }
                
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/employees
        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if email already exists
                if (_employees.Any(e => e.Email == employee.Email))
                {
                    return BadRequest(new { message = "An employee with this email already exists" });
                }

                employee.Id = _nextId++;
                _employees.Add(employee);
                
                _logger.LogInformation($"Created employee with id: {employee.Id}");
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
                
                if (existingEmployee == null)
                {
                    _logger.LogWarning($"Employee with id {id} not found");
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                // Check if email already exists for a different employee
                if (_employees.Any(e => e.Email == employee.Email && e.Id != id))
                {
                    return BadRequest(new { message = "An employee with this email already exists" });
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Department = employee.Department;
                existingEmployee.Position = employee.Position;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.Salary = employee.Salary;

                _logger.LogInformation($"Updated employee with id: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = _employees.FirstOrDefault(e => e.Id == id);
                
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with id {id} not found");
                    return NotFound(new { message = $"Employee with id {id} not found" });
                }

                _employees.Remove(employee);
                
                _logger.LogInformation($"Deleted employee with id: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
