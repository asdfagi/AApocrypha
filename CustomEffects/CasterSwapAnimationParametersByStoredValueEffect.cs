using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{// harvested from ITA - thanks millie!
    public class CasterSwapAnimationParametersByStoredValueEffect : SetCasterAnimationParameterEffect
    {
        // Token: 0x060000B9 RID: 185 RVA: 0x0000BD90 File Offset: 0x00009F90
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            int num = caster.SimpleGetStoredValue(this.storedValue);
            bool flag = num == this.LeftValue;
            if (flag)
            {
                this._parameterValue = this.setAsIfLeft;
            }
            bool flag2 = num == this.RightValue;
            if (flag2)
            {
                this._parameterValue = this.setAsIfRight;
            }
            return base.PerformEffect(stats, caster, targets, areTargetSlots, entryVariable, out exitAmount);
        }

        // Token: 0x04000032 RID: 50
        public string storedValue;

        // Token: 0x04000033 RID: 51
        public int LeftValue;

        // Token: 0x04000034 RID: 52
        public int RightValue;

        // Token: 0x04000035 RID: 53
        public int setAsIfLeft;

        // Token: 0x04000036 RID: 54
        public int setAsIfRight;
    }
}
