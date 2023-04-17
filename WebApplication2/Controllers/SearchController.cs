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
            
            var students = _context.student.Where(s => s.studentname.Contains(name)).ToList();

            return View(students);
        }

        public IActionResult ByCourse(string course)
        {
            var studentsAndGrades = _context.grade
                .Where(g => g.coursecode == course)
                .Select(g => new { g.student, g.grade1 })
                .ToList();
            Console.WriteLine(studentsAndGrades.Count);

            ViewBag.students = studentsAndGrades;

            return View();
        }

        public IActionResult ByGrade(char grade)
        {
            
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
    
            return View();
        }




        public IActionResult FailedStudents()
        {
            var failedStudents = _context.grade
                .Where(g => g.grade1 == 'F')
                .Join(_context.student, g => g.studentid,s => s.id, (g,s) => new{g,s} )
                .Join(_context.course,
                    gs =>gs.g.coursecode,
                    c => c.coursecode,
                    (gs,c) => new { gs.g.studentid, gs.s.studentname, c.coursename})
                .ToList();

            ViewBag.failed = failedStudents;

            return View();
        }
    }
}
