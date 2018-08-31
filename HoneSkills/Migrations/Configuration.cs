namespace HoneSkills.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HoneSkills.PlutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HoneSkills.PlutoContext context)
        {
            #region Add Tags
            var tags = new Dictionary<string, Tag>
            {
                {"c#", new Tag {Id = 1, Name = "c#"}},
                {"angularjs", new Tag {Id = 2, Name = "angularjs"}},
                {"javascript", new Tag {Id = 3, Name = "javascript"}},
                {"nodejs", new Tag {Id = 4, Name = "nodejs"}},
                {"oop", new Tag { Id = 5, Name = "oop" }},
                {"linq", new Tag {Id = 6, Name = "linq"}},
            };
            foreach (var tag in tags.Values)
                context.Tags.AddOrUpdate(t => t.Id, tag);
            #endregion

            #region Add Authors
            var authors = new List<Author>
            {
                new Author
                {
                    Id = 1,
                    Name = "Dennis James Matildo"
                },
                new Author
                {
                    Id = 2,
                    Name = "Ejay Matildo"
                },
                new Author
                {
                    Id = 3,
                    Name = "Khalid Darkouch",
                    Courses = new Collection<Course>()
                },
                new Author
                {
                    Id = 4,
                    Name = "Mark Anthony Consellado",
                    Courses = new Collection<Course>()
                }
            };
            foreach(var author in authors)
            {
                context.Authors.AddOrUpdate(a => a.Id, author);
            }
            #endregion

            #region Add Courses
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Title = "C# Basics",
                    Author = authors[0],
                    FullPrice = 49,
                    Description = "Description for C# Basics",
                    Level = CourseLevel.Beginner,
                    Tags = new Collection<Tag>()
                    {
                        tags["c#"]
                    }
                },
                new Course
                {
                    Id = 2,
                    Title = "C# Intermediate",
                    Author = authors[0],
                    FullPrice = 49,
                    Description = "Description C# Intermediate",
                    Level = CourseLevel.Intermediate,
                    Tags = new Collection<Tag>
                    {
                        tags["c#"],
                        tags["oop"]
                    }
                },
                new Course
                {
                    Id = 3,
                    Title = "C# Advanced",
                    Author = authors[0],
                    FullPrice = 49,
                    Description = "Description C# Advanced",
                    Level = CourseLevel.Advance,
                    Tags = new Collection<Tag>
                    {
                        tags["c#"]
                    }
                },
                new Course
                {
                    Id = 4,
                    Title = "Javascript: Understanding the Weird Parts",
                    Author = authors[1],
                    FullPrice = 149,
                    Description = "Description for Javascript",
                    Level = CourseLevel.Intermediate,
                    Tags = new Collection<Tag>
                    {
                        tags["javascript"]
                    }
                },
                new Course
                {
                    Id = 5,
                    Title = "Learn and Understand AngularJS",
                    Author = authors[1],
                    FullPrice = 99,
                    Description = "Description for AngularJS",
                    Level = CourseLevel.Intermediate,
                    Tags = new Collection<Tag>
                    {
                        tags["angularjs"]
                    }
                },
                new Course
                {
                    Id = 6,
                    Title = "Learn and Understand NodeJS",
                    Author = authors[2],
                    FullPrice = 149,
                    Description = "Description for NodeJS",
                    Level = CourseLevel.Intermediate,
                    Tags = new Collection<Tag>
                    {
                        tags["nodejs"]
                    }
                },
                new Course
                {
                    Id = 7,
                    Title = "Programming for Complete Beginners",
                    Author = authors[2],
                    FullPrice = 45,
                    Description = "Description for Beginners",
                    Level = CourseLevel.Beginner,
                    Tags = new Collection<Tag>
                    {
                        tags["c#"]
                    }
                },
                new Course
                {
                    Id = 8,
                    Title = "A 16 Hour C# Course with Visual Studio 2013",
                    Author = authors[3],
                    FullPrice = 150,
                    Description = "Description for Beginners",
                    Level = CourseLevel.Beginner,
                    Tags = new Collection<Tag>
                    {
                        tags["c#"]
                    }
                },
                new Course
                {
                    Id = 9,
                    Title = "Learn JavaScript Through Visual Studio 2013",
                    Author = authors[3],
                    FullPrice = 20,
                    Description = "Description for Javascript",
                    Level = CourseLevel.Beginner,
                    Tags = new Collection<Tag>
                    {
                        tags["javascript"]
                    }
                }
            };
            foreach (var course in courses)
                context.Courses.AddOrUpdate(c => c.Id, course);
            #endregion
        }
    }
}
