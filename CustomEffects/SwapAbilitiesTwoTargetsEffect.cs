using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.CanvasScaler;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class SwapAbilitiesTwoTargetsEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets.Length == 2)
            {
                if (targets[0].HasUnit && targets[1].HasUnit)
                {
                    if (targets[0].Unit.ID == targets[1].Unit.ID) { return false; }
                    if (targets[0].IsTargetCharacterSlot && targets[1].IsTargetCharacterSlot)
                    {
                        try
                        {
                            CharacterCombat ch1 = targets[0].Unit as CharacterCombat;
                            CharacterCombat ch2 = targets[1].Unit as CharacterCombat;

                            var abilityList1 = ch1.CombatAbilities;
                            var abilityList2 = ch2.CombatAbilities;

                            ch1.CombatAbilities = abilityList2;
                            ch2.CombatAbilities = abilityList1;

                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(ch1.ID));
                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(ch2.ID));

                            exitAmount++;
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning("Ability Swapper | an error has occurred!\n" + e);
                            return false;
                        }
                    }
                    if (!targets[0].IsTargetCharacterSlot && !targets[1].IsTargetCharacterSlot)
                    {
                        try
                        {
                            EnemyCombat en1 = targets[0].Unit as EnemyCombat;
                            EnemyCombat en2 = targets[1].Unit as EnemyCombat;

                            var abilityList1 = en1.Abilities;
                            var abilityList2 = en2.Abilities;

                            en1.Abilities = abilityList2;
                            en2.Abilities = abilityList1;

                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(en1.ID));
                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(en2.ID));

                            exitAmount++;
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning("Ability Swapper | an error has occurred!\n" + e);
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            return exitAmount > 0;
        }
    }
}
