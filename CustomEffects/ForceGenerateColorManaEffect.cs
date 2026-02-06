using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.CustomEffects
{
    public class ForceGenerateColorManaEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public ManaColorSO mana;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }

            exitAmount = entryVariable;
            CombatManager.Instance.ProcessImmediateAction(new ForceAddManaToManaBarAction(mana, entryVariable, caster.IsUnitCharacter, caster.ID));
            return true;
        }
    }
}
