using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Tracking
{
    public class RegisteredObjs
    {
        Dictionary<string, Objects.ObjectStruct> dictionary = new Dictionary<string, Objects.ObjectStruct>();

        public bool AddObject(string name, Symbols.Symbol symbolRef)
        {
            if (!dictionary.ContainsKey(Hash.GetHashString(name)))
            {
                //dictionary.Add(name, new Objects.ObjectStruct(name, symbolRef));
                return true;
            }

            return false;
        }

        public Objects.ObjectStruct GetObject(string name)
        {
            if (dictionary.ContainsKey(Hash.GetHashString(name)))
            {
                Objects.ObjectStruct ret;
                dictionary.TryGetValue(Hash.GetHashString(name), out ret);
                return ret;
            }

            return null;
        }
    }
}
