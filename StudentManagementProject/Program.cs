using CSharpFinalProject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;

namespace StudentAttendanceSystem
{
    // Define the user types as an enum
    public enum UserType
    {
        Admin,
        Teacher,
        Student
    }

    // Define a base class for all users
    public abstract class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        // A user can belong to one or more courses
        public virtual ICollection<Course> Courses { get; set; }
    }

    // Define a class for admin users
    public class Admin : User
    {
        // An admin can create teachers, courses, and students
        public void CreateTeacher(string name, string username, string password)
        {
            using (var db = new AttendanceDbContext())
            {
                // Check if the username is already taken
                if (db.Users.Any(u => u.Username == username))
                {
                    Console.WriteLine("Username already exists.");
                    return;
                }

                // Create a new teacher object and add it to the database
                var teacher = new Teacher()
                {
                    Name = name,
                    Username = username,
                    Password = password
                };
                db.Users.Add(teacher);
                db.SaveChanges();
                Console.WriteLine("Teacher created successfully.");
            }
        }
        public void DeleteTeacher(int teacherId)
        {
            using (var db = new AttendanceDbContext())
            {
                var teacher = db.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == teacherId);

                if (teacher == null)
                {
                    Console.WriteLine("Teacher not found.");
                    return;
                }

                db.Users.Remove(teacher);
                db.SaveChanges();
                Console.WriteLine("Teacher deleted successfully.");
            }
        }
        public void UpdateTeacher(int idToUpdate)
        {
            using (var db = new AttendanceDbContext())
            {
                var teacherToUpdate = db.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == idToUpdate);

                if (teacherToUpdate != null)
                {
                    Console.Write("Enter new Teacher Name: ");
                    teacherToUpdate.Name = Console.ReadLine();
                    Console.Write("Enter new UserName: ");
                    teacherToUpdate.Username = Console.ReadLine();
                    Console.Write("Enter new PassWord: ");
                    teacherToUpdate.Password = Console.ReadLine();

                    db.SaveChanges();
                    Console.WriteLine("Teacher Update successfully.");

                }
                else
                {
                    Console.WriteLine("Teacher not found.");
                }

            }
            
        }


        public void CreateCourse(string courseName, decimal fees, DateTime startDate, DateTime endDate)
        {
            using (var db = new AttendanceDbContext())
            {
                // Check if the course name is already taken
                if (db.Courses.Any(c => c.CourseName == courseName))
                {
                    Console.WriteLine("Course name already exists.");
                    return;
                }

                // Create a new course object and add it to the database
                var course = new Course()
                {
                    CourseName = courseName,
                    Fees = fees,
                    StartDate = startDate,
                    EndDate = endDate
                };
                db.Courses.Add(course);
                db.SaveChanges();
                Console.WriteLine("Course created successfully.");
            }
        }
       
        public void CreateStudent(string name, string username, string password)
        {
            using (var db = new AttendanceDbContext())
            {
                // Check if the username is already taken
                if (db.Users.Any(u => u.Username == username))
                {
                    Console.WriteLine("Username already exists.");
                    return;
                }

                // Create a new student object and add it to the database
                var student = new Student()
                {
                    Name = name,
                    Username = username,
                    Password = password
                };
                db.Users.Add(student);
                db.SaveChanges();
                Console.WriteLine("Student created successfully.");
            }
        }
        public void UpdateStudent(int idToUpdate)
        {
            using (var db = new AttendanceDbContext())
            {
                var toUpdate = db.Users.OfType<Student>().FirstOrDefault(t => t.Id == idToUpdate);

                if (toUpdate != null)
                {
                    Console.Write("Enter new Student Name: ");
                    toUpdate.Name = Console.ReadLine();
                    Console.Write("Enter new UserName: ");
                    toUpdate.Username = Console.ReadLine();
                    Console.Write("Enter new PassWord: ");
                    toUpdate.Password = Console.ReadLine();

                    db.SaveChanges();
                    Console.WriteLine("Student Update successfully.");

                }
                else
                {
                    Console.WriteLine("Student not found.");
                }

            }

        }
        public void DeleteStudent(int studentId)
        {
            using (var db = new AttendanceDbContext())
            {
                var student = db.Users.OfType<Student>().FirstOrDefault(s => s.Id == studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                db.Users.Remove(student);
                db.SaveChanges();
                Console.WriteLine("Student deleted successfully.");
            }
        }



        // An admin can assign a teacher to a course
        public void AssignTeacher(int teacherId, int courseId)
        {
            using (var db = new AttendanceDbContext())
            {
                // Find the teacher and the course by their ids
                var teacher = db.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == teacherId);
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

                // Check if they exist and are valid
                if (teacher == null)
                {
                    Console.WriteLine("Teacher not found.");
                    return;
                }

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                if (teacher.Courses == null)
                {
                    teacher.Courses = new List<Course>(); // Initialize the collection
                }

                if (!teacher.Courses.Contains(course))
                {
                    // Add the course to the teacher's courses collection and save the changes
                    teacher.Courses.Add(course);
                    db.SaveChanges();
                    Console.WriteLine("Teacher assigned to course successfully.");
                }
                else
                {
                    Console.WriteLine("Teacher is already assigned to this course.");
                }
                // Add the course to the teacher's courses collection and save the changes

            }
        }

        // An admin can assign a student to a course
        public void AssignStudent(int studentId, int courseId)
        {
            using (var db = new AttendanceDbContext())
            {
                // Find the student and the course by their ids
                var student = db.Users.OfType<Student>().FirstOrDefault(s => s.Id == studentId);
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

                // Check if they exist and are valid
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                if (student.Courses == null)
                {
                    student.Courses = new List<Course>(); // Initialize the collection
                }

                if (!student.Courses.Contains(course))
                {
                    // Add the course to the teacher's courses collection and save the changes
                    student.Courses.Add(course);
                    db.SaveChanges();
                    Console.WriteLine("Student enrolled in course successfully.");
                }
                else
                {
                    Console.WriteLine("Student is already enrolled in this course.");
                    return;
                }
            }
        }

        // An admin can set the class schedule for a course
        public void SetClassSchedule(int courseId, string day, string time, int totalClasses)
        {
            using (var db = new AttendanceDbContext())
            {
                // Find the course by its id
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

                // Check if it exists and is valid
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                if (course.ClassSchedule != null)
                {
                    Console.WriteLine("Course already has a class schedule.");
                    return;
                }

                // Create a new class schedule object and add it to the database
                var classSchedule = new ClassSchedule()
                {
                    CourseId = courseId,
                    Day = day,
                    Time = time,
                    TotalClasses = totalClasses
                };
                db.ClassSchedules.Add(classSchedule);
                db.SaveChanges();
                Console.WriteLine("Class schedule set successfully.");
            }
        }
    }

    // Define a class for teacher users
    public class Teacher : User
    {
        // A teacher can check the attendance report of any course
        public void CheckAttendanceReport(int courseId)
        {
            using (var db = new AttendanceDbContext())
            {
                // Find the course by its id
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

                // Check if it exists and is valid
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                if (course.ClassSchedule == null)
                {
                    Console.WriteLine("Course does not have a class schedule.");
                    return;
                }

                // Print the course name and the class schedule
                Console.WriteLine($"Course name: {course.CourseName}");
                Console.WriteLine($"Class schedule: {course.ClassSchedule.Day} {course.ClassSchedule.Time}, {course.ClassSchedule.TotalClasses} classes");

                // Print the header for the attendance report table
                Console.WriteLine("Student name\t\tAttendance");

                // Loop through each student enrolled in the course
                foreach (var student in course.Students)
                {
                    // Print the student name
                    Console.Write($"{student.Name}\t\t");

                    // Loop through each class date of the course
                    for (int i = 1; i <= course.ClassSchedule.TotalClasses; i++)
                    {
                        // Get the class date by adding i days to the start date of the course
                        var classDate = course.StartDate.AddDays(i);

                        // Check if the student has given attendance on that date
                        var attendance = db.Attendances.FirstOrDefault(a => a.StudentId == student.Id && a.CourseId == courseId && a.Date == classDate);

                        // Print a check mark or a cross depending on the attendance status
                        if (attendance != null && attendance.Status == true)
                        {
                            Console.Write("✓\t");
                        }
                        else
                        {
                            Console.Write("✗\t");
                        }
                    }

                    // Print a new line after each student row
                    Console.WriteLine();
                }
            }
        }
    }

    // Define a class for student users
    public class Student : User
    {
        // A student can give attendance in the course he/she is enrolled in
        public void GiveAttendance(int courseId)
        {
            using (var db = new AttendanceDbContext())
            {
                // Find the course by its id
                var course = db.Courses.FirstOrDefault(c => c.Id == courseId);

                // Check if it exists and is valid
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                if (course.ClassSchedule == null)
                {
                    Console.WriteLine("Course does not have a class schedule.");
                    return;
                }

                if (!this.Courses.Contains(course))
                {
                    Console.WriteLine("You are not enrolled in this course.");
                    return;
                }

                // Get the current date and time
                var currentDate = DateTime.Now.Date;
                var currentTime = DateTime.Now.TimeOfDay;

                // Check if the current date and time are within the class schedule range
                if (currentDate < course.StartDate || currentDate > course.EndDate)
                {
                    Console.WriteLine("You can't give attendance outside of the course duration.");
                    return;
                }

                if (currentDate.DayOfWeek.ToString() != course.ClassSchedule.Day)
                {
                    Console.WriteLine("You can't give attendance on this day.");
                    return;
                }

                var startTime = TimeSpan.Parse(course.ClassSchedule.Time.Split('-')[0]);
                var endTime = TimeSpan.Parse(course.ClassSchedule.Time.Split('-')[1]);

                if (currentTime < startTime || currentTime > endTime)
                {
                    Console.WriteLine("You can't give attendance outside of the class time.");
                    return;
                }

                // Check if the student has already given attendance on this date
                var attendance = db.Attendances.FirstOrDefault(a => a.StudentId == this.Id && a.CourseId == courseId && a.Date == currentDate);

                if (attendance != null)
                {
                    Console.WriteLine("You have already given attendance for this class.");
                    return;
                }

                // Create a new attendance object and add it to the database
                var newAttendance = new Attendance()
                {
                    StudentId = this.Id,
                    CourseId = courseId,
                    Date = currentDate,
                    Status = true
                };
                db.Attendances.Add(newAttendance);
                db.SaveChanges();
                Console.WriteLine("Attendance given successfully.");
            }
        }
    }

    // Define a class for courses
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourseName { get; set; }

        [Required]
        public decimal Fees { get; set; }

        // A course can have one or more teachers
        public virtual ICollection<Teacher> Teachers { get; set; }

        // A course can have one or more students
        public virtual ICollection<Student> Students { get; set; }

        // A course can have one class schedule
        public virtual ClassSchedule ClassSchedule { get; set; }

        // A course can have a start date and an end date
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    // Define a class for class schedules
    public class ClassSchedule
    {
        [Key]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Day { get; set; }

        [Required]
        [MaxLength(20)]
        public string Time { get; set; }

        [Required]
        public int TotalClasses { get; set; }

        // A class schedule belongs to one course
        public virtual Course Course { get; set; }
    }

    // Define a class for attendances
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Status { get; set; }

        // An attendance belongs to one student and one course
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }

    // Define a class for the database context


    // Define a class for the main program logic
    public class Program
    {
        // Define a static field to store the current user
        private static User currentUser;

        // Define a static method to display the main menu and handle user input
        private static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Attendance System");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    MainMenu();
                    break;
            }
        }

        // Define a static method to login the user and check their type
        private static void Login()
        {
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();
            Console.Write("Enter your password: ");
            var password = Console.ReadLine();

            using (var db = new AttendanceDbContext())
            {
                // Find the user by their username and password
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                // Check if the user exists and is valid
                if (user == null)
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                    Login();
                    return;
                }
                else
                {
                    Console.WriteLine("You are Login Successfully.");
                }

                // Set the current user to the logged in user
                currentUser = user;

                // Check the user type and display the corresponding menu
                switch (user)
                {
                    case Admin admin:
                        AdminMenu(admin);
                        break;
                    case Teacher teacher:
                        TeacherMenu(teacher);
                        break;
                    case Student student:
                        StudentMenu(student);
                        break;
                    default:
                        Console.WriteLine("Unknown user type. Please contact the admin.");
                        MainMenu();
                        break;
                }
            }
        }

        // Define a static method to display the admin menu and handle user input
        private static void AdminMenu(Admin admin)
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Hello, {admin.Name}. You are logged in as an admin.");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Create a teacher");
            Console.WriteLine("2. Update a teacher");
            Console.WriteLine("3. Delete a Teacher");
            Console.WriteLine("4. Create a student");
            Console.WriteLine("5. Update a student");
            Console.WriteLine("6. Delete a student");
            Console.WriteLine("7. Create a course");
            Console.WriteLine("8. Assign a teacher to a course");
            Console.WriteLine("9. Assign a student to a course");
            Console.WriteLine("10. Set the class schedule for a course");
            Console.WriteLine("11. Logout");
            
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateTeacher(admin);
                    break;
                case "2":
                    UpdateTeacher(admin);
                    break;
                case "3":
                    DeleteTeacher(admin);
                    break;
                case "4":
                    CreateStudent(admin);
                    break;
                case "5":
                    UpdateStudent(admin);
                    break;
                case "6":
                    DeleteStudent(admin);
                    break;
                case "7":
                    CreateCourse(admin);
                    break;
                case "8":
                    AssignTeacher(admin);
                    break;
                case "9":
                    AssignStudent(admin);
                    break;
                case "10":
                    SetClassSchedule(admin);
                    break;
                case "11":
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    AdminMenu(admin);
                    break;
            }
        }

        // Define a static method to create a teacher using the admin object
        private static void CreateTeacher(Admin admin)
        {
            Console.Write("Enter the name of the teacher: ");
            var name = Console.ReadLine();
            Console.Write("Enter the username of the teacher: ");
            var username = Console.ReadLine();
            Console.Write("Enter the password of the teacher: ");
            var password = Console.ReadLine();

            // Call the admin method to create a teacher
            admin.CreateTeacher(name, username, password);

            // Go back to the admin menu
            AdminMenu(admin);
        }
        private static void DeleteTeacher(Admin admin)
        {
            Console.Write("Enter the Id of the DeleteTeacher: ");
            int idToDelete = int.Parse(Console.ReadLine());
            admin.DeleteTeacher(idToDelete);
            AdminMenu(admin);
        }
        private static void UpdateTeacher(Admin admin)
        {
            Console.Write("Enter Teacher ID to update: ");
            int idToUpdate = int.Parse(Console.ReadLine());
            admin.UpdateTeacher(idToUpdate);
            AdminMenu(admin);
        }
       
        // Define a static method to create a course using the admin object
        private static void CreateCourse(Admin admin)
        {
            Console.Write("Enter the name of the course: ");
            var courseName = Console.ReadLine();
            Console.Write("Enter the fees of the course: ");
            var fees = decimal.Parse(Console.ReadLine());
            Console.Write("Enter the start date and time of the course (yyyy-MM-dd HH:mm:ss): ");
            DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            Console.Write("Enter the end date and time of the course (yyyy-MM-dd HH:mm:ss): ");
            DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            // Call the admin method to create a course
            admin.CreateCourse(courseName, fees, startDate, endDate);

            // Go back to the admin menu
            AdminMenu(admin);
        }

        // Define a static method to create a student using the admin object
        private static void CreateStudent(Admin admin)
        {
            Console.Write("Enter the name of the student: ");
            var name = Console.ReadLine();
            Console.Write("Enter the username of the student: ");
            var username = Console.ReadLine();
            Console.Write("Enter the password of the student: ");
            var password = Console.ReadLine();

            // Call the admin method to create a student
            admin.CreateStudent(name, username, password);

            // Go back to the admin menu
            AdminMenu(admin);
        }
        private static void UpdateStudent(Admin admin)
        {
            Console.Write("Enter Student ID to update: ");
            int idToUpdate = int.Parse(Console.ReadLine());
            admin.UpdateStudent(idToUpdate);
            AdminMenu(admin);
        }
        private static void DeleteStudent(Admin admin)
        {
            Console.Write("Enter the Id of the DeleteStudent: ");
            int idToDelete = int.Parse(Console.ReadLine());
            admin.DeleteStudent(idToDelete);
            AdminMenu(admin);
        }


        // Define a static method to assign a teacher to a course using the admin object
        private static void AssignTeacher(Admin admin)
        {
            Console.Write("Enter the id of the teacher: ");
            var teacherId = int.Parse(Console.ReadLine());
            Console.Write("Enter the id of the course: ");
            var courseId = int.Parse(Console.ReadLine());

            // Call the admin method to assign a teacher to a course
            admin.AssignTeacher(teacherId, courseId);

            // Go back to the admin menu
            AdminMenu(admin);
        }

        // Define a static method to assign a student to a course using the admin object
        private static void AssignStudent(Admin admin)
        {
            Console.Write("Enter the id of the student: ");
            var studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter the id of the course: ");
            var courseId = int.Parse(Console.ReadLine());

            // Call the admin method to assign a student to a course
            admin.AssignStudent(studentId, courseId);

            // Go back to the admin menu
            AdminMenu(admin);
        }

        // Define a static method to set the class schedule for a course using the admin object
        private static void SetClassSchedule(Admin admin)
        {
            Console.Write("Enter the id of the course: ");
            var courseId = int.Parse(Console.ReadLine());
            Console.Write("Enter the day of the class (e.g. Sunday): ");
            var day = Console.ReadLine();
            Console.Write("Enter the time of the class (e.g. 8PM - 10PM): ");
            var time = Console.ReadLine();
            Console.Write("Enter the total number of classes: ");
            var totalClasses = int.Parse(Console.ReadLine());

            // Call the admin method to set the class schedule for a course
            admin.SetClassSchedule(courseId, day, time, totalClasses);

            // Go back to the admin menu
            AdminMenu(admin);
        }
        // Define a static method to display the teacher menu and handle user input
        private static void TeacherMenu(Teacher teacher)
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            //main code start from here
            Console.WriteLine($"Hello, {teacher.Name}. You are logged in as a teacher.");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Check the attendance report of a course");
            Console.WriteLine("2. Logout");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CheckAttendanceReport(teacher);
                    break;
                case "2":
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    TeacherMenu(teacher);
                    break;
            }
        }

        // Define a static method to check the attendance report of a course using the teacher object
        private static void CheckAttendanceReport(Teacher teacher)
        {
            Console.Write("Enter the id of the course: ");
            var courseId = int.Parse(Console.ReadLine());

            // Call the teacher method to check the attendance report of a course
            teacher.CheckAttendanceReport(courseId);

            // Go back to the teacher menu
            TeacherMenu(teacher);
        }

        // Define a static method to display the student menu and handle user input
        private static void StudentMenu(Student student)
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            //Main Code Start
            Console.WriteLine($"Hello, {student.Name}. You are logged in as a student.");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Give attendance in a course");
            Console.WriteLine("2. Logout");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GiveAttendance(student);
                    break;
                case "2":
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    StudentMenu(student);
                    break;
            }
        }

        // Define a static method to give attendance in a course using the student object
        private static void GiveAttendance(Student student)
        {
            Console.Write("Enter the id of the course: ");
            var courseId = int.Parse(Console.ReadLine());

            // Call the student method to give attendance in a course
            student.GiveAttendance(courseId);

            // Go back to the student menu
            StudentMenu(student);
        }

        // Define a static method to logout the user and go back to the main menu
        private static void Logout()
        {
            // Set the current user to null
            currentUser = null;

            // Go back to the main menu
            MainMenu();
        }

        // Define the main entry point of the program
        public static void Main(string[] args)
        {
            // Display the main menu
            MainMenu();
        }
    }
}
