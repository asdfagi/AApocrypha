using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Enemies.Bosses
{
    public class LornFluke
    {
        public static void Add()
        {
            if (!LoadedDBsHandler._StatusFieldDB._StatusEffects.ContainsKey("Weakness_ID")) { return; }

            Enemy fluke = new Enemy("The Regret-Beyond-Death", "LornFluke_BOSS")
            { 
                Health = 150,
                HealthColor = Pigments.Red,
                Size = 3,
                CombatSprite = ResourceLoader.LoadSprite("LornFlukeTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("LornFlukeTimeline", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LornFlukeTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Ouroboros_Head_BOSS").deathSound,
                UnitTypes = [UnitType_GameIDs.Fish.ToString(), "Neathy"],
            };
            fluke.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/LornFluke_Boss/LornFluke_Boss.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/LornFluke_Boss/LornFluke_Giblets.prefab").GetComponent<ParticleSystem>());
            fluke.AddPassives([Passives.Slippery]);

            StatusEffect_Apply_Effect weaken = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            weaken._Status = StatusField.GetCustomStatusEffect("Weakness_ID");

            StatusEffect_ApplyByPrevious_Effect weakenByPrevious = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            weakenByPrevious._Status = StatusField.GetCustomStatusEffect("Weakness_ID");

            DamageWithStatusBonusIgnoresWeaknessEffect damageTargetWeakBoost = ScriptableObject.CreateInstance<DamageWithStatusBonusIgnoresWeaknessEffect>();
            damageTargetWeakBoost._status = StatusField.GetCustomStatusEffect("Weakness_ID");
            damageTargetWeakBoost._bonusAmount = 1;
            damageTargetWeakBoost._bonusStacking = true;

            DamageAdvancedWithCasterStatusBonusEffect damageCasterWeakBoost = ScriptableObject.CreateInstance<DamageAdvancedWithCasterStatusBonusEffect>();
            damageCasterWeakBoost._status = StatusField.GetCustomStatusEffect("Weakness_ID");
            damageCasterWeakBoost._bonusAmount = 2;
            damageCasterWeakBoost._bonusStacking = true;

            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;

            HalveSpecificStatusEffectEffect HalveWeak = ScriptableObject.CreateInstance<HalveSpecificStatusEffectEffect>();
            HalveWeak._statusID = "Weakness_ID";

            RemoveStatusEffectEffect NoWeak = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            NoWeak._status = StatusField.GetCustomStatusEffect("Weakness_ID");

            GenerateColorManaEffect GiveBluePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveBluePigment.mana = Pigments.Blue;

            CasterStoredValueChangeWithMaxEffect GrieveUp = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
            GrieveUp.m_unitStoredDataID = "LornFlukeGrieveStoredValue";
            GrieveUp._minimumValue = 0;
            GrieveUp._maximumValue = 3;
            GrieveUp._exitValueIsChange = false;
            GrieveUp._increase = true;
            GrieveUp._randomBetweenPrevious = false;
            GrieveUp._usePreviousExitValue = false;

            CasterStoredValueChangeWithMaxEffect GrieveClear = ScriptableObject.CreateInstance<CasterStoredValueChangeWithMaxEffect>();
            GrieveClear.m_unitStoredDataID = "LornFlukeGrieveStoredValue";
            GrieveClear._minimumValue = 0;
            GrieveClear._maximumValue = 3;
            GrieveClear._exitValueIsChange = false;
            GrieveClear._increase = false;
            GrieveClear._randomBetweenPrevious = false;
            GrieveClear._usePreviousExitValue = false;

            fluke.CombatEnterEffects = [Effects.GenerateEffect(GrieveClear, 100, Targeting.Slot_SelfSlot)];

            ExtraVariableForNext_SVEffect GrieveGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            GrieveGet.m_unitStoredDataID = "LornFlukeGrieveStoredValue";

            PreviousComparatorCheckEffect ThreePlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            ThreePlus._atOrAbove = true;
            ThreePlus._entryIsComparator = false;
            ThreePlus._fixedComparator = 3;

            QueueTimelineAbilityByNameEffect QueueLament = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            QueueLament._abilityName = "Lamentations of Axile";

            TargetStatusCheckEffect HasWeak = ScriptableObject.CreateInstance<TargetStatusCheckEffect>();
            HasWeak._status = StatusField.GetCustomStatusEffect("Weakness_ID");

            StatusEffect_ApplyWithRandomDistribution_Effect weakenRandom = ScriptableObject.CreateInstance<StatusEffect_ApplyWithRandomDistribution_Effect>();
            weakenRandom.usePrevious = true;
            weakenRandom.previousIsRange = false;
            weakenRandom.status = StatusField.GetCustomStatusEffect("Weakness_ID");

            DirectDeathEffect die = ScriptableObject.CreateInstance<DirectDeathEffect>();
            die._obliterationDeath = false;
            die._killUnderMaxHealth = false;
            die._ExitValueIsHealthRemaining = true;

            PreviousComparatorCheckEffect OneMinus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            OneMinus._atOrAbove = false;
            OneMinus._entryIsComparator = false;
            OneMinus._fixedComparator = 2;

            Ability attackLeft = new Ability("Piercing Regrets", "AApocrypha_LornFlukeAttackLeft_A")
            {
                Description = "Deal a Painful amount of damage to the Left Opposing and Left party members, increased by the amount of Weakness on them. This damage is unaffected by Weakness." +
                "\nApply the damage dealt as Weakness to this enemy." +
                "\nIf there is a Central Opposing party member, move this enemy to the Right.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Slash,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([0]),
                Effects =
                [
                    Effects.GenerateEffect(damageTargetWeakBoost, 5, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(weakenByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageTargetWeakBoost, 5, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(weakenByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            attackLeft.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([0]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            attackLeft.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);
            attackLeft.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Status_Weakness"]);
            attackLeft.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([1]), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            attackLeft.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability attackRight = new Ability("Puncturing Sorrows", "AApocrypha_LornFlukeAttackRight_A")
            {
                Description = "Deal a Painful amount of damage to the Right Opposing and Right party members, increased by the amount of Weakness on them. This damage is unaffected by Weakness." +
                "\nApply the damage dealt as Weakness to this enemy." +
                "\nIf there is a Central Opposing party member, move this enemy to the Left.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Slash,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([2]),
                Effects =
                [
                    Effects.GenerateEffect(damageTargetWeakBoost, 5, Targeting.GenerateBigUnitSlotTarget([2])),
                    Effects.GenerateEffect(weakenByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageTargetWeakBoost, 5, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(weakenByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            attackRight.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([2]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            attackRight.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);
            attackRight.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Status_Weakness"]);
            attackRight.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([1]), [nameof(IntentType_GameIDs.Misc_Hidden)]);
            attackRight.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability recall = new Ability("Do You Recall?", "AApocrypha_LornFlukeDoYouRecall_A")
            {
                Description = "Apply 2 Weakness to All party members except the Central Opposing. Apply 2 Weakness to this enemy.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Cry,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(weaken, 2, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(weaken, 2, Targeting.Slot_OpponentAllLefts),
                    Effects.GenerateEffect(weaken, 2, Targeting.GenerateBigUnitSlotTarget([2])),
                    Effects.GenerateEffect(weaken, 2, Targeting.Slot_OpponentAllRights),
                    Effects.GenerateEffect(weaken, 2, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            recall.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([0]), ["Status_Weakness"]);
            recall.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, ["Status_Weakness"]);
            recall.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([2]), ["Status_Weakness"]);
            recall.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, ["Status_Weakness"]);
            recall.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Status_Weakness"]);

            Ability lightning = new Ability("Song of Lightning", "AApocrypha_LornFlukeSongOfLightning_A")
            {
                Description = "Halve this enemy's Weakness." +
                "\nDeal a Little damage to all party members Not Opposing this enemy. Weakness increases this damage instead of decreasing it.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false),
                Effects =
                [
                    Effects.GenerateEffect(HalveWeak, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageCasterWeakBoost, 2, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            lightning.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], false), [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Misc)]);
            lightning.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Rem_Status_Weakness"]);

            Ability disgrace = new Ability("Our Shapeful Disgrace", "AApocrypha_LornFlukeOurShapefulDisgrace_A")
            {
                Description = "Deal an Agonizing amount of damage to this enemy. If 1 or less damage was dealt, remove all Weakness from this enemy.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Quills,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(OneMinus, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(NoWeak, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
            };
            disgrace.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Misc_Hidden), "Rem_Status_Weakness"]);

            ExtraAbilityInfo recallextra = new()
            {
                ability = recall.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo lightningextra = new()
            {
                ability = lightning.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo disgraceextra = new()
            {
                ability = disgrace.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            Ability grieve = new Ability("Grieve", "AApocrypha_LornFlukeWaaaa_A")
            {
                Description = "The Regret-Beyond-Death sheds tears for its lost home, producing one Blue pigment. Halve the amount of Weakness on the Opposing party members and apply the Weakness removed to this enemy." +
                "\nEvery third time Grieve is used, queue \"Lamentations of Axile\". Otherwise, move this enemy to the Left or Right.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(GiveBluePigment, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalveWeak, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(weakenByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GrieveUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GrieveGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ThreePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(GrieveClear, 100, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(QueueLament, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            grieve.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Mana_Generate)]);
            grieve.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Weakness"]);
            grieve.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Status_Weakness", nameof(IntentType_GameIDs.Misc_Additional), nameof(IntentType_GameIDs.Swap_Sides)]);

            ExtraAbilityInfo grieveextra = new()
            {
                ability = grieve.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            Ability lament = new Ability("Lamentations of Axile", "AApocrypha_LornFlukeSuperAbility_A")
            {
                Description = "Halve this enemy's Weakness, then randomly distribute Weakness among All party members equal to the amount removed. If this enemy had no Weakness, instantly kill the Central Opposing party member and randomly distribute Weakness equal to their health to All party members." +
                "\n\n\"If we could remember those days. If only we could remember.\"",
                Cost = [Pigments.Blue, Pigments.Blue, Pigments.Blue, Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Abandoned,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(HasWeak, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalveWeak, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(weakenRandom, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(die, 1, Targeting.GenerateBigUnitSlotTarget([1]), Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(weakenRandom, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(false, 4)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            lament.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Rem_Status_Weakness"]);
            lament.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Weakness"]);
            lament.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            lament.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([1]), [nameof(IntentType_GameIDs.Damage_Death)]);
            lament.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Weakness"]);

            fluke.AddPassives([CustomPassives.BonusSuiteGenerator([recallextra, lightningextra, disgraceextra]), Passives.BonusAttackGenerator(grieveextra)]);

            fluke.AddEnemyAbilities([
                attackLeft.GenerateEnemyAbility(true),
                attackRight.GenerateEnemyAbility(true),
                lament.GenerateEnemyAbility(true),
            ]);
            fluke.AddEnemy(false, false, false);
            //LoadedAssetsHandler.GetEnemy("LornFluke_BOSS").enemyTemplate = LoadedAssetsHandler.GetEnemy("Mung_EN").enemyTemplate;
            //if (AApocrypha.CrossMod.GlitchsFreaks) { LoadedAssetsHandler.GetEnemy("LornFluke_BOSS").enemyTemplate = LoadedAssetsHandler.GetEnemy("Enno_EN").enemyTemplate; }

            string achievementID = "LornFlukeBoss_ACH";
            string unlockID = "LornFluke_BOSS";
            string itemID = "SpineOfRegret_TW";

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

            ModdedAchievements bossAchievement = new ModdedAchievements("The Griever", "Slay the Regret-Beyond-Death.", ResourceLoader.LoadSprite("AchievementBossLornFluke", null, 32, null), achievementID);
            bossAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.BossesTitleLabel);

            string[] flukeTips =
            [
                //"",
                //"",
                "Bring smaller parties when fighting the Regret-Beyond-Death; large parties will get annihilated.", // thanks The Charcarrion
            ];
            BrutalAPI.Dialogues.AddCustom_GameOver_BossLines("LornFluke_BOSS", flukeTips);
        }
    }
}
