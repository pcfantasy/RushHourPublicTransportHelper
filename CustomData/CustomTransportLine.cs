using ColossalFramework;
using RushHourPublicTransportHelper.UI;
using RushHourPublicTransportHelper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RushHourPublicTransportHelper.CustomData
{
    public static class CustomTransportLine
    {
        public static int CalculateTargetVehicleCount(ref TransportLine transportLine)
        {
            TransportInfo info = transportLine.Info;
            float num = transportLine.m_totalLength;
            if (num == 0f && transportLine.m_stops != 0)
            {
                NetManager instance = Singleton<NetManager>.instance;
                ushort stops = transportLine.m_stops;
                ushort num2 = stops;
                int num3 = 0;
                while (num2 != 0)
                {
                    ushort num4 = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        ushort segment = instance.m_nodes.m_buffer[num2].GetSegment(i);
                        if (segment != 0 && instance.m_segments.m_buffer[segment].m_startNode == num2)
                        {
                            num += instance.m_segments.m_buffer[segment].m_averageLength;
                            num4 = instance.m_segments.m_buffer[segment].m_endNode;
                            break;
                        }
                    }
                    num2 = num4;
                    if (num2 == stops)
                    {
                        break;
                    }
                    if (++num3 >= 32768)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
            int budget = Singleton<EconomyManager>.instance.GetBudget(info.m_class);
            //Non-stock code begin
            //DebugLog.LogToFileOnly("Change budget begin," + Singleton<SimulationManager>.instance.m_currentGameTime.Hour.ToString());
            if (IsWeekend(Singleton<SimulationManager>.instance.m_currentGameTime))
            {
                if (MainDataStore.WeekEndPlan[FindLineID(transportLine)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((OptionUI.morningBudgetWeekDay + 1) * 0.3f * budget);
                    } 
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((OptionUI.eveningBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((OptionUI.deepNightBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                }
                else if (MainDataStore.WeekEndPlan[FindLineID(transportLine)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((OptionUI.morningBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((OptionUI.eveningBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((OptionUI.deepNightBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                }
            }
            else
            {
                //PlanA
                if (MainDataStore.WeekDayPlan[FindLineID(transportLine)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((OptionUI.morningBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((OptionUI.eveningBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((OptionUI.deepNightBudgetWeekDay + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekDay + 1) * 0.3f * budget);
                    }
                }
                else if (MainDataStore.WeekDayPlan[FindLineID(transportLine)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((OptionUI.morningBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((OptionUI.eveningBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((OptionUI.deepNightBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((OptionUI.otherBudgetWeekEnd + 1) * 0.3f * budget);
                    }
                }
            }
            //Non-stock code end
            budget = (budget * transportLine.m_budget + 50) / 100;
            return Mathf.CeilToInt((float)budget * num / (info.m_defaultVehicleDistance * 100f));
        }

        public static bool IsWeekend(this DateTime dateTime)
        {
            if (dateTime.DayOfWeek != DayOfWeek.Saturday)
            {
                return dateTime.DayOfWeek == DayOfWeek.Sunday;
            }
            return true;
        }

        public static ushort FindLineID(TransportLine transportLine)
        {
            for (int i = 0; i < 256; i++)
            {
                if (Singleton<TransportManager>.instance.m_lines.m_buffer[i].m_flags.IsFlagSet(TransportLine.Flags.Created))
                {
                    if (transportLine.m_lineNumber != 0)
                    {
                        if (transportLine.Info.m_transportType == Singleton<TransportManager>.instance.m_lines.m_buffer[i].Info.m_transportType)
                        {
                            if (transportLine.m_lineNumber == Singleton<TransportManager>.instance.m_lines.m_buffer[i].m_lineNumber)
                            {
                                return (ushort)i;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
