using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatheMage
{
    class MapManager
    {
        int Currentx = 0;
        int Currenty = 0;

        bool Check = true;

        public List<int> Builder(int diff)
        {
            int CurrentTrace = 0;

            int CurrentCount = 0;

            List<int> output = new List<int>();

            while(diff > 0)
            {
                CurrentTrace = Randomize.Rnd(1, 4);

                BlockCheck(output, CurrentTrace);

                switch(CurrentTrace){
                    case 1:
                        if(Currenty - 64 > 0 && Check == true)
                        {
                            Currenty -= 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Currenty -= 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Check = false;
                        }

                        break;

                    case 2:
                        if (Currentx + 64 < 320 && Check == true)
                        {
                            Currentx += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Currentx += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Check = false;
                        }

                        break;

                    case 3:
                        if (Currenty + 64 < 150 && Check == true)
                        {
                            Currenty += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Currenty += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Check = false;
                        }

                        break;

                    case 4:
                        if (Currentx - 64 > 0 && Check == true)
                        {
                            Currentx += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Currentx += 32;
                            output[CurrentCount] = Currentx;
                            CurrentCount++;
                            output[CurrentCount] = Currenty;
                            CurrentCount++;

                            Check = false;
                        }

                        break;

                    default:

                        break;

                }

                diff--;
            }

            return null;
        }

        private void BlockCheck(List<int> input, int Direction)
        {
            switch (Direction)
            {
                case 1:

                    for(int i = 0; i < input.Count; i++)
                    {
                        if(input[i] == Currenty - 64)
                        {
                            Check = true;
                        }
                    }

                    break;
                case 2:

                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i] == Currentx + 64)
                        {
                            Check = true;
                        }
                    }

                    break;
                case 3:

                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i] == Currenty + 64)
                        {
                            Check = true;
                        }
                    }

                    break;
                case 4:

                    for (int i = 0; i < input.Count; i++)
                    {
                        if (input[i] == Currentx - 64)
                        {
                            Check = true;
                        }
                    }

                    break;
            }

        }
    }
}
