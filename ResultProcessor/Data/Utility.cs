using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Data
{
    public static class Utility
    {
        public static  char GetGrade(float score)
        {
            char grade;
            if (score > 70 || score <= 100)
            {
                grade = 'A';
            }
            else if (score >= 60 || score <=69)
            {
                grade = 'B';
            }
            else if (score >= 50 || score <=59)
            {
                grade = 'C';
            }
            else if (score >= 40 || score <=45)
            {
                grade = 'D';
            }
            else
            {
                grade = 'F';
            }

            return grade;
        }
    }
}
