using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace Content
{

    public static class Utils
    {
        public static readonly double Pi = 3.141592653589793238462643383279;
        
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                return value;
            }

            else if (min == max)
            {
                return min;
            }

            else if (value <= min)
            {
                return min;
            }
            else if (value >= max)
            {
                return max;
            }
            else
            {
                return value;
            }


        }

        public static Vector2 VecLerp(Vector2 inital, Vector2 final, float lerpVal)
        {
            return inital + (final - inital) * (float)Clamp(lerpVal, 0, 1);

        }

        public static float Lerp(float init, float final, float lerpVal)
        {
            return init + (final - init) * lerpVal;

        }

        public static int Closer(float val, float bound1, float bound2)
        {
            float a = Mathf.Abs(bound1-val);
            float b = Mathf.Abs(bound2-val);

            if(a == b)
            {
                return 0;
            }
            else if (a < b)
            {
                return -1;
            }
            else if (a > b)
            {
                return 1;
            }
            else
            {
                return 0;
            }


        }

        public static double UnwrapAngle(double angle)
        {
            if (angle >= 0)
            {
                var tempAngle = angle % (Mathf.Pi*2);
                return tempAngle == 360 ? 0 : tempAngle;
            }
            else
                return (Mathf.Pi * 2) - (-1 * angle) % (Mathf.Pi * 2);
        }

        public static Color ColorLerp(Color c1, Color c2, float lerpVal)
        {
            float lerpVal2 = (float)Clamp(lerpVal, 0, 1);

            return new Color(Lerp(c1.r, c2.r, lerpVal2), Lerp(c1.g, c2.g, lerpVal2), Lerp(c1.b, c2.b, lerpVal2), Lerp(c1.a, c2.a, lerpVal2));
        }

        public static float TurnTowards(float cur, float target, float speed)
        {
            float wantDir;
            float currDir;
            float directiondiff;
            float maxTurn;

            // want - this is your target direction \\
            wantDir = (float)UnwrapAngle(target);

            // max turn - this is the max number of degrees to turn \\
            maxTurn = speed;

            // current - this is your current direction \\
            currDir = (float)UnwrapAngle(cur);

            if (wantDir >= (currDir + Mathf.Pi))
            {
                currDir += (Mathf.Pi*2);
            }
            else
            {
                if (wantDir < (currDir - Mathf.Pi))
                {
                    wantDir += (Mathf.Pi * 2);
                }
            }

            directiondiff = wantDir - currDir;

            //if(directiondiff>)

            if (directiondiff < -maxTurn)
            {
                directiondiff = -maxTurn;
            }

            if (directiondiff > maxTurn)
            {
                directiondiff = maxTurn;
            }

            // return the resultant directional change \\
            return cur + directiondiff;
        }

    }
}