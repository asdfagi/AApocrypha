using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class Hexed : StatusEffect_SO
    {
        public override bool IsPositive => false;
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            Debug.Log("Hexed | applied!");
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), caller);
            ApplyOrRemoveCostOfColorEffect purplizeCost = ScriptableObject.CreateInstance<ApplyOrRemoveCostOfColorEffect>();
            purplizeCost._mana = Pigments.Purple;
            EffectInfo[] effects = [Effects.GenerateEffect(purplizeCost, 1, Targeting.Slot_SelfSlot)];
            CombatManager.Instance.AddSubAction(new EffectAction(effects, (caller as IUnit)));
        }
        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            Debug.Log("Hexed | cleared!");
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), caller);
            ApplyOrRemoveCostOfColorEffect cleanseCost = ScriptableObject.CreateInstance<ApplyOrRemoveCostOfColorEffect>();
            cleanseCost._mana = Pigments.Purple;
            cleanseCost._removeCost = true;
            EffectInfo[] effects = [Effects.GenerateEffect(cleanseCost, 1, Targeting.Slot_SelfSlot)];
            CombatManager.Instance.AddSubAction(new EffectAction(effects, (caller as IUnit)));
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (!CanReduceDuration)
            {
                return;
            }

            int contentMain = holder.m_ContentMain;
            holder.m_ContentMain -= 1;
            if (!TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain)
            {
                effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, true);
            }
        }
    }
}
