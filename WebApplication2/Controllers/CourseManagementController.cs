using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using WebApplication2.Models;
using System.Linq;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    public class CourseManagementController : Controller
    {
        private readonly MyDbContext _context = new ();


        public IActionResult Index()
        {
            ViewBag.coursess = _context.course.ToList();
            return View();
        }

        public IActionResult CourseDetails(string courseCode)
        {
            var course = _context.course.FirstOrDefault(c => c.coursecode == courseCode);
            if (course != null)
            {
                 var studentsAndGrades = _context.grade
                                .Where(g => g.coursecode == courseCode)
                               .Select(g => new { g.student, g.grade1 })
                               .ToList(); 
               
                 
                  ViewBag.Students = studentsAndGrades;
                  ViewBag.Course = course;
                  return View();
            }

            return Ok();

        }

        public IActionResult AddStudent(string courseCode)
        {
            ViewBag.courseCode = courseCode;
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(string courseCode, int studentId, char grade)
        {
            Console.WriteLine(courseCode);
            var newGrade = new grade
            {
                coursecode = courseCode,
                studentid = studentId,
                grade1 = char.ToUpper(grade)
            };
            
            Console.WriteLine(_context.grade.Count());
            _context.grade.Add(newGrade);
            _context.SaveChanges();
            
            Console.WriteLine(_context.grade.Count());

            return RedirectToAction("CourseDetails", new { courseCode });
        }

        public IActionResult RemoveStudent(string courseCode, int studentId)
        {
            var gradeToRemove = _context.grade.Find(courseCode, studentId);
            if (gradeToRemove != null)
            {
                _context.grade.Remove(gradeToRemove);
                _context.SaveChanges();
            }

            return RedirectToAction("CourseDetails", new { courseCode });
        }

        public IActionResult UpdateGrade(string courseCode, int studentId, char newGrade)
        {
            var gradeToUpdate = _context.grade.Find(courseCode, studentId);
            if (gradeToUpdate != null)
            {
                gradeToUpdate.grade1 = newGrade;
                _context.SaveChanges();
            }

            return RedirectToAction("CourseDetails", new { courseCode });
        }
    }
}
