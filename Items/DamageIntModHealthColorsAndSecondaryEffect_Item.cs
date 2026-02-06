using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class DamageIntModHealthColorsAndSecondaryEffect_Item : BaseItem
    {
        public IntDamageModHealthColorsAndEffectWearable item;

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

        public DamageIntModHealthColorsAndSecondaryEffect_Item(string itemID = "DefaultID_Item", int integer = 1, bool useDealt = false, bool useInt = false, bool roundNegatives = false)
        {
            item = ScriptableObject.CreateInstance<IntDamageModHealthColorsAndEffectWearable>();
            item._integerToModify = integer;
            item._useDealt = useDealt;
            item._useSimpleInt = useInt;
            item._roundNegatives = roundNegatives;
            InitializeItemData(itemID);
        }
    }
}
