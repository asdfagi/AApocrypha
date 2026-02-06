using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ActivateTRUTHMusicEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            OverworldCombatSharedDataSO current = CombatManager.Instance._informationHolder.CombatData;
            //CombatManager.Instance._soundManager.StopCombatMusicTrack();
            CombatManager.Instance._soundManager.PlayCombatMusicTrack("event:/AAMusic/EXCELSIOR/Necrobiome");

            return true;
        }
    }
}
