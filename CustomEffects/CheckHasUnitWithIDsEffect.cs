using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckHasUnitWithIDsEffect : EffectSO
    {
        public string[] _ids = [""];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (!targets[i].HasUnit)
                {
                    return false;
                }
                if (targets[i].Unit is EnemyCombat enemy)
                {
                    if (_ids.Contains(enemy.Enemy.name))
                    {
                        exitAmount++;
                    }
                }
            }
            Debug.Log(exitAmount > 0);
            return exitAmount > 0;
        }
    }
}
