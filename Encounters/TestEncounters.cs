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
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Parabola/Parabola_SmokingShore_CombatEnv.prefab", "ParabolaSmokingShoreCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("Test_Sign", ResourceLoader.LoadSprite("testencountericon", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API(EncounterType.Specific, "H_Zone01_Test_Medium_EnemyBundle", "Test_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/Drowning",
                //RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_ChoirBoy_Easy_EnemyBundle")._roarReference.roarEvent,
                //RoarEvent = LoadedAssetsHandler.GetEnemy("Sepulchre_EN").deathSound,
                RoarEvent = "event:/AAEnemy/Phobias/PhobiasRoar",
                SpecialEnvironmentID = "ParabolaSmokingShoreCombatEnv",
            };
            testMedium.AddSpecialEnvironment("ParabolaSmokingShoreCombatEnv");
            testMedium.CreateNewEnemyEncounterData([
                "Phobia_Phobias_EN",
                "Phobia_Phobias_EN",
            ], [1, 3]);
            //testMedium.SimpleAddEncounter(1, "Threshold_EN");
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
