using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Enemies
{
    public class MachineGnomes
    {
        public static void Add()
        {
            ChangeToRandomHealthColorEffect randomize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            randomize._healthColors =
            [
                        Pigments.Blue,
                        Pigments.Red,
                        Pigments.Yellow,
                        Pigments.Purple
            ];

            GainLootOneOfCustomCharactersEffect GnomeReward = ScriptableObject.CreateInstance<GainLootOneOfCustomCharactersEffect>();
            GnomeReward._characterCopies = ["Gnome_CH", "GnomePurple_CH", "GnomeBlue_CH", "GnomeGreen_CH"];
            GnomeReward._rank = 0;
            GnomeReward._nameAddition = new NameAdditionLocID();

            Enemy gnomes = new Enemy("Machine Gnomes", "MachineGnomes_EN")
            {
                Health = 50,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("GnomesTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("GnomesOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/GnomesHurt",
                DeathSound = "event:/AAEnemy/GnomesDeath",
                CombatEnterEffects =
                [
                    Effects.GenerateEffect(randomize, 1, Targeting.Slot_SelfSlot),
                ],
                CombatExitEffects = 
                [
                    Effects.GenerateEffect(GnomeReward, 1, Targeting.Slot_SelfSlot),    
                ],
            };
            gnomes.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Gnomes_Enemy/Gnomes_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Gnomes_Enemy/Gnomes_Giblets.prefab").GetComponent<ParticleSystem>());

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            SpawnEnemyAnywhereWithHealthByPreviousEffect GnomeSpawn = ScriptableObject.CreateInstance<SpawnEnemyAnywhereWithHealthByPreviousEffect>();
            GnomeSpawn.enemy = gnomes.enemy;
            GnomeSpawn._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            SpecificUnitsByPassiveTargeting AllGnomes = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            AllGnomes._passive = Passives.GetCustomPassive("Gnome_PA");
            AllGnomes.targetUnitAllySlots = true;
            AllGnomes.slotOffsets = [0];

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            CopyAndSpawnOneOfCustomCharactersAnywhereEffect GnomePartyJoin = ScriptableObject.CreateInstance<CopyAndSpawnOneOfCustomCharactersAnywhereEffect>();
            GnomePartyJoin._characterCopies = ["Gnome_CH", "GnomePurple_CH", "GnomeBlue_CH", "GnomeGreen_CH"];
            GnomePartyJoin._permanentSpawn = false;
            GnomePartyJoin._rank = 0;
            GnomePartyJoin._usePreviousAsHealth = false;
            GnomePartyJoin._extraModifiers = [];
            GnomePartyJoin._nameAddition = new NameAdditionLocID();

            ExtraVariableForNextEffect Blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            CheckIsAliveMultiplyByEntryOrPreviousEffect IsUnitPassPrevious = ScriptableObject.CreateInstance<CheckIsAliveMultiplyByEntryOrPreviousEffect>();
            IsUnitPassPrevious._usePreviousExitValue = true;

            Ability splitgroup = new Ability("Split the Group", "AApocrypha_SplitGroup_A")
            {
                Description = "Deal a Painful amount of indirect damage to this enemy. If the damage did not kill, spawn a horde of Machine Gnomes with maximum health equal to twice the damage dealt.",
                Cost = [],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(IndirectDamage, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(IsUnitPassPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GnomeSpawn, 2, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            splitgroup.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6)]);
            splitgroup.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);

            Ability dance = new Ability("Dance, Dance!", "AApocrypha_Dance_A")
            {
                Description = "Move All gnomes to the Left or Right.",
                Cost = [],
                Visuals = Visuals.Tango,
                AnimationTarget = AllGnomes,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, AllGnomes),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            dance.AddIntentsToTarget(AllGnomes, [nameof(IntentType_GameIDs.Swap_Sides)]);

            Ability playwiththem = new Ability("Play With Them", "AApocrypha_PlayWithThem_A")
            {
                Description = "If there is an empty slot on the party's side, deal a Painful amount of indirect damage to this enemy and send a Gnome to join the party.",
                Cost = [],
                Visuals = CustomVisuals.StaticColorVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([0], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([1], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([2], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([3], false)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.GenerateGenericTarget([4], false)),
                    Effects.GenerateEffect(Blank, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, true, true, true, true], [1, 2, 3, 4, 5])),
                    Effects.GenerateEffect(IndirectDamage, 5, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(GnomePartyJoin, 1, Targeting.Generic_Opponent_Middle, PreviousTrue),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal,
            };
            playwiththem.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_Spawn)]);
            playwiththem.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6)]);

            gnomes.AddPassives([Passives.Slippery, Passives.GetCustomPassive("AA_Heterochromia_PA"), Passives.GetCustomPassive("Gnome_PA")]);

            gnomes.AddEnemyAbilities(
            [
                splitgroup.GenerateEnemyAbility(true),
                dance.GenerateEnemyAbility(true),
                playwiththem.GenerateEnemyAbility(true),
            ]);

            gnomes.AddEnemy(true, true, true);
        }
    }
}
