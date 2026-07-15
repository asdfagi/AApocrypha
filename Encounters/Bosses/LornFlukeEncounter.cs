using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters.Bosses
{
    public class LornFlukeEncounter
    {
        public static void Add()
        {
            if (!LoadedDBsHandler._StatusFieldDB._StatusEffects.ContainsKey("Weakness_ID")) { return; }

            //EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/AmdusiasCombatEnv.prefab", "AmdusiasCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("LornFluke_Sign", ResourceLoader.LoadSprite("LornFlukeTimeline", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API flukeBoss = new EnemyEncounter_API(EncounterType.Specific, "LornFluke_BOSS", "LornFluke_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/AboutYourBusiness",
                RoarEvent = "event:/AAEnemy/IntroPlaceholder",//"event:/AAEnemy/Amdusias/AmdusiasIntro",
                //UsesCustomOverworldRoom = true,
                //CustomOverworldRoomID = "SirenBossbawbaebawrareebqbqebPortalRoom",
                //SpecialEnvironmentID = "AmdusiasCombatEnv",
                BossID = "LornFluke_BOSS",
            };
            flukeBoss.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone02_Ouroboros_EnemyBundle")._specialCombatEnvironment;
            flukeBoss.UsesSpecialEnvironment = true;
            flukeBoss.CreateNewEnemyEncounterData([
                "LornFluke_BOSS",
            ], [1]);
            flukeBoss.AddEncounterToDataBases();
            Debug.Log("Lorn Fluke loading | " + BossType_GameIDs.TheOuroboros.ToString());
            LoadedDBsHandler.VSAnimDB.m_VSData.TryGetValue(BossType_GameIDs.TheOuroboros.ToString(), out var ouroborosVSData);
            Misc.AddCustom_VSAnimationData("LornFluke_BOSS", new VsBossData
            {
                animation = AApocrypha.assetBundle.LoadAsset<AnimationClip>("Assets/Apocrypha_Enemies/LornFluke_Boss/VsLornFlukeAnim.anim"),
                roarTime = 6.25f,
                arenaSprite = ouroborosVSData.arenaSprite,//ResourceLoader.LoadSprite("AmdusiasArea", null, 32, null),
                extraArenaSprite = ResourceLoader.LoadSprite("LornFlukeTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("LornFlukeSplash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("LornFlukeNameplate", null, 32, null),
                extraSignatureSprite = ResourceLoader.LoadSprite("LornFlukeTimeline", null, 32, null)
            });
            LoadedDBsHandler._PortalDB.AddBackgroundPortal("LornFluke_BOSS", ResourceLoader.LoadSprite("LornFlukePortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));
            EnemyEncounterUtils.AddEncounterToZoneSelector("LornFluke_BOSS", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Boss);
        }
    }
}
