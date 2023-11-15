using Server.DataModels; 
using Server.DataAccess;
using System;

public class DatabaseInitializer
{
    public void Initialize()
    {
        var context = new MyDbContext();

        // this SHOULD clear and then create the db if we are lucky
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var projectList = new[] {
            new Project { Name = "My own" },
            new Project { Name = "Free Time" },
            new Project { Name = "Work" }
        };
        foreach (var project in projectList)
        {
            context.Projects.Add(project);
        }
        context.SaveChanges(); 

        AddUsers(context);
        AddTimeLogs(context, projectList);
    }


    private void AddUsers(MyDbContext context)
    {
        var rng = new Random();
        var firstNames = new[] { "John", "Gringo", "Mark", "Lisa", "Maria", "Sonya", "Philip", "Jose", "Lorenzo", "George", "Justin" };
        var lastNames = new[] { "Johnson", "Lamas", "Jackson", "Brown", "Mason", "Rodriguez", "Roberts", "Thomas", "Rose", "McDonald" };
        var domains = new[] { "hotmail.com", "gmail.com", "live.com" };

        for (int i = 0; i < 100; i++) // making 100 users
        {
            var firstName = firstNames[rng.Next(firstNames.Length)];
            var lastName = lastNames[rng.Next(lastNames.Length)];
            var domain = domains[rng.Next(domains.Length)];
            // making a name out of the email and the domain
            var email = $"{firstName}.{lastName}@{domain}".ToLower();

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            context.Users.Add(user);
        }
        context.SaveChanges();
    }

      private void AddTimeLogs(MyDbContext context, Project[] projectList)
    {
        var rng = new Random(); 
        var allUsers = context.Users.ToList(); // getting all users 

        foreach (var user in allUsers)
        {
            var logCount = rng.Next(1, 21); 

            for (int i = 0; i < logCount; i++) 
            {
                var randomDate = DateTime.Now.AddDays(-rng.Next(30)); // a date in the last 30 days
                var hours = (float)(rng.NextDouble() * (8.00 - 0.25) + 0.25); 

                var log = new TimeLog
                {
                    UserId = user.UserId,
                    ProjectId = projectList[rng.Next(projectList.Length)].ProjectId,
                    Date = randomDate,
                    HoursWorked = hours
                };

                context.TimeLogs.Add(log); 
        }
        context.SaveChanges();
    }
}
}