using System.Diagnostics;

namespace libIL2AIL.Parse
{
    //Why?!
    public static class VEEE
    {
        //Fucking hell
        static Tracking.ErrorList e;
        public static void Init()
        {
            e = new Tracking.ErrorList();
        }

        public static Tracking.ErrorList getList() { return e; }

        //Check if this fucking thing is still alive
        public static void HeartBeat()
        {
            Debug.Print("Recieved heartbeat!");
            //Tracking.ErrorList newe = e; //Just to waste RAM ;)
        }
    }
}
