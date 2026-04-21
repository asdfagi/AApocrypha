using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TarnishedDivinityEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("Dogma_Sign", ResourceLoader.LoadSprite("DogmaOverworld", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_Zone03_TarnishedDivinity_Hard_EnemyBundle", "Dogma_Sign")
            {
                MusicEvent = "event:/AAMusic/Ridiculon/PulsoProfundum",
                RoarEvent = "event:/AASFX/Nothing_SFX",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "TarnishedDivinity_EN",
            ], [2]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_TarnishedDivinity_Hard_EnemyBundle", 5, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
