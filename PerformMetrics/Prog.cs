using CortexAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace PerformMetrics
{
    public class Prog

    {
        private static ArrayList data;
        private static ArrayList datamot;
        private static ArrayList datafac;
        private static DataStreamExample dse;

        public static ArrayList Perf { get => data; set => data = value; }
        public static ArrayList Mot { get => datamot; set => datamot = value; }
        public static ArrayList Fac { get => datafac; set => datafac = value; }
        public static DataStreamExample Dse { get => dse; set => dse = value; }

        
        public static void Main()
        {
           //
        }

        public static void ReturnData()
        {
            Perf = new ArrayList();

            Mot = new ArrayList();
            Fac = new ArrayList();

            dse = new DataStreamExample();
            
            dse.AddStreams("met");
            dse.AddStreams("mot");
            dse.AddStreams("fac");

            dse.OnSubscribed += SubscribedOK;
            dse.OnPerfDataReceived += OnPerfReceived;
            dse.OnMotionDataReceived += OnMotReceived;
            dse.OnFacDataReceived += OnFacReceived;



            dse.Start();

            //Console.WriteLine("Press Esc to exit");
            //while (Console.ReadKey().Key != ConsoleKey.Escape) { }

            Thread.Sleep(13000); //wait 13 sec for receive the data
            
            

            //dse.UnSubscribe();
            //Thread.Sleep(5000);

            //dse.CloseSession();
            //Thread.Sleep(5000);


        }

       



            private static void SubscribedOK(object sender, Dictionary<string, JArray> e)
        {
            foreach (string key in e.Keys)
            {
                if (key == "met")
                {
                    //ArrayList ar = e[key].ToObject<ArrayList>();
                    //foreach (String name in  ar){
                     //Console.WriteLine(name);
                   // }
                }
                if (key == "mot")
                {
                    
                }
                if (key == "fac")
                {
                    //ArrayList ar = e[key].ToObject<ArrayList>();
                    //foreach (String name in  ar){
                    //Console.WriteLine(name);
                     //}
                }
            }
        }

        private static void OnPerfReceived(object sender, ArrayList data)
        {
            /* 0: Engagement
             * 1: Excitement
             * 2: Long Term Exictement
             * 3: Stress/Frustration
             * 4: Relaxation
             * 5: Interest
             * 6: Focus
            */

            Perf = data;

        }
        private static void OnMotReceived(object sender, ArrayList data)
        {
            // 0: COUNTER_MEMS
            // 1: INTERPOLATED_MEMS
            // 2: GYROX
            // 3: GYROY
            // 4: GYROZ
            // 5: ACCX
            // 6: ACCY
            // 7: ACCZ
            // 8: MAGX
            // 9: MAGY
            // 10: MAGZ

            Mot = data;

        }
        private static void OnFacReceived(object sender, ArrayList data)
        {
            // 0: COUNTER_MEMS
            // 1: INTERPOLATED_MEMS
            // 2: GYROX
            // 3: GYROY
            // 4: GYROZ
            // 5: ACCX
            // 6: ACCY
            // 7: ACCZ
            // 8: MAGX
            // 9: MAGY
            // 10: MAGZ

            Fac = data;

        }



    }
}
