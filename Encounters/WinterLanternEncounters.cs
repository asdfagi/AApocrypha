using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class WinterLanternEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("WinterLantern_Sign", ResourceLoader.LoadSprite("WinterLanternTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API winterLanternMedium = new EnemyEncounter_API(0, "H_ZoneSiren_WinterLantern_Medium_EnemyBundle", "WinterLantern_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/MartyrsTribunal",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "BirdBath_EN",
                    "Boiler_EN",
                ], null);
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "BirdBath_EN",
                    "BirdBath_EN",
                ], null);
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "Tumult_EN",
                    "TumultShell_EN",
                ], null);
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "Olmic_EN",
                ], null);
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "BirdBath_EN",
                    "Tassnn_EN",
                ], null);
            winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "BirdBath_EN",
                    "PetrifiedPuker_EN",
                    "PetrifiedPuker_EN",
                ], null);
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "BirdBath_EN",
                    "Euryale_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                winterLanternMedium.CreateNewEnemyEncounterData(
                [
                    "WinterLantern_EN",
                    "Orphan_EN",
                    "Orphan_EN",
                ], null);
            }
            winterLanternMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_WinterLantern_Medium_EnemyBundle", 8, "TheSiren_Zone1", BundleDifficulty.Medium);
        }
    }
}
