using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.CanvasScaler;

namespace A_Apocrypha.CustomStatusField
{
    public class FrostbiteSE : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;
            IUnit caster = caller as IUnit;
            caster.TryGetStoredData("FrostbiteIntensityStoredValue", out var frostbit);
            frostbit.m_MainData = 0;

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnRoundFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            IUnit caster = caller as IUnit;
            caster.TryGetStoredData("FrostbiteIntensityStoredValue", out var frostbit);
            if (frostbit.m_MainData >= 1)
            {
                bool ruptureCheck = false;
                if (caster.ContainsStatusEffect(StatusField.Ruptured.StatusID))
                {
                    RemoveStatusEffectEffect DeRupture = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                    DeRupture._status = StatusField.Ruptured;

                    ruptureCheck = true;
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(DeRupture, 1, Targeting.Slot_SelfSlot)], caster, 0));
                    //caster.TryRemoveStatusEffect(StatusField.Ruptured.StatusID);
                }
                caster.Damage(frostbit.m_MainData * (ruptureCheck ? 3 : 2), null, DeathType_GameIDs.DirectDeath.ToString(), 0, true, true, false, "AA_Frost_Damage");
            }
            frostbit.m_MainData = -1;

            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnRoundFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is IUnit u)
            {
                u.TryGetStoredData("FrostbiteIntensityStoredValue", out var frostbit);
                frostbit.m_MainData += 1;
            }
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is IUnit u)
            {
                ReduceDuration(holder, sender as IStatusEffector);
            }
        }
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
