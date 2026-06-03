using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{// harvested from ITA - thanks millie
    public class MoveByCasterStoredValueEffect : SwapToOneSideEffect
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            int num = caster.SimpleGetStoredValue(this.storedValue);
            bool flag = num == this.LeftValue;
            if (flag)
            {
                this._swapRight = false;
            }
            else
            {
                bool flag2 = num == this.RightValue;
                if (flag2)
                {
                    this._swapRight = true;
                }
            }
            return base.PerformEffect(stats, caster, targets, areTargetSlots, entryVariable, out exitAmount);
        }

        // Token: 0x0400002C RID: 44
        public string storedValue;

        // Token: 0x0400002D RID: 45
        public int LeftValue;

        // Token: 0x0400002E RID: 46
        public int RightValue;
    }
}
