using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ExtraLootDupeCasterItemEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = entryVariable;
            if (caster.IsUnitCharacter && caster.HeldItem != null)
            {
                //Debug.Log("Item Duplicator | registered name: " + caster.HeldItem.name + " - if this is wrong, use " + caster.HeldItem._itemName);
                stats.AddExtraLootAddition(caster.HeldItem.name);
                exitAmount++;
            }
            else
            {
                //Debug.Log($"Item Duplicator | caster is alleged to not have any usable items? isCasterCharacter = {(caster.IsUnitCharacter ? "true" : "false")}, caster.HasUsableItem = {(caster.HasUsableItem ? "true" : "false")}");
            }
            return exitAmount > 0;
        }
    }
}
