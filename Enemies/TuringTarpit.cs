using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using UnityEngine.Rendering.PostProcessing;

namespace A_Apocrypha.Enemies
{
    public class TuringTarpit
    {
        public static void Add()
        {
            if (!AApocrypha.CrossMod.pigmentIridescent || !AApocrypha.CrossMod.pigmentEntropic || !AApocrypha.CrossMod.pigmentClusterfuck || !AApocrypha.CrossMod.pigmentWhite) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Malfunction_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Atrophy_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Collapse_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Enamored_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Disjunct_ID")) { return; }

            ChangeToRandomHealthColorEffect nowDoTheEsotericShuffle = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            nowDoTheEsotericShuffle._healthColors = [
                LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                LoadedDBsHandler.PigmentDB.GetPigment("White")
            ];

            ChangeToRandomHealthColorEffect iridize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            iridize._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")];

            ChangeToRandomHealthColorEffect clusterize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            clusterize._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")];

            ChangeToRandomHealthColorEffect entropize = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            entropize._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase")];

            ChangeToRandomHealthColorEffect bleach = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            bleach._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("White")];

            CasterStoredValueSetEffect LockstepDirSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            LockstepDirSet._valueName = "LockstepDir_SV";

            CasterStoredValueSetEffect LockstepNumSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            LockstepNumSet._valueName = "LockstepAmount_SV";

            Enemy turing = new Enemy("Turing Tarpit", "TuringTarpit_EN")
            {
                Health = 200,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("White"),
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("TuringTarpitTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TuringTarpitOverworld", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Turing/TuringHurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
                UnitTypes = ["Robot"],
                AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_NoDuplicate>(),
                CombatEnterEffects = [
                    Effects.GenerateEffect(LockstepDirSet, 1),
                    Effects.GenerateEffect(LockstepNumSet, 1),
                ]
            };
            turing.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/TuringTarpit_Enemy/TuringTarpit_Enemy.prefab", AApocrypha.assetBundle, null);//AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/BFElemental_Enemy/BFElemental_Giblets.prefab").GetComponent<ParticleSystem>());
            turing.AddPassives([CustomPassives.SaltLockstepGenerator(1), Passives.MultiAttack2, Passives.GetCustomPassive("AA_PoorFacedICEW_PA"), Passives.Leaky1]);
            if (LoadedAssetsHandler.GetEnemy("Omission_EN") != null)
            {
                turing.DeathSound = LoadedAssetsHandler.GetEnemy("Omission_EN").deathSound;
            }

            SpecificOpponentsByHealthColorIDTargeting AllIridescent = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorIDTargeting>();
            AllIridescent._colorID = "Iridescent";
            AllIridescent.getAllUnitSelfSlots = false;
            AllIridescent.targetUnitAllySlots = true;
            AllIridescent.slotOffsets = [0];

            SpecificOpponentsByHealthColorIDTargeting AllEntropic = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorIDTargeting>();
            AllEntropic._colorID = "EntropicBase";
            AllEntropic.getAllUnitSelfSlots = false;
            AllEntropic.targetUnitAllySlots = true;
            AllEntropic.slotOffsets = [0];

            SpecificOpponentsByHealthColorIDTargeting AllClusterfuck = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorIDTargeting>();
            AllClusterfuck._colorID = "Clusterfuck";
            AllClusterfuck.getAllUnitSelfSlots = false;
            AllClusterfuck.targetUnitAllySlots = true;
            AllClusterfuck.slotOffsets = [0];

            SpecificOpponentsByHealthColorIDTargeting AllWhite = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorIDTargeting>();
            AllWhite._colorID = "White";
            AllWhite.getAllUnitSelfSlots = false;
            AllWhite.targetUnitAllySlots = true;
            AllWhite.slotOffsets = [0];

            SpecificOpponentsByMultipleHealthColorsTargeting AllOther = ScriptableObject.CreateInstance<SpecificOpponentsByMultipleHealthColorsTargeting>();
            AllOther._colors = [
                LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                LoadedDBsHandler.PigmentDB.GetPigment("White"),
            ];
            AllOther._contains = false;
            AllOther.getAllUnitSelfSlots = false;
            AllOther.targetUnitAllySlots = true;
            AllOther.slotOffsets = [0];
            AllOther.blacklist = true;

            StatusEffect_Apply_Effect ApplyMalf = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyMalf._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");

            StatusEffect_Apply_Effect ApplyAtrophy = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyAtrophy._Status = StatusField.GetCustomStatusEffect("Atrophy_ID");

            StatusEffect_Apply_Effect ApplyCollapse = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyCollapse._Status = StatusField.GetCustomStatusEffect("Collapse_ID");

            StatusEffect_Apply_Effect ApplyEnamored = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyEnamored._Status = StatusField.GetCustomStatusEffect("Enamored_ID");

            StatusEffect_Apply_Effect ApplyDisjunct = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyDisjunct._Status = StatusField.GetCustomStatusEffect("Disjunct_ID");

            AttackVisualsSO GlitchVisuals = LoadedAssetsHandler.GetCharacterAbility("SamDefrag_A").visuals;

            //Velato Piet Brainfuck Whitespace

