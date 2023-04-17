using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class SearchController : Controller
    {
        private readonly MyDbContext _context = new ();

        public IActionResult Index()
        {
            var courses = _context.course
                .Where(g => true)
                .ToList();
            ViewBag.Courses = courses;
            
            return View();
        }

        public IActionResult ByName(string name)
        {
            
            // Your logic to search for students by partial name
            var students = _context.student.Where(s => s.studentname.Contains(name)).ToList();

            // Pass the result to the view
            return View(students);
        }

        public IActionResult ByCourse(string course)
        {
            // Your logic to get students and their grades for a specific course
            var studentsAndGrades = _context.grade
                .Where(g => g.coursecode == course)
                .Select(g => new { g.student, g.grade1 })
                .ToList();
            Console.WriteLine(studentsAndGrades.Count);

            ViewBag.students = studentsAndGrades;

            // Pass the result to the view
            return View();
        }

        public IActionResult ByGrade(char grade)
        {
            
            // Your logic to get students and their grades equal to or better than the specified grade
            var studentsAndGrades = _context.grade
                .Where(g => g.grade1 <= char.ToUpper(grade))
                 .Join(_context.student, g => g.studentid,s => s.id, (g,s) => new{g,s} )
                .Join(_context.course,
                    gs =>gs.g.coursecode,
                    c => c.coursecode,
                    (gs,c) => new {gs.g.coursecode, gs.g.studentid, gs.s.studentname, c.coursename,gs.g.grade1})
                .ToList();
            
            
            Console.WriteLine(studentsAndGrades.Count);

            ViewBag.grade = studentsAndGrades;
    
            // Pass the result to the view
            return View();
        }




        public IActionResult FailedStudents()
        {
            // Your logic to get students who failed and the courses they failed in
            var failedStudents = _context.grade
                .Where(g => g.grade1 == 'F')
                .Join(_context.student, g => g.studentid,s => s.id, (g,s) => new{g,s} )
                .Join(_context.course,
                    gs =>gs.g.coursecode,
                    c => c.coursecode,
                    (gs,c) => new { gs.g.studentid, gs.s.studentname, c.coursename})
                .ToList();

            ViewBag.failed = failedStudents;

            // Pass the result to the view
            return View();
        }
    }
}
