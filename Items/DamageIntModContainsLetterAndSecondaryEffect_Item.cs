using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class DamageIntModContainsLetterAndSecondaryEffect_Item : BaseItem
    {
        public IntDamageModContainsLetterAndEffectWearable item;

        public override BaseWearableSO Item => item;

        public bool AffectDamageDealtInsteadOfReceived
        {
            set
            {
                item._useDealt = value;
            }
        }

        public bool UseSimpleIntegerInsteadOfDamage
        {
            set
            {
                item._useSimpleInt = value;
            }
        }

        public bool RoundNegatives
        {
            set
            {
                item._roundNegatives = value;
            }
        }

        public int IntegerToModify
        {
            set
            {
                item._integerToModify = value;
            }
        }

        public TriggerCalls[] SecondaryTriggerOn
        {
            get
            {
                return item._secondPerformTriggersOn;
            }
            set
            {
                item._secondPerformTriggersOn = value;
            }
        }

        public bool SecondaryDoesPopUpInfo
        {
            set
            {
                item._secondDoesPerformItemPopUp = value;
            }
        }

        public EffectorConditionSO[] SecondaryConditions
        {
            get
            {
                return item._secondPerformConditions;
            }
            set
            {
                item._secondPerformConditions = value;
            }
        }

        public bool SecondaryConsumeOnUse
        {
            set
            {
                item._GetsConsumedOnSecondaryUse = value;
            }
        }

        public EffectInfo[] SecondaryEffects
        {
            get
            {
                return item._secondEffects;
            }
            set
            {
                item._secondEffects = value;
            }
        }

        public bool SecondaryIsEffectImmediate
        {
            set
            {
                item._secondImmediateEffect = value;
            }
        }

        public DamageIntModContainsLetterAndSecondaryEffect_Item(string itemID = "DefaultID_Item", int integer = 1, bool useDealt = false, bool useInt = false, bool roundNegatives = false, char targetLetter = 'A', bool stackBonus = false)
        {
            item = ScriptableObject.CreateInstance<IntDamageModContainsLetterAndEffectWearable>();
            item._integerToModify = integer;
            item._useDealt = useDealt;
            item._useSimpleInt = useInt;
            item._roundNegatives = roundNegatives;
            item._targetLetter = targetLetter;
            item._stackBonuses = stackBonus;
            InitializeItemData(itemID);
        }
    }
}
