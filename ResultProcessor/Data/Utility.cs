using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.Data
{
    public static class Utility
    {
        public static  char GetGrade(int score)
        {
            char grade;
            if (score >= 70 || score == 100)
            {
                grade = 'A';
            }
            else if (score >= 60 || score ==69)
            {
                grade = 'B';
            }
            else if (score >= 50 || score ==59)
            {
                grade = 'C';
            }
            else if (score >= 40 || score ==45)
            {
                grade = 'D';
            }
            else
            {
                grade = 'F';
            }

            return grade;
        }

        public static int GradePoint(char grade, int hours)
        {


            int points = 0;

            grade = char.ToUpper(grade);
            switch (grade)
            {
                case 'A':
                    points = hours * 5;
                    break;
                case 'B':
                    points = hours * 4;
                    break;
                case 'C':
                    points = hours * 3;
                    break;
                case 'D':
                    points = hours * 2;
                    break;
                case 'E':
                    points = hours * 1;
                    break;
                case 'F':
                    points = hours * 0;
                    break;
                default:
                    break;
            }//switch
            return points;
        }
    

    }
}
