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
using System.IO;

namespace RushHourPublicTransportHelper
{
    public class RushHourPublicTransportHelper : IUserMod
    {
        public static bool IsEnabled = false;

        public string Name
        {
            get { return "Rush Hour PublicTransport Helper"; }
        }

        public string Description
        {
            get { return "Allow you to adjust PublicTransport vehicle num for rush hour/deep night/weekend"; }
        }

        public void OnEnabled()
        {
            IsEnabled = true;
            FileStream fs = File.Create("RushHourPublicTransportHelper.txt");
            fs.Close();
        }

        public void OnDisabled()
        {
            IsEnabled = false;
        }
    }
}