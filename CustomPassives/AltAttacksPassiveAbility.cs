using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// from Hell Island Fell
namespace A_Apocrypha.Custom_Passives
{
    public class AltAttacksPassiveAbility : BasePassiveAbilitySO
    {
        [Header("ExtraAttack Data")]
        public List<ExtraAbilityInfo> _altAbilities;

        public List<ExtraAbilityInfo> _weights;
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (args is List<string> list)
            {
                list.Add(_weights[ChooseAbility()].ability?.name);
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            foreach (ExtraAbilityInfo ability in _altAbilities)
            {
                unit.AddExtraAbility(ability);
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            foreach (ExtraAbilityInfo ability in _altAbilities)
            {
                unit.TryRemoveExtraAbility(ability);
            }
        }

        public int ChooseAbility()
        {
            return UnityEngine.Random.Range(0, _weights.Count);
        }
    }
}