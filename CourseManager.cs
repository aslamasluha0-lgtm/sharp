namespace StudentCourseManagement;

public class CourseManager
{
    private readonly Dictionary<int, Course> _courses = new();

    public ActionLogger? Logger { get; set; }

    public bool AddCourse(int courseId, string courseName)
    {
        if (_courses.ContainsKey(courseId))
        {
            Logger?.Invoke($"Course {courseId} already exists.");
            return false;
        }

        var course = new Course { CourseId = courseId, CourseName = courseName };
        _courses.Add(courseId, course);
        Logger?.Invoke($"Added course: {course.CourseName} (Id {courseId}).");
        return true;
    }

    public IReadOnlyCollection<Course> ListCourses()
        => _courses.Values.ToList().AsReadOnly();

    public Course? GetCourse(int courseId)
        => _courses.TryGetValue(courseId, out var course) ? course : null;

    public bool CourseExists(int courseId)
        => _courses.ContainsKey(courseId);
}
