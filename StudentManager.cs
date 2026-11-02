namespace StudentCourseManagement;

public class StudentManager
{
    private readonly List<Student> _students = new();

    public ActionLogger? Logger { get; set; }

    public int NextId => _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;

    public bool AddStudent(Student student)
    {
        if (_students.Any(s => s.Id == student.Id))
        {
            Logger?.Invoke($"Student Id {student.Id} already exists.");
            return false;
        }

        _students.Add(student);
        Logger?.Invoke($"Added student: {student.Name} (Id {student.Id}).");
        return true;
    }

    public IReadOnlyList<Student> ListStudents()
        => _students.AsReadOnly();

    public bool UpdateStudent(int id, string name, int age, int courseId)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            Logger?.Invoke($"Update failed: student Id {id} not found.");
            return false;
        }

        student.Name = name;
        student.Age = age;
        student.CourseId = courseId;
        Logger?.Invoke($"Updated student Id {id}.");
        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            Logger?.Invoke($"Delete failed: student Id {id} not found.");
            return false;
        }

        _students.Remove(student);
        Logger?.Invoke($"Deleted student: {student.Name} (Id {id}).");
        return true;
    }

    public IEnumerable<Student> GetStudentsAboveAge(int age)
        => _students.Where(s => s.Age > age);

    public IEnumerable<Student> GetStudentsByCourse(int courseId)
        => _students.Where(s => s.CourseId == courseId);
}
