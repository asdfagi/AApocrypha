using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TestEncounters
    {
        public static void Add()
        {
            Debug.LogWarning("Encounters | Warning! TestEncounters.cs is enabled!");
            Portals.AddPortalSign("Test_Sign", ResourceLoader.LoadSprite("testencountericon", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_Zone01_Test_Medium_EnemyBundle", "Test_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_ChoirBoy_Easy_EnemyBundle")._roarReference.roarEvent,
                //RoarEvent = LoadedAssetsHandler.GetEnemy("Sepulchre_EN").deathSound,
                //RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
            ], [2]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
