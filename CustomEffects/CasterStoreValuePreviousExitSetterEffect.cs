using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{// harvested from ITA - thanks millie!

    public class CasterStoreValuePreviousExitSetterEffect : EffectSO
    {
        // Token: 0x06000870 RID: 2160 RVA: 0x0010F120 File Offset: 0x0010D320
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            UnitStoreDataHolder unitStoreDataHolder;
            bool flag = caster.TryGetStoredData(this.m_unitStoredDataID, out unitStoreDataHolder, true);
            bool flag2 = !this._ignoreIfContains || !flag;
            bool result;
            if (flag2)
            {
                unitStoreDataHolder.m_MainData = base.PreviousExitValue;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        // Token: 0x04000153 RID: 339
        public bool _ignoreIfContains;

        // Token: 0x04000154 RID: 340
        [UnitStoreValueNamesIDsEnumRef]
        public string m_unitStoredDataID = "";
    }
}
