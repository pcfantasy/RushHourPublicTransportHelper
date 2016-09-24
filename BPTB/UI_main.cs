using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Collections;

namespace BPTB
{
    public class BPTBGUI : UIPanel
    {
        public static readonly string cacheName = "BPTBGUI";

        private static readonly float WIDTH = 650f;

        private static readonly float HEIGHT = 200f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 10f;

        private static readonly float SPACING22 = 22f;

        private ItemClass.Availability CurrentMode;

        public static BPTBGUI instance;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        private UIDragHandle m_DragHandler;

        private UIButton m_closeButton;

        private UILabel m_title;

        private UILabel m_use6_10_120xbudget_text;
        private UILabel m_use10_16_80xbudget_text;
        private UILabel m_use16_20_120xbudget_text;
        private UILabel m_use23_5_30xbudget_text;

        private UICheckBox m_use6_10_120xbudget;
        private UICheckBox m_use10_16_80xbudget;
        private UICheckBox m_use16_20_120xbudget;
        private UICheckBox m_use23_5_30xbudget;


        private RCISlider _RCIValueSlider_6_10;
        private RCISlider _RCIValueSlider_10_16;
        private RCISlider _RCIValueSlider_16_20;
        private RCISlider _RCIValueSlider_23_5;

        public override void Awake()
        {
            base.Awake();
            //this._RCIValueSlider_6_10 = base.AddUIComponent<RCISlider>();
        }

