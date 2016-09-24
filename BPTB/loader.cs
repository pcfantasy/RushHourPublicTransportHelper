using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace BPTB
{
    public class Loader : LoadingExtensionBase
    {
        public static UIView parentGuiView;
        public static BPTBGUI guiPanel;

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
            if (BPTB.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame || mode == LoadMode.LoadMap || mode == LoadMode.NewMap)
                {
                    Loader.SetupGui();
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "setup_gui now");
                }
            }
        }

        public override void OnLevelUnloading()
        {
            if (BPTB.IsEnabled & Loader.isGuiRunning)
            {
                Loader.RemoveGui();
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public static void SetupGui()
        {
            Loader.parentGuiView = null;
            Loader.parentGuiView = UIView.GetAView();
            if (Loader.guiPanel == null)
            {
                Loader.guiPanel = (BPTBGUI)Loader.parentGuiView.AddUIComponent(typeof(BPTBGUI));
            }
            Loader.isGuiRunning = true;
        }

        public static void RemoveGui()
        {
            Loader.isGuiRunning = false;
            if (Loader.parentGuiView != null)
            {
                Loader.parentGuiView = null;
            }
        }
    }
}
