using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ColossalFramework;


namespace BPTB
{
    public static class pcfantasy_EM
    {
        public static void Init()
        {
            DebugLog.Log("Init pcfantasy_EM");
            try
            {
                var inst = Singleton<EconomyManager>.instance;
                var m_serviceBudgetNight = typeof(EconomyManager).GetField("m_serviceBudgetNight", BindingFlags.NonPublic | BindingFlags.Instance);
                var m_serviceBudgetDay = typeof(EconomyManager).GetField("m_serviceBudgetDay", BindingFlags.NonPublic | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogError("No instance of EconomyManager found!");
                    return;
                }
                _serviceBudgetNight = m_serviceBudgetNight.GetValue(inst) as int[];
                _serviceBudgetDay = m_serviceBudgetDay.GetValue(inst) as int[];
                if (_serviceBudgetDay == null || _serviceBudgetDay == null)
                {
                    DebugLog.LogError("Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogError("Exception: " + ex.Message);
            }
        }
        private static int[] _serviceBudgetNight;
        private static int[] _serviceBudgetDay;
        private static bool _init = false;

        public static int GetBudget(EconomyManager manage , ItemClass itemClass)
        {
            if (_init == false)
            {
                pcfantasy_EM.Init();
                _init = true;
            }
            var inst = Singleton<SimulationManager>.instance;
            float current_time = inst.m_currentDayTimeHour;
            int num;
            int publicServiceIndex = ItemClass.GetPublicServiceIndex(itemClass.m_service);
            int publicSubServiceIndex = ItemClass.GetPublicSubServiceIndex(itemClass.m_subService);
            if (publicServiceIndex == -1)
            {
                num = -1;
            }
            int result;
            if (publicSubServiceIndex != -1)
            {
                result = 12 + publicSubServiceIndex;
            }
            else
            {
                result = publicServiceIndex;
            }

            num = result;


            if (BPTB.is_16_20_120_budget && (current_time > 15.0) && (current_time < 20.0) && ((num == 12) || (num == 13) || (num == 18)))
            {
                return (int)(pcfantasy_EM.GetBudget(new EconomyManager(), itemClass.m_service, itemClass.m_subService, Singleton<SimulationManager>.instance.m_isNightTime)*1.2);
            }
            else if (BPTB.is_23_5_30_budget && ((current_time > 23.0) || (current_time < 5.0)) && ((num == 12) || (num == 13) || (num == 18)))
            {
                return (int)(pcfantasy_EM.GetBudget(new EconomyManager(), itemClass.m_service, itemClass.m_subService, Singleton<SimulationManager>.instance.m_isNightTime)*0.3);
            }
            else if (BPTB.is_6_10_120_budget && (current_time > 6.0) && (current_time < 10.0) && ((num == 12) || (num == 13) || (num == 18)))
            {
                return (int)(pcfantasy_EM.GetBudget(new EconomyManager(), itemClass.m_service, itemClass.m_subService, Singleton<SimulationManager>.instance.m_isNightTime)*1.2);
            }
            else if (BPTB.is_10_16_80_budget && (current_time > 10.0) && (current_time < 15.0) && ((num == 12) || (num == 13) || (num == 18)))
            {
                return (int)(pcfantasy_EM.GetBudget(new EconomyManager(), itemClass.m_service, itemClass.m_subService, Singleton<SimulationManager>.instance.m_isNightTime)*0.8);
            }
            else
            {
                return pcfantasy_EM.GetBudget(new EconomyManager(), itemClass.m_service, itemClass.m_subService, Singleton<SimulationManager>.instance.m_isNightTime);
            }
        }


        public static int GetBudget(EconomyManager manage, ItemClass.Service service, ItemClass.SubService subService, bool night)
        {
            if (_init == false)
            {
                pcfantasy_EM.Init();
                _init = true;
            }
            //orginal private static int PublicClassIndex(ItemClass.Service service, ItemClass.SubService subService)

            int num;
            int publicServiceIndex = ItemClass.GetPublicServiceIndex(service);
            int publicSubServiceIndex = ItemClass.GetPublicSubServiceIndex(subService);
            if (publicServiceIndex == -1)
            {
                num = -1;
            }
            int result;
            if (publicSubServiceIndex != -1)
            {
                result = 12 + publicSubServiceIndex;
            }
            else
            {
                result = publicServiceIndex;
            }

            num = result;

            //orignal public static int GetBudget(EconomyManager manage, ItemClass.Service service, ItemClass.SubService subService, bool night)
            if (num == -1)
            {
                return 0;
            }
            if (night)
            {
                    return _serviceBudgetNight[num];                   
            }
                    return _serviceBudgetDay[num];
        }
    }
}
