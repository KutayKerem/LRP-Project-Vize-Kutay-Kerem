namespace LabYonetimSistemi.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "Student";
}

public class Lab
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Computer> Computers { get; set; } = new();
}

public class Computer
{
    public int Id { get; set; }
    public string? AssetCode { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Processor { get; set; } = string.Empty;
    public int Ram { get; set; }
    public bool HasHdmi { get; set; }
    public bool HasInternet { get; set; }
    public bool HasVeyon { get; set; }
    public int LabId { get; set; }
    public string? AssignedStudentUsername { get; set; }
}

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Grade { get; set; }
    public int ComputerId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
}

public class Issue
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
    public int ComputerId { get; set; }
}