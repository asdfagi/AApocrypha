using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class Logos
    {
        public static void Add()
        {
            Enemy redlogos = new Enemy("Crimson Logos", "CrimsonLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineRed", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineRed", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy"],
            };
            redlogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/Logos_Enemy.prefab", AApocrypha.assetBundle, null);

            Enemy bluelogos = new Enemy("Cerulean Logos", "CeruleanLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineBlue", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineBlue", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy"],
            };
            bluelogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosBlue_Enemy.prefab", AApocrypha.assetBundle, null);

            Enemy yellowlogos = new Enemy("Cerulean Logos", "CeruleanLogos_EN")
            {
                Health = 40,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("LogosTimelineYellow", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("LogosTimelineYellow", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                UnitTypes = ["Neathy"],
            };
            yellowlogos.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Logos_Enemy/LogosYellow_Enemy.prefab", AApocrypha.assetBundle, null);

            DirectDeathEffect Obliterate = ScriptableObject.CreateInstance<DirectDeathEffect>();
            Obliterate._obliterationDeath = true;

            FieldEffect_Apply_Effect ApplyFire = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyFire._Field = StatusField.OnFire;

            FieldEffect_Apply_Effect ApplyRandomFire = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyRandomFire._Field = StatusField.OnFire;
            ApplyRandomFire._UseRandomBetweenPrevious = true;

            RemoveFieldEffectEffect RemoveFire = ScriptableObject.CreateInstance<RemoveFieldEffectEffect>();
            RemoveFire._field = StatusField.OnFire;

            FieldEffect_Apply_Effect ApplyConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyConstricted._Field = StatusField.Constricted;

            SpecificOpponentsByFieldTargeting OpponentsInFire = ScriptableObject.CreateInstance<SpecificOpponentsByFieldTargeting>();
            OpponentsInFire._fieldEffectID = StatusField.OnFire._FieldID;
            OpponentsInFire.targetUnitAllySlots = false;
            OpponentsInFire.slotOffsets = [0];

            SpecificOpponentsByFieldTargeting OneOpponentInFire = ScriptableObject.CreateInstance<SpecificOpponentsByFieldTargeting>();
            OneOpponentInFire._fieldEffectID = StatusField.OnFire._FieldID;
            OneOpponentInFire.targetUnitAllySlots = false;
            OneOpponentInFire.slotOffsets = [0];
            OneOpponentInFire.oneOfTargets = true;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            AnimationVisualsEffect TorchAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TorchAnim._visuals = Visuals.Torched;
            TorchAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect TorchAnim2 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TorchAnim2._visuals = Visuals.Torched;
            TorchAnim2._animationTarget = Targeting.Slot_FrontAndSides;

            DamageEffect DamageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            DamageByPrevious._usePreviousExitValue = true;

            DamageOnDoubleCascadeEffect DamageCascadeByPrevious = ScriptableObject.CreateInstance<DamageOnDoubleCascadeEffect>();
            DamageCascadeByPrevious._usePreviousExitValue = true;
            DamageCascadeByPrevious._cascadeIsIndirect = true;
            DamageCascadeByPrevious._decreaseAsPercentage = true;
            DamageCascadeByPrevious._cascadeDecrease = 75;
            DamageCascadeByPrevious._returnKillAsSuccess = true;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            FieldEffect_Apply_Effect ShieldByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldByPrevious._Field = StatusField.Shield;
            ShieldByPrevious._UsePreviousExitValueAsMultiplier = true;

            FieldEffect_Apply_Effect FireByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            FireByPrevious._Field = StatusField.OnFire;
            FireByPrevious._UsePreviousExitValueAsMultiplier = true;

            FieldEffect_ApplyWithRandomDistribution_Effect FireByPrevious2 = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            FireByPrevious2.field = StatusField.OnFire;
            FireByPrevious2.usePrevious = true;

            AllTargeting AllUnitsTargeting = ScriptableObject.CreateInstance<AllTargeting>();
            AllUnitsTargeting._units = true;

            Ability westwheel = new Ability("Turn To The West", "AApocrypha_LogosLeft_A")
            {
                Description = "Move Left, then apply 1 Fire to the newly Opposing position.",
                Cost = [Pigments.Grey, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TorchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyFire, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            westwheel.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Field_Fire)]);
            westwheel.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability eastwheel = new Ability("Turn To The East", "AApocrypha_LogosRight_A")
            {
                Description = "Move Right, then apply 1 Fire to the newly Opposing position.",
                Cost = [Pigments.Red, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(TorchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyFire, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VeryFast,
            };
            eastwheel.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Field_Fire)]);
            eastwheel.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability tonguesoffire = new Ability("Tongues Of Fire", "AApocrypha_FireTongues_A")
            {
                Description = "Deal a Painful amount of damage to All party members standing in Fire. Apply 0-2 Fire to the Left, Right and Opposing spaces.",
                Cost = [Pigments.Red, Pigments.Grey, Pigments.Red],
                Visuals = Visuals.Torched,
                AnimationTarget = OpponentsInFire,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, OpponentsInFire),
                    Effects.GenerateEffect(TorchAnim2, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0),
                    Effects.GenerateEffect(ApplyRandomFire, 2, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            tonguesoffire.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Damage_3_6)]);
            tonguesoffire.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Field_Fire)]);

            Ability everythinginplace = new Ability("Everything In Its Place", "AApocrypha_EverythingInPlace_A")
            {
                Description = "Apply 1 Fire to all party members standing in Fire. Apply 1 Constricted to a random party member standing in Fire.",
                Cost = [Pigments.Grey, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Torched,
                AnimationTarget = OpponentsInFire,
                Effects =
                [
                    Effects.GenerateEffect(ApplyFire, 1, OpponentsInFire),
                    Effects.GenerateEffect(ApplyConstricted, 1, OneOpponentInFire),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            everythinginplace.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Field_Fire)]);
            everythinginplace.AddIntentsToTarget(OpponentsInFire, [nameof(IntentType_GameIDs.Field_Constricted)]);

            Ability becomefire = new Ability("To Become Fire", "AApocrypha_BecomeFire_A")
            {
                Description = "Remove all Fire from the Left, Opposing and Right spaces. Deal damage to the Opposing party member equal to twice the amount of Fire removed. Damage cascades indirectly to the Left and Right with a 25% falloff.\nIf this damage kills, remove all Fire from All party member spaces.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Conductor,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(DamageCascadeByPrevious, 2, Targeting.GenerateSlotTarget([0, -1, 1, -2, 2, -3, 3, -4, 4], false)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_OpponentAllSlots, PreviousTrue),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            becomefire.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Rem_Field_Fire)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Left)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            becomefire.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Right)]);

            Ability lifepreserved = new Ability("Life Preserved", "AApocrypha_LifePreserved_A")
            {
                Description = "Remove all Fire from All allied positions and apply Shield to each allied position equal to 3 times the amount of Fire removed from that position.",
                Cost = [Pigments.Blue, Pigments.Blue, Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Shield,
                AnimationTarget = Targeting.Slot_AllyAllSlots,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.GenerateGenericTarget([4], true)),
                    Effects.GenerateEffect(ShieldByPrevious, 3, Targeting.GenerateGenericTarget([4], true)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            lifepreserved.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire)]);
            lifepreserved.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Field_Shield)]);

            Ability exchangeburdens = new Ability("An Exchange Of Burdens", "AApocrypha_ExchangeBurdens_A")
            {
                Description = "Remove all Fire from All positions and apply it to this enemy.\nRemove all Fire from this enemy's position and distribute it randomly to All occupied positions.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Conductor,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_AllyAllSlots),
                    Effects.GenerateEffect(FireByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(FireByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RemoveFire, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(FireByPrevious2, 1, AllUnitsTargeting),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            exchangeburdens.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire)]);
            exchangeburdens.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Rem_Field_Fire)]);
            exchangeburdens.AddIntentsToTarget(Targeting.Unit_AllAllySlots, [nameof(IntentType_GameIDs.Field_Fire)]);
            exchangeburdens.AddIntentsToTarget(Targeting.Unit_AllOpponentSlots, [nameof(IntentType_GameIDs.Field_Fire)]);

            ExtraAbilityInfo tonguesextra = new()
            {
                ability = tonguesoffire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo placeextra = new()
            {
                ability = everythinginplace.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo becomeextra = new()
            {
                ability = becomefire.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo lifeextra = new()
            {
                ability = lifepreserved.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            ExtraAbilityInfo burdensextra = new()
            {
                ability = exchangeburdens.GenerateEnemyAbility().ability,
                rarity = Rarity.Uncommon,
            };

            redlogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), CustomPassives.AltAttacksGenerator([tonguesextra, placeextra, becomeextra])]);
            bluelogos.AddPassives([Passives.MultiAttack3, Passives.InfernoGenerator(1), Passives.GetCustomPassive("MadeOfFire_PA"), CustomPassives.AltAttacksGenerator([tonguesextra, lifeextra, burdensextra])]);

            redlogos.AddEnemyAbilities(
            [
                westwheel,
                eastwheel,
            ]);

            bluelogos.AddEnemyAbilities(
            [
                westwheel,
                eastwheel,
            ]);

            redlogos.AddEnemy(true, true, false);
            bluelogos.AddEnemy(false, false, false);
        }
    }
}
