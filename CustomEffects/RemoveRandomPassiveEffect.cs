using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class RemoveRandomPassiveEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<TargetSlotInfo> targetsList = new List<TargetSlotInfo>();
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit == true || target.Unit != null)
                {
                    targetsList.Add(target);
                }
            }
            targets = targetsList.ToArray();

            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    List <BasePassiveAbilitySO> passives = new List<BasePassiveAbilitySO>();
                    if (targetSlotInfo.Unit is CharacterCombat targetCH)
                    {
                        passives = targetCH.PassiveAbilities;
                        
                    } else if (targetSlotInfo.Unit is EnemyCombat targetEN)
                    {
                        passives = targetEN.PassiveAbilities;

                    }
                    if (passives.Count > 0)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, passives.Count);
                        BasePassiveAbilitySO passiveToDel = passives[randomIndex];
                        if (targetSlotInfo.HasUnit && targetSlotInfo.Unit.TryRemovePassiveAbility(passiveToDel.m_PassiveID))
                        {
                            exitAmount++;
                        }
                    }
                        
                    
                }
                
            }

            return exitAmount > 0;
        }
    }
}
