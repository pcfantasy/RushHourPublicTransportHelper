using System.IO;
using ColossalFramework.Plugins;

namespace BPTB
{
    public static class DebugLog
    {
        public static void Init()
        {
            //_logFile = File.CreateText("D:\\log.txt");
            //_logFile.AutoFlush = true;
        }

        public static void Close()
        {
            //_logFile.Close();
        }

        public static void LogToFileOnly(string msg)
        {
            //_logFile.WriteLine(msg);
            using (FileStream fileStream = new FileStream("pcfantasy_BPTB.txt", FileMode.Append))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(msg);
                streamWriter.Flush();
            }
        }

        public static void Log(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, msg);
            //_logFile.WriteLine(msg);
        }

        public static void LogWarning(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, msg);
            //_logFile.WriteLine("Warning: " + msg);
        }

        public static void LogError(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, msg);
            //_logFile.WriteLine("Error: " + msg);
        }
    }
}