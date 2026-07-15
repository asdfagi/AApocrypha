using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine.Tilemaps;
using static A_Apocrypha.Enemies.Bloatfinger;

namespace A_Apocrypha.Enemies
{
    public class Exuviae
    {
        public static void Add()
        {
            Enemy exuvia = new Enemy("Discarded Exuviae", "DiscardedExuviae_EN")
            {
                Health = 5,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TeneralNymphOverworld", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("TeneralNymphOverworld", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TeneralNymphOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                UnitTypes = ["Friendly"],
            };
            exuvia.AddPassives([Passives.Slippery, Passives.Withering, Passives.GetCustomPassive("AA_NymphExuviaHandler_PA")]);
            exuvia.AddEnemy(false, false, false);
            LoadedAssetsHandler.GetEnemy("DiscardedExuviae_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("TaintedYolk_EN").enemyTemplate;
        }
    }
}
