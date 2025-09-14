using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ConsumeCasterColorManaEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(caster.ID, caster.IsUnitCharacter);
            string manaConsumedSound = stats.audioController.manaConsumedSound;
            exitAmount = stats.MainManaBar.ConsumeAmountMana(caster.HealthColor, entryVariable, jumpInfo, manaConsumedSound);
            return exitAmount > 0;
        }
    }
}
