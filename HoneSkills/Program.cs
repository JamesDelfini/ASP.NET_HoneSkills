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
        }
    }
}
