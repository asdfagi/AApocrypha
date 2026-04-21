using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies.Bosses
{
    public class Amdusias
    {
        public static void Add()
        {
            Enemy amdusias = new Enemy("Amdusias", "Amdusias_BOSS")
            {
                Health = 60,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AmdusiasTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AcolyteDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Combat/StatusEffects/SE_Divine_Trg",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                UnitTypes = ["Anomaly"],
            };
            amdusias.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Amdusias_Boss/Amdusias_Boss.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Acolyte_Enemy/Acolyte_Giblets.prefab").GetComponent<ParticleSystem>());
            amdusias.AddPassives([Passives.GetCustomPassive("Shy_PA"), Passives.GetCustomPassive("Zelator_PA")]);
            
            Enemy acolyte = new Enemy("Acolyte", "AcolyteMinion_EN")
            {
                Health = 12,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AmdusiasAcolyteTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AcolyteDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AcolyteTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Combat/StatusEffects/SE_Divine_Trg",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
            };
            acolyte.AddPassives([Passives.Skittish, Passives.GetCustomPassive("Zelator_PA"), Passives.Withering]);

            Enemy asterism = new Enemy("Asterism", "AsterismMinion_EN")
            {
                Health = 12,
                HealthColor = Pigments.RedPurple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AmdusiasAsterismTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AsterismDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AsterismTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Combat/StatusEffects/SE_Divine_Trg",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
            };
            asterism.AddPassives([Passives.Formless, Passives.GetCustomPassive("Zelator_PA"), Passives.Withering]);

            SpecificEnemiesTargeting BossTarget = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            BossTarget._enemies = ["Amdusias_BOSS"];
            BossTarget.targetUnitAllySlots = true;
            BossTarget.slotOffsets = [0];

            SpawnEnemyAnywhereEffect SummonGuy = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            SummonGuy.enemy = acolyte.enemy;
            SummonGuy._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            StatusEffect_Apply_Effect HexedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            HexedApply._Status = StatusField.GetCustomStatusEffect("Hexed_ID");

            SpecificAlliesByHealthColorTargeting ThePurples = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            ThePurples.slotOffsets = [0];
            ThePurples.targetUnitAllySlots = false;
            ThePurples._color = Pigments.Purple;
            ThePurples._contains = true;
            ThePurples.getAllUnitSelfSlots = false;

            SpecificAlliesByHealthColorTargeting OpposingPurples = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            OpposingPurples.slotOffsets = [0];
            OpposingPurples.targetUnitAllySlots = true;
            OpposingPurples._color = Pigments.Purple;
            OpposingPurples._contains = true;
            OpposingPurples.getAllUnitSelfSlots = true;

            AnimationVisualsEffect WaitAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            WaitAnim._visuals = CustomVisuals.Nothing;
            WaitAnim._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect RingAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            RingAnim._visuals = Visuals.Bell;
            RingAnim._animationTarget = Targeting.Slot_SelfSlot;

            RemovePassiveEffect NonusAttack = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            NonusAttack.m_PassiveID = Passives.Example_BonusAttack_Mangle.m_PassiveID;

            DisplayMessageEffect AmdusiasSaysASwearWordLmao = ScriptableObject.CreateInstance<DisplayMessageEffect>();
            AmdusiasSaysASwearWordLmao._text = "<color=#" + ColorUtility.ToHtmlStringRGB(new Color32(200, 200, 200, 255)) + ">Damn it.</color>";

            TryUnlockAchievementEffect AmdusiasBonusUnlock = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            AmdusiasBonusUnlock._unlockID = "ComedyAmdusiasCrowd";

            Ability bonusSummon = new Ability("Congregation", "AApocrypha_AmdusiasSummon_A")
            {
                Description = "If there is room on the battlefield, one of Amdusias' acolytes emerges from the crowd to battle.",
                Cost = [Pigments.RedPurple, Pigments.RedPurple],
                Visuals = Visuals.Bell,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_AllyAllSlots),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<AmdusiasCultistHandlerEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(SummonGuy, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                    Effects.GenerateEffect(WaitAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 3])),
                    Effects.GenerateEffect(RingAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [3, 4])),
                    Effects.GenerateEffect(AmdusiasSaysASwearWordLmao, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [4, 5])),
                    Effects.GenerateEffect(NonusAttack, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [5, 6])),
                    Effects.GenerateEffect(AmdusiasBonusUnlock, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [6, 7])),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            bonusSummon.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);
            ExtraAbilityInfo summonExtra = new()
            {
                ability = bonusSummon.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };
            BasePassiveAbilitySO amdusionusAttack = Passives.BonusAttackGenerator(summonExtra);
            NonusAttack.m_PassiveID = amdusionusAttack.m_PassiveID;
            amdusias.AddPassive(amdusionusAttack);

            TargetSplitOrReplaceHealthEffect purplify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify._color = Pigments.Purple;
            purplify._colorBlacklist = [Pigments.Grey];
            purplify._transformBlacklist = true;

            Ability censer = new Ability("Miasmatic Censer", "AApocrypha_AmdusiasCenser_A")
            {
                Description = "Deal a Little damage to the Left, Right and Opposing party members, then split purple into their health color, transforming it if it was grey. Apply 1 Hexed to all targets whose health color did not change.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = ITAVisuals.Stank,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(purplify, 1, Targeting.GenerateSlotTarget([-1], false)),
                    Effects.GenerateEffect(HexedApply, 1, Targeting.GenerateSlotTarget([-1], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(purplify, 1, Targeting.GenerateSlotTarget([0], false)),
                    Effects.GenerateEffect(HexedApply, 1, Targeting.GenerateSlotTarget([0], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(purplify, 1, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(HexedApply, 1, Targeting.GenerateSlotTarget([1], false), Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            censer.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Mana_Modify), "Status_Hexed"]);

            Ability visions = new Ability("Unknowable Visions", "AApocrypha_AmdusiasMassHex_A")
            {
                Description = "Apply 2 Hexed to all party members Opposing an enemy with purple-containing health.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = OpposingPurples,
                Effects =
                [
                    Effects.GenerateEffect(HexedApply, 2, OpposingPurples),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Fast,
            };
            visions.AddIntentsToTarget(ThePurples, [nameof(IntentType_GameIDs.Misc)]);
            visions.AddIntentsToTarget(OpposingPurples, ["Status_Hexed"]);

            Ability eyes = new Ability("Eyes Upon Thee", "AApocrypha_AmdusiasMassAttack_A")
            {
                Description = "Deal a Painful amount of damage to all party members Opposing an enemy with purple-containing health.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Visuals = CustomVisuals.StarfallVisualsSO,
                AnimationTarget = OpposingPurples,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, OpposingPurples),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Fast,
            };
            eyes.AddIntentsToTarget(ThePurples, [nameof(IntentType_GameIDs.Misc)]);
            eyes.AddIntentsToTarget(OpposingPurples, [nameof(IntentType_GameIDs.Damage_3_6)]);

            SpecificEnemiesTargeting AllAcolytes = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            AllAcolytes._enemies = ["AcolyteMinion_EN"];
            AllAcolytes.slotOffsets = [0];
            AllAcolytes.targetUnitAllySlots = true;
            AllAcolytes.getAllUnitSelfSlots = true;

            CasterRandomTransformationEffect Transformation1 = ScriptableObject.CreateInstance<CasterRandomTransformationEffect>();
            Transformation1._maintainMaxHealth = false;
            Transformation1._fullyHeal = false;
            Transformation1._currentToMaxHealth = false;
            Transformation1._maintainTimelineAbilities = false;
            Transformation1._possibleTransformations = [
                new TransformOption(asterism.enemy),
            ];

            AnimationVisualsEffect TransformVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TransformVisuals._animationTarget = Targeting.Slot_SelfSlot;
            TransformVisuals._visuals = Visuals.FeelTheRhythm;

            RandomTargetPerformEffectViaSubaction Transform = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            Transform.effects = 
            [
                Effects.GenerateEffect(TransformVisuals),
                Effects.GenerateEffect(Transformation1),
            ];

            Ability exalt = new Ability("Exaltation", "AApocrypha_AmdusiasTransformation_A")
            {
                Description = "Compel a random Acolyte to give in and be transformed.",
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(Transform, 1, AllAcolytes),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.ExtremelySlow,
            };
            exalt.AddIntentsToTarget(AllAcolytes, [nameof(IntentType_GameIDs.Other_Transform_Instument)]);

            Ability hex = new Ability("Hex", "AApocrypha_AmdusiasMinionHex_A")
            {
                Description = "Apply 1 Hexed to the Left and Right party members.",
                Cost = [Pigments.Purple, Pigments.RedPurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(HexedApply, 1, Targeting.Slot_OpponentSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            hex.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1], false), ["Status_Hexed"]);
            hex.AddIntentsToTarget(Targeting.GenerateSlotTarget([1], false), ["Status_Hexed"]);

            Ability sunder = new Ability("Sunder", "AApocrypha_AmdusiasMinionSunder_A")
            {
                Description = "Apply 2 Hexed to the Opposing party member.",
                Cost = [Pigments.Red, Pigments.RedPurple],
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(HexedApply, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            sunder.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Hexed"]);

            amdusias.AddEnemyAbilities([
                censer.GenerateEnemyAbility(true),
                visions.GenerateEnemyAbility(true),
                eyes.GenerateEnemyAbility(true),
                exalt.GenerateEnemyAbility(true),
            ]);

            acolyte.AddEnemyAbilities([
                hex.GenerateEnemyAbility(false),
                sunder.GenerateEnemyAbility(false),
            ]);

            asterism.AddEnemyAbilities([
                LoadedAssetsHandler.GetEnemy("Asterism_EN").abilities[0],
                LoadedAssetsHandler.GetEnemy("Asterism_EN").abilities[1],
                LoadedAssetsHandler.GetEnemy("Asterism_EN").abilities[2],
                LoadedAssetsHandler.GetEnemy("Asterism_EN").abilities[3],
            ]);

            amdusias.AddEnemy(false, false, false);
            acolyte.AddEnemy(false, false, false);
            asterism.AddEnemy(false, false, false);

            string achievementID = "AmdusiasBoss_ACH";
            string unlockID = "Amdusias_BOSS";
            string itemID = "AnomalousScepter_TW";

            BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, itemID);
            UnlockableModData unlockData = new UnlockableModData(unlockID);
            unlockData.hasModdedAchievementUnlock = true;
            unlockData.moddedAchievementID = achievementID;
            unlockData.hasItemUnlock = true;
            unlockData.items = [itemID];

            ListedUnlockCheck unlockCheck = ScriptableObject.CreateInstance<ListedUnlockCheck>();
            unlockCheck.unlockID = unlockID;
            unlockCheck.unlockData = unlockData;
            Unlocks.AddUnlock_BeatBoss(unlockCheck);

            ModdedAchievements bossAchievement = new ModdedAchievements("The Zelator", "Murder Amdusias.", ResourceLoader.LoadSprite("AchievementBossAmdusias", null, 32, null), achievementID);
            bossAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.BossesTitleLabel);

            string[] amdusiasTips =
            [
                "Getting Hexed may be dangerous, but it helps get rid of the buildup of purple-containing pigment from thinning the crowd.",
                "Beware the transformed Acolytes! They still hit harder against Hexed party members, which can easily kill the unprepared.",
                "Keep the crowd small! Half of Amdusias' actions target more party members the more Acolytes are in the fight."
            ];
            BrutalAPI.Dialogues.AddCustom_GameOver_BossLines("Amdusias_BOSS", amdusiasTips);
            LoadedAssetsHandler.GetEnemy("AcolyteMinion_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Acolyte_EN").enemyTemplate;
            LoadedAssetsHandler.GetEnemy("AsterismMinion_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Asterism_EN").enemyTemplate;
        }
    }
}
