namespace StudentCourseManagement;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;

    public override string ToString()
        => $"CourseId: {CourseId}, CourseName: {CourseName}";
}
