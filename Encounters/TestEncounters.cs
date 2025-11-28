using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TestEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Test_Sign", ResourceLoader.LoadSprite("testencountericon", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_Zone01_Test_Medium_EnemyBundle", "Test_Sign")
            {
                MusicEvent = "event:/AAMusic/BelowZion",
                RoarEvent = "event:/AAEnemy/SistersRoar",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "SomeoneSister_EN",
                "NooneSister_EN",
            ], [1, 3]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
