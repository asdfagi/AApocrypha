using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterRandomTransformationNotCasterWithRarePoolEffect : EffectSO
    {
        public bool _fullyHeal = true;

        public bool _maintainTimelineAbilities;

        public bool _maintainMaxHealth;

        public bool _currentToMaxHealth;

        public List<TransformOption> _possibleTransformations;

        public List<TransformOption> _possibleRareTransformations;

        public int _rarityPercentage = 100; // 100 is always common, 0 is always rare

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_possibleTransformations == null || _possibleTransformations.Count <= 0)
            {
                return false;
            }
            if (_possibleRareTransformations == null || _possibleRareTransformations.Count <= 0)
            {
                return false;
            }

            bool rarePool = false;
            if (UnityEngine.Random.Range(0, 100) >= _rarityPercentage) {
                rarePool = true;
            }

            List<TransformOption> newPossible = new List<TransformOption>();
            foreach (TransformOption option in (rarePool ? _possibleRareTransformations : _possibleTransformations))
            {
                if (caster.IsUnitCharacter == false)
                {
                    EnemyCombat casterEN = caster as EnemyCombat;
                    if (option.enemyTransformation.name == casterEN.Enemy.name)
                    {
                        Debug.Log($"Transform No Repeat | not adding {option.enemyTransformation.name}, matches caster");
                        continue;
                    }
                }
                if (caster.IsUnitCharacter == true)
                {
                    CharacterCombat casterCH = caster as CharacterCombat;
                    if (option.characterTransformation == casterCH.Character.name)
                    {
                        Debug.Log($"Transform No Repeat | not adding {option.characterTransformation}, matches caster");
                        continue;
                    }
                }
                newPossible.Add(option);
            }

            if (newPossible.Count <= 0)
            {
                return false;
            }

            int index = UnityEngine.Random.Range(0, newPossible.Count);
            if (caster.IsUnitCharacter)
            {
                CharacterSO character = LoadedAssetsHandler.GetCharacter(newPossible[index].characterTransformation);
                if (character == null || character.Equals(null))
                {
                    return false;
                }

                return stats.TryTransformCharacter(caster.ID, character, _fullyHeal, _maintainMaxHealth, _currentToMaxHealth);
            }

            return stats.TryTransformEnemy(caster.ID, newPossible[index].enemyTransformation, _fullyHeal, _maintainTimelineAbilities, _maintainMaxHealth, _currentToMaxHealth);
        }
    }
}
