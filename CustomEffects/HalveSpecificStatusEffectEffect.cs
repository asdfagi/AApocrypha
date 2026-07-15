using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class HalveSpecificStatusEffectEffect : EffectSO
    {
        public string _statusID;

        //public bool m_AffectFieldEffects = true;

        //public bool m_AffectStatusEffects = true;

        //public List<string> _blacklist = new List<string>();

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            /*if (entryVariable <= 0)
            {
                return false;
            }
            entryVariable = Math.Abs(entryVariable) * -1; //clever math trick seen in Salt Enemies (Quest/Unlocks/UnlocksSix.cs - Scalpel Item)*/

            RemoveStatusEffectEffect wipeStatus = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            wipeStatus._status = StatusField.GetCustomStatusEffect(_statusID);

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is CharacterCombat targetCH)
                    {
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (status.StatusID == _statusID)
                            {
                                if (status.StatusContent > 1)
                                {
                                    Debug.Log("Halve | halving...");
                                    int modifier = Mathf.Max(1, Mathf.CeilToInt(status.StatusContent / 2));
                                    modifier = Math.Abs(modifier) * -1;
                                    if (status.TryAddContent(modifier, 0))
                                    {
                                        targetCH.StatusEffectValuesChanged(status.StatusID, modifier, true);
                                        exitAmount += Math.Abs(modifier);
                                    }
                                    Debug.Log("Halve | halved!");
                                }
                                else
                                {
                                    Debug.Log("Halve | removing...");
                                    exitAmount += status.StatusContent;
                                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(wipeStatus, 1, Targeting.Slot_SelfSlot)], targetCH, 0));
                                    Debug.Log("Halve | removed!");
                                }
                            }
                        }
                    } 
                    else if (target.Unit is EnemyCombat targetEN)
                    {
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (status.StatusID == _statusID)
                            {
                                if (status.StatusContent >= 1)
                                {
                                    int modifier = Mathf.Max(1, Mathf.CeilToInt(status.StatusContent / 2));
                                    modifier = Math.Abs(modifier) * -1;
                                    if (status.TryAddContent(modifier, 0))
                                    {
                                        targetEN.StatusEffectValuesChanged(status.StatusID, modifier, true);
                                        exitAmount += Math.Abs(modifier);
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
