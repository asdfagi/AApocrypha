using A_Apocrypha.CustomOther;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class BrokenFuseDamageIncrease_Item : BaseItem
    {
        public InhibitorDamageWearable item;

        public override BaseWearableSO Item => item;

        public int NormalAddition
        {
            set => item._toAdd1 = value;
        }

        public int RobotAddition
        {
            set => item._toAdd0 = value;
        }

        public int NormalAddition2
        {
            set => item._toAdd1from = value;
        }

        public int RobotAddition2
        {
            set => item._toAdd0from = value;
        }

        public bool AffectDamageDealtInsteadOfReceived
        {
            set => item._useDealt = value;
        }

        public bool UseSimpleIntegerInsteadOfDamage
        {
            set => item._useSimpleInt = value;
        }

        public bool UseRangeFromTo
        {
            set => item._useRange = value;
        }

        public BrokenFuseDamageIncrease_Item(string itemID = "DefaultID_Item", int additionNormal = 1, int additionRobot = 1, int additionNormal2 = 1, int additionRobot2 = 1, bool useDealt = false, bool useInt = false, bool useRange = false)
        {
            item = ScriptableObject.CreateInstance<InhibitorDamageWearable>();
            item._toAdd0 = additionRobot;
            item._toAdd1 = additionNormal;
            item._toAdd0from = additionRobot2;
            item._toAdd1from = additionNormal2;
            item._useDealt = useDealt;
            item._useSimpleInt = useInt;
            item._useRange = useRange;
            InitializeItemData(itemID);
        }
    }
}
