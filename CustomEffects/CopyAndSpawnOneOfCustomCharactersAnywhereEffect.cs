using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace A_Apocrypha.CustomEffects
{
    public class CopyAndSpawnOneOfCustomCharactersAnywhereEffect : EffectSO
    {
        public string[] _characterCopies = [""];

        public int _rank;

        public NameAdditionLocID _nameAddition;

        public bool _permanentSpawn;

        public bool _usePreviousAsHealth;

        public WearableStaticModifierSetterSO[] _extraModifiers;

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

            int currentHealth = (_usePreviousAsHealth ? Mathf.Max(1, base.PreviousExitValue) : character.GetMaxHealth(_rank));
            int[] usedAbilities = character.GenerateAbilities();
            WearableStaticModifiers modifiers = new WearableStaticModifiers();
            WearableStaticModifierSetterSO[] extraModifiers = _extraModifiers;
            for (int i = 0; i < extraModifiers.Length; i++)
            {
                extraModifiers[i].OnAttachedToCharacter(modifiers, character, _rank);
            }

            string nameAdditionData = LocUtils.GameLoc.GetNameAdditionData(_nameAddition);
            for (int j = 0; j < entryVariable; j++)
            {
                CombatManager.Instance.AddSubAction(new SpawnCharacterAction(character, -1, trySpawnAnyways: false, nameAdditionData, _permanentSpawn, _rank, usedAbilities, currentHealth, modifiers));
            }

            exitAmount = entryVariable;
            return true;
        }
    }
}
