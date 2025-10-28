using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class CustomJumbleGuts
    {
        public static void Add()
        {
            GenerateCasterHealthManaEffect PigmentHealth = ScriptableObject.CreateInstance<GenerateCasterHealthManaEffect>();

            Ability boil = new Ability("Boil", "AApocrypha_JumbleBoil_A")
            {
                Description = "Produces 1 Pigment of this enemy's health colour.\nDeals a Painful amount of damage to the Opposing party member.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(PigmentHealth, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            boil.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            boil.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability scald = new Ability("Scald", "AApocrypha_JumbleScald_A")
            {
                Description = "Produces 1 Pigment of this enemy's health colour.\nDeals an Agonizing amount of damage to the Opposing party member.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(PigmentHealth, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            scald.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            scald.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            Ability flood = new Ability("Flood", "AApocrypha_JumbleFlood_A")
            {
                Description = "Vomits and produces 3 Pigment of this enemy's health colour.",
                Cost = [],
                Visuals = Visuals.Puke,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(PigmentHealth, 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            flood.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            Enemy testJumble = new Enemy("Testing Jumble Guts", "TestJumbleGuts_EN")
            {
                Health = 11,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DevotedSpoggleDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Clotted_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Clotted_EN").deathSound,
            };
            testJumble.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TestJumbleGuts_Enemy/TestJumbleGuts_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/TestJumbleGuts_Enemy/TestJumbleGuts_Giblets.prefab").GetComponent<ParticleSystem>());
            testJumble.AddPassives([Passives.Pure, Passives.Transfusion, Passives.Slippery]);

            testJumble.AddEnemyAbilities(
                [
                    boil,
                    flood,
                ]);
            testJumble.AddEnemy(false, false, false);

            Enemy testJumble2 = new Enemy("Testing2 Jumble Guts", "TestJumbleGuts2_EN")
            {
                Health = 11,
                HealthColor = Pigments.SplitPigment([Pigments.Grey, Pigments.Purple]),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DevotedSpoggleDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Clotted_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Clotted_EN").deathSound,
            };
            testJumble2.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TestJumbleGuts_Enemy/TestJumbleGuts2_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/TestJumbleGuts_Enemy/TestJumbleGuts2_Giblets.prefab").GetComponent<ParticleSystem>());
            testJumble2.AddPassives([Passives.Pure, Passives.Transfusion, Passives.Slippery]);

            testJumble2.AddEnemyAbilities(
                [
                    boil,
                    flood,
                ]);
            testJumble2.AddEnemy(false, false, false);

            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                RainbowRefractionEffect RefractionEffect = ScriptableObject.CreateInstance<RainbowRefractionEffect>();
                RefractionEffect.manas = [
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Yellow,
                    Pigments.Purple,
                ];

                GenerateHealthColorManaPerTargetEffect PigmentSpam = ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>();

                Enemy rainbowGuts = new Enemy("Coruscating Jumble Guts", "CoruscatingJumbleGuts_EN")
                {
                    Health = 22,
                    HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Rainbow"),
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("RainbowGutsTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("RainbowGutsDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("RainbowGutsTimeline", new Vector2(0.5f, 0f), 32),
                    DamageSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Flummoxing_EN").damageSound,
                    DeathSound = LoadedAssetsHandler.GetEnemy("JumbleGuts_Flummoxing_EN").deathSound,
                };
                rainbowGuts.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/RainbowGuts_Enemy/RainbowGuts_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/RainbowGuts_Enemy/RainbowGuts_Giblets.prefab").GetComponent<ParticleSystem>());
                rainbowGuts.AddPassives([Passives.Pure, Passives.Transfusion, Passives.Slippery, Passives.LeakyGenerator(2)]);

                Ability refraction = new Ability("Prismatic Refraction", "AApocrypha_PrismaticRefraction_A")
                {
                    Description = "Split the health of All non-grey, non-rainbow units with a random new colour, then make All other units generate 1 Pigment of their health colour.\nIf any unit has 4 or more unique health colours, change its health colour to rainbow.\n(Unusual pigments from other mods may also remain unchanged.)",
                    Cost = [],
                    Visuals = CustomVisuals.StaticColorVisualsSO,
                    AnimationTarget = Targeting.AllUnits,
                    Effects =
                    [
                        Effects.GenerateEffect(RefractionEffect, 4, Targeting.Unit_OtherAllies),
                        Effects.GenerateEffect(RefractionEffect, 4, Targeting.Unit_AllOpponents),
                        Effects.GenerateEffect(PigmentSpam, 1, Targeting.Unit_OtherAllies),
                        Effects.GenerateEffect(PigmentSpam, 1, Targeting.Unit_AllOpponents),
                ],
                    Rarity = Rarity.Common,
                    Priority = Priority.Normal,
                };
                refraction.AddIntentsToTarget(Targeting.Unit_OtherAllies, ["AA_Pigment_Transform"]);
                refraction.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["AA_Pigment_Transform"]);
                refraction.AddIntentsToTarget(Targeting.Unit_OtherAllies, [nameof(IntentType_GameIDs.Mana_Generate)]);
                refraction.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Mana_Generate)]);

                rainbowGuts.AddEnemyAbilities(
                    [
                        scald,
                        flood,
                        refraction,
                    ]);
                rainbowGuts.AddEnemy(true, true, true);
            }
        }
    }
}
