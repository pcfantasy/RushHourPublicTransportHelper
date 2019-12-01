using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourPublicTransportHelper.Util
{
    public class MainDataStore
    {
        public static byte[] WeekDayPlan = new byte[256];
        public static byte[] WeekEndPlan = new byte[256];
        public static byte[] saveData = new byte[512];
        public static ushort lastLineID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < MainDataStore.WeekDayPlan.Length; i++)
            {
                WeekDayPlan[i] = 0;
                WeekEndPlan[i] = 0;
            }
        }

        public static void save()
        {
            int i = 0;
            SaveAndRestore.save_bytes(ref i, WeekDayPlan, ref saveData);
            SaveAndRestore.save_bytes(ref i, WeekEndPlan, ref saveData);
        }

        public static void load()
        {
            int i = 0;
            WeekDayPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekDayPlan.Length);
            WeekEndPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekEndPlan.Length);
        }
    }
}
