using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class Simulacrum
    {
        public static void Add()
        {
            CasterChangeNameEnemyEffect NameChangeSimulacrum = ScriptableObject.CreateInstance<CasterChangeNameEnemyEffect>();
            NameChangeSimulacrum.namePoolID = "simulacrum";

            PercentageEffectCondition FiftyPercent = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            FiftyPercent.percentage = 50;

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition Previous2True = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2True.wasSuccessful = true;
            Previous2True.previousAmount = 2;

            PreviousEffectCondition Previous3True = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            Previous2True.wasSuccessful = true;
            Previous2True.previousAmount = 3;

            AnimationVisualsEffect CopyAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            CopyAnim._animationTarget = Targeting.Slot_SelfSlot;
            CopyAnim._visuals = CustomVisuals.StaticColorVisualsSO;

            DisplayPassiveChangeUIActionEffect CopyThatDisplay = ScriptableObject.CreateInstance<DisplayPassiveChangeUIActionEffect>();
            CopyThatDisplay.passiveName = "IconCopyThat";
            CopyThatDisplay.localPassive = true;

            Enemy simulacrum = new Enemy("Simulacrum", "Simulacrum_EN")
            {
                Health = 50,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/SimulacrumHurt",
                DeathSound = "event:/AAEnemy/SimulacrumDeath",
                CombatEnterEffects = [
                    Effects.GenerateEffect(NameChangeSimulacrum, 1, Targeting.Slot_SelfSlot)
                ]
            };
            simulacrum.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Simulacrum_Enemy/Simulacrum_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Simulacrum_Enemy/Simulacrum_Giblets.prefab").GetComponent<ParticleSystem>());

            Ability comeagain = new Ability("Come Again?", "AApocrypha_ComeAgain_A")
            {
                Description = "This enemy has a 50% chance to remove all abilities and passives granted by Copy That from itself, then trigger Copy That again.\n(Technically, it removes *all* passives besides the initial four.)",
                Cost = [],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SimulacrumWipeCopyEffect>(), 1, Targeting.Slot_SelfSlot, FiftyPercent),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CopyThatEffect>(), 2, Targeting.Unit_AllOpponents, PreviousTrue),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.ExtremelySlow,
            };
            comeagain.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            comeagain.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Passive_CopyThat"]);

            ExtraAbilityInfo comeagainextra = new()
            {
                ability = comeagain.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            simulacrum.AddPassives([Passives.Skittish3, Passives.MultiAttack2, CustomPassives.CopyThatGenerator(2), Passives.BonusAttackGenerator(comeagainextra)]);

            simulacrum.AddEnemy(false, false, false);

        }
    }
}
