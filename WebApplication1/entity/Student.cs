using System;

namespace WebApplication1.entity
{
    public class Student
    {
        public Int32 id { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public String note { get; set; }
        public String number { get; set; }
        public String name { get; set; }
        public Int32 age { get; set; }

        public Student()
        {

        }

        public Student(int id, DateTime create_time, DateTime update_time, string note, string number, string name, int age)
        {
            this.id = id;
            this.create_time = create_time;
            this.update_time = update_time;
            this.note = note;
            this.number = number;
            this.name = name;
            this.age = age;
        }
    }
}