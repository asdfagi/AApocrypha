using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetSplitOrReplaceHealthFromListEffect : EffectSO
    {
        public ManaColorSO[] _colors = [];
        public List<ManaColorSO> _colorBlacklist;
        public bool _transformBlacklist = true;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_colors.Length <= 0 || _colorBlacklist == null) { return false; }
            
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    IUnit targetUnit = target.Unit;
                    if (_colorBlacklist.Contains(targetUnit.HealthColor) && !_transformBlacklist) { continue; }
                    List<ManaColorSO> newColorSort = new List<ManaColorSO>();
                    foreach (ManaColorSO mana in _colors)
                    {
                        if (targetUnit.HealthColor.SharesPigmentColor(mana))
                        {
                            continue;
                        }
                        newColorSort.Add(mana);
                    }
                    if (newColorSort.Count <= 0) { continue; }
                    ManaColorSO _color;
                    if (newColorSort.Count > 1)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, newColorSort.Count);
                        _color = newColorSort[randomIndex];
                    }
                    else
                    {
                        _color = newColorSort[0];
                    }
                    if (_colorBlacklist.Contains(targetUnit.HealthColor))
                    {
                        if (_transformBlacklist)
                        {
                            targetUnit.ChangeHealthColor(_color);
                            exitAmount++;
                        }
                        else { continue; }
                    }
                    else
                    {
                        List<ManaColorSO> newColors = [];
                        List<ManaColorSO> validInputColors = [];
                        //Debug.Log($"testing unit {targetUnit.Name} with health color {targetColor.pigmentID}");
                        foreach (ManaColorSO loadedPigment in LoadedDBsHandler.PigmentDB._PigmentPool.Values)
                        {
                            //Debug.Log($"testing pigment {loadedPigment.pigmentID}...");
                            if (targetUnit.HealthColor.ContainsPigment([loadedPigment.pigmentID]))
                            {
                                //Debug.Log($"match found - adding {loadedPigment.pigmentID}");
                                newColors.Add(loadedPigment);
                            }
                        }
                        if (newColors.Count == 0)
                        {
                            Debug.LogWarning($"Health Splitter | no valid pigments registered in unit {targetUnit.Name} - skipping...");
                            continue;
                        }
                        newColors.Add(_color);
                        if (targetUnit.ChangeHealthColor(Pigments.SplitPigment(newColors.ToArray()))) { exitAmount++; }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
