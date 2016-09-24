using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Reflection;
using System.Runtime.InteropServices;
using ColossalFramework.Globalization;

namespace BPTB
{
    public class BPTB : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool is_6_10_120_budget = true;
        public static bool is_16_20_120_budget = true;
        public static bool is_10_16_80_budget = true;
        public static bool is_23_5_30_budget = true;
        public static RedirectCallsState state;
        public static RedirectCallsState state1;

        public string Name
        {
            get { return "Better Public Transport budget"; }
        }

        public string Description
        {
            get { return "Allow you to set more budget in morning and evening to send more transport cars, and set less budget in deep night"; }
        }

        public void OnEnabled()
        {
            BPTB.IsEnabled = true;
            var srcMethod = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(bool) }, null);
            var destMethod = typeof(pcfantasy_EM).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(EconomyManager), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(bool) }, null);
            state = RedirectionHelper.RedirectCalls(srcMethod, destMethod);
            var srcMethod1 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var destMethod1 = typeof(pcfantasy_EM).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(EconomyManager), typeof(ItemClass) }, null);
            state1 = RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

        }

        public void OnDisabled()
        {
            BPTB.IsEnabled = false;
            var srcMethod = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(bool) }, null);
            var srcMethod1 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            RedirectionHelper.RevertRedirect(srcMethod, state);
            RedirectionHelper.RevertRedirect(srcMethod1, state1);

        }

        //        public void OnSettingsUI(UIHelperBase helper)
        //        {
        //            UIHelperBase group = helper.AddGroup("Better_public_transport_budget setting");
        //
        //        }
    }

}