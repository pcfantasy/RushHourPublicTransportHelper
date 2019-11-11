using System.IO;
using ColossalFramework.Plugins;

namespace RushHourPublicTransportHelper
{
    public static class DebugLog
    {
        public static void LogToFileOnly(string msg)
        {
            using (FileStream fileStream = new FileStream("RushHourPublicTransportHelper.txt", FileMode.Append))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(msg);
                streamWriter.Flush();
            }
        }

        public static void LogWarning(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, msg);
        }
    }
}