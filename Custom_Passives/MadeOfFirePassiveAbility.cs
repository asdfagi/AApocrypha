using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Custom_Passives
{
    public class MadeOfFirePassiveAbility : BasePassiveAbilitySO
    {
        public EffectorConditionSO[] _secondPerformConditions;
        public override void TriggerPassive(object sender, object args)
        {
            IPassiveEffector passiveEffector = sender as IPassiveEffector;
            DamageReceivedValueChangeException damage = args as DamageReceivedValueChangeException;
            if (damage.damageTypeID == CombatType_GameIDs.Dmg_Fire.ToString())
            {
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(passiveEffector.ID, passiveEffector.IsUnitCharacter, base.GetPassiveLocData().text, this.passiveIcon));
                damage.AddModifier(new MultiplyIntValueModifier(false, 0));
            }
        }
        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }
        public override void OnPassiveConnected(IUnit unit)
        {
            Debug.Log($"Made Of Fire | passive connected to unit {unit.Name}");
        }
        public override void OnPassiveDisconnected(IUnit unit)
        {
            Debug.Log($"Made Of Fire | passive disconnected from unit {unit.Name}");
        }
        public override void CustomOnTriggerAttached(IPassiveEffector caller)
        {
            CombatManager.Instance.AddObserver(CustomTryTriggerPassive, "ShouldConnectAndDisconnectFieldEffectsTrigger", caller);
            Debug.Log("Made Of Fire | trigger attached");
        }

        public override void CustomOnTriggerDettached(IPassiveEffector caller)
        {
            CombatManager.Instance.RemoveObserver(CustomTryTriggerPassive, "ShouldConnectAndDisconnectFieldEffectsTrigger", caller);
            Debug.Log("Made Of Fire | trigger dettached");
        }

        public override void FinalizeCustomTriggerPassive(object sender, object args, int triggerID)
        {
            Debug.Log("Made Of Fire | mysterious finalize thing triggered");
            if (sender is IPassiveEffector passiveEffector && !passiveEffector.Equals(null))
            {
                /*if (_secondDoesPerformPopUp)
                {
                    CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(passiveEffector.ID, passiveEffector.IsUnitCharacter, GetPassiveLocData().text, passiveIcon));
                }*/

                IUnit caster = sender as IUnit;
                Debug.Log("Made Of Fire | the actual effect is now running");
                BooleanReference booleanReference = args as BooleanReference;
                if (booleanReference != null)
                {
                    Debug.Log($"Made Of Fire | booleanReference is ({(booleanReference.value ? "true, allegedly" : "not true, apparently")} ({booleanReference.value.ToString()}) - setting to false");
                    booleanReference.value = false;
                }
            }
        }

        public void CustomTryTriggerPassive(object sender, object args)
        {
            if (!(sender is IPassiveEffector passiveEffector) || passiveEffector.Equals(null) || !passiveEffector.CanPassiveTrigger(m_PassiveID))
            {
                Debug.LogWarning("Made Of Fire | trigger failed");
                return;
            }

            if (_secondPerformConditions != null && !_secondPerformConditions.Equals(null))
            {
                EffectorConditionSO[] secondPerformConditions = _secondPerformConditions;
                for (int i = 0; i < secondPerformConditions.Length; i++)
                {
                    if (!secondPerformConditions[i].MeetCondition(passiveEffector, args))
                    {
                        Debug.Log("Made Of Fire | condition blocked trigger");
                        return;
                    }
                }
            }

            Debug.Log("Made Of Fire | checks passed, (hopefully) activating passive action...");
            IUnit caster = sender as IUnit;
            BooleanReference booleanReference = args as BooleanReference;
            if (booleanReference != null)
            {
                Debug.Log($"Made Of Fire | booleanReference is ({(booleanReference.value ? "true, allegedly" : "not true, apparently")} ({booleanReference.value.ToString()}) - setting to false");
                booleanReference.value = false;
            }
            else
            {
                Debug.LogWarning($"Made Of Fire | something has gone wrong! parameters: booleanReference ({(booleanReference?.value)}) - args: ({args})");
            }
        }
    }
}
