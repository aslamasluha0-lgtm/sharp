using StudentCourseManagement;

var courseManager = new CourseManager();
var studentManager = new StudentManager();

ActionLogger logger = message => Console.WriteLine($"[LOG] {message}");
courseManager.Logger = logger;
studentManager.Logger = logger;

courseManager.AddCourse(1, ".NET");
courseManager.AddCourse(2, "Java");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Student-Course Management System ===");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. List Students");
    Console.WriteLine("3. Update Student");
    Console.WriteLine("4. Delete Student");
    Console.WriteLine("5. Add Course");
    Console.WriteLine("6. List Courses");
    Console.WriteLine("7. Filter Students Above Age");
    Console.WriteLine("8. Students In Course");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddStudent();
            break;
        case "2":
            PrintStudents(studentManager.ListStudents());
            break;
        case "3":
            UpdateStudent();
            break;
        case "4":
            DeleteStudent();
            break;
        case "5":
            AddCourse();
            break;
        case "6":
            PrintCourses();
            break;
        case "7":
            FilterByAge();
            break;
        case "8":
            FilterByCourse();
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

void AddStudent()
{
    var id = studentManager.NextId;
    var name = ReadName();
    if (name is null) return;

    var age = ReadInt("Age: ");
    if (age is null) return;

    var courseId = ReadCourseId();
    if (courseId is null) return;

    studentManager.AddStudent(new Student
    {
        Id = id,
        Name = name,
        Age = age.Value,
        CourseId = courseId.Value
    });
}

void UpdateStudent()
{
    var id = ReadInt("Student Id to update: ");
    if (id is null) return;

    var name = ReadName();
    if (name is null) return;

    var age = ReadInt("New age: ");
    if (age is null) return;

    var courseId = ReadCourseId();
    if (courseId is null) return;

    studentManager.UpdateStudent(id.Value, name, age.Value, courseId.Value);
}

void DeleteStudent()
{
    var id = ReadInt("Student Id to delete: ");
    if (id is null) return;

    studentManager.DeleteStudent(id.Value);
}

void AddCourse()
{
    var id = ReadInt("Course Id: ");
    if (id is null) return;

    var name = ReadName("Course name: ");
    if (name is null) return;

    courseManager.AddCourse(id.Value, name);
}

void PrintCourses()
{
    var courses = courseManager.ListCourses();
    if (courses.Count == 0)
    {
        Console.WriteLine("No courses found.");
        return;
    }

    foreach (var course in courses)
    {
        Console.WriteLine(course);
    }
}

void FilterByAge()
{
    var age = ReadInt("Minimum age (exclusive): ");
    if (age is null) return;

    PrintStudents(studentManager.GetStudentsAboveAge(age.Value));
}

void FilterByCourse()
{
    var courseId = ReadCourseId();
    if (courseId is null) return;

    PrintStudents(studentManager.GetStudentsByCourse(courseId.Value));
}

void PrintStudents(IEnumerable<Student> students)
{
    var list = students.ToList();
    if (list.Count == 0)
    {
        Console.WriteLine("No students found.");
        return;
    }

    foreach (var student in list)
    {
        var courseName = courseManager.GetCourse(student.CourseId)?.CourseName ?? "Unknown";
        Console.WriteLine($"{student} ({courseName})");
    }
}

string? ReadName(string prompt = "Name: ")
{
    Console.Write(prompt);
    var value = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(value))
    {
        Console.WriteLine("Name cannot be empty.");
        return null;
    }

    return value;
}

int? ReadInt(string prompt)
{
    Console.Write(prompt);
    if (int.TryParse(Console.ReadLine(), out var value))
    {
        return value;
    }

    Console.WriteLine("Invalid number.");
    return null;
}

int? ReadCourseId()
{
    var courseId = ReadInt("Course Id: ");
    if (courseId is null) return null;

    if (!courseManager.CourseExists(courseId.Value))
    {
        Console.WriteLine("Course does not exist. Add the course first.");
        return null;
    }

    return courseId;
}
