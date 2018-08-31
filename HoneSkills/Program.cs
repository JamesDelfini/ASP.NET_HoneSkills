using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoneSkills
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float FullPrice { get; set; }
            
        public CourseLevel Level { get; set; }
        // One:Many Relationship
        public Author Author { get; set; }
        // Many:Many Relationship
        public IList<Tag> Tags { get; set; }
    }
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
     }
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }
    public enum CourseLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advance = 3
    }
    public class Test
    {
        public int Id { get; set; }
        public string Body { get; set; }
    }

    public class PlutoContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Test> Tests { get; set; }

        // If the default conventation of the database is changed.
        public PlutoContext()
            : base("name=DefaultConnection")
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            // LINQ has two different to write queries which are LINQ Syntax (Like SQL) or Extension Methods.

            // LINQ Syntax
            var query =
                from c in context.Courses
                where c.Title.Contains("c#")
                orderby c.Title
                select c;
            foreach(var course in query)
                Console.WriteLine(course.Title);

            Console.WriteLine();

            // Extension Methods
            var courses = context.Courses
                .Where(c => c.Title.Contains("c#"))
                .OrderBy(c => c.Title);

            foreach (var course in courses)
                Console.WriteLine(course.Title);

            Console.WriteLine();


            // LINQ SYNTAX Queries
            // Queries
            var newQuery =
                from c in context.Courses
                where c.Author.Id == 1
                orderby c.Level descending, c.Title
                select new { Title = c.Title, Author = c.Author.Name };

            foreach(var course in newQuery)
                Console.WriteLine("{0} by {1}", course.Title, course.Author);

            Console.WriteLine();

            // Groupings
            var groups =
                from c in context.Courses
                group c by c.Level
                into g
                select g;

            foreach (var group in groups)
            {
                Console.WriteLine("{0} ({1})", group.Key, group.Count());

                foreach (var course in group)
                    Console.WriteLine("\t{0}", course.Title);
            }

            Console.WriteLine();

            // Joins 
            // Inner Join
            var innerJoin =
                from c in context.Courses
                join a in context.Authors on c.Author.Id equals a.Id
                select new { Title = c.Title, Author = a.Name };
            foreach (var inner in innerJoin)
                Console.WriteLine("{0} by {1}", inner.Title, inner.Author);

            Console.WriteLine();

            // Group Join 
            var groupJoin = 
                from c in context.Authors
                join a in context.Courses on c.Id equals a.Author.Id into g
                select new { Count = g.Count(), Author = c.Name };
            foreach (var group in groupJoin)
                Console.WriteLine("{0} has {1} courses.", group.Author, group.Count);

            Console.WriteLine();

            // Cross Join
            var crossJoin = 
                from c in context.Courses
                from a in context.Authors
                select new { Title = c.Title, Author = a.Name };
            foreach (var cross in crossJoin)
                Console.WriteLine("{0} by {1}", cross.Title, cross.Author);

            Console.WriteLine();

            // LINQ EXTENSIONS
            // QUERIES
            var extCourses = context.Courses
                .Where(c => c.Level == CourseLevel.Beginner)
                .OrderByDescending(c => c.Title)
                .ThenByDescending(c => c.Level)
                .Select(c => new { Title = c.Title, AuthorName = c.Author.Name });
            foreach (var ext in extCourses)
                Console.WriteLine("{0} \n \t by {1}", ext.Title, ext.AuthorName);

            Console.WriteLine();

            var tags1 = context.Courses
                .Where(c => c.Level == CourseLevel.Beginner)
                .OrderByDescending(c => c.Title)
                .ThenByDescending(c => c.Level)
                .Select(c => c.Tags);
            Console.WriteLine();
            Console.WriteLine("Using Select is a list of another list of tags.");
            foreach (var tag in tags1)
            {
                foreach (var _tag in tag)
                    Console.WriteLine(_tag.Name);
            }

            var tags2 = context.Courses
                .Where(c => c.Level == CourseLevel.Beginner)
                .OrderByDescending(c => c.Title)
                .ThenByDescending(c => c.Level)
                .SelectMany(c => c.Tags);
            Console.WriteLine();
            Console.WriteLine("Using SelectMany is only list of tags.");
            foreach (var tag in tags2)
                Console.WriteLine(tag.Name);

            Console.WriteLine();

            // Groupings
            var extGroupings = context.Courses.GroupBy(c => c.Level);
            foreach(var group in extGroupings)
            {
                Console.WriteLine(group.Key);
                foreach (var course in group)
                    Console.WriteLine("\t" + course.Title);
            }

            Console.WriteLine();
            // Joins
            // Inner Join
            var extInnerJoin = context.Courses
                .Join(context.Authors,  
                        c => c.Author.Id, 
                        a => a.Id, 
                        (course, author) => new
                            {
                                Title = course.Title,
                                Author = author.Name
                            });
            foreach (var ext in extInnerJoin)
                Console.WriteLine("{0} by {1}", ext.Title, ext.Author);

            // Group Join
            var extGroupJoin = context.Authors.GroupJoin(context.Courses, a => a.Id, c => c.Author.Id, (author, course) => new
            {
                Author = author.Name,
                 Courses = course
                // Courses = course.Count()
            });

            // Cross Join
            var extCrossJoin = context.Authors.SelectMany(a => context.Courses, (author, course) => new
            {
                Author = author.Name,
                Title = course.Title
            });

            //Partitioning
            var pages = context.Courses.Skip(10).Take(10);
            // Element Operators
            var element = context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);
            //context.Courses.LastOrDefault; // Except using with Database SQL
            //context.Courses.SingleOrDefault; // Returns results of a single output.
            //context.Courses.All(c => c.FullPrice > 10) // Returns a boolean; All, Any
            //context.Courses.Count() // Returns a number of items; Count, Max, Min, Average
        }
    }
}
