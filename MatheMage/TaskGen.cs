using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatheMage_REDUX
{
    class TaskGen
    {
        public static string[] Generate(int diff)
        {
            Random rnd = new Random();
            if (diff == 1)
            {
                string[] output = new string[6];
                int f1 = rnd.Next(1, 99);
                int f2 = rnd.Next(1, 99);
                int sym = rnd.Next(1, 4);
                int ans = 0;
                string ssym;
                switch (sym)
                {
                    case 1:
                        ssym = "+";
                        ans = f1 + f2;
                        break;
                    case 2:
                        ssym = "-";
                        ans = f1 - f2;
                        break;
                    case 3:
                        ssym = "*";
                        ans = f1 * f2;
                        break;
                    default:
                        ssym = ".";
                        break;
                }
                output[0] = f1.ToString() + ' ' + ssym.ToString() + f2.ToString() + " = x" ;
                int ansnum = rnd.Next(1, 4);
                output[1] = rnd.Next(0, 200).ToString();
                output[2] = rnd.Next(0, 200).ToString();
                output[3] = rnd.Next(0, 200).ToString();
                output[4] = rnd.Next(0, 200).ToString();
                output[5] = ansnum.ToString();
                output[ansnum] = ans.ToString();
                return output;
            }else
                return null;
        }
    }
}
