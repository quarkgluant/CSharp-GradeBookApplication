using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("not enough ranked students");
            var threshold = (int)Math.Ceiling(Students.Count() * 0.2);
            var grades = Students.OrderByDescending(e => e.AverageGrade)
                                 .Select(e => e.AverageGrade)
                                 .ToList();

            switch (averageGrade)
            {
                case var d when d >= grades[threshold - 1]:
                    return 'A';
                case var d when d >= grades[2 * threshold - 1]:
                    return 'B';
                case var d when d >= grades[3 * threshold - 1]:
                    return 'C';
                case var d when d >= grades[4 * threshold - 1]:
                    return 'D';
                default:
                    return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();                
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }

}
