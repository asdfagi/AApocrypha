using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ReloadTimelineEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            try
            {
                if (stats.timeline.RoundTurnUIInfo.Length > 0)
                {
                    CombatManager.Instance.AddUIAction(new PopulateTimelineUIAction(stats.timeline.RoundTurnUIInfo));
                    CombatManager.Instance.AddUIAction(new UpdateTimelinePointerUIAction(stats.timeline.CurrentTurn));
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Reload Timeline | uh-oh! error: {e}");
                return false;
            }
            return true;
        }
    }
}
