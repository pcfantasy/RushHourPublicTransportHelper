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
    public class RCISlider : UISlider
    {
        private UISprite _ThumbObj;

        public override void Start()
        {
            base.Start();
            this._ThumbObj = base.AddUIComponent<UISprite>();
            this._ThumbObj.spriteName = "SliderBudget";
            base.size = new Vector2(200f, 10f);
            base.backgroundSprite = "LevelBarBackground";
            this.canFocus = true;
            base.maxValue = 100f;
            base.minValue = 0f;
            base.orientation = UIOrientation.Horizontal;
            base.scrollWheelAmount = 1f;
            base.stepSize = 1f;
            base.thumbObject = this._ThumbObj;
            base.thumbOffset = new Vector2(0f, 2f);
        }
    }
}
