using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BloatfingerEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Bloatfinger_Sign", ResourceLoader.LoadSprite("BloatfingerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API bloatfingerMedium = new EnemyEncounter_API(0, "H_Zone02_Bloatfinger_Medium_EnemyBundle", "Bloatfinger_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/WhyWeWearFaces",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "MusicMan_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "JumbleGuts_Clotted_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "JumbleGuts_Waning_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "JumbleGuts_Hollowing_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "SculptorBirdSculpture_EN",
                    "JumbleGuts_Flummoxing_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "JumbleGuts_Clotted_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "JumbleGuts_Waning_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "JumbleGuts_Hollowing_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "JumbleGuts_Flummoxing_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "Bloatfinger_EN",
                    "Scrungie_EN",
                ], null);
            bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                    "SilverSuckle_EN",
                ], null);
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                bloatfingerMedium.CreateNewEnemyEncounterData(
                [
                    "Bloatfinger_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "CoruscatingJumbleGuts_EN",
                ], null);
            }
            bloatfingerMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_Bloatfinger_Medium_EnemyBundle", 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}
