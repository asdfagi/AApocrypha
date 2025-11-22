using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class LeftOrRightToOpposeEnemyChanceForNextEffect : EffectSO
    {
        public bool _inverted = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            bool leftOccupied = false;
            if (caster.SlotID != 0)
            {
                leftOccupied = stats.combatSlots.GetOpponentSlotTarget(caster.SlotID, -1, caster.IsUnitCharacter).HasUnit;
            }
            if (leftOccupied) { Debug.Log("Navigator | Left slot Opposing occupied"); }
            bool rightOccupied = false;
            if (caster.SlotID != 4)
            {
                rightOccupied = stats.combatSlots.GetOpponentSlotTarget(caster.SlotID, 1, caster.IsUnitCharacter).HasUnit;
            }
            if (rightOccupied) { Debug.Log("Navigator | Right slot Opposing occupied"); }
            if (_inverted)
            {
                if (caster.SlotID == 0)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Leftmost, move Right");
                }
                else if (caster.SlotID == 4)
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Rightmost, move Left");
                }
                else if (leftOccupied && !rightOccupied)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Only Left Opposing occupied, move Right");
                }
                else if (rightOccupied && !leftOccupied)
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Only Right Opposing occupied, move Left");
                }
                else if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Ambiguous, fate says move Right");
                }
                else
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Ambiguous, fate says move Left");
                }
                // 0/false is Left, 1/true is Right
            }
            else
            {
                if (caster.SlotID == 0)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Leftmost, move Right");
                }
                else if (caster.SlotID == 4)
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Rightmost, move Left");
                }
                else if (leftOccupied && !rightOccupied)
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Only Left Opposing occupied, move Left");
                }
                else if (rightOccupied && !leftOccupied)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Only Right Opposing occupied, move Right");
                }
                else if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    exitAmount = 1;
                    Debug.Log("Navigator | Ambiguous, fate says move Right");
                }
                else
                {
                    exitAmount = 0;
                    Debug.Log("Navigator | Ambiguous, fate says move Left");
                }
                // 0/false is Left, 1/true is Right
            }
            return exitAmount > 0;
        }
    }
}
