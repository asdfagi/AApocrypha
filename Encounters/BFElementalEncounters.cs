using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BFElementalEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("BFElementalSign", ResourceLoader.LoadSprite("BFElementalTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API brainEasy = new EnemyEncounter_API(0, Abyss.H.BFElemental.Easy, "BFElementalSign")
                {
                    MusicEvent = "event:/AAMusic/VVVVVV/PressureCooker",
                    RoarEvent = "event:/AAEnemy/BFElemental/BFElementalRoar",
                };
                brainEasy.SimpleAddEncounter(2, "BFElemental_EN");
                brainEasy.SimpleAddEncounter(1, "BFElemental_EN", 1, "BasicElemental_EN");
                brainEasy.SimpleAddEncounter(1, "BFElemental_EN", 1, "WanderFellow_EN");
                brainEasy.SimpleAddEncounter(1, "BFElemental_EN", 2, "Streetlight_EN");
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    brainEasy.SimpleAddEncounter(1, "BFElemental_EN", 1, "EyePalm_EN");
                    brainEasy.SimpleAddEncounter(1, "BFElemental_EN", 1, "AbandonedPuppet_EN");
                }
                brainEasy.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.BFElemental.Easy, 5, "TheAbyss_Zone3", BundleDifficulty.Easy);

                EnemyEncounter_API brainMed = new EnemyEncounter_API(0, Abyss.H.BFElemental.Med, "BFElementalSign")
                {
                    MusicEvent = "event:/AAMusic/VVVVVV/PressureCooker",
                    RoarEvent = "event:/AAEnemy/BFElemental/BFElementalRoar",
                };
                brainMed.SimpleAddEncounter(2, "BFElemental_EN", 1, "BasicElemental_EN");
                brainMed.SimpleAddEncounter(2, "BFElemental_EN", 1, "YesMan_EN");
                brainMed.SimpleAddEncounter(1, "BFElemental_EN", 1, "WRK_EN", 2, "Streetlight_EN");
                brainMed.SimpleAddEncounter(1, "BFElemental_EN", 2, "Wug_EN");
                brainMed.SimpleAddEncounter(1, "BFElemental_EN", 1, "WanderFellow_EN", 1, Jumble.Entropic);
                brainMed.SimpleAddEncounter(1, "BFElemental_EN", 1, "WanderFellow_EN", 1, Jumble.Clusterfuck);
                brainMed.SimpleAddEncounter(1, "BFElemental_EN", 1, "WanderFellow_EN", 1, Spoggle.Entropic, 1, "Crossword_EN");
                brainMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.BFElemental.Med, 8, "TheAbyss_Zone3", BundleDifficulty.Medium);
            } 
        }
    }
}
