using ColossalFramework.UI;
using ICities;
using RushHourPublicTransportHelper.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RushHourPublicTransportHelper.UI
{
    public class OptionUI : MonoBehaviour
    {
        public static int morningBudgetWeekDay;
        public static int eveningBudgetWeekDay;
        public static int deepNightBudgetWeekDay;
        public static int otherBudgetWeekDay;
        public static int morningBudgetWeekEnd;
        public static int eveningBudgetWeekEnd;
        public static int deepNightBudgetWeekEnd;
        public static int otherBudgetWeekEnd;
        public static void makeSettings(UIHelperBase helper)
        {
            // tabbing code is borrowed from RushHour mod
            // https://github.com/PropaneDragon/RushHour/blob/release/RushHour/Options/OptionHandler.cs
            LoadSetting();
            UIHelper actualHelper = helper as UIHelper;
            UIComponent container = actualHelper.self as UIComponent;

            UITabstrip tabStrip = container.AddUIComponent<UITabstrip>();
            tabStrip.relativePosition = new Vector3(0, 0);
            tabStrip.size = new Vector2(container.width - 20, 40);

            UITabContainer tabContainer = container.AddUIComponent<UITabContainer>();
            tabContainer.relativePosition = new Vector3(0, 40);
            tabContainer.size = new Vector2(container.width - 20, container.height - tabStrip.height - 20);
            tabStrip.tabPages = tabContainer;

            int tabIndex = 0;
            // Lane_ShortCut

            AddOptionTab(tabStrip, Localization.Get("WeekDayPlan"));
            tabStrip.selectedIndex = tabIndex;

            UIPanel currentPanel = tabStrip.tabContainer.components[tabIndex] as UIPanel;
            currentPanel.autoLayout = true;
            currentPanel.autoLayoutDirection = LayoutDirection.Vertical;
            currentPanel.autoLayoutPadding.top = 5;
            currentPanel.autoLayoutPadding.left = 10;
            currentPanel.autoLayoutPadding.right = 10;

            UIHelper panelHelper = new UIHelper(currentPanel);

            var generalGroup = panelHelper.AddGroup(Localization.Get("MorningRushHour")) as UIHelper;
            generalGroup.AddDropdown(Localization.Get("MorningBudget"), new string[] { "30%", "60%", "90%","120%", "150%", "180%", "210%" }, morningBudgetWeekDay, (index) => GetMorningBudgetWeekDay(index));
            var generalGroup1 = panelHelper.AddGroup(Localization.Get("EveningRushHour")) as UIHelper;
            generalGroup1.AddDropdown(Localization.Get("EveningBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, eveningBudgetWeekDay, (index) => GetEveningBudgetWeekDay(index));
            var generalGroup2 = panelHelper.AddGroup(Localization.Get("DeepNight")) as UIHelper;
            generalGroup2.AddDropdown(Localization.Get("DeepNightBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, deepNightBudgetWeekDay, (index) => GetDeepNightBudgetWeekDay(index));
            var generalGroup3 = panelHelper.AddGroup(Localization.Get("OtherTime")) as UIHelper;
            generalGroup3.AddDropdown(Localization.Get("OtherTimeBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, otherBudgetWeekDay, (index) => GetOtherBudgetWeekDay(index));

            ++tabIndex;

            AddOptionTab(tabStrip, Localization.Get("WeekEndPlan"));
            tabStrip.selectedIndex = tabIndex;

            currentPanel = tabStrip.tabContainer.components[tabIndex] as UIPanel;
            currentPanel.autoLayout = true;
            currentPanel.autoLayoutDirection = LayoutDirection.Vertical;
            currentPanel.autoLayoutPadding.top = 5;
            currentPanel.autoLayoutPadding.left = 10;
            currentPanel.autoLayoutPadding.right = 10;

            panelHelper = new UIHelper(currentPanel);

            generalGroup = panelHelper.AddGroup(Localization.Get("MorningRushHour")) as UIHelper;
            generalGroup.AddDropdown(Localization.Get("MorningBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, morningBudgetWeekEnd, (index) => GetMorningBudgetWeekEnd(index));
            generalGroup1 = panelHelper.AddGroup(Localization.Get("EveningRushHour")) as UIHelper;
            generalGroup1.AddDropdown(Localization.Get("EveningBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, eveningBudgetWeekEnd, (index) => GetEveningBudgetWeekEnd(index));
            generalGroup2 = panelHelper.AddGroup(Localization.Get("DeepNight")) as UIHelper;
            generalGroup2.AddDropdown(Localization.Get("DeepNightBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, deepNightBudgetWeekEnd, (index) => GetDeepNightBudgetWeekEnd(index));
            generalGroup3 = panelHelper.AddGroup(Localization.Get("OtherTime")) as UIHelper;
            generalGroup3.AddDropdown(Localization.Get("OtherTimeBudget"), new string[] { "30%", "60%", "90%", "120%", "150%", "180%", "210%" }, otherBudgetWeekEnd, (index) => GetOtherBudgetWeekEnd(index));
            SaveSetting();
        }
        private static UIButton AddOptionTab(UITabstrip tabStrip, string caption)
        {
            UIButton tabButton = tabStrip.AddTab(caption);

            tabButton.normalBgSprite = "SubBarButtonBase";
            tabButton.disabledBgSprite = "SubBarButtonBaseDisabled";
            tabButton.focusedBgSprite = "SubBarButtonBaseFocused";
            tabButton.hoveredBgSprite = "SubBarButtonBaseHovered";
            tabButton.pressedBgSprite = "SubBarButtonBasePressed";

            tabButton.textPadding = new RectOffset(10, 10, 10, 10);
            tabButton.autoSize = true;
            tabButton.tooltip = caption;

            return tabButton;
        }

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RushHourPublicTransportHelper_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(morningBudgetWeekDay);
            streamWriter.WriteLine(eveningBudgetWeekDay);
            streamWriter.WriteLine(deepNightBudgetWeekDay);
            streamWriter.WriteLine(otherBudgetWeekDay);
            streamWriter.WriteLine(morningBudgetWeekEnd);
            streamWriter.WriteLine(eveningBudgetWeekEnd);
            streamWriter.WriteLine(deepNightBudgetWeekEnd);
            streamWriter.WriteLine(otherBudgetWeekEnd);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RushHourPublicTransportHelper_setting.txt"))
            {
                FileStream fs = new FileStream("RushHourPublicTransportHelper_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out morningBudgetWeekDay)) morningBudgetWeekDay = 6;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetWeekDay)) eveningBudgetWeekDay = 5;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetWeekDay)) deepNightBudgetWeekDay = 0;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetWeekDay)) otherBudgetWeekDay = 2;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out morningBudgetWeekEnd)) morningBudgetWeekEnd = 3;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetWeekEnd)) eveningBudgetWeekEnd = 3;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetWeekEnd)) deepNightBudgetWeekEnd = 0;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetWeekEnd)) otherBudgetWeekEnd = 3;
                sr.Close();
                fs.Close();
            }
        }


        public static void GetMorningBudgetWeekDay(int index)
        {
            morningBudgetWeekDay = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetEveningBudgetWeekDay(int index)
        {
            eveningBudgetWeekDay = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetDeepNightBudgetWeekDay(int index)
        {
            deepNightBudgetWeekDay = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetOtherBudgetWeekDay(int index)
        {
            otherBudgetWeekDay = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetMorningBudgetWeekEnd(int index)
        {
            morningBudgetWeekEnd = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetEveningBudgetWeekEnd(int index)
        {
            eveningBudgetWeekEnd = index;
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetDeepNightBudgetWeekEnd(int index)
        {
            deepNightBudgetWeekEnd = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public static void GetOtherBudgetWeekEnd(int index)
        {
            otherBudgetWeekEnd = index;
            SaveSetting();
            //MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            //method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
    }
}
