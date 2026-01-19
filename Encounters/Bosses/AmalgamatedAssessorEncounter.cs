using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters.Bosses
{
    public class AmalgamatedAssessorEncounter
    {
        public static void Add()
        {
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/AssessorCombatEnv.prefab", "AssessorCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("Assessor_Sign", ResourceLoader.LoadSprite("AssessorTimeline", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API assessorBoss = new EnemyEncounter_API(EncounterType.Specific, "AmalgamatedAssessor_BOSS", "Assessor_Sign")
            {
                MusicEvent = "event:/AAMusic/EntityResearchers/Silyeog",
                RoarEvent = "event:/AAEnemy/Assessor/AssessorIntro",
                UsesCustomOverworldRoom = true,
                CustomOverworldRoomID = "SirenBossPortalRoom",
                SpecialEnvironmentID = "AssessorCombatEnv",
                BossID = "AmalgamatedAssessor_BOSS",
            };
            assessorBoss.AddSpecialEnvironment("AssessorCombatEnv");
            assessorBoss.CreateNewEnemyEncounterData([
                "AmalgamatedAssessor_BOSS",
            ], [1]);
            assessorBoss.AddEncounterToDataBases();
            Misc.AddCustom_VSAnimationData("AmalgamatedAssessor_BOSS", new VsBossData
            {
                animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Enemies/Assessor_Boss/VsAssessorAnim.anim"),
                roarTime = 4.00f,
                arenaSprite = ResourceLoader.LoadSprite("AssessorArea", null, 32, null),
                extraArenaSprite = ResourceLoader.LoadSprite("DuneThresherTimelineWhy", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("AssessorSplash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("assessor_nameplate", null, 32, null),
                extraSignatureSprite = ResourceLoader.LoadSprite("DuneThresherTimelineWhy", null, 32, null)
            });
            LoadedDBsHandler._PortalDB.AddBackgroundPortal("AmalgamatedAssessor_BOSS", ResourceLoader.LoadSprite("AssessorPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("AmalgamatedAssessor_BOSS", 10, "TheSiren_Zone1", BundleDifficulty.Boss);
        }
    }
}
