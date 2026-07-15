using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class SpiriferForkHitEffectCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    if (damaged.IsUnitCharacter || damaged.AbilityCount <= 1) { return false; } // end early if target is character or has 1 or less abilities

                    BooleanReference booleanReference = new BooleanReference(entryValue: false);
                    CombatManager.Instance.ProcessImmediateAction(new CheckBundleDifficultyIAction(booleanReference, BundleDifficulty.Boss));
                    if (booleanReference.value) { return false; } // end early if this is a boss encounter

                    if (CombatManager.Instance._informationHolder.Run.inGameData.GetBoolData("SpiriferForkUsed")) { return false; } // end early if a spirifer's fork was already used this combat
                    Debug.Log("Spirifer's Fork | abstraction time!");
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterRemoveRandomAbilityEffect>(), 1, Targeting.Slot_SelfSlot)], reference.damagedUnit, 0));
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(ScriptableObject.CreateInstance<RemoveTargetTimelineAbilityEffect>(), 10, Targeting.Slot_SelfSlot)], reference.damagedUnit, 0));

                    CombatManager.Instance._informationHolder.Run.inGameData.SetBoolData("SpiriferForkUsed", true);
                }
                return true;
            }
            return false;
        }
    }
}
