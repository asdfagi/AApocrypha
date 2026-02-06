using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class UnravellingTime
    {
        public static void Add()
        {
            GenerateColorsByListManaEffect GiveRandomPigment = ScriptableObject.CreateInstance<GenerateColorsByListManaEffect>();
            GiveRandomPigment._manaColors = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue, Pigments.Yellow, Pigments.Yellow, Pigments.Purple];

            PerformEffectPassiveAbility randomBlooded2 = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            randomBlooded2.name = "Random4Blooded_2_PA";
            randomBlooded2._passiveName = "Random-Blooded (2)";
            randomBlooded2.m_PassiveID = "PigmentBlooded";
            randomBlooded2.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodPrimary");
            randomBlooded2._characterDescription = "Upon receiving direct damage this party member produces 2 additional pigment of the four basic colors.";
            randomBlooded2._enemyDescription = "Upon receiving direct damage this enemy produces 2 additional pigment of the four basic colors.";
            randomBlooded2._triggerOn = [TriggerCalls.OnDirectDamaged];
            randomBlooded2.doesPassiveTriggerInformationPanel = true;
            randomBlooded2.effects =
            [
                Effects.GenerateEffect(GiveRandomPigment, 2, Targeting.Slot_SelfSlot),
            ];

            StatusEffect_Apply_Effect Hasten = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Hasten._RandomBetweenPrevious = true;
            Hasten._Status = StatusField.GetCustomStatusEffect("Haste_ID");

            StatusEffect_Apply_Effect Stunnen = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Stunnen._RandomBetweenPrevious = true;
            Stunnen._Status = StatusField.Stunned;

            TargetPerformEffectViaSubaction UnravelSelf = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            UnravelSelf.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Hasten, 2, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Stunnen, 1, Targeting.Slot_SelfSlot),
            ];

            PerformEffectPassiveAbility weftUnravel = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            weftUnravel.name = "WeftUnravelling_PA";
            weftUnravel._passiveName = "Unravelling";
            weftUnravel.m_PassiveID = "Unravelling";
            weftUnravel.passiveIcon = ResourceLoader.LoadSprite("IconUnravelling");
            weftUnravel._characterDescription = "On death, apply 0-2 Haste and 0-1 Stunned to all party members.";
            weftUnravel._enemyDescription = "On death, apply 0-2 Haste and 0-1 Stunned to all enemies.";
            weftUnravel._triggerOn = [TriggerCalls.OnDeath];
            weftUnravel.doesPassiveTriggerInformationPanel = true;
            weftUnravel.effects =
            [
                Effects.GenerateEffect(UnravelSelf, 1, Targeting.Unit_OtherAllies),
            ];

            Enemy weft = new Enemy("Weft of Unravelling Time", "UnravellingTime_EN")
            {
                Health = 12,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("UnravellingTimeTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Spoggle_Resonant_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Spoggle_Resonant_EN").deathSound,
                UnitTypes = ["Parabolan"],
            };
            weft.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/UnravellingTime_Enemy/UnravellingTime_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/UnravellingTime_Enemy/UnravellingTime_Giblets.prefab").GetComponent<ParticleSystem>());
            weft.AddPassives([randomBlooded2, Passives.Withering, weftUnravel]);

            SpecificEnemiesTargeting NotWeft = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            NotWeft.blacklist = true;
            NotWeft._enemies = ["UnravellingTime_EN"];
            NotWeft.slotOffsets = [0];
            NotWeft.targetUnitAllySlots = true;
            NotWeft._excludeCaster = true;

            StatusEffect_Apply_Effect Hasten2 = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Hasten2._RandomBetweenPrevious = false;
            Hasten2._Status = StatusField.GetCustomStatusEffect("Haste_ID");

            StatusEffect_Apply_Effect Stunnen2 = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Stunnen2._RandomBetweenPrevious = false;
            Stunnen2._Status = StatusField.Stunned;
            Stunnen2._JustOneRandomTarget = true;

            AnimationVisualsEffect HastenAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            HastenAnim._visuals = Visuals.Genesis;
            HastenAnim._animationTarget = Targeting.Slot_SelfSlot;

            RandomTargetPerformEffectViaSubaction BoostEnemy = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            BoostEnemy.effects =
            [
                Effects.GenerateEffect(HastenAnim, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Hasten2, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Stunnen2, 1, NotWeft),
            ];

            RandomTargetPerformEffectViaSubaction BoostFool = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            BoostFool.effects =
            [
                Effects.GenerateEffect(HastenAnim, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Hasten2, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(Stunnen2, 1, Targeting.Unit_OtherAllies),
            ];

            Ability abilityEnemySide = new Ability("What Was, Will Be", "AApocrypha_WeftAbilityEnemy_A")
            {
                Description = "Apply 1 Haste to a random enemy and 1 Stunned to a random other enemy.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(BoostEnemy, 1, Targeting.Unit_OtherAllies),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.ExtremelySlow,
            };
            abilityEnemySide.AddIntentsToTarget(Targeting.Unit_OtherAllies, ["Status_Haste", nameof(IntentType_GameIDs.Status_Stunned)]);

            Ability abilityFoolSide = new Ability("What Will Be, Was", "AApocrypha_WeftAbilityFool_A")
            {
                Description = "Apply 1 Haste to a random party member and 1 Stunned to a random other party member.",
                Cost = [Pigments.Grey, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(BoostFool, 1, Targeting.Unit_AllOpponents),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.ExtremelySlow,
            };
            abilityFoolSide.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Haste", nameof(IntentType_GameIDs.Status_Stunned)]);

            weft.AddEnemyAbilities([
                abilityEnemySide,
                abilityFoolSide,
            ]);
            
            weft.AddEnemy(true, false, false);
        }
    }
}
