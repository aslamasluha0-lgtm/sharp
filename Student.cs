namespace StudentCourseManagement;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int CourseId { get; set; }

    public override string ToString()
        => $"Id: {Id}, Name: {Name}, Age: {Age}, CourseId: {CourseId}";
}
