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
                MusicEvent = "event:/AAMusic/DoYouHearGnomes",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Keko/CHR_ENM_Keko_Roar",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
            ], null);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
