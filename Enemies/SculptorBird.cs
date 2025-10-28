using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine;

namespace A_Apocrypha.Enemies
{
    public class SculptorBird
    {
        public static void Add()
        {
            Enemy sculptorbird = new Enemy("Sculptor Bird", "SculptorBird_EN")
            {
                Health = 35,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SculptorBirdTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SculptorBirdDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SculptorBirdTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Scrungie_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Scrungie_EN").deathSound,
                UnitTypes = ["Bird"],
                AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_SculptorBird>(),
            };
            sculptorbird.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/SculptorBird_Enemy/SculptorBird_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/SculptorBird_Enemy/SculptorBird_Giblets.prefab").GetComponent<ParticleSystem>());
            
            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition Previous2True = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2True.wasSuccessful = true;
            Previous2True.previousAmount = 2;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            PreviousEffectCondition Previous2False = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2False.wasSuccessful = false;
            Previous2False.previousAmount = 2;

            RemoveFieldEffectEffect RemoveShield = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveShield._field = StatusField.Shield;

            AnimationVisualsEffect GazeAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            GazeAnim._animationTarget = Targeting.Slot_Front;
            GazeAnim._visuals = CustomVisuals.GazeVisualsSO;

            AnimationVisualsEffect RendAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            RendAnim._animationTarget = Targeting.Slot_Front;
            RendAnim._visuals = Visuals.RendRight;

            AnimationVisualsEffect TalonAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TalonAnim._animationTarget = Targeting.Slot_Front;
            TalonAnim._visuals = Visuals.Talons;

            IncreaseStatusEffectsEffect IncreaseBadStatus = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseBadStatus._increasePositives = false;

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            RemoveStatusEffectEffect ScarsRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            ScarsRemove._status = StatusField.Scars;

            RemoveStatusEffectEffect RupturedRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            RupturedRemove._status = StatusField.Ruptured;

            RemoveStatusEffectEffect OilRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            OilRemove._status = StatusField.OilSlicked;

            RemoveStatusEffectEffect FrailRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            FrailRemove._status = StatusField.Frail;

            SwapToOneRandomSideXTimesEffect SwapRandomFar = ScriptableObject.CreateInstance<SwapToOneRandomSideXTimesEffect>();

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            CheckHasMaxHealthEffect HealthIsMax = ScriptableObject.CreateInstance<CheckHasMaxHealthEffect>();

            Ability shellscraper = new Ability("Shell Scraper", "AApocrypha_ShellScraper_A")
            {
                Description = "Remove all Shield from the Left position. If no Shield was removed, deal a Painful amount of damage to the Left party member, then apply 2 Frail to them.\nMove this enemy to the Left.",
                Cost = [Pigments.Blue, Pigments.Red],
                Visuals = Visuals.Talons,
                AnimationTarget = Targeting.Slot_OpponentLeft,
                Effects =
                [
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentLeft, PreviousFalse),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_OpponentLeft, Previous2False),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            shellscraper.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            shellscraper.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);
            shellscraper.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Status_Frail)]);
            shellscraper.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability skinscraper = new Ability("Skin Scraper", "AApocrypha_SkinScraper_A")
            {
                Description = "Remove all Shield from the Right position. If no Shield was removed, deal a Painful amount of damage to the Right party member, then apply 2 Frail to them.\nMove this enemy to the Right.",
                Cost = [Pigments.Blue, Pigments.Red],
                Visuals = Visuals.Talons,
                AnimationTarget = Targeting.Slot_OpponentRight,
                Effects =
                [
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentRight, PreviousFalse),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_OpponentRight, Previous2False),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            skinscraper.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            skinscraper.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);
            skinscraper.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Status_Frail)]);
            skinscraper.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability marrowscraper = new Ability("Marrow Scraper", "AApocrypha_MarrowScraper_A")
            {
                Description = "Move this enemy to the Left twice or to the Right twice, then remove all Shield from the newly Opposing position.\nDeal a Painful amount of damage to the Opposing party member, then apply 2 Frail to them.\n\"Sculptor birds value their sculptures greatly - damaging one is certain to enrage them.\"",
                Cost = [Pigments.Blue, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TalonAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Normal,
            };
            marrowscraper.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            marrowscraper.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            marrowscraper.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
            marrowscraper.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6)]);
            marrowscraper.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);

            Ability demandperfection = new Ability("Demand Perfection", "AApocrypha_DemandPerfection_A")
            {
                Description = "Move this enemy to the Left twice or to the Right twice.\nIncrease all negative status effects on the Opposing party member by 2. Slightly Heal them if this fails.\nIf the Opposing party member is not at full health, deal an Agonizing amount of damage to them.",
                Cost = [Pigments.Blue, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapRandomFar, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(GazeAnim, 1, Targeting.Slot_Front, PreviousTrue),
                    Effects.GenerateEffect(IncreaseBadStatus, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 2, Targeting.Slot_Front, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(HealthIsMax, 1, Targeting.Slot_Front, PreviousTrue),
                    Effects.GenerateEffect(RendAnim, 1, Targeting.Slot_Front, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 9, Targeting.Slot_Front, Previous2False),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal,
            };
            demandperfection.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            demandperfection.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            demandperfection.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            demandperfection.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_1_4)]);
            demandperfection.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            demandperfection.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability exciseflaws = new Ability("Excise Flaws", "AApocrypha_ExciseFlaws_A")
            {
                Description = "Attempt to remove Scars, Ruptured, Oil Slicked and Frail from this enemy. For each status removed, deal a Painful amount of indirect damage to this enemy.",
                Cost = [Pigments.Blue, Pigments.Red],
                Visuals = Visuals.Talons,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScarsRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(RupturedRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(OilRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                    Effects.GenerateEffect(FrailRemove, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IndirectDamage, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            exciseflaws.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_Scars)]);
            exciseflaws.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_Ruptured)]);
            exciseflaws.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_OilSlicked)]);
            exciseflaws.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_Frail)]);
            exciseflaws.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6)]);

            ExtraAbilityInfo marrowscraperextra = new()
            {
                ability = marrowscraper.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            sculptorbird.AddPassives([Passives.Slippery, Passives.ParentalGenerator(marrowscraperextra)]);

            sculptorbird.AddEnemyAbilities(
            [
                shellscraper.GenerateEnemyAbility(true),
                skinscraper.GenerateEnemyAbility(true),
                demandperfection.GenerateEnemyAbility(true),
                exciseflaws.GenerateEnemyAbility(true),
            ]);

            sculptorbird.AddEnemy(true, true, true);

            Enemy sculpture = new Enemy("Sculpture", "SculptorBirdSculpture_EN")
            {
                Health = 8,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SculptureTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("SculptureDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SculptureTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
            };
            sculpture.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/SculptorBird_Enemy/Sculpture_Enemy.prefab", AApocrypha.assetBundle, null);
            sculpture.AddPassives([Passives.Inanimate, Passives.Anchored, Passives.Infantile, Passives.Withering]);

            sculpture.AddEnemy(true, false, false);

            if (AApocrypha.CrossMod.Siren)
            {
                AnimationVisualsEffect PaintAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
                PaintAnim._animationTarget = Targeting.Slot_SelfSlot;
                PaintAnim._visuals = Visuals.OilSlicked;

                CheckHasUnitWithIDsEffect HasBirdBath = ScriptableObject.CreateInstance<CheckHasUnitWithIDsEffect>();
                HasBirdBath._ids = ["BirdBath_EN", "Boiler_EN"];

                StatusEffect_Apply_Effect CurseApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
                CurseApply._Status = StatusField.Cursed;

                RemoveStatusEffectEffect CurseRemove = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                CurseRemove._status = StatusField.Cursed;

                StatusEffectCheckerEffect HasCurse = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
                HasCurse._status = StatusField.Cursed;

                Ability wetpaint = new Ability("Wet Paint", "AApocrypha_WetPaint_A")
                {
                    Description = "If there is a Boiler or Bird Bath to the Left or Right of this enemy, Curse this enemy.",
                    Cost = [Pigments.Blue, Pigments.Red],
                    Visuals = Visuals.OilSlicked,
                    AnimationTarget = Targeting.Slot_SelfSlot,
                    Effects =
                    [
                        Effects.GenerateEffect(HasBirdBath, 1, Targeting.Slot_AllyLeft),
                        Effects.GenerateEffect(CurseApply, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                        Effects.GenerateEffect(HasBirdBath, 1, Targeting.Slot_AllyRight),
                        Effects.GenerateEffect(CurseApply, 1, Targeting.Slot_SelfSlot, PreviousTrue),
                    ],
                    Rarity = Rarity.Impossible,
                    Priority = Priority.VeryFast,
                };
                wetpaint.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc)]);
                wetpaint.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Cursed)]);

                ExtraAbilityInfo wetpaintextra = new()
                {
                    ability = wetpaint.GenerateEnemyAbility().ability,
                    rarity = Rarity.Impossible,
                };

                Ability shellscraper2 = new Ability("Shell Scraper", "AApocrypha_ShellScraperSiren_A")
                {
                    Description = "Remove all Shield from the Left position. If no Shield was removed, deal a Painful amount of damage to the Left party member, then apply 2 Frail to them.\nIf this enemy is Cursed, Curse the Left party member.\nRemove Cursed from this enemy and move it to the Left.",
                    Cost = [Pigments.Blue, Pigments.Red],
                    Visuals = Visuals.Talons,
                    AnimationTarget = Targeting.Slot_OpponentLeft,
                    Effects =
                    [
                        Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_OpponentLeft),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentLeft, PreviousFalse),
                        Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_OpponentLeft, Previous2False),
                        Effects.GenerateEffect(HasCurse, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CurseApply, 1, Targeting.Slot_OpponentLeft, PreviousTrue),
                        Effects.GenerateEffect(CurseRemove, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.VeryRare,
                    Priority = Priority.Normal,
                };
                shellscraper2.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
                shellscraper2.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);
                shellscraper2.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Status_Frail)]);
                shellscraper2.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Status_Cursed)]);
                shellscraper2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_Cursed)]);
                shellscraper2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);

                Ability skinscraper2 = new Ability("Skin Scraper", "AApocrypha_SkinScraperSiren_A")
                {
                    Description = "Remove all Shield from the Right position. If no Shield was removed, deal a Painful amount of damage to the Right party member, then apply 2 Frail to them.\nIf this enemy is Cursed, Curse the Right party member.\nRemove Cursed from this enemy and move it to the Right.",
                    Cost = [Pigments.Blue, Pigments.Red],
                    Visuals = Visuals.Talons,
                    AnimationTarget = Targeting.Slot_OpponentRight,
                    Effects =
                    [
                        Effects.GenerateEffect(RemoveShield, 1, Targeting.Slot_OpponentRight),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentRight, PreviousFalse),
                        Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_OpponentRight, Previous2False),
                        Effects.GenerateEffect(HasCurse, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CurseApply, 1, Targeting.Slot_OpponentRight, PreviousTrue),
                        Effects.GenerateEffect(CurseRemove, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    ],
                    Rarity = Rarity.VeryRare,
                    Priority = Priority.Normal,
                };
                skinscraper2.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Rem_Field_Shield)]);
                skinscraper2.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);
                skinscraper2.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Status_Frail)]);
                skinscraper2.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Status_Cursed)]);
                skinscraper2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Rem_Status_Cursed)]);
                skinscraper2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

                Enemy sirenbird = new Enemy("Sculptor Bird", "SculptorBirdSiren_EN")
                {
                    Health = 35,
                    HealthColor = Pigments.Red,
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("SirenBirdTimeline", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("SirenBirdDead", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("SirenBirdTimeline", new Vector2(0.5f, 0f), 32),
                    DamageSound = LoadedAssetsHandler.GetEnemy("Scrungie_EN").damageSound,
                    DeathSound = LoadedAssetsHandler.GetEnemy("Scrungie_EN").deathSound,
                    UnitTypes = ["Bird"],
                    AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_SculptorBird>(),
                };
                sirenbird.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/SculptorBird_Enemy/SirenBird_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/SculptorBird_Enemy/SculptorBird_Giblets.prefab").GetComponent<ParticleSystem>());

                sirenbird.AddPassives([Passives.Slippery, Passives.BonusAttackGenerator(wetpaintextra)]);

                sirenbird.AddEnemyAbilities(
                [
                    shellscraper2.GenerateEnemyAbility(true),
                    skinscraper2.GenerateEnemyAbility(true),
                    demandperfection.GenerateEnemyAbility(true),
                    exciseflaws.GenerateEnemyAbility(true),
                ]);

                sirenbird.AddEnemy(true, true, true);
            }
        }
    }
}
