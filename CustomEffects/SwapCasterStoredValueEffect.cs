using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{//harvested from ITA - thanks millie!

    // Token: 0x0200004A RID: 74
    public class SwapCasterStoredValueEffect : EffectSO
    {
        // Token: 0x060000B7 RID: 183 RVA: 0x0000BD1C File Offset: 0x00009F1C
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            int num = caster.SimpleGetStoredValue(this.storedValue);
            bool flag = num == this.firstValue;
            if (flag)
            {
                caster.SimpleSetStoredValue(this.storedValue, this.secondValue);
            }
            else
            {
                caster.SimpleSetStoredValue(this.storedValue, this.firstValue);
            }
            exitAmount = caster.SimpleGetStoredValue(this.storedValue);
            return true;
        }

        // Token: 0x0400002F RID: 47
        public string storedValue;

        // Token: 0x04000030 RID: 48
        public int firstValue;

        // Token: 0x04000031 RID: 49
        public int secondValue;
    }
}
