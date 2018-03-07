using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class Rounding
    {
        public static void Run()
        {
            double cash = 0;
            int roundDecimal = 2;
            int roundOption = 3;

            Console.Write("Cash: ");
            cash = double.Parse(Console.ReadLine());

            Console.Write("Round Number Decimal: ");
            roundDecimal = int.Parse(Console.ReadLine());

            double x = Math.Pow(10, roundDecimal);

            double afterRound = RoundingOption4(cash, x);

            Console.WriteLine("Before Round: " + cash);
            Console.WriteLine("After Round: " + afterRound);
        }

        ///<summary>
        ///Decimal value 0, 1, 2 -> 0 => 1.00, 1.01, 1.02 -> 1.00
        ///Decimal value 3, 4, 5, 6, 7 -> 5 => 1.03, 1.04, 1.05, 1.06, 1.07 -> 1.05
        ///Decimal value 8, 9 -> 10 => 1.08, 1.09 -> 1.10
        ///</summary>
        public static double RoundingOption1(double number, double x = 100)
        {
            int xX = (int)(number * x);
            int p1 = xX % 10;
            xX -= p1;
            switch (p1)
            {
                case 0:
                case 1:
                case 2: p1 = 0; break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7: p1 = 5; break;
                case 8:
                case 9: xX += 10; p1 = 0; break;
            }
            xX += p1;

            return (double)xX / x;
        }

        ///<summary>
        ///Decimal value 0, 1, 2, 3, 4 -> 0 => 1.00, 1.01, 1.02, 1.03, 1.04 -> 1.00
        ///Decimal value 5 -> 5 => 1.05 -> 1.05
        ///Decimal value 6, 7, 8, 9 -> 10 => 1.06, 1.07, 1.08, 1.09 -> 1.10
        ///</summary>
        public static double RoundingOption2(double number, double x = 100)
        {
            int xX = (int)(number * x);
            int p1 = xX % 10;
            xX -= p1;
            switch (p1)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4: p1 = 0; break;
                case 5: break;
                case 6:
                case 7:
                case 8:
                case 9: xX += 10; p1 = 0; break;
            }
            xX += p1;

            return (double)xX / x;
        }

        ///<summary>
        ///Decimal value 0, 1, 2, 3, 4 -> 0 => 1.00, 1.01, 1.02, 1.03, 1.04 -> 1.00
        ///Decimal value 5, 6, 7, 8, 9 -> 10 => 1.06, 1.07, 1.08, 1.09 -> 1.10
        ///</summary>
        public static double RoundingOption3(double number, double x = 100)
        {
            int xX = (int)(number * x);
            int p1 = xX % 10;
            xX -= p1;
            switch (p1)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4: p1 = 0; break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: xX += 10; p1 = 0; break;
            }
            xX += p1;

            return (double)xX / x;
        }

        ///<summary>
        ///Decimal value 0, 1, 2, 3, 4 -> 0 => 1.00, 1.01, 1.02, 1.03, 1.04 -> 1.00
        ///Decimal value 5, 6, 7, 8, 9 -> 5 => 1.06, 1.07, 1.08, 1.09 -> 1.05
        ///</summary>
        public static double RoundingOption4(double number, double x = 100)
        {
            int xX = (int)(number * x);
            int p1 = xX % 10;
            xX -= p1;
            switch (p1)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4: p1 = 0; break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: p1 = 5; break;
            }
            xX += p1;

            return (double)xX / x;
        }
    }
}
