using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class FungusColumn
    {
        public static void Add()
        {
            Enemy funguscolumm = new Enemy("Fungus Column", "FungusColumn_EN")
            {
                Health = 20,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("FungusColumnTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("FungusColumnDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("FungusColumnTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                UnitTypes = ["Neathy"],
            };
            funguscolumm.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/FungusColumn_Enemy/FungusColumn_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/FungusColumn_Enemy/FungusColumn_Giblets.prefab").GetComponent<ParticleSystem>());
            funguscolumm.AddPassives([]);

            StatusEffect_Apply_Effect RandomPreviousPoisonedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RandomPreviousPoisonedApply._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            RandomPreviousPoisonedApply._RandomBetweenPrevious = true;

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            SwapToSidesEffect SwapEither = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition Previous2False = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2False.wasSuccessful = false;
            Previous2False.previousAmount = 2;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            StatusEffectCheckerEffect HasRuptured = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            HasRuptured._status = StatusField.Ruptured;

            Ability noxiousspores = new Ability("Noxious Spores", "AApocrypha_NoxiousSpores_A")
            {
                Description = "Apply 2-4 Poisoned to the Left, Right and Opposing party members.",
                Cost = [Pigments.Red],
                Visuals = ITAVisuals.Stank,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 2),
                    Effects.GenerateEffect(RandomPreviousPoisonedApply, 4, Targeting.Slot_FrontAndSides),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            noxiousspores.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Poisoned"]);

            Ability lashingfronds = new Ability("Lashing Fronds", "AApocrypha_Lashing Fronds_A")
            {
                Description = "Deal a Painful amount of damage to the Left and Right party members.\nApply 2 Ruptured to the Left and Right party members, or move them away from this enemy if they are already Ruptured.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Slash,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(HasRuptured, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_OpponentLeft, PreviousTrue),
                    Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_OpponentLeft, Previous2False),
                    Effects.GenerateEffect(HasRuptured, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(SwapRight, 1, Targeting.Slot_OpponentRight, PreviousTrue),
                    Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_OpponentRight, Previous2False),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            lashingfronds.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            lashingfronds.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            lashingfronds.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Swap_Left)]);
            lashingfronds.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Swap_Right)]);

            funguscolumm.AddEnemyAbilities(
                [
                    noxiousspores,
                    lashingfronds,
                ]);
            funguscolumm.AddEnemy(true, true, false);
        }
    }
}
