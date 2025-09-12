using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SimulacrumEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Simulacrum_Sign", ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API simulacrumMedium = new EnemyEncounter_API(0, "H_Zone03_Simulacrum_Medium_EnemyBundle", "Simulacrum_Sign")
            {
                MusicEvent = "event:/AAMusic/Homunculus",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            simulacrumMedium.CreateNewEnemyEncounterData(
            [
                "Simulacrum_EN",
            ], null);
            simulacrumMedium.CreateNewEnemyEncounterData(
            [
                "Simulacrum_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            simulacrumMedium.CreateNewEnemyEncounterData(
            [
                "Simulacrum_EN",
                "InHisImage_EN",
            ], null);
            simulacrumMedium.CreateNewEnemyEncounterData(
            [
                "Simulacrum_EN",
                "InHerImage_EN",
            ], null);
            simulacrumMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_Simulacrum_Medium_EnemyBundle", 9999, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
        }
    }
}
