using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters.Bosses
{
    public class AmdusiasEncounter
    {
        public static void Add()
        {
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/AmdusiasCombatEnv.prefab", "AmdusiasCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("Amdusias_Sign", ResourceLoader.LoadSprite("AmdusiasTimeline", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API amdusiasBoss = new EnemyEncounter_API(EncounterType.Specific, "Amdusias_BOSS", "Amdusias_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/MachineInTheWalls",
                RoarEvent = "event:/AAEnemy/Amdusias/AmdusiasIntro",
                //UsesCustomOverworldRoom = true,
                //CustomOverworldRoomID = "SirenBossbawbaebabarwerwerearewreawrarereabqebqbqebPortalRoom",
                SpecialEnvironmentID = "AmdusiasCombatEnv",
                BossID = "Amdusias_BOSS",
            };
            amdusiasBoss.AddSpecialEnvironment("AmdusiasCombatEnv");
            amdusiasBoss.CreateNewEnemyEncounterData([
                "Amdusias_BOSS",
            ], [2]);
            amdusiasBoss.AddEncounterToDataBases();
            Misc.AddCustom_VSAnimationData("Amdusias_BOSS", new VsBossData
            {
                animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Enemies/Amdusias_Boss/VsAmdusiasAnim.anim"),
                roarTime = 6.25f,
                arenaSprite = ResourceLoader.LoadSprite("AmdusiasArea", null, 32, null),
                extraArenaSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("AmdusiasSplash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("AmdusiasNameplateTall", null, 32, null),
                extraSignatureSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null)
            });
            LoadedDBsHandler._PortalDB.AddBackgroundPortal("Amdusias_BOSS", ResourceLoader.LoadSprite("AmdusiasPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));
            EnemyEncounterUtils.AddEncounterToZoneSelector("Amdusias_BOSS", 10, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Boss);
        }
    }
}
