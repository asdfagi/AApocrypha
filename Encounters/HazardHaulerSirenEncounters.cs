using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class HazardHaulerSirenEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("HazardHauler_Sign", ResourceLoader.LoadSprite("HazardHaulerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API hazardHaulerSirenMedium = new EnemyEncounter_API(0, "H_ZoneSiren_HazardHauler_Medium_EnemyBundle", "HazardHauler_Sign")
            {
                MusicEvent = "event:/AAMusic/AnAxe/HarmfulIfInhaled",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "SandSifter_EN",
                    "BirdBath_EN",
                    "BirdBath_EN",
                ], null);
            hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                ], null);
            hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                    "Tassnn_EN",
                ], null);
            hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "HazardHauler_Siren_EN",
                    "BirdBath_EN",
                    "BirdBath_EN",
                ], null);
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                    "Ecstasy_Red_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                if (AApocrypha.CrossMod.EnemyPack)
                {
                    hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                    [
                        "HazardHauler_Siren_EN",
                        "BirdBath_EN",
                        "Erelim_EN",
                    ], null);
                }
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                hazardHaulerSirenMedium.CreateNewEnemyEncounterData(
                [
                    "HazardHauler_Siren_EN",
                    "Boiler_EN",
                    "StarVampire_EN",
                ]);
            }
            hazardHaulerSirenMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_HazardHauler_Medium_EnemyBundle", 6, "TheSiren_Zone1", BundleDifficulty.Medium); //6
        }
    }
}
