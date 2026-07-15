using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ReduceSetStatusEffectEffect : EffectSO
    {
        public StatusEffect_SO _Status;

        //public bool m_AffectFieldEffects = true;

        //public bool m_AffectStatusEffects = true;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (entryVariable <= 0)
            {
                return false;
            }
            entryVariable = Math.Abs(entryVariable) * -1; //clever math trick seen in Salt Enemies (Quest/Unlocks/UnlocksSix.cs - Scalpel Item)

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is CharacterCombat targetCH)
                    {
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (status.StatusID == _Status.StatusID)
                            {
                                RemoveStatusEffectEffect wipeStatus = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                                wipeStatus._status = StatusField.GetCustomStatusEffect(status.StatusID);

                                if (status.StatusContent > Math.Abs(entryVariable))
                                {
                                    if (status.TryAddContent(entryVariable, 0))
                                    {
                                        targetCH.StatusEffectValuesChanged(status.StatusID, entryVariable, true);
                                        exitAmount += Math.Abs(entryVariable);
                                    }
                                }
                                else
                                {
                                    exitAmount += status.StatusContent;
                                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(wipeStatus, 1, Targeting.Slot_SelfSlot)], targetCH, 0));
                                }
                            }
                        }
                    } 
                    else if (target.Unit is EnemyCombat targetEN)
                    {
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (status.StatusID == _Status.StatusID)
                            {
                                RemoveStatusEffectEffect wipeStatus = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                                wipeStatus._status = StatusField.GetCustomStatusEffect(status.StatusID);

                                if (status.StatusContent > Math.Abs(entryVariable))
                                {
                                    if (status.TryAddContent(entryVariable, 0))
                                    {
                                        targetEN.StatusEffectValuesChanged(status.StatusID, entryVariable, true);
                                        exitAmount += Math.Abs(entryVariable);
                                    }
                                }
                                else
                                {
                                    exitAmount += status.StatusContent;
                                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(wipeStatus, 1, Targeting.Slot_SelfSlot)], targetEN, 0));
                                }
                            }
                        }
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
