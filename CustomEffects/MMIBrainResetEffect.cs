using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class MMIBrainResetEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            BaseWearableSO newItem = LoadedAssetsHandler.GetWearable("MMI_SW");
            bool num = caster.TrySetUpNewItem(newItem);
            if (num)
            {
                exitAmount = 1;
            }

            return num;
        }
    }
}
