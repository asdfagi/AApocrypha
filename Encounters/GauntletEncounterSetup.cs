using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class GauntletEncounterSetup
    {
        public static void Add()
        {
            SetCasterAnimationParameterEffect Easy = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Easy._parameterName = "EncounterDifficulty";
            Easy._parameterValue = 1;

            SetCasterAnimationParameterEffect Medium = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Medium._parameterName = "EncounterDifficulty";
            Medium._parameterValue = 2;

            SetCasterAnimationParameterEffect Hard = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Hard._parameterName = "EncounterDifficulty";
            Hard._parameterValue = 3;

            SetCasterAnimationParameterEffect Shore = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Shore._parameterName = "EncounterArea";
            Shore._parameterValue = 1;

            SetCasterAnimationParameterEffect Orpheum = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Orpheum._parameterName = "EncounterArea";
            Orpheum._parameterValue = 2;

            SetCasterAnimationParameterEffect Garden = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Garden._parameterName = "EncounterArea";
            Garden._parameterValue = 3;

            SetCasterAnimationParameterEffect Money = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            Money._parameterName = "Variant";
            Money._parameterValue = 1;

            SetCasterAnimationParameterEffect ItemS = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            ItemS._parameterName = "Variant";
            ItemS._parameterValue = 2;

            SetCasterAnimationParameterEffect ItemT = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            ItemT._parameterName = "Variant";
            ItemT._parameterValue = 3;

            Enemy testEnabler = new Enemy("Kill to Enable Hologram", "G_TestEnabler_EN")
            {
                Health = 1,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("MaceratorDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AASFX/Nothing_SFX",
                DeathSound = "event:/AASFX/Nothing_SFX",
                CombatEnterEffects = [Effects.GenerateEffect(Easy), Effects.GenerateEffect(Shore)],
                CombatExitEffects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<ActivateGauntletHoloEffect>())],
            };
            testEnabler.PrepareEnemyPrefab("Assets/Gauntlet/EncounterSelection_Enemy.prefab", AApocrypha.assetBundle, null);
            testEnabler.AddPassives([]);

            testEnabler.AddEnemy(false, false, false);

            Enemy testDisabler = new Enemy("Kill to Disable Hologram", "G_TestDisabler_EN")
            {
                Health = 1,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("MaceratorDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AASFX/Nothing_SFX",
                DeathSound = "event:/AASFX/Nothing_SFX",
                CombatEnterEffects = [Effects.GenerateEffect(Medium), Effects.GenerateEffect(Garden)],
                CombatExitEffects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<ActivateGauntletBunkerEffect>())],
            };
            testDisabler.PrepareEnemyPrefab("Assets/Gauntlet/EncounterSelection_Enemy.prefab", AApocrypha.assetBundle, null);
            testDisabler.AddPassives([]);

            testDisabler.AddEnemy(false, false, false);

            Enemy testFace = new Enemy("Kill to Happy", "G_TestHappy_EN")
            {
                Health = 1,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("MaceratorDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AASFX/Nothing_SFX",
                DeathSound = "event:/AASFX/Nothing_SFX",
                CombatEnterEffects = [Effects.GenerateEffect(Money)],
                CombatExitEffects = [Effects.GenerateEffect(ScriptableObject.CreateInstance<AdjustGauntletEmotionEffect>(), 1)],
            };
            testFace.PrepareEnemyPrefab("Assets/Gauntlet/RewardSelection_Enemy.prefab", AApocrypha.assetBundle, null);
            testFace.AddPassives([]);

            testFace.AddEnemy(false, false, false);

            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Gauntlet/GauntletCalmEnv.prefab", "GauntletCalmEnv", AApocrypha.assetBundle);
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Gauntlet/GauntletCombatEnv.prefab", "GauntletCombatEnv", AApocrypha.assetBundle);
            Portals.AddPortalSign("GauntletSimulation_Sign", ResourceLoader.LoadSprite("GauntletIcon", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API gauntletTest = new EnemyEncounter_API(EncounterType.Specific, "H_Zone01_Gauntlet_EnemyBundle", "GauntletSimulation_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/FalsePastCombatMix",
                RoarEvent = "event:/AASFX/Nothing_SFX",
            };
            gauntletTest.AddSpecialEnvironment("GauntletCalmEnv");
            gauntletTest.CreateNewEnemyEncounterData([
                "G_TestEnabler_EN",
                "G_TestDisabler_EN",
                "G_TestHappy_EN",
            ], [0, 4, 2]);
            gauntletTest.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Gauntlet_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
