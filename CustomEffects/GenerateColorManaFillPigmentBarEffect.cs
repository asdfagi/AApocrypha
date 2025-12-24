using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class GenerateColorManaFillPigmentBarEffect : EffectSO
    {
        public ManaColorSO mana;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                if (manaSlot.ManaColor == null)
                {
                    exitAmount++;
                }
            }
            CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(mana, exitAmount, caster.IsUnitCharacter, caster.ID));
            return exitAmount > 0;
        }
    }
}
