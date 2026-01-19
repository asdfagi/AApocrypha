using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ReturnTargetsScrabbleScoreEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            // point value source: https://www.scrabblepages.com/scrabble/rules/
            char[] scoreOne = ['a', 'e', 'i', 'l', 'n', 'o', 'r', 's', 't', 'u', '1'];
            char[] scoreTwo = ['d', 'g', '2'];
            char[] scoreThree = ['b', 'c', 'm', 'p', '3'];
            char[] scoreFour = ['f', 'h', 'v', 'w', 'y', '4'];
            char[] scoreFive = ['k', '5'];
            char[] scoreSix = ['6'];
            char[] scoreSeven = ['7'];
            char[] scoreEight = ['j', 'x', '8'];
            char[] scoreNine = ['9'];
            char[] scoreTen = ['q', 'z'];

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    Debug.Log("Scrabble Scorer | scoring name " + target.Unit.Name);
                    foreach (char value in target.Unit.Name)
                    {
                        if (scoreOne.Contains(char.ToLower(value))) { exitAmount++; }
                        if (scoreTwo.Contains(char.ToLower(value))) { exitAmount += 2; }
                        if (scoreThree.Contains(char.ToLower(value))) { exitAmount += 3; }
                        if (scoreFour.Contains(char.ToLower(value))) { exitAmount += 4; }
                        if (scoreFive.Contains(char.ToLower(value))) { exitAmount += 5; }
                        if (scoreSix.Contains(char.ToLower(value))) { exitAmount += 6; }
                        if (scoreSeven.Contains(char.ToLower(value))) { exitAmount += 7; }
                        if (scoreEight.Contains(char.ToLower(value))) { exitAmount += 8; }
                        if (scoreNine.Contains(char.ToLower(value))) { exitAmount += 9; }
                        if (scoreTen.Contains(char.ToLower(value))) { exitAmount += 10; }
                        Debug.Log($"Scrabble Scorer | adding value of {value} - new total {exitAmount}");
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
