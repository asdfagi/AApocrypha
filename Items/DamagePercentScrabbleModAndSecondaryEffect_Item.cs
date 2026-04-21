using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class DamagePercentScrabbleModAndSecondaryEffect_Item : BaseItem
    {
        public PercentDamageByTargetScrabbleModAndEffectWearable item;

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

        public bool DoesIncreaseDamage
        {
            set
            {
                item._doesIncrease = value;
            }
        }

        public int PercentageToModify
        {
            set
            {
                item._percentageToModify = value;
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

        public DamagePercentScrabbleModAndSecondaryEffect_Item(string itemID = "DefaultID_Item", int percentage = 1, bool useDealt = false, bool useInt = false, bool doesIncreaseDmg = false)
        {
            item = ScriptableObject.CreateInstance<PercentDamageByTargetScrabbleModAndEffectWearable>();
            item._percentageToModify = percentage;
            item._useDealt = useDealt;
            item._useSimpleInt = useInt;
            item._doesIncrease = doesIncreaseDmg;
            InitializeItemData(itemID);
        }
    }
}
