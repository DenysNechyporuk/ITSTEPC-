namespace FileSystemHW
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] Grades { get; set; }
        public string Specialty { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} | {Specialty} | Grades :: {string.Join(", ", Grades)}";
        }
    }

    class FileWorker
    {
        public static void SaveStudents(Student[] students, string filename)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                writer.Write(students.Length);
                foreach (Student s in students)
                {
                    writer.Write(s.FirstName);
                    writer.Write(s.LastName);
                    writer.Write(s.Specialty);
                    writer.Write(s.Grades.Length);
                    foreach (int grade in s.Grades)
                        writer.Write(grade);
                }
            }
        }

        public static Student[] LoadStudents(string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                Student[] students = new Student[count];
                for (int i = 0; i < count; i++)
                {
                    Student s = new Student();
                    s.FirstName = reader.ReadString();
                    s.LastName = reader.ReadString();
                    s.Specialty = reader.ReadString();
                    int gradesCount = reader.ReadInt32();
                    s.Grades = new int[gradesCount];
                    for (int j = 0; j < gradesCount; j++)
                        s.Grades[j] = reader.ReadInt32();
                    students[i] = s;
                }
                return students;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Student[] students =
            {
                new Student { FirstName = "John", LastName = "Smith", Specialty = "CS", Grades = new int[] { 90, 85, 92 } },
                new Student { FirstName = "Mary", LastName = "Jones", Specialty = "Math", Grades = new int[] { 78, 88, 95 } },
                new Student { FirstName = "Bob", LastName = "Brown", Specialty = "CS", Grades = new int[] { 70, 75, 80 } }
            };

            string file = "students.bin";

            FileWorker.SaveStudents(students, file);
            Console.WriteLine("Saved");

            Student[] loaded = FileWorker.LoadStudents(file);
            Console.WriteLine("Loaded :: ");
            foreach (Student s in loaded)
                Console.WriteLine(s);
        }
    }
}