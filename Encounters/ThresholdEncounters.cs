using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ThresholdEncounters
    {
        public static void Add()
        {
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/ThresholdCombatEnv.prefab", "ThresholdCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("Threshold_Sign", ResourceLoader.LoadSprite("ThresholdGateTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API thresholdHard = new EnemyEncounter_API(EncounterType.Specific, "H_ZoneSiren_Threshold_Hard_EnemyBundle", "Threshold_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/DeanimusThreshold",
                RoarEvent = "event:/AASFX/Nothing_SFX",
                SpecialEnvironmentID = "ThresholdCombatEnv",
            };
            thresholdHard.AddSpecialEnvironment("ThresholdCombatEnv");
            thresholdHard.CreateNewEnemyEncounterData([
                "ThresholdGate_EN",
            ], [2]);
            thresholdHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_Threshold_Hard_EnemyBundle", 4, "TheSiren_Zone1", BundleDifficulty.Hard);
        }
    }
}
