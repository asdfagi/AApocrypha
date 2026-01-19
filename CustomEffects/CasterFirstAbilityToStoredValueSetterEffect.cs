using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.CustomEffects
{
    public class CasterFirstAbilityToStoredValueSetterEffect : EffectSO
    {
        public bool _ignoreIfContains;

        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CombatAbility firstAbility = null;
            if (caster.IsUnitCharacter)
            {
                CharacterCombat ch = caster as CharacterCombat;
                if (ch.CombatAbilities.Count >= 1) { firstAbility = ch.CombatAbilities[0]; }
            }
            else if (!caster.IsUnitCharacter)
            {
                EnemyCombat en = caster as EnemyCombat;
                if (en.Abilities.Count >= 1) { firstAbility = en.Abilities[0]; }
            }
            if (firstAbility == null) { return false; }
            Debug.Log("storing entry " + firstAbility.ability.name + " to storage");
            bool flag = caster.TryGetStoredData(m_unitStoredDataID, out var abilityValue);
            if (!_ignoreIfContains || !flag)
            {
                if (caster.IsUnitCharacter)
                {
                    CharacterCombat ch = caster as CharacterCombat;
                    foreach (var holder in ch.StoredValues)
                    {
                        holder.Value.m_ObjectData = firstAbility;
                    }
                }
                else if (!caster.IsUnitCharacter)
                {
                    EnemyCombat en = caster as EnemyCombat;
                    foreach (var holder in en.StoredValues)
                    {
                        holder.Value.m_ObjectData = firstAbility;
                    }
                }
                Debug.Log("stored, hopefully");
                return true;
            }

            return false;
        }
    }
}
