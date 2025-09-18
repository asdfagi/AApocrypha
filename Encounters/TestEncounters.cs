using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TestEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Test_Sign", ResourceLoader.LoadSprite("IconCometGaze", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API(0, "H_Zone01_Test_Medium_EnemyBundle", "Test_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-WhimperAndWhine",
                RoarEvent = "event:/Characters/Enemies/WrithingSpoggle/CHR_ENM_WrithingSpoggle_Roar",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "CellularSpoggle_EN",
            ], [2]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
