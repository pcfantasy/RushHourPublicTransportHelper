using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using RushHourPublicTransportHelper.UI;
using RushHourPublicTransportHelper.Util;
using UnityEngine;

namespace RushHourPublicTransportHelper
{
    public class Loader : LoadingExtensionBase
    {
        public class Detour
        {
            public MethodInfo OriginalMethod;
            public MethodInfo CustomMethod;
            public RedirectCallsState Redirect;

            public Detour(MethodInfo originalMethod, MethodInfo customMethod)
            {
                this.OriginalMethod = originalMethod;
                this.CustomMethod = customMethod;
                this.Redirect = RedirectionHelper.RedirectCalls(originalMethod, customMethod);
            }
        }

        public static List<Detour> Detours { get; set; }
        public static bool DetourInited = false;
        public static UIView parentGuiView;
        public static UIPanel PBLInfo;
        public static PBLUI PBLUI;
        public static GameObject PBLWindowGameObject;

        internal static LoadMode CurrentLoadMode;
        public static bool isGuiRunning = false;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            Loader.CurrentLoadMode = mode;
            if (RushHourPublicTransportHelper.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    Loader.SetupGui();
                }
            }
        }

        public override void OnLevelUnloading()
        {
            if (RushHourPublicTransportHelper.IsEnabled & Loader.isGuiRunning)
            {
                Loader.RemoveGui();
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public static void SetupPBLUIGui()
        {
            PBLWindowGameObject = new GameObject("PBLWindowGameObject");
            PBLUI = (PBLUI)PBLWindowGameObject.AddComponent(typeof(PBLUI));
            PBLInfo = UIView.Find<UIPanel>("(Library) PublicTransportWorldInfoPanel");
            if (PBLInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) PublicTransportWorldInfoPanel\nAvailable panels are:\n");
            }
            PBLUI.transform.parent = PBLInfo.transform;
            PBLUI.size = new Vector3(150, 100);
            PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
            UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
            PBLUI.position = new Vector3(UILabel.relativePosition.x, PBLInfo.size.y - (UILabel.relativePosition.y + 130f));
            PBLInfo.eventVisibilityChanged += PBLInfo_eventVisibilityChanged;
        }
        public static void PBLInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            PBLUI.isEnabled = value;
            if (value)
            {
                //initialize PBL ui again
                PBLUI.transform.parent = PBLInfo.transform;
                PBLUI.size = new Vector3(150, 100);
                PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
                UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
                //DebugLog.LogToFileOnly(UILabel.relativePosition.x.ToString() + "    " +  UILabel.relativePosition.y.ToString());
                PBLUI.position = new Vector3(UILabel.relativePosition.x, PBLInfo.size.y - (UILabel.relativePosition.y + 130f));
                PBLUI.refeshOnce = true;
                PBLUI.Show();
            }
            else
            {
                PBLUI.Hide();
            }
        }

        public static void SetupGui()
        {
            parentGuiView = null;
            parentGuiView = UIView.GetAView();
            SetupPBLUIGui();
            isGuiRunning = true;
        }

        public static void RemoveGui()
        {
            Loader.isGuiRunning = false;
            if (Loader.parentGuiView != null)
            {
                Loader.parentGuiView = null;
            }
            PBLUI._initialized = false;
        }
    }
}

