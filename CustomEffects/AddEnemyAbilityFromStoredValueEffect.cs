using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class AddEnemyAbilityFromStoredValueEffect : EffectSO
    {
        public string _storedValueID;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_storedValueID == null)
                return false;

            caster.TryGetStoredData(_storedValueID, out var abilityValue);
            CombatAbility abilityToAdd = abilityValue.m_ObjectData as CombatAbility;

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is EnemyCombat enemy)
                    {
                        Debug.Log($"Ability Adder | target enemy found ({enemy.Name})");
                        if (enemy.Abilities.Count < entryVariable)
                        {
                            Debug.Log($"Ability Adder | adding ability {abilityToAdd.ability.name} to {enemy.Name}...");
                            enemy.Abilities.Add(abilityToAdd);
                            Debug.Log($"Ability Adder | success!");
                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(enemy.ID));
                            exitAmount++;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
