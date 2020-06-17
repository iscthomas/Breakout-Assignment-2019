//The eSize class is an enumeration that is used to store the values for the brick and paddle sizes so that they can easily be changed if desired.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public enum eSize //Contains the enumerations for the brick and paddle sizes so that they can be adjusted easily.
    {
            SIZEW = 136,    //width of brick
            SIZEH = 40,     //height of brick
            PSIZEW = 125,   //width of Paddle
            PSIZEH = 20,     //height of Paddle   
    }
}
