using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Aggregate
    {
        public static void Add()
        {
            DamageWithPigmentRepetitionsEffect DamageRepeat3HealthColor = ScriptableObject.CreateInstance<DamageWithPigmentRepetitionsEffect>();
            DamageRepeat3HealthColor._threshold = 3;
            DamageRepeat3HealthColor._contains = true;
            DamageRepeat3HealthColor._indirect = false;
            DamageRepeat3HealthColor._usePreviousExitValue = false;
            DamageRepeat3HealthColor._ignoreShield = false;
            DamageRepeat3HealthColor._useCasterHealthColor = true;
            DamageRepeat3HealthColor._visuals = Visuals.Exsanguinate;

            Ability raspT1 = new Ability("Rasp", "AApocrypha_AggregateRasp_A")
            {
                Description = "Deal a Barely Painful amount of damage to the Opposing party member. Repeat this once for every 3 pigment in the pigment tray containing this enemy's health color.",
                Cost = [],
                Visuals = Visuals.MotherlyLove,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DamageRepeat3HealthColor, 3, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            raspT1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), "AA_MultiX", nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability raspT2 = new Ability("Abrade", "AApocrypha_AggregateAbrade_A")
            {
                Description = "Deal a Painful amount of damage to the Opposing party member. Repeat this once for every 3 pigment in the pigment tray containing this enemy's health color.",
                Cost = [],
                Visuals = Visuals.MotherlyLove,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DamageRepeat3HealthColor, 5, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            raspT2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), "AA_MultiX", nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability raspT3 = new Ability("Disintegrate", "AApocrypha_AggregateDisintegrate_A")
            {
                Description = "Deal an Agonizing amount of damage to the Opposing party member. Repeat this once for every 3 pigment in the pigment tray containing this enemy's health color.",
                Cost = [],
                Visuals = Visuals.MotherlyLove,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(DamageRepeat3HealthColor, 7, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            raspT3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), "AA_MultiX", nameof(IntentType_GameIDs.Mana_Modify)]);

            Ability spoilT1 = new Ability("Spoil", "AApocrypha_AggregateSpoil_A")
            {
                Description = "Fully convert 3 random pigment in the pigment tray containing this enemy's health color. Split this enemy's health color into 3 random pigment in the pigment tray.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PurifyNumberPigmentCasterHealthColorEffect>(), 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PutrefyNumberPigmentCasterHealthColorEffect>(), 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            spoilT1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform", "AA_Pigment_Transform"]);

            Ability spoilT2 = new Ability("Fester", "AApocrypha_AggregateFester_A")
            {
                Description = "Fully convert 5 random pigment in the pigment tray containing this enemy's health color. Split this enemy's health color into 5 random pigment in the pigment tray.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PurifyNumberPigmentCasterHealthColorEffect>(), 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PutrefyNumberPigmentCasterHealthColorEffect>(), 5, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            spoilT2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform", "AA_Pigment_Transform"]);

            Ability spoilT3 = new Ability("Putrefy", "AApocrypha_AggregatePutrefy_A")
            {
                Description = "Fully convert all pigment in the pigment tray containing this enemy's health color. Split this enemy's health color into all pigment in the pigment tray.",
                Cost = [],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PurifyNumberPigmentCasterHealthColorEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PutrefyNumberPigmentCasterHealthColorEffect>(), 10, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            spoilT3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Pigment_Transform", "AA_Pigment_Transform"]);

            Enemy redMold = new Enemy("Vicious Aggregate", "RedAggregate_EN")
            {
                Health = 14,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AggregateRedTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AggregateRedDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AggregateRedTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            redMold.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateRed_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateRed_Giblets.prefab").GetComponent<ParticleSystem>());
            redMold.AddPassives([Passives.Pure, Passives.GetCustomPassive("AA_Contaminant1_PA"), Passives.GetCustomPassive("Confrontational_PA")]);

            DamageWithPigmentBonusEffect damageHealthBonus = ScriptableObject.CreateInstance<DamageWithPigmentBonusEffect>();
            damageHealthBonus._contains = true;
            damageHealthBonus._cap = 10;

            StatusEffect_ApplyByPrevious_Effect Bleed = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            Bleed._Status = StatusField.Ruptured;
            Bleed._entryVariableAsPercentage = true;

            Ability redSpines = new Ability("Eviscerating Spines", "AApocrypha_AggregateRedSpines_A")
            {
                Description = "Deal damage to the Opposing party member equal to the amount of pigment in the pigment tray containing this enemy's health color. Apply half the amount of damage dealt as Ruptured to the Opposing party member.",
                Cost = [],
                Visuals = Visuals.Shank,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damageHealthBonus, 0, Targeting.Slot_Front),
                    Effects.GenerateEffect(Bleed, 50, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            redSpines.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), "AA_Damage_Prop", nameof(IntentType_GameIDs.Status_Ruptured)]);

            redMold.AddEnemyAbilities([
                raspT1,
                spoilT1,
                redSpines,
            ]);
            redMold.AddEnemy(true, true, false);

            Enemy purpleMold = new Enemy("Mysterious Aggregate", "PurpleAggregate_EN")
            {
                Health = 14,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AggregatePurpleTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AggregatePurpleDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AggregatePurpleTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            purpleMold.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregatePurple_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregatePurple_Giblets.prefab").GetComponent<ParticleSystem>());
            purpleMold.AddPassives([Passives.Pure, Passives.GetCustomPassive("AA_Contaminant1_PA"), Passives.GetCustomPassive("Confrontational_PA")]);

            StatusEffect_ApplyWithRandomDistribution_Effect HexedSpread = ScriptableObject.CreateInstance<StatusEffect_ApplyWithRandomDistribution_Effect>();
            HexedSpread.usePrevious = true;
            HexedSpread.previousIsRange = false;
            HexedSpread.status = StatusField.GetCustomStatusEffect("Hexed_ID");

            PigmentAmountCheckEffect CasterHealthCheck = ScriptableObject.CreateInstance<PigmentAmountCheckEffect>();
            CasterHealthCheck._useCasterHealthColor = true;
            CasterHealthCheck._contains = true;

            Ability purpleWhispers = new Ability("Wicked Whispers", "AApocrypha_AggregatePurpleWhispers_A")
            {
                Description = "Randomly distribute Hexed to all party members equal to the amount of pigment in the pigment tray containing this enemy's health color.",
                Cost = [],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Unit_AllOpponents,
                Effects =
                [
                    Effects.GenerateEffect(CasterHealthCheck, 0),
                    Effects.GenerateEffect(HexedSpread, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            purpleWhispers.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Misc), "Status_Hexed"]);

            purpleMold.AddEnemyAbilities([
                raspT1,
                spoilT1,
                purpleWhispers,
            ]);
            purpleMold.AddEnemy(true, true, false);
            //LoadedAssetsHandler.GetEnemy("PurpleAggregate_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Macerator_EN").enemyTemplate;

            Enemy blueMold = new Enemy("Dolorous Aggregate", "BlueAggregate_EN")
            {
                Health = 26,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AggregateBlueTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AggregateBlueDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AggregateBlueTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            blueMold.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateBlue_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateBlue_Giblets.prefab").GetComponent<ParticleSystem>());
            blueMold.AddPassives([Passives.Pure, Passives.GetCustomPassive("AA_Contaminant1_PA"), Passives.GetCustomPassive("Confrontational_PA")]);

            SpecificAlliesByHealthColorTargeting TheBlues = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            TheBlues.slotOffsets = [0];
            TheBlues.targetUnitAllySlots = false;
            TheBlues._color = Pigments.Blue;
            TheBlues._contains = true;
            TheBlues.getAllUnitSelfSlots = false;
            TheBlues._excludeCaster = true;

            SpecificOpponentsByHealthColorTargeting OtherBlues = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorTargeting>();
            OtherBlues.slotOffsets = [0];
            OtherBlues.targetUnitAllySlots = false;
            OtherBlues._color = Pigments.Blue;
            OtherBlues._contains = true;
            OtherBlues.getAllUnitSelfSlots = false;

            SpecificAlliesByHealthColorTargeting TheGraynt = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            TheGraynt.slotOffsets = [0];
            TheGraynt.targetUnitAllySlots = false;
            TheGraynt._color = Pigments.Blue;
            TheGraynt._contains = true;
            TheGraynt.getAllUnitSelfSlots = false;
            TheGraynt.blacklist = true;

            SpecificOpponentsByHealthColorTargeting OtherGraynt = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorTargeting>();
            OtherGraynt.slotOffsets = [0];
            OtherGraynt.targetUnitAllySlots = false;
            OtherGraynt._color = Pigments.Blue;
            OtherGraynt._contains = true;
            OtherGraynt.getAllUnitSelfSlots = false;
            OtherGraynt.blacklist = true;

            TargetSplitOrReplaceHealthEffect bluify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            bluify._color = Pigments.Blue;
            bluify._colorBlacklist = [Pigments.Grey];
            bluify._transformBlacklist = false;

            RandomTargetPerformEffectViaSubaction bluifyAction = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            bluifyAction.effects = [Effects.GenerateEffect(bluify, 1, Targeting.Slot_SelfSlot)];

            Ability blueRiver = new Ability("River Of Tears", "AApocrypha_AggregateBlueRiver_A")
            {
                Description = "Make all other units with blue-containing health generate 1 pigment of their health color." +
                "\nSplit blue into the health color of a random non-grey party member and a random other non-grey enemy.",
                Cost = [],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 1, OtherBlues),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateHealthColorManaPerTargetEffect>(), 1, TheBlues),
                    Effects.GenerateEffect(bluifyAction, 1, OtherGraynt),
                    Effects.GenerateEffect(bluifyAction, 1, TheGraynt),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            blueRiver.AddIntentsToTarget(Targeting.Unit_OtherAllies, [nameof(IntentType_GameIDs.Mana_Modify)]);
            blueRiver.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Mana_Modify)]);

            blueMold.AddEnemyAbilities([
                raspT2,
                spoilT2,
                blueRiver,
            ]);
            blueMold.AddEnemy(true, true, false);

            Enemy yellowMold = new Enemy("Oleaginous Aggregate", "YellowAggregate_EN")
            {
                Health = 26,
                HealthColor = Pigments.Yellow,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AggregateYellowTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AggregateYellowDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AggregateYellowTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            yellowMold.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateYellow_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Aggregate_Enemy/AggregateYellow_Giblets.prefab").GetComponent<ParticleSystem>());
            yellowMold.AddPassives([Passives.Pure, Passives.GetCustomPassive("AA_Contaminant1_PA"), Passives.GetCustomPassive("Confrontational_PA")]);

            DamageWithStatusBonusEffect OilBonusDamage = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            OilBonusDamage._status = StatusField.OilSlicked;
            OilBonusDamage._bonusAmount = 1;
            OilBonusDamage._bonusStacking = true;
            OilBonusDamage._indirect = false;

            StatusEffect_ApplyWithRandomDistribution_Effect Oiling = ScriptableObject.CreateInstance<StatusEffect_ApplyWithRandomDistribution_Effect>();
            Oiling.usePrevious = true;
            Oiling.previousIsRange = false;
            Oiling.status = StatusField.OilSlicked;

            PerformEffectViaSubaction OilDamageAction = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            OilDamageAction.effects = [
                    Effects.GenerateEffect(OilBonusDamage, 4, Targeting.Slot_Front),
            ];

            Ability yellowOil = new Ability("Oil Of Vitriol", "AApocrypha_AggregateYellowOil_A")
            {
                Description = "Randomly distribute Oil Slicked to the Left, Right and Opposing party members equal to the amount of pigment in the pigment tray containing this enemy's health color." +
                "\nDeal a Painful amount of damage to the Opposing party member, increased by the amount of Oil Slicked they have.",
                Cost = [],
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(CasterHealthCheck, 0),
                    Effects.GenerateEffect(Oiling, 1, Targeting.Slot_FrontAndSides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilDamageAction, 0),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            yellowOil.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Status_OilSlicked)]);
            yellowOil.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_3_6)]);

            yellowMold.AddEnemyAbilities([
                raspT2,
                spoilT2,
                yellowOil,
            ]);
            yellowMold.AddEnemy(true, true, false);
        }
    }
}
