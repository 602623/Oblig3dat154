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
            // List all courses
            ViewBag.coursess = _context.course.ToList();
            return View();
        }

        public IActionResult CourseDetails(string courseCode)
        {
            // List all students and their grades for the selected course
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
            // Show a form to add a new student to the course
            ViewBag.courseCode = courseCode;
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(string courseCode, int studentId, char grade)
        {
            // Add the new student to the course with their grade
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
            // Remove the student from the course
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
            // Update the student's grade in the course
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
