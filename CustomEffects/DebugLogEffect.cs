using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class DebugLogEffect : EffectSO
    {
        public string _logText = "";
        public string _logType = "";
        public bool _checkPrevious = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = entryVariable;
            if (_checkPrevious && PreviousExitValue != 0) { exitAmount = PreviousExitValue; }
            switch (_logType)
            {
                case "Warning":
                    Debug.LogWarning(caster.Name + " (entry: " + exitAmount.ToString() + ") | " + _logText);
                    return true;
                case "Error":
                    Debug.LogError(caster.Name + " (entry: " + exitAmount.ToString() + ") | " + _logText);
                    return true;
                default:
                    Debug.Log(caster.Name + " (entry: " + exitAmount.ToString() + ") | " + _logText);
                    return true;
            }
        }
    }
}
