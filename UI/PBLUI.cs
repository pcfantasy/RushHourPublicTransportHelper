using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;

namespace RushHourPublicTransportHelper.UI
{
    public class PBLUI : UIPanel
    {
        public static readonly string cacheName = "PBLUI";
        private static readonly float SPACING = 15f;
        public PublicTransportWorldInfoPanel baseBuildingWindow;
        public static bool refeshOnce = false;
        public static bool _initialized = false;
        private UILabel try1;

        public override void Update()
        {
            RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            canFocus = true;
            isInteractive = true;
            isVisible = true;
            opacity = 1f;
            cachedName = cacheName;
            RefreshDisplayData();
        }

        private void DoOnStartup()
        {
            ShowOnGui();
        }

        private void ShowOnGui()
        {
            try1 = AddUIComponent<UILabel>();
            try1.text = "FAMILY_MONEY";
            try1.relativePosition = new Vector3(0, 50f);
            try1.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce)
            {
            }
        }
    }
}
