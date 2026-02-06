using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class UnlockContentByIDEffect : EffectSO
    {
        public string _unlockID = "";
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            try
            {
                CombatManager.Instance._informationHolder.UnlockableManager.TryUnlockFromID(_unlockID);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Direct Unlock | uh-oh! error: {e}");
                return false;
            }
        }
    }
}
