using _2.OneToMany;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        MyDbContext db = new MyDbContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var department = new Department { Name = "Dep" };

        department.Employees.Add(new Employee { Name = "Pesho" });

        var result = db.Departments
            .Where(d => d.Id == 1)
            .Select(d => new
            {
                d.Name,
                Employess = d.Employees.Count
            })
            .FirstOrDefault();

        db.Departments.Add(department);
        db.SaveChanges();
    }
}

