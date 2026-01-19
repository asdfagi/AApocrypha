using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class GenerateColorsByListManaFillPigmentBarEffect : EffectSO
    {
        public ManaColorSO[] _manaColors = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_manaColors.Length <= 0) {
                Debug.Log("Mixed Pigment Condense | early return triggered");
                return false;
            }
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                if (manaSlot.ManaColor == null)
                {
                    Debug.Log("Mixed Pigment Condense | manacolor is null");
                    int randomIndex = UnityEngine.Random.Range(0, _manaColors.Length);
                    Debug.Log($"Mixed Pigment Condense | chosen color: {_manaColors[randomIndex].name}");
                    CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(_manaColors[randomIndex], 1, caster.IsUnitCharacter, caster.ID));
                    exitAmount++;
                }
            }
            Debug.Log("Mixed Pigment Condense | final value: " + exitAmount);
            return exitAmount > 0;
        }
    }
}
