using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering;

namespace A_Apocrypha.CustomEffects
{
    public class CasterAnalysisStoreValueSetterEffect : EffectSO
    {
        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            Dictionary<int, string> results = [];
            List<int> keylist = [];

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    results.Add(target.Unit.ID, target.Unit.Name);
                    keylist.Add(target.Unit.ID);
                }
            }
            foreach (var result in results)
            {
                Debug.Log($"Analyzer | result entry {result.Value} with ID {result.Key}");
            }
            while (results.Count > 1)
            {
                int randomindex = UnityEngine.Random.Range(0, results.Count);
                results.Remove(keylist[randomindex]);
                keylist.RemoveAt(randomindex);
            }
            foreach (var result in results)
            {
                Debug.Log($"Analyzer | modified results, result entry {result.Value} with ID {result.Key}");
            }
            if (results.Values.Count > 0)
            {
                int targetID = keylist[0];
                results.TryGetValue(keylist[0], out string targetName);
                Debug.Log("storing entry " + entryVariable + " to storage");
                caster.SimpleSetStoredValue(m_unitStoredDataID, targetID);
                if (caster.IsUnitCharacter)
                {
                    CharacterCombat ch = caster as CharacterCombat;
                    foreach (var holder in ch.StoredValues)
                    {
                        holder.Value.m_MainString = targetName;
                    }
                }
                else if (!caster.IsUnitCharacter)
                {
                    EnemyCombat en = caster as EnemyCombat;
                    foreach (var holder in en.StoredValues)
                    {
                        holder.Value.m_MainString = targetName;
                    }
                }
                Debug.Log("stored as " + targetName + " - " + targetID);
                return true;
            }
            return false;
        }
    }
}
