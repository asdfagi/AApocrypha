using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace A_Apocrypha.CustomOther
{
    public class CustomAbilityRarity
    {
        public static Dictionary<int, RaritySO> Rarity_DB = [];
        public static Dictionary<int, RaritySO> Rarity_NoReroll_DB = [];

        public static RaritySO Weight(int Value, bool CanBeRerolled = true)
        {
            //Check if we have already created a RaritySO with the desired value.
            if (CanBeRerolled)
            {
                if (Rarity_DB.ContainsKey(Value))
                {
                    Rarity_DB.TryGetValue(Value, out RaritySO rarity);
                    return rarity;
                }
            }
            else
            {
                if (Rarity_NoReroll_DB.ContainsKey(Value))
                {
                    Rarity_NoReroll_DB.TryGetValue(Value, out RaritySO rarity);
                    return rarity;
                }
            }

            //Because we could not give a pre-existing RaritySO we will make a new one.
            RaritySO NewRarity = ScriptableObject.CreateInstance<RaritySO>();
            NewRarity.rarityValue = Value;

            if (CanBeRerolled)
            {
                NewRarity.canBeRerolled = true;
                Rarity_DB.Add(Value, NewRarity);
            }
            else
            {
                NewRarity.canBeRerolled = false;
                Rarity_NoReroll_DB.Add(Value, NewRarity);
            }

            return NewRarity;
        }

        public static Ability[] AutoSprite(Ability[] abils)
        {
            foreach (Ability a in abils)
            {
                a.AbilitySprite = LoadedAssetsHandler.GetEnemyAbility("Bash_A").abilitySprite;
            }

            return abils;
        }
    }
}
