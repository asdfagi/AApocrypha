using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class AddRandomPassiveEffect : EffectSO
    {
        public BasePassiveAbilitySO[] _passivesToAdd;

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
                    List<BasePassiveAbilitySO> filteredPassives = new List<BasePassiveAbilitySO>();
                    foreach (BasePassiveAbilitySO passive in _passivesToAdd)
                    {
                        string mID = passive.m_PassiveID;
                        if (targetSlotInfo.Unit is CharacterCombat unitCH)
                        {
                            bool addToFiltered = true;
                            foreach (BasePassiveAbilitySO targetPassive in unitCH.PassiveAbilities)
                            {
                                string target_mID = targetPassive.m_PassiveID;
                                if (mID == target_mID)
                                {
                                    addToFiltered = false;
                                    break;
                                }
                            }
                            if (addToFiltered)
                            {
                                filteredPassives.Add(passive);
                            }
                        }
                        else if (targetSlotInfo.Unit is EnemyCombat unitEN)
                        {
                            bool addToFiltered = true;
                            foreach (BasePassiveAbilitySO targetPassive in unitEN.PassiveAbilities)
                            {
                                string target_mID = targetPassive.m_PassiveID;
                                if (mID == target_mID)
                                {
                                    addToFiltered = false;
                                    break;
                                }
                            }
                            if (addToFiltered)
                            {
                                filteredPassives.Add(passive);
                            }
                        }
                    }

                    bool limitReached = false;
                    if (targetSlotInfo.Unit is CharacterCombat targetCH)
                    {
                        if (targetCH.PassiveAbilities.Count >= entryVariable)
                        {
                            limitReached = true;
                        }
                    }
                    else if (targetSlotInfo.Unit is EnemyCombat targetEN)
                    {
                        if (targetEN.PassiveAbilities.Count >= entryVariable)
                        {
                            limitReached = true;
                        }
                    }

                    if (filteredPassives.Count > 0 && !limitReached)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, filteredPassives.Count);
                        Debug.Log($"AddRandomPassive | filteredPassives.Count = {filteredPassives.Count} | randomIndex = {randomIndex}");
                        BasePassiveAbilitySO passiveToAdd = filteredPassives[randomIndex];
                        if (targetSlotInfo.HasUnit && targetSlotInfo.Unit.AddPassiveAbility(passiveToAdd))
                        {
                            exitAmount++;
                        }
                        Debug.Log($"AddRandomPassive | adding passive {passiveToAdd._passiveName} with mID {passiveToAdd.m_PassiveID}");
                    }
                }
                
            }

            return exitAmount > 0;
        }
    }
}
