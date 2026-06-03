using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace A_Apocrypha.Custom_Passives
{
    public class BonusSuiteAndSecondaryEffectPassiveAbility : BasePassiveAbilitySO
    {
        [Header("ExtraAttack Data")]
        public List<ExtraAbilityInfo> _suiteAbilities;

        public TriggerCalls[] _secondTriggerOn;

        public EffectorConditionSO[] _secondPerformConditions;

        public bool _secondDoesPerformPopUp = true;

        public EffectInfo[] _secondEffects;
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (args is List<string> list)
            {
                list.Add(_suiteAbilities[ChooseAbility()].ability?.name);
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            List<string> addedOnes = new List<string>();
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log("added ability " + ability.ability.name);
                if (!addedOnes.Contains(ability.ability._abilityName))
                {
                    unit.AddExtraAbility(ability);
                    addedOnes.Add(ability.ability._abilityName);
                }
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log(ability.ability.name);
                unit.TryRemoveExtraAbility(ability);
            }
        }

        public int ChooseAbility()
        {
            return UnityEngine.Random.Range(0, _suiteAbilities.Count);
        }

        public override void CustomOnTriggerAttached(IPassiveEffector caller)
        {
            TriggerCalls[] secondTriggerOn = _secondTriggerOn;
            for (int i = 0; i < secondTriggerOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondTriggerOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.AddObserver(CustomTryTriggerPassive, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IPassiveEffector caller)
        {
            TriggerCalls[] secondTriggerOn = _secondTriggerOn;
            for (int i = 0; i < secondTriggerOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondTriggerOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.RemoveObserver(CustomTryTriggerPassive, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void FinalizeCustomTriggerPassive(object sender, object args, int triggerID)
        {
            if (sender is IPassiveEffector passiveEffector && !passiveEffector.Equals(null))
            {
                if (_secondDoesPerformPopUp)
                {
                    CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(passiveEffector.ID, passiveEffector.IsUnitCharacter, GetPassiveLocData().text, passiveIcon));
                }

                IUnit caster = sender as IUnit;
                CombatManager.Instance.AddSubAction(new EffectAction(_secondEffects, caster));
            }
        }

        public void CustomTryTriggerPassive(object sender, object args)
        {
            if (!(sender is IPassiveEffector passiveEffector) || passiveEffector.Equals(null) || !passiveEffector.CanPassiveTrigger(m_PassiveID))
            {
                return;
            }

            if (_secondPerformConditions != null && !_secondPerformConditions.Equals(null))
            {
                EffectorConditionSO[] secondPerformConditions = _secondPerformConditions;
                for (int i = 0; i < secondPerformConditions.Length; i++)
                {
                    if (!secondPerformConditions[i].MeetCondition(passiveEffector, args))
                    {
                        return;
                    }
                }
            }

            CombatManager.Instance.AddSubAction(new PerformPassiveCustomAction(this, sender, args, 0));
        }
    }
}
