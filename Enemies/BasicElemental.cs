using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class BasicElemental
    {
        public static void Add()
        {
            Enemy basic = new Enemy("BASIC Elemental", "BasicElemental_EN")
            {
                Health = 30,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("BasicElementalTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("BasicElementalOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
                UnitTypes = ["Loathing"],
            };
            basic.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/BasicElemental_Enemy/BasicElemental_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/BasicElemental_Enemy/BasicElemental_Giblets.prefab").GetComponent<ParticleSystem>());
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                if (LoadedAssetsHandler.GetEnemy("Omission_EN") != null)
                {
                    basic.DamageSound = LoadedAssetsHandler.GetEnemy("Omission_EN").damageSound;
                    basic.DeathSound = LoadedAssetsHandler.GetEnemy("Omission_EN").deathSound;
                }
            }

            AnimationVisualsIfUnitEffect PunchAnim = ScriptableObject.CreateInstance<AnimationVisualsIfUnitEffect>();
            PunchAnim._visuals = Visuals.Clobber_Left;
            PunchAnim._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect PunchAlwaysAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            PunchAlwaysAnim._visuals = Visuals.Clobber_Left;
            PunchAlwaysAnim._animationTarget = Targeting.Slot_Front;

            Ability forloop = new Ability("FOR", "AApocrypha_BasicElementalFor_A")
            {
                Description = "10 LET dmg$ = 2" +
                "\n20 LET target$ = \"Opposing\"" +
                "\n30 FOR X = 1 TO 5" +
                "\n40 PRINT \"PUNCH!\"" +
                "\n50 NEXT X",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Clobber_Left,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(PunchAlwaysAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(PunchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(PunchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(PunchAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            forloop.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2), "AA_Multi5"]);

            Ability lrhit = new Ability("RND", "AApocrypha_BasicElementalRand_A")
            {
                Description = "10 LET dmg$ = 7\n" +
                "20 LET target$ = \"Opposing\"" +
                "\n30 LET rand$ = 2 * RND" +
                "\n40 IF INT(rand$) = 0 THEN LET move$ = \"Left\"" +
                "\n50 IF INT(rand$) = 1 THEN LET move$ = \"Right\"" +
                "\n60 PRINT move$" +
                "\n70 PRINT \"PUNCH!\"",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(PunchAlwaysAnim, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 7, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            lrhit.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides)]);
            lrhit.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability sorrynothing = new Ability("LOOP", "AApocrypha_BasicElementalLoop_A")
            {
                Description = "10 PRINT \"HELP\"" +
                "\n20 GOTO 10",
                Cost = [Pigments.Grey],
                Effects =
                [
                ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            sorrynothing.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["AA_Nothing"]);

            basic.AddEnemyAbilities([
                forloop.GenerateEnemyAbility(true),
                lrhit.GenerateEnemyAbility(true),
                sorrynothing.GenerateEnemyAbility(true),
            ]);

            GenericTargetting_BySlot_Index_Caster LeftAndCaster = ScriptableObject.CreateInstance<GenericTargetting_BySlot_Index_Caster>();
            LeftAndCaster.slotPointerDirections = [0];
            LeftAndCaster.getAllies = true;

            GenericTargetting_BySlot_Index_Caster CenterAndCaster = ScriptableObject.CreateInstance<GenericTargetting_BySlot_Index_Caster>();
            CenterAndCaster.slotPointerDirections = [2];
            CenterAndCaster.getAllies = true;

            GenericTargetting_BySlot_Index_Caster RightAndCaster = ScriptableObject.CreateInstance<GenericTargetting_BySlot_Index_Caster>();
            RightAndCaster.slotPointerDirections = [4];
            RightAndCaster.getAllies = true;

            Ability swapL = new Ability("GOTO 10", "AApocrypha_BasicElementalSwapL_A")
            {
                Description = "Swap this enemy to the Leftmost position.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, LeftAndCaster),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            swapL.AddIntentsToTarget(LeftAndCaster, [nameof(IntentType_GameIDs.Swap_Mass)]);

            Ability swapC = new Ability("GOTO 30", "AApocrypha_BasicElementalSwapC_A")
            {
                Description = "Swap this enemy to the Center position.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, CenterAndCaster),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            swapC.AddIntentsToTarget(CenterAndCaster, [nameof(IntentType_GameIDs.Swap_Mass)]);

            Ability swapR = new Ability("GOTO 50", "AApocrypha_BasicElementalSwapR_A")
            {
                Description = "Swap this enemy to the Rightmost position.",
                Cost = [Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapTwoTargetsEffect>(), 1, RightAndCaster),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            swapR.AddIntentsToTarget(RightAndCaster, [nameof(IntentType_GameIDs.Swap_Mass)]);

            ExtraAbilityInfo extraL = new()
            {
                ability = swapL.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo extraC = new()
            {
                ability = swapC.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo extraR = new()
            {
                ability = swapR.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            basic.AddPassives([Passives.Unstable, Passives.GetCustomPassive("Random4Blooded_2_PA"), CustomPassives.BonusSuiteGenerator([extraL, extraC, extraR])]);

            basic.AddEnemy(true, false, false);
        }
    }
}
