using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class MMI_Item : BaseItem
    {
        public MMIWearable item;

        public override BaseWearableSO Item => item;

        public EffectInfo[] Effects
        {
            get
            {
                return item._firstEffects;
            }
            set
            {
                item._firstEffects = value;
            }
        }

        public bool IsEffectImmediate
        {
            set
            {
                item._firstImmediateEffect = value;
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

        public MMI_Item(string itemID = "DefaultID_Item", EffectInfo[] effects = null, bool immediate = false)
        {
            item = ScriptableObject.CreateInstance<MMIWearable>();
            item._firstImmediateEffect = immediate;
            item._firstEffects = effects;
            InitializeItemData(itemID);
        }
    }
}
