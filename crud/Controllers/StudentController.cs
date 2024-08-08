using crud.Model;
using crud.Request;
using crud.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
        

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly PostgresDbContext mpostgresDbContext;

        public StudentController(PostgresDbContext ipostgresDbContext)
        {
            mpostgresDbContext = ipostgresDbContext;
        }
        [HttpPost("createStudent")]

        public string CreateStudent(StudentRequest studentRequest)
        {
            Student student = new Student()
            {
                Name = studentRequest.Name,
                Age = studentRequest.Age,

            };
            mpostgresDbContext.Student.Add(student);
            mpostgresDbContext.SaveChanges();
            return $"Created Successfully{student.Id}";
        }
        [HttpGet("getAllStudentsByStudenResponse")]

        public List<StudentResponse> GetAllStudent()
        {
            List<StudentResponse> student = (from studen in mpostgresDbContext.Student
                                             select new StudentResponse

                                             {
                                                 Id = studen.Id,
                                                 Name = studen.Name,
                                                 Age = studen.Age,
                                             }).ToList();
            return student;

        }
        [HttpGet("getAllStudentsbyStudent")]
        public List<Student> GetAllStudents()
        {
            List<Student> student = mpostgresDbContext.Student.ToList();
            return student;

        }
        [HttpGet("getById")]
        public List<StudentResponse> GetById(int id)
        {
            List<StudentResponse> student = (from studen in mpostgresDbContext.Student
                                             where studen.Id == id
                                             select new StudentResponse

                                             {
                                                 Id = studen.Id,
                                                 Name = studen.Name,
                                                 Age = studen.Age,
                                                 Discription = (studen.Age >= 18 ? "Adult" : "Minor")
                                             }).ToList();
            return student;


        }
        [HttpPut("updataData")]

        public string UpdateData(UpdateRequest updateRequest)
        {
            Student student = mpostgresDbContext.Student.Where(x => x.Id == updateRequest.Id).FirstOrDefault();


            student.Age = updateRequest.Age;
            student.Name = updateRequest.Name;

            mpostgresDbContext.Student.Update(student);
            mpostgresDbContext.SaveChanges();
            return "Created Successfully";
        }


        [HttpDelete("studentUpdate")]

        public bool StudentUpdate(int id)
        {
            Student student = mpostgresDbContext.Student.Where(x => x.Id == id).FirstOrDefault();
            if (student != null)
            {
                mpostgresDbContext.Student.Remove(student);
                mpostgresDbContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }




        }
        [HttpDelete("studentDelete")]


        public bool StudentDelete(int id)
        {
            // Student student = mpostgresDbContext.Student.Where(x => x.Id == id).FirstOrDefault();
            Student? student = (from stu in mpostgresDbContext.Student
                                where stu.Id == id
                                select
                                new Student
                                {
                                    Id = stu.Id,
                                    Name = stu.Name,
                                    Age = stu.Age,
                                }).FirstOrDefault();
            mpostgresDbContext.Student.Remove(student);
            mpostgresDbContext.SaveChanges();
            return student != null;



        }

        [HttpGet("getAllById")]

        public List<OfficeResponse> GetAllById(int id, bool hierarchyOrder)
        {

            if (hierarchyOrder)
            {
                List<OfficeResponse> officeSystem = (from office in mpostgresDbContext.Office
                                                     where office.Id == id
                                                     select new OfficeResponse()
                                                     {
                                                         Id = office.Id,
                                                         Name = office.Name,
                                                         ChildName = (from office1 in mpostgresDbContext.Office
                                                                      where office1.DefId == office.Id
                                                                      select new OfficeResponse1
                                                                      {
                                                                          Id = office1.Id,
                                                                          Name = office1.Name,
                                                                          Offices = (from office2 in mpostgresDbContext.Office
                                                                                     where office2.DefId == office1.Id
                                                                                     select new OfficeResponse2
                                                                                     {
                                                                                         Id = office2.Id,
                                                                                         Name = office2.Name,
                                                                                         System = (from office3 in mpostgresDbContext.Office
                                                                                                   where office3.DefId == office2.Id
                                                                                                   select new OfficeResponse3
                                                                                                   {
                                                                                                       Id = office3.Id,
                                                                                                       Name = office3.Name,
                                                                                                   }).ToList(),

                                                                                     }).ToList(),
                                                                      }).ToList(),

                                                     }).ToList();

                return officeSystem;
            }
            else
            {
                List<OfficeResponse> officeSystem = (from office3 in mpostgresDbContext.Office
                                                     where office3.Id == id
                                                     select new OfficeResponse()
                                                     {
                                                         Id = office3.Id,
                                                         Name = office3.Name,
                                                         ChildName = (from office2 in mpostgresDbContext.Office
                                                                      where office2.Id == office3.DefId
                                                                      select new OfficeResponse1
                                                                      {
                                                                          Id = office2.Id,
                                                                          Name = office2.Name,
                                                                          Offices = (from office1 in mpostgresDbContext.Office
                                                                                     where office1.Id == office2.DefId
                                                                                     select new OfficeResponse2
                                                                                     {
                                                                                         Id = office1.Id,
                                                                                         Name = office1.Name,
                                                                                         System = (from office in mpostgresDbContext.Office
                                                                                                   where office.Id == office1.DefId
                                                                                                   select new OfficeResponse3
                                                                                                   {
                                                                                                       Id = office.Id,
                                                                                                       Name = office.Name,
                                                                                                   }).ToList(),
                                                                                     }).ToList(),
                                                                      }).ToList(),
                                                     }).ToList();
                return officeSystem;
            }





        }



    }
}

   

    