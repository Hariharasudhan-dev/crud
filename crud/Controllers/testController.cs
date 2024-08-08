using crud.Model;
using crud.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private readonly PostgresDbContext mPostgresDbContext;

        public testController(PostgresDbContext postgresDbContext)
        {
            mPostgresDbContext = postgresDbContext;
        }
        [HttpGet]
        public async Task<List<string>> Getlist()
        {
            List<string> fruits = new List<string>() { "apple", "orange", "watermelon" };
            return fruits;
        }
        [HttpGet("getbyid")]
        public async Task<string> GetbyID(string car)
        {
            string res = "";
            string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
            foreach (string i in cars)
            {
                if (car.Contains(i))
                {
                    res = i;
                }

            }
            return res;
        }


        [HttpGet("getAllEmployee")]
        public async Task<List<Employee>> GetAllEmplopyee()
        {
            List<Employee> result = mPostgresDbContext.Employee.ToList();
            return result;
        }
        [HttpPost("createEmployee")]
        public string CreateStudent(StudentRequest studentRequest)
        {
            Employee employee = new Employee()
            {
                Name = studentRequest.Name,
                Age = studentRequest.Age,

            };
            mPostgresDbContext.Employee.Add(employee);
            mPostgresDbContext.SaveChanges();
            return "Created Successfully";
        }


    } 
}