            Ability frontirid = new Ability("Velato", "AApocrypha_TuringIridescent_A")
            {
                Description = "Change the Opposing party members' health color to Iridescent. Apply 4 Enamored to each target whose health color did not change.",
                Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Iridescent")],
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(iridize, 1, Targeting.BigEnemy_Front_Offset_0),
                    Effects.GenerateEffect(ApplyEnamored, 4, Targeting.BigEnemy_Front_Offset_0, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(iridize, 1, Targeting.BigEnemy_Front_Offset_1),
                    Effects.GenerateEffect(ApplyEnamored, 4, Targeting.BigEnemy_Front_Offset_1, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            frontirid.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Pigment_Transform", nameof(IntentType_GameIDs.Misc_Hidden), "Status_Enamored"]);

            Ability frontcluster = new Ability("Piet", "AApocrypha_TuringClusterfuck_A")
            {
                Description = "Change the Opposing party members' health color to Clusterfuck. Apply 4 Malfunction to each target whose health color did not change.",
                Cost = [LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck")],
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(clusterize, 1, Targeting.BigEnemy_Front_Offset_0),
                    Effects.GenerateEffect(ApplyMalf, 4, Targeting.BigEnemy_Front_Offset_0, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(clusterize, 1, Targeting.BigEnemy_Front_Offset_1),
                    Effects.GenerateEffect(ApplyMalf, 4, Targeting.BigEnemy_Front_Offset_1, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            frontcluster.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Pigment_Transform", nameof(IntentType_GameIDs.Misc_Hidden), "Status_Malfunction"]);

            Ability frontentropic = new Ability("Brainfuck", "AApocrypha_TuringEntropic_A")
            {
                Description = "Change the Opposing party members' health color to Entropic. Apply 4 Collapse to each target whose health color did not change.",
                Cost = [LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase")],
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(entropize, 1, Targeting.BigEnemy_Front_Offset_0),
                    Effects.GenerateEffect(ApplyCollapse, 4, Targeting.BigEnemy_Front_Offset_0, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(entropize, 1, Targeting.BigEnemy_Front_Offset_1),
                    Effects.GenerateEffect(ApplyCollapse, 4, Targeting.BigEnemy_Front_Offset_1, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            frontentropic.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Pigment_Transform", nameof(IntentType_GameIDs.Misc_Hidden), "Status_Collapse"]);

            Ability frontwhite = new Ability("Whitespace", "AApocrypha_TuringWhite_A")
            {
                Description = "Change the Opposing party members' health color to White. Apply 4 Atrophy to each target whose health color did not change.",
                Cost = [LoadedDBsHandler.PigmentDB.GetPigment("White")],
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(bleach, 1, Targeting.BigEnemy_Front_Offset_0),
                    Effects.GenerateEffect(ApplyAtrophy, 4, Targeting.BigEnemy_Front_Offset_0, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(bleach, 1, Targeting.BigEnemy_Front_Offset_1),
                    Effects.GenerateEffect(ApplyAtrophy, 4, Targeting.BigEnemy_Front_Offset_1, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            frontwhite.AddIntentsToTarget(Targeting.Slot_Front, ["AA_Pigment_Transform", nameof(IntentType_GameIDs.Misc_Hidden), "Status_Atrophy"]);

            Ability nothingiseasy = new Ability("Nothing is Easy", "AApocrypha_TuringStatus_A")
            {
                Description = "Apply 4 of a status effect to each party member, determined by their health color." +
                "\nIridescent => Enamored" +
                "\nClusterfuck => Malfunction" +
                "\nEntropic => Collapse" +
                "\nWhite => Atrophy" +
                "\nOther => Disjunct",
                Cost = [Pigments.Grey],
                Visuals = ITAVisuals.Divide,
                AnimationTarget = Targeting.Unit_AllOpponents,
                Effects =
                [
                    Effects.GenerateEffect(ApplyEnamored, 4, AllIridescent),
                    Effects.GenerateEffect(ApplyMalf, 4, AllClusterfuck),
                    Effects.GenerateEffect(ApplyCollapse, 4, AllEntropic),
                    Effects.GenerateEffect(ApplyAtrophy, 4, AllWhite),
                    Effects.GenerateEffect(ApplyDisjunct, 4, AllOther),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            nothingiseasy.AddIntentsToTarget(AllIridescent, ["Status_Enamored"]);
            nothingiseasy.AddIntentsToTarget(AllClusterfuck, ["Status_Malfunction"]);
            nothingiseasy.AddIntentsToTarget(AllEntropic, ["Status_Collapse"]);
            nothingiseasy.AddIntentsToTarget(AllWhite, ["Status_Atrophy"]);
            nothingiseasy.AddIntentsToTarget(AllOther, ["Status_Disjunct"]);

            ExtraAbilityInfo nothingextra = new()
            {
                ability = nothingiseasy.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            turing.AddPassive(Passives.BonusAttackGenerator(nothingextra));

            turing.AddEnemyAbilities([
                frontirid.GenerateEnemyAbility(true),
                frontcluster.GenerateEnemyAbility(true),
                frontentropic.GenerateEnemyAbility(true),
                frontwhite.GenerateEnemyAbility(true),
            ]);
            turing.AddEnemy(false, false, false);
        }
    }
}
