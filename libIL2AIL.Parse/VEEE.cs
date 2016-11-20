/* It is at this stage that I have gone crazy
 * 
 * My love for you will always grow
 * Til the day we say I do
 * I will never stop saying I love you
 * My love for you will never die
*/

using System;
using System.Diagnostics;
using libIL2AIL.Tracking;

namespace libIL2AIL.Parse
{
    //Why?!
    public static class VEEE
    {
        //Fucking hell
        static ErrorList e;
        static RegisteredObjs o;

        public static void Init()
        {
            e = new ErrorList();
            o = new RegisteredObjs();
        }

        public static void Dispose()
        {
            if (e != null) ((IDisposable)e).Dispose();
            if(o != null) ((IDisposable)o).Dispose();

            e = null;
            o = null;
        }

        public static ErrorList getErrorList() { return e; }
        public static RegisteredObjs getObjectList() { return o; }

        //Check if this fucking thing is still alive
        public static void HeartBeat()
        {
            Debug.Print("Recieved heartbeat!");
            //Tracking.ErrorList newe = e; //Just to waste RAM ;)
        }
    }
}
