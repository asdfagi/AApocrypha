using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class SandSifter
    {
        public static void Add()
        {
            Enemy sandsifter = new Enemy("Sand Sifter", "SandSifter_EN")
            {
                Health = 16,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SandSifterDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SandSifterHurt",
                DeathSound = "event:/AAEnemy/SandSifterDeath",
                UnitTypes = ["Robot"],
            };
            sandsifter.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/SandSifter_Enemy/SandSifter_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/SandSifter_Enemy/SandSifter_Giblets.prefab").GetComponent<ParticleSystem>());

            Enemy sandsiftersummon = new Enemy("Sand Sifter", "SandSifterSummon_EN")
            {
                Health = 8,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SandSifterDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SandSifterHurt",
                DeathSound = "event:/AAEnemy/SandSifterDeath",
                UnitTypes = ["Robot"],
            };

            SwapToOneRandomSideXTimesEffect SwapRandomFar = ScriptableObject.CreateInstance<SwapToOneRandomSideXTimesEffect>();

            ForceGenerateRandomManaBetweenEffect WeirdRandomPigmentSimple = ScriptableObject.CreateInstance<ForceGenerateRandomManaBetweenEffect>();
            WeirdRandomPigmentSimple.possibleMana = new ManaColorSO[]
            {
                Pigments.Red,
                Pigments.Red,
                Pigments.Red,
                Pigments.Red,
                Pigments.Red,
                Pigments.Blue,
                Pigments.Blue,
                Pigments.Blue,
                Pigments.Blue,
                Pigments.Purple,
                Pigments.Purple,
                Pigments.Yellow,
                Pigments.Yellow,
                Pigments.Grey
            };

            ForceGenerateRandomManaBetweenEffect WeirdRandomPigmentSplit = ScriptableObject.CreateInstance<ForceGenerateRandomManaBetweenEffect>();
            WeirdRandomPigmentSplit.possibleMana = new ManaColorSO[]
            {
                Pigments.RedBlue,
                Pigments.BlueRed,
                Pigments.RedBlue,
                Pigments.BlueRed,
                Pigments.RedYellow,
                Pigments.RedYellow,
                Pigments.BlueYellow,
                Pigments.RedPurple,
                Pigments.RedPurple,
                Pigments.BluePurple,
                Pigments.YellowPurple,
                Pigments.PurpleYellow,
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Yellow
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Blue,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Red,
                    Pigments.Blue,
                    Pigments.Yellow,
                    Pigments.Purple
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Grey,
                    Pigments.Green
                }),
                Pigments.SplitPigment(new ManaColorSO[]
                {
                    Pigments.Green,
                    Pigments.Grey
                }),
            };

            GenerateTargetHealthColorEffect PigmentByTargetHealth = ScriptableObject.CreateInstance<GenerateTargetHealthColorEffect>();

            Ability surfacesample = new Ability("Surface Sample", "AApocrypha_SurfaceSample_A")
            {
                Description = "Produce 2 Pigment of random colours, then move this enemy to the Left twice or to the Right twice.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(WeirdRandomPigmentSimple, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            surfacesample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            surfacesample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            surfacesample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability coresample = new Ability("Core Sample", "AApocrypha_CoreSample_A")
            {
                Description = "Produce 3 Pigment of random split colours, then move this enemy to the Left or Right.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Bosch,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(WeirdRandomPigmentSplit, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(SwapRandomFar, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            coresample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            coresample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability bloodsample = new Ability("Blood Sample", "AApocrypha_BloodSample_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member, then produce 1 Pigment of their health colour and move this enemy to the Left twice or to the Right twice.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Absolve,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(PigmentByTargetHealth, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            bloodsample.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            bloodsample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);
            bloodsample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            bloodsample.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            sandsifter.AddEnemyAbilities(
            [
                surfacesample.GenerateEnemyAbility(true),
                coresample.GenerateEnemyAbility(true),
                bloodsample.GenerateEnemyAbility(true),
            ]);

            sandsiftersummon.AddEnemyAbilities(
            [
                surfacesample.GenerateEnemyAbility(true),
                coresample.GenerateEnemyAbility(true),
                bloodsample.GenerateEnemyAbility(true),
            ]);

            sandsifter.AddPassives([]);
            sandsifter.AddEnemy(true, true, true);

            sandsiftersummon.AddPassives([Passives.Withering]);
            sandsiftersummon.AddEnemy(false, false, false);

            LoadedAssetsHandler.GetEnemy("SandSifterSummon_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("SandSifter_EN").enemyTemplate;
        }
    }
}
