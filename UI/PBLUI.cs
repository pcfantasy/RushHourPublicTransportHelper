using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using ICities;
using ColossalFramework.Plugins;

namespace RushHourPublicTransportHelper.UI
{
    public class PBLUI : UIPanel
    {
        public static readonly string cacheName = "PBLUI";
        private static readonly float SPACING = 15f;
        public PublicTransportWorldInfoPanel baseBuildingWindow;
        public static bool refeshOnce = false;
        public static bool _initialized = false;
        public static int aTraffic = 0;
        public static int bTraffic = 0;
        public static int cTraffic = 0;
        public static int dTraffic = 0;
        //private UILabel try1;

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
            var uIDropDown = CreateDropDown(this);
            uIDropDown.items = new string[] { "LOW_TRAFFIC", "MEDIUM_TRAFFIC", "HEAVY_TRAFFIC" };
            uIDropDown.selectedIndex = bTraffic;
            uIDropDown.size = new Vector2(130f, 25f);
            uIDropDown.relativePosition = new Vector3(0f, 0f);
            uIDropDown.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                bTraffic = sel;
            };
            var uIDropDown1 = CreateDropDown(this);
            uIDropDown1.items = new string[] { "LOW_TRAFFIC", "MEDIUM_TRAFFIC", "HEAVY_TRAFFIC" };
            uIDropDown1.selectedIndex = bTraffic;
            uIDropDown1.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                bTraffic = sel;
            };
            uIDropDown1.size = new Vector2(130f, 25f);
            uIDropDown1.relativePosition = new Vector3(0f, 30f);
            //helper.AddDropdown("SIDEB_TRAFFIC", new string[] { "LOW_TRAFFIC", "MEDIUM_TRAFFIC", "HEAVY_TRAFFIC" }, bTraffic, (index1) => GetEffortIdex1(index1));
            //helper.AddDropdown("SIDEB_TRAFFIC", new string[] { "LOW_TRAFFIC", "MEDIUM_TRAFFIC", "HEAVY_TRAFFIC" }, bTraffic, (index1) => GetEffortIdex1(index1));
        }

        public UITextureAtlas GetAtlas(string name)
        {
            UITextureAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UITextureAtlas)) as UITextureAtlas[];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name == name)
                {
                    return array[i];
                }
            }
            return null;
        }

        public UIDropDown CreateDropDown(UIComponent parent)
        {
            UIDropDown dropDown = parent.AddUIComponent<UIDropDown>();
            dropDown.atlas = GetAtlas("Ingame");
            dropDown.size = new Vector2(90f, 30f);
            dropDown.listBackground = "GenericPanelLight";
            dropDown.itemHeight = 30;
            dropDown.itemHover = "ListItemHover";
            dropDown.itemHighlight = "ListItemHighlight";
            dropDown.normalBgSprite = "ButtonMenu";
            dropDown.disabledBgSprite = "ButtonMenuDisabled";
            dropDown.hoveredBgSprite = "ButtonMenuHovered";
            dropDown.focusedBgSprite = "ButtonMenu";
            dropDown.listWidth = 90;
            dropDown.listHeight = 500;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropDown.popupColor = new Color32(45, 52, 61, byte.MaxValue);
            dropDown.popupTextColor = new Color32(170, 170, 170, byte.MaxValue);
            dropDown.zOrder = 1;
            dropDown.textScale = 0.8f;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropDown.selectedIndex = 0;
            dropDown.textFieldPadding = new RectOffset(8, 0, 8, 0);
            dropDown.itemPadding = new RectOffset(14, 0, 8, 0);
            UIButton button = dropDown.AddUIComponent<UIButton>();
            dropDown.triggerButton = button;
            button.atlas = GetAtlas("Ingame");
            button.text = "";
            button.size = dropDown.size;
            button.relativePosition = new Vector3(0f, 0f);
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Left;
            button.normalFgSprite = "IconDownArrow";
            button.hoveredFgSprite = "IconDownArrowHovered";
            button.pressedFgSprite = "IconDownArrowPressed";
            button.focusedFgSprite = "IconDownArrowFocused";
            button.disabledFgSprite = "IconDownArrowDisabled";
            button.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            button.horizontalAlignment = UIHorizontalAlignment.Right;
            button.verticalAlignment = UIVerticalAlignment.Middle;
            button.zOrder = 0;
            button.textScale = 0.8f;
            dropDown.eventSizeChanged += delegate (UIComponent c, Vector2 t)
            {
                button.size = t;
                dropDown.listWidth = (int)t.x;
            };
            return dropDown;
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
