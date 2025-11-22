using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SculptorBirdSirenEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SirenBird_Sign", ResourceLoader.LoadSprite("SirenBirdTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sirenBirdMedium = new EnemyEncounter_API(0, "H_ZoneSiren_SculptorBird_Medium_EnemyBundle", "SirenBird_Sign")
            {
                MusicEvent = "event:/AAMusic/DepressionShop",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Scrungie/CHR_ENM_Scrungie_Roar",
            };
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                    "Boiler_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "BirdBath_EN",
                    "Tumult_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Tumult_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Tumult_EN",
                    "TumultShell_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "JumbleGuts_Hollowing_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "PetrifiedPuker_EN",
                ], null);
            sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "BirdBath_EN",
                    "Tassnn_EN",
                ], null);
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                    "OneShooter_EN",
                ], null);
                sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Tassnn_EN",
                    "OneShooter_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "Euryale_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "BirdBath_EN",
                    "StarVampire_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sirenBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBirdSiren_EN",
                    "Boiler_EN",
                    "BirdBath_EN",
                    "Ecstasy_Yellow_EN",
                ], null);
            }
            sirenBirdMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_SculptorBird_Medium_EnemyBundle", 8, "TheSiren_Zone1", BundleDifficulty.Medium);
        }
    }
}
