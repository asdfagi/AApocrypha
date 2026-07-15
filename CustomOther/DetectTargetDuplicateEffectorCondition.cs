using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class DetectTargetDuplicateEffectorCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    int counter = 0;
                    if (damaged is CharacterCombat damagedCH)
                    { 
                        Dictionary<int, CharacterCombat> chars = CombatManager.Instance._stats.CharactersOnField;
                        Debug.Log("Duplicate Detector | looking for character " + damagedCH.Character.name);
                        foreach (CharacterCombat character in chars.Values)
                        {
                            if (character.Character.name == damagedCH.Character.name)
                            {
                                counter++;
                            }
                        }
                    } else if (damaged is EnemyCombat damagedEN)
                    {
                        Dictionary<int, EnemyCombat> enemies = CombatManager.Instance._stats.EnemiesOnField;
                        Debug.Log("Duplicate Detector | looking for enemy " + damagedEN.Enemy.name);
                        foreach (EnemyCombat enemy in enemies.Values)
                        {
                            if (enemy.Enemy.name == damagedEN.Enemy.name)
                            {
                                counter++;
                            }
                        }
                    }
                    Debug.Log("Duplicate Detector | final count: " + counter);
                    if (counter >= 2) { return true; }
                }
            }
            return false;
        }
    }
}
