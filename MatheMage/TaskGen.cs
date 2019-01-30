using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatheMage
{
    class TaskGen
    {
        //Генерация заданий
        public static string[] Generate(int diff)
        {
            bool check = false;
            Random rnd = new Random();
            if (diff == 1)
            {
                string[] output = new string[6];
                
                int sym = rnd.Next(1, 4);
                int ans = -1;
                int f1 = 0;
                int f2 = 0;
                string ssym = ".";
                while (ans > 200 || ans < 0)
                {
                    f1 = rnd.Next(1, 99);
                    f2 = rnd.Next(1, 99);
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
                }
                output[0] = f1.ToString() + ' ' + ssym.ToString() + " " + f2.ToString() + " = x" ;
                int anssnum = rnd.Next(1, 4);

                for (int i = 1; i <= 4; i++)
                {
                    check = false;
                    while (!check)
                    {
                        output[i] = rnd.Next(Convert.ToInt32(ans) - 10, Convert.ToInt32(ans + 10)).ToString();
                        if (Convert.ToInt32(output[i]) != ans)
                        {
                            check = true;
                        }
                    }
                }

                output[5] = anssnum.ToString();
                output[anssnum] = ans.ToString();
                return output;
            }else
                return null;
        }
    }
}
