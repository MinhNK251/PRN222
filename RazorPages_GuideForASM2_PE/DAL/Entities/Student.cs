namespace RazorPagesSample.ServiceLayers.DTOs
{
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public Student(int id, string studentName) { 
            StudentId = id;
            StudentName = studentName;
        }

        public Student() { }
    }
}
