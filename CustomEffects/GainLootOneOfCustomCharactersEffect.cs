using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace A_Apocrypha.CustomEffects
{
    public class GainLootOneOfCustomCharactersEffect : EffectSO
    {
        public string[] _characterCopies = [""];

        public int _rank;

        public NameAdditionLocID _nameAddition;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<string> characterCopyList = _characterCopies.ToList();
            while (characterCopyList.Count > 1)
            {
                int randomIndex = UnityEngine.Random.Range(0, characterCopyList.Count);
                characterCopyList.RemoveAt(randomIndex);
            }

            CharacterSO character = LoadedAssetsHandler.GetCharacter(characterCopyList[0]);
            if (character == null || character.Equals(null))
            {
                return false;
            }

            int maxHealth = character.GetMaxHealth(_rank);
            int[] usedAbs = character.GenerateAbilities();
            string nameAdditionData = LocUtils.GameLoc.GetNameAdditionData(_nameAddition);
            for (int i = 0; i < entryVariable; i++)
            {
                SpawnedCharacterAddition newCharacter = new SpawnedCharacterAddition(character, nameAdditionData, _rank, usedAbs, maxHealth);
                stats.GainCharacterLoot(newCharacter);
            }

            exitAmount = entryVariable;
            return true;
        }
    }
}
