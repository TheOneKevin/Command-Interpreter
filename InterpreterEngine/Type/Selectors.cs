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

    /// <summary>
    /// Random Player selector. Minecraft selector @r, selects a
    /// random player that fulfills all the conditions/variables
    /// set below.
    /// </summary>
    public class RandPlayer
    {
        public int x, y, z, radiusMax, radiusMin, dx, dy, dz;
        public string score; public int scoreMax, scoreMin;
        public int count, levelMax, levelMin, ry, rym, rx, rxm;
        public string team, tag;
        public int gameMode; public string name;
        public string identifier() { return "@r"; }
        public RandPlayer(int x, int y, int z, int r)
        {
            this.x = x; this.y = y; this.z = z; this.radiusMax = r;
        }
    }
    /// <summary>
    /// All player selector. Selects all players on the map/server
    /// that fulfills all the conditions/variables below.
    /// Minecraft selector @a
    /// </summary>
    public class AllPlayer
    {
        public int x, y, z, radiusMax, radiusMin, dx, dy, dz;
        public string score; public int scoreMax, scoreMin;
        public int count, levelMax, levelMin, ry, rym, rx, rxm;
        public string team, tag;
        public int gameMode; public string name;
        public string identifier() { return "@a"; }
        public AllPlayer(int x, int y, int z, int r)
        {
            this.x = x; this.y = y; this.z = z; this.radiusMax = r;
        }
    }
    /// <summary>
    /// Nearest player selector. Selects the nearest player on the map/server
    /// that fulfills all the conditions/variables below.
    /// Minecraft selector @p
    /// </summary>
    public class NearestPlayer
    {
        public int x, y, z, radiusMax, radiusMin, dx, dy, dz;
        public string score; public int scoreMax, scoreMin;
        public int count, levelMax, levelMin, ry, rym, rx, rxm;
        public string team, tag;
        public int gameMode; public string name;
        public string identifier() { return "@p"; }
        public NearestPlayer(int x, int y, int z, int r)
        {
            this.x = x; this.y = y; this.z = z; this.radiusMax = r;
        }
    }
    /// <summary>
    /// All player selector. Selects all entities on the map/server
    /// that fulfills all the conditions/variables below.
    /// Minecraft selector @e
    /// </summary>
    public class AllEntities
    {
        public int x, y, z, radiusMax, radiusMin, dx, dy, dz;
        public string score; public int scoreMax, scoreMin;
        public int count, levelMax, levelMin, ry, rym, rx, rxm;
        public string team, tag;
        public string name, type;
        public string identifier() { return "@e"; }
        public AllEntities(int x, int y, int z, int r)
        {
            this.x = x; this.y = y; this.z = z; this.radiusMax = r;
        }
    }
}
