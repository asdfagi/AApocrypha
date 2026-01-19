using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetPreviousValueComparatorEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<int> results = [];

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    results.Add(target.SlotID + 1);
                }
            }

            while (results.Count > 1)
            {
                int randomindex = UnityEngine.Random.Range(0, results.Count);
                results.RemoveAt(randomindex);
            }

            if (results.Count > 0)
            {
                if (results[0] == PreviousExitValue)
                {
                    exitAmount = results[0];
                    Debug.Log("Targeter | targeter check passed, chosen target in slot " + exitAmount);
                }
                else
                {
                    Debug.Log("Targeter | targeter check failed");
                }
            }
            return exitAmount > 0;
        }
    }
}
