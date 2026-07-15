using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class AgeOfDestructionEnemies
    {
        public static void Add()
        {
            string ColorID = ColorUtility.ToHtmlStringRGB(new Color32(150, 0, 0, 255));

            Enemy radiator = new Enemy("<color=#" + ColorID + ">Exothermic Obliterator</color>", "AgeOfDestruction_Obliterator_EN")
            {
                Health = 60,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AgeOfDestructionTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/EmplacementHurt",
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                UnitTypes = ["Robot"],
            };
            radiator.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/AgeOfDestruction/Radiator_Enemy.prefab", AApocrypha.assetBundle, null);
            radiator.AddPassives([Passives.GetCustomPassive("RedBlooded_1_PA"), Passives.GetCustomPassive("MadeOfFire_PA")]);
            if (LoadedAssetsHandler.GetPassive("Illegible_PA") != null) { radiator.AddPassive(Passives.GetCustomPassive("Illegible_PA")); }

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            StatusEffect_Apply_Effect Irradiate = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Irradiate._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            Ability attackpartyleft = new Ability("Infantry Deterrent", "AApocrypha_DestructionObliteratorLeftParty_A")
            {
                Description = "Apply 1 Irradiated to the Far Left party member and all party members to their Left.",
                Cost = [],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.GenerateSlotTarget([-2, -3, -4], false),
                Effects = [
                    Effects.GenerateEffect(Irradiate, 1, Targeting.GenerateSlotTarget([-2, -3, -4], false)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
            };
            attackpartyleft.AddIntentsToTarget(Targeting.GenerateSlotTarget([-2, -3, -4], false), ["Status_Irradiated"]);

            Ability attackpartycenter = new Ability("Infantry Dispersal", "AApocrypha_DestructionObliteratorCenterParty_A")
            {
                Description = "Apply 1 Irradiated to the Left, Right and Opposing party members.",
                Cost = [],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects = [
                    Effects.GenerateEffect(Irradiate, 1, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
            };
            attackpartycenter.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Irradiated"]);

            Ability attackpartyright = new Ability("Infantry Denial", "AApocrypha_DestructionObliteratorRightParty_A")
            {
                Description = "Apply 1 Irradiated to the Far Right party member and all party members to their Right.",
                Cost = [],
                Visuals = CustomVisuals.MicrowaveVisualsSO,
                AnimationTarget = Targeting.GenerateSlotTarget([2, 3, 4], false),
                Effects = [
                    Effects.GenerateEffect(Irradiate, 1, Targeting.GenerateSlotTarget([2, 3, 4], false)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
            };
            attackpartyright.AddIntentsToTarget(Targeting.GenerateSlotTarget([2, 3, 4], false), ["Status_Irradiated"]);

            radiator.AddEnemyAbilities([
                attackpartyleft.GenerateEnemyAbility(true),
                attackpartycenter.GenerateEnemyAbility(true),
                attackpartyright.GenerateEnemyAbility(true),
            ]);

            StatusEffect_Apply_Effect OilRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilRandom._Status = StatusField.OilSlicked;
            OilRandom._JustOneRandomTarget = true;

            FieldEffect_Apply_Effect Burn = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            Burn._Field = StatusField.OnFire;

            Ability attackenemyleft = new Ability("Salted Earth", "AApocrypha_DestructionObliteratorLeftEnemy_A")
            {
                Description = "Move as far Left as possible. After each successful movement, apply 1 Fire to this enemy's position and 1 Oil-Slicked to a random other enemy.",
                Cost = [],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.VeryFast,
            };
            attackenemyleft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Field_Fire)]);
            attackenemyleft.AddIntentsToTarget(Targeting.Unit_OtherAllies, [nameof(IntentType_GameIDs.Status_OilSlicked)]);

            Ability attackenemyright = new Ability("Burning Fields", "AApocrypha_DestructionObliteratorRightEnemy_A")
            {
                Description = "Move as far Right as possible. After each successful movement, apply 1 Fire to this enemy's position and 1 Oil-Slicked to a random other enemy.",
                Cost = [],
                Visuals = Visuals.Torched,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Burn, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(OilRandom, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.VeryFast,
            };
            attackenemyright.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Field_Fire)]);
            attackenemyright.AddIntentsToTarget(Targeting.Unit_OtherAllies, [nameof(IntentType_GameIDs.Status_OilSlicked)]);

            ExtraAbilityInfo leftExtra = new()
            {
                ability = attackenemyleft.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo rightExtra = new()
            {
                ability = attackenemyright.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            radiator.AddPassives([CustomPassives.BonusSuiteRerollGenerator("Formless", [leftExtra, rightExtra]), Passives.Withering]);

            radiator.AddEnemy(true, false, false);
        }
    }
}
