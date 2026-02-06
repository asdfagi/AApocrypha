using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class IntDamageModAndEffectWearable : BaseWearableSO
    {
        // dmg mod vars
        public bool _useDealt;

        public bool _useHealing;

        public bool _useSimpleInt;

        public bool _roundNegatives = true;

        [Min(1f)]
        public int _integerToModify = 1;

        // second effect vars
        public TriggerCalls[] _secondPerformTriggersOn;

        public EffectorConditionSO[] _secondPerformConditions;

        public bool _secondDoesPerformItemPopUp = true;

        public bool _GetsConsumedOnSecondaryUse;

        public bool _secondImmediateEffect;

        public EffectInfo[] _secondEffects;

        public override bool IsItemImmediate => true;

        public override bool DoesItemTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (_useSimpleInt)
            {
                if (args is IntValueChangeException ex && !ex.Equals(null))
                {
                    ex.AddModifier(new AdditionValueModifier(dmgDealt: false, _integerToModify, _roundNegatives));
                }
            }
            else if (_useHealing)
            {
                if (_useDealt)
                {
                    if (args is HealingDealtValueChangeException ex2 && !ex2.Equals(null))
                    {
                        ex2.AddModifier(new AdditionValueModifier(dmgDealt: true, _integerToModify, _roundNegatives));
                    }
                }
                else if (args is HealingReceivedValueChangeException ex3 && !ex3.Equals(null))
                {
                    ex3.AddModifier(new AdditionValueModifier(dmgDealt: false, _integerToModify, _roundNegatives));
                }
            }
            else if (_useDealt)
            {
                if (args is DamageDealtValueChangeException ex4 && !ex4.Equals(null))
                {
                    ex4.AddModifier(new AdditionValueModifier(dmgDealt: true, _integerToModify, _roundNegatives));
                }
            }
            else if (args is DamageReceivedValueChangeException ex5 && !ex5.Equals(null))
            {
                ex5.AddModifier(new AdditionValueModifier(dmgDealt: false, _integerToModify, _roundNegatives));
            }
        }

        public override void CustomOnTriggerAttached(IWearableEffector caller)
        {
            TriggerCalls[] secondPerformTriggersOn = _secondPerformTriggersOn;
            for (int i = 0; i < secondPerformTriggersOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondPerformTriggersOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.AddObserver(TryPerformWearable, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IWearableEffector caller)
        {
            TriggerCalls[] secondPerformTriggersOn = _secondPerformTriggersOn;
            for (int i = 0; i < secondPerformTriggersOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondPerformTriggersOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.RemoveObserver(TryPerformWearable, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void FinalizeCustomTriggerItem(object sender, object args, int triggerID)
        {
            if (sender is IWearableEffector wearableEffector && !wearableEffector.Equals(null) && !wearableEffector.IsWearableConsumed)
            {
                bool itemConsumed = false;
                if (_GetsConsumedOnSecondaryUse)
                {
                    itemConsumed = true;
                    wearableEffector.ConsumeWearable();
                }

                if (_secondDoesPerformItemPopUp)
                {
                    CombatManager.Instance.AddUIAction(new ShowItemInformationUIAction(wearableEffector.ID, GetItemLocData().text, itemConsumed, wearableImage));
                }

                IUnit caster = sender as IUnit;
                if (_secondImmediateEffect)
                {
                    CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction(_secondEffects, caster));
                }
                else
                {
                    CombatManager.Instance.AddSubAction(new EffectAction(_secondEffects, caster));
                }
            }
        }

        public void TryPerformWearable(object sender, object args)
        {
            if (!(sender is IWearableEffector wearableEffector) || wearableEffector.Equals(null) || !wearableEffector.CanWearableTrigger)
            {
                return;
            }

            if (_secondPerformConditions != null && !_secondPerformConditions.Equals(null))
            {
                EffectorConditionSO[] secondPerformConditions = _secondPerformConditions;
                for (int i = 0; i < secondPerformConditions.Length; i++)
                {
                    if (!secondPerformConditions[i].MeetCondition(wearableEffector, args))
                    {
                        return;
                    }
                }
            }

            if (_secondImmediateEffect)
            {
                CombatManager.Instance.ProcessImmediateAction(new PerformItemCustomImmediateAction(this, sender, args, 0));
            }
            else
            {
                CombatManager.Instance.AddSubAction(new PerformItemCustomAction(this, sender, args, 0));
            }
        }
    }
}
