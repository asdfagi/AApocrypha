using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class RiftEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Rift_Sign", ResourceLoader.LoadSprite("RiftTimelineOutlined", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_Zone02_Rift_Hard_EnemyBundle", "Rift_Sign")
            {
                MusicEvent = "event:/AAMusic/ExtraStageQuasar",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "RiftMiniboss_EN",
            ], [2]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_Rift_Hard_EnemyBundle", 7, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Hard);
        }
    }
}
