using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace A_Apocrypha.CustomEffects
{
    public class CasterStealLettersFromTargetEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            string initialName = "";
            if (caster is CharacterCombat ch)
            {
                initialName = ch._currentName;
            } else if (caster is EnemyCombat en)
            {
                initialName = en._currentName;
            }
            if (initialName == "") { return false; }
            Debug.Log("CROSSWORD | initialName is chosen: " + initialName);
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is CharacterCombat targetCH)
                    {
                        List<char> charList = new List<char>();
                        if (targetCH._currentName == null || targetCH._currentName == "") { continue; }
                        foreach (char c in targetCH._currentName)
                        {
                            if (!charList.Contains(char.ToLower(c)))
                            {
                                charList.Add(char.ToLower(c));
                            }
                        }
                        char targetChar = charList[UnityEngine.Random.Range(0, charList.Count)];

                        string cutName = "";
                        bool upper = false;

                        foreach (char c in targetCH._currentName)
                        {
                            if (char.ToLower(c) == targetChar)
                            {
                                initialName += c;
                                exitAmount++;
                                Debug.Log("CROSSWORD | " + initialName);
                                if (char.IsUpper(c))
                                {
                                    upper = true;
                                }
                            }
                            else
                            {
                                if (upper)
                                {
                                    cutName += Char.ToUpper(c);
                                    upper = false;
                                }
                                else
                                {
                                    cutName += c;
                                }
                            }
                        }

                        targetCH._currentName = cutName;

                        foreach (CharacterCombatUIInfo characterInfo in stats.combatUI._charactersInCombat.Values)
                        {
                            if (characterInfo.SlotID == targetCH.SlotID)
                            {
                                characterInfo.Name = targetCH._currentName;
                            }
                        }
                    }
                    else if (target.Unit is EnemyCombat targetEN)
                    {
                        List<char> charList = new List<char>();
                        if (targetEN._currentName == null || targetEN._currentName == "") { continue; }
                        foreach (char c in targetEN._currentName)
                        {
                            if (!charList.Contains(char.ToLower(c)))
                            {
                                charList.Add(char.ToLower(c));
                            }
                        }
                        char targetChar = charList[UnityEngine.Random.Range(0, charList.Count)];

                        string cutName = "";
                        bool upper = false;

                        foreach (char c in targetEN._currentName)
                        {
                            if (char.ToLower(c) == targetChar)
                            {
                                initialName += c;
                                exitAmount++;
                                if (char.IsUpper(c))
                                {
                                    upper = true;
                                }
                            }
                            else
                            {
                                if (upper)
                                {
                                    cutName += Char.ToUpper(c);
                                    upper = false;
                                }
                                else
                                {
                                    cutName += c;
                                }
                            }
                        }

                        targetEN._currentName = cutName;

                        foreach (EnemyCombatUIInfo enemyInfo in stats.combatUI._enemiesInCombat.Values)
                        {
                            if (enemyInfo.SlotID == targetEN.SlotID)
                            {
                                enemyInfo.Name = targetEN._currentName;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("CROSSWORD | invalid unit?");
                    }
                }
            }
            if (caster is CharacterCombat ch2)
            {
                ch2._currentName = initialName;

                foreach (CharacterCombatUIInfo characterInfo in stats.combatUI._charactersInCombat.Values)
                {
                    if (characterInfo.SlotID == ch2.SlotID)
                    {
                        characterInfo.Name = ch2._currentName;
                    }
                }
            }
            else if (caster is EnemyCombat en2)
            {
                en2._currentName = initialName;

                foreach (EnemyCombatUIInfo enemyInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    if (enemyInfo.SlotID == en2.SlotID)
                    {
                        enemyInfo.Name = en2._currentName;
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
