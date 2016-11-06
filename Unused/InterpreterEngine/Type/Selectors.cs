using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterEngine.Type
{
    /* We need to create a class for each command selector, and create
    *  A variable for each condition that goes with the selector.
    *  Can be found on the Minecraft Wiki here: http://minecraft.gamepedia.com/Commands */

    public class Selectors
    {
        public int x, y, z, radiusMax, radiusMin, dx, dy, dz;
        public string score; public int scoreMax, scoreMin;
        public int count, levelMax, levelMin, ry, rym, rx, rxm;
        public string team, tag;
        public int gameMode; public string name, type;
        public Selectors(int x, int y, int z, int r)
        {
            this.x = x; this.y = y; this.z = z; this.radiusMax = r;
        }
    }
}
