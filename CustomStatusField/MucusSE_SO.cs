using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class MucusSE_SO : StatusEffect_SO
    {
        public override bool IsPositive => false;
        
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnBeingDamaged.ToString(), caller);
            //CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnDamaged.ToString(), caller);
        }
        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnBeingDamaged.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnDamaged.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            DamageReceivedValueChangeException ex = args as DamageReceivedValueChangeException;
            IUnit caster = sender as IUnit;
            if (ex.amount <= 0) { return; }
            if (ex.directDamage)
            {
                IStatusEffector effector = sender as IStatusEffector;
                int _mucusCount = 0;
                if (caster is CharacterCombat casterCH)
                {
                    Dictionary<int, CharacterCombat> alliesInField = CombatManager.Instance._stats.CharactersOnField;
                    foreach (int key in alliesInField.Keys)
                    {
                        if (key == caster.ID) { continue; }
                        if (alliesInField.TryGetValue(key, out CharacterCombat ally))
                        {
                            foreach (IStatusEffect checkStatus in ally.StatusEffects)
                            {
                                if (checkStatus.StatusID == "Mucus_ID")
                                {
                                    Debug.Log("Mucus | increasing _mucusCount " + _mucusCount + " by " + checkStatus.StatusContent);
                                    _mucusCount += checkStatus.StatusContent;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (caster is EnemyCombat casterEN)
                {
                    Dictionary<int, EnemyCombat> alliesInField = CombatManager.Instance._stats.EnemiesOnField;
                    foreach (int key in alliesInField.Keys)
                    {
                        if (key == caster.ID) { continue; }
                        if (alliesInField.TryGetValue(key, out EnemyCombat ally))
                        {
                            foreach (IStatusEffect checkStatus in ally.StatusEffects)
                            {
                                if (checkStatus.StatusID == _StatusID)
                                {
                                    Debug.Log("Mucus | increasing _mucusCount " + _mucusCount + " by " + checkStatus.StatusContent);
                                    _mucusCount += checkStatus.StatusContent;
                                    break;
                                }
                            }
                        }
                    }
                }
                Debug.Log("Mucus | final _mucusCount: " + _mucusCount);
                ex.AddModifier(new ScarsValueModifier(_mucusCount));
            }
            if (ex.damageTypeID == CombatType_GameIDs.Dmg_Fire.ToString())
            {
                RemoveStatusEffectEffect DryMucus = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                DryMucus._status = StatusField.GetCustomStatusEffect(_StatusID);

                CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(DryMucus, 1, Targeting.Slot_SelfSlot)], caster, 0));
            } else if (ex.directDamage) { ReduceDuration(holder, sender as IStatusEffector); }
        }

        /*public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            DamageReceivedValueChangeException ex = args as DamageReceivedValueChangeException;
        }*/
        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (!CanReduceDuration)
            {
                return;
            }

            int contentMain = holder.m_ContentMain;
            int newContentMain = contentMain - Mathf.CeilToInt(contentMain * 0.5f);
            holder.m_ContentMain = newContentMain;
            if (!TryRemoveStatusEffect(holder, effector) && contentMain != newContentMain)
            {
                effector.StatusEffectValuesChanged(_StatusID, newContentMain - contentMain, true);
            }
        }
    }
}