using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    // copied from the Ruinful Revelry github
    public class PlayOneShotEffect : EffectSO
    {
        public string soundPath = string.Empty;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = this.PreviousExitValue;
            CombatManager.Instance.AddUIAction(new PlayStatusEffectSoundAndWaitUIAction(soundPath, 0));
            return true;
        }
    }
}