        public override void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.T))
            {
                this.ProcessVisibility();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "ctrl+T found");
            }
            base.Update();
        }

        public override void Start()
        {
            base.Start();
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            BPTBGUI.instance = this;
            base.size = new Vector2(BPTBGUI.WIDTH, BPTBGUI.HEIGHT);
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 200), (float)(Loader.parentGuiView.fixedHeight / 2 - 350));
            base.opacity = 1f;
            base.cachedName = BPTBGUI.cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = "Better public transport control panel";
            this.m_title.relativePosition = new Vector3(BPTBGUI.WIDTH / 2f - this.m_title.width / 2f - 25f, BPTBGUI.HEADER / 2f - this.m_title.height / 2f);
            this.m_title.textAlignment = UIHorizontalAlignment.Center;
            this.m_closeButton = base.AddUIComponent<UIButton>();
            this.m_closeButton.normalBgSprite = "buttonclose";
            this.m_closeButton.hoveredBgSprite = "buttonclosehover";
            this.m_closeButton.pressedBgSprite = "buttonclosepressed";
            this.m_closeButton.relativePosition = new Vector3(BPTBGUI.WIDTH - 35f, 5f, 10f);
            this.m_closeButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                base.Hide();
            };
            base.Hide(); //dont show in the beginning
            this.DoOnStartup();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();
        }

        private void ShowOnGui()
        {
            this.m_use6_10_120xbudget = base.AddUIComponent<UICheckBox>();
            this.m_use6_10_120xbudget.relativePosition = new Vector3(BPTBGUI.SPACING, 50f);
            this.m_use6_10_120xbudget_text = base.AddUIComponent<UILabel>();
            this.m_use6_10_120xbudget_text.relativePosition = new Vector3(this.m_use6_10_120xbudget.relativePosition.x + this.m_use6_10_120xbudget.width + BPTBGUI.SPACING * 3f, this.m_use6_10_120xbudget.relativePosition.y + 5f);
            this.m_use6_10_120xbudget_text.tooltip = "Force 6-10 transport budget 1.2X previous in-game setting";
            this.m_use6_10_120xbudget.height = 16f;
            this.m_use6_10_120xbudget.width = 16f;
            this.m_use6_10_120xbudget.label = this.m_use6_10_120xbudget_text;
            this.m_use6_10_120xbudget.text = "Force 6-10 transport budget 1.2X previous in-game setting";
            UISprite uISprite = this.m_use6_10_120xbudget.AddUIComponent<UISprite>();
            uISprite.height = 20f;
            uISprite.width = 20f;
            uISprite.relativePosition = new Vector3(0f, 0f);
            uISprite.spriteName = "check-unchecked";
            uISprite.isVisible = true;
            UISprite uISprite2 = this.m_use6_10_120xbudget.AddUIComponent<UISprite>();
            uISprite2.height = 20f;
            uISprite2.width = 20f;
            uISprite2.relativePosition = new Vector3(0f, 0f);
            uISprite2.spriteName = "check-checked";
            this.m_use6_10_120xbudget.checkedBoxObject = uISprite2;
            this.m_use6_10_120xbudget.isChecked = true;
            this.m_use6_10_120xbudget.isEnabled = true;
            this.m_use6_10_120xbudget.isVisible = true;
            this.m_use6_10_120xbudget.canFocus = true;
            this.m_use6_10_120xbudget.isInteractive = true;
            this.m_use6_10_120xbudget.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                this.m_use6_10_120xbudget_OnCheckChanged(component, eventParam);
            };

            this.m_use10_16_80xbudget = base.AddUIComponent<UICheckBox>();
            this.m_use10_16_80xbudget.relativePosition = new Vector3(BPTBGUI.SPACING, this.m_use6_10_120xbudget.relativePosition.y + BPTBGUI.SPACING22);
            this.m_use10_16_80xbudget_text = base.AddUIComponent<UILabel>();
            this.m_use10_16_80xbudget_text.relativePosition = new Vector3(this.m_use10_16_80xbudget.relativePosition.x + this.m_use10_16_80xbudget.width + BPTBGUI.SPACING * 3f, this.m_use10_16_80xbudget.relativePosition.y + 5f);
            this.m_use10_16_80xbudget_text.tooltip = "Force 10-16 transport budget 0.8X previous in-game setting";
            this.m_use10_16_80xbudget.height = 16f;
            this.m_use10_16_80xbudget.width = 16f;
            this.m_use10_16_80xbudget.label = this.m_use10_16_80xbudget_text;
            this.m_use10_16_80xbudget.text = "Force 10-16 transport budget 0.8X previous in-game setting";
            UISprite uISprite3 = this.m_use10_16_80xbudget.AddUIComponent<UISprite>();
            uISprite3.height = 20f;
            uISprite3.width = 20f;
            uISprite3.relativePosition = new Vector3(0f, 0f);
            uISprite3.spriteName = "check-unchecked";
            uISprite3.isVisible = true;
            UISprite uISprite4 = this.m_use10_16_80xbudget.AddUIComponent<UISprite>();
            uISprite4.height = 20f;
            uISprite4.width = 20f;
            uISprite4.relativePosition = new Vector3(0f, 0f);
            uISprite4.spriteName = "check-checked";
            this.m_use10_16_80xbudget.checkedBoxObject = uISprite4;
            this.m_use10_16_80xbudget.isChecked = true;
            this.m_use10_16_80xbudget.isEnabled = true;
            this.m_use10_16_80xbudget.isVisible = true;
            this.m_use10_16_80xbudget.canFocus = true;
            this.m_use10_16_80xbudget.isInteractive = true;
            this.m_use10_16_80xbudget.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                this.m_use10_16_80xbudget_OnCheckChanged(component, eventParam);
            };


            this.m_use16_20_120xbudget = base.AddUIComponent<UICheckBox>();
            this.m_use16_20_120xbudget.relativePosition = new Vector3(BPTBGUI.SPACING, this.m_use10_16_80xbudget.relativePosition.y + BPTBGUI.SPACING22);
            this.m_use16_20_120xbudget_text = base.AddUIComponent<UILabel>();
            this.m_use16_20_120xbudget_text.relativePosition = new Vector3(this.m_use16_20_120xbudget.relativePosition.x + this.m_use16_20_120xbudget.width + BPTBGUI.SPACING * 3f, this.m_use16_20_120xbudget.relativePosition.y + 5f);
            this.m_use16_20_120xbudget_text.tooltip = "Force 16-20 transport budget 1.2X previous in-game setting";
            this.m_use16_20_120xbudget.height = 16f;
            this.m_use16_20_120xbudget.width = 16f;
            this.m_use16_20_120xbudget.label = this.m_use16_20_120xbudget_text;
            this.m_use16_20_120xbudget.text = "Force 16-20 transport budget 1.2X previous in-game setting";
            UISprite uISprite5 = this.m_use16_20_120xbudget.AddUIComponent<UISprite>();
            uISprite5.height = 20f;
            uISprite5.width = 20f;
            uISprite5.relativePosition = new Vector3(0f, 0f);
            uISprite5.spriteName = "check-unchecked";
            uISprite5.isVisible = true;
            UISprite uISprite6 = this.m_use16_20_120xbudget.AddUIComponent<UISprite>();
            uISprite6.height = 20f;
            uISprite6.width = 20f;
            uISprite6.relativePosition = new Vector3(0f, 0f);
            uISprite6.spriteName = "check-checked";
            this.m_use16_20_120xbudget.checkedBoxObject = uISprite6;
            this.m_use16_20_120xbudget.isChecked = true;
            this.m_use16_20_120xbudget.isEnabled = true;
            this.m_use16_20_120xbudget.isVisible = true;
            this.m_use16_20_120xbudget.canFocus = true;
            this.m_use16_20_120xbudget.isInteractive = true;
            this.m_use16_20_120xbudget.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                this.m_use16_20_120xbudget_OnCheckChanged(component, eventParam);
            };

            this.m_use23_5_30xbudget = base.AddUIComponent<UICheckBox>();
            this.m_use23_5_30xbudget.relativePosition = new Vector3(BPTBGUI.SPACING, this.m_use16_20_120xbudget.relativePosition.y + BPTBGUI.SPACING22);
            this.m_use23_5_30xbudget_text = base.AddUIComponent<UILabel>();
            this.m_use23_5_30xbudget_text.relativePosition = new Vector3(this.m_use23_5_30xbudget.relativePosition.x + this.m_use23_5_30xbudget.width + BPTBGUI.SPACING * 3f, this.m_use23_5_30xbudget.relativePosition.y + 5f);
            this.m_use23_5_30xbudget_text.tooltip = "Force 23-5 transport budget 0.3X previous in-game setting";
            this.m_use23_5_30xbudget.height = 16f;
            this.m_use23_5_30xbudget.width = 16f;
            this.m_use23_5_30xbudget.label = this.m_use23_5_30xbudget_text;
            this.m_use23_5_30xbudget.text = "Force 23-5 transport budget 0.3X previous in-game setting";
            UISprite uISprite7 = this.m_use23_5_30xbudget.AddUIComponent<UISprite>();
            uISprite7.height = 20f;
            uISprite7.width = 20f;
            uISprite7.relativePosition = new Vector3(0f, 0f);
            uISprite7.spriteName = "check-unchecked";
            uISprite7.isVisible = true;
            UISprite uISprite8 = this.m_use23_5_30xbudget.AddUIComponent<UISprite>();
            uISprite8.height = 20f;
            uISprite8.width = 20f;
            uISprite8.relativePosition = new Vector3(0f, 0f);
            uISprite8.spriteName = "check-checked";
            this.m_use23_5_30xbudget.checkedBoxObject = uISprite8;
            this.m_use23_5_30xbudget.isChecked = true;
            this.m_use23_5_30xbudget.isEnabled = true;
            this.m_use23_5_30xbudget.isVisible = true;
            this.m_use23_5_30xbudget.canFocus = true;
            this.m_use23_5_30xbudget.isInteractive = true;
            this.m_use23_5_30xbudget.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                this.m_use23_5_30xbudget_OnCheckChanged(component, eventParam);
            };

            //side 1 
            //this._RCIValueSlider.relativePosition = new Vector3(BPTBGUI.SPACING, this.m_use23_5_30xbudget1.relativePosition.y + BPTBGUI.SPACING22);
            //this._RCIValueSlider.eventValueChanged += new PropertyChangedEventHandler<float>(this._RCIValueSlider_eventValueChanged1);
        }

        //private void _RCIValueSlider_eventValueChanged1(UIComponent component, float value)
        //{
        //    DebugLog.Log(value.ToString());
        //}

        private void m_use6_10_120xbudget_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                BPTB.is_6_10_120_budget = true;
                //DebugLog.Log("Tubiao liangle");
            }
            else
            {
                BPTB.is_6_10_120_budget = false;
                //DebugLog.Log("Tubiao miele");
            }
        }

        private void m_use23_5_30xbudget_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                BPTB.is_23_5_30_budget = true;
                //DebugLog.Log("23_5Tubiao liangle");
            }
            else
            {
                BPTB.is_23_5_30_budget = false;
                //DebugLog.Log("23_5Tubiao miele");
            }
        }

        private void m_use16_20_120xbudget_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                BPTB.is_16_20_120_budget = true;
                //DebugLog.Log("15-20Tubiao liangle");
            }
            else
            {
                BPTB.is_16_20_120_budget = false;
                //DebugLog.Log("15-20Tubiao miele");
            }
        }

        private void m_use10_16_80xbudget_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                BPTB.is_10_16_80_budget = true;
                //DebugLog.Log("6-10Tubiao liangle");
            }
            else
            {
                BPTB.is_10_16_80_budget = false;
                //DebugLog.Log("6-10Tubiao miele");
            }
        }






        private void ProcessVisibility()
        {
            if (!base.isVisible)
            {
                base.Show();
            }
            else
            {
                base.Hide();
            }
        }
    }
}
