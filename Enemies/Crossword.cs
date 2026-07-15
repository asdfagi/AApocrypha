using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class Crossword
    {
        public static void Add()
        {
            CasterEnemyVariantHandlerEffect CrosswordVariantHandler = ScriptableObject.CreateInstance<CasterEnemyVariantHandlerEffect>();
            CrosswordVariantHandler._variantNumber = 4;

            CasterStoredValueSetEffect LockstepDirSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            LockstepDirSet._valueName = "LockstepDir_SV";

            CasterStoredValueSetEffect LockstepNumSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            LockstepNumSet._valueName = "LockstepAmount_SV";

            AttackVisualsSO NullVisuals = CustomVisuals.Nothing;
            if (LoadedAssetsHandler.GetEnemy("Continuum_BOSS") != null) { NullVisuals = LoadedAssetsHandler.GetEnemyAbility("ContLeft_A").visuals; }

            AttackVisualsSO GlitchVisuals = ITAVisuals.Divide;
            if (LoadedAssetsHandler.GetCharacter("Sam_CH") != null) { GlitchVisuals = LoadedAssetsHandler.GetCharacterAbility("SamDefrag_A").visuals; }

            Enemy crossword = new Enemy("Crossword", "Crossword_EN")
            {
                Health = 30,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("CrosswordTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("CrosswordTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                CombatEnterEffects = [
                    Effects.GenerateEffect(CrosswordVariantHandler),
                    Effects.GenerateEffect(LockstepDirSet, 1),
                    Effects.GenerateEffect(LockstepNumSet, 3),
                ],
            };
            crossword.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Crossword_Enemy/Crossword_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Macerator_Enemy/Macerator_Giblets.prefab").GetComponent<ParticleSystem>());
            crossword.AddPassives([Passives.GetCustomPassive("Random4Blooded_2_PA"), CustomPassives.SaltLockstepGenerator(3)]);

            DirectDeathEffect obliterate = ScriptableObject.CreateInstance<DirectDeathEffect>();
            obliterate._obliterationDeath = true;
            obliterate._ExitValueIsHealthRemaining = false;
            obliterate._killUnderMaxHealth = false;

            Ability burglar = new Ability("Noun, 7 Across: A person who illegally enters buildings to steal things.", "AApocrypha_Burglar_A")
            {
                Description = "Steal every instance of a random letter from the Left, Right and Opposing party members' names, chosen per party member." +
                "\nIf this renders a party member nameless, obliterate them.",
                Cost = [Pigments.Grey],
                Visuals = NullVisuals,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterStealLettersFromTargetEffect>(), 1, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(CrosswordVariantHandler),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckTargetHasEmptyNameEffect>(), 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(obliterate, 1, Targeting.Slot_OpponentLeft, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckTargetHasEmptyNameEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(obliterate, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckTargetHasEmptyNameEffect>(), 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(obliterate, 1, Targeting.Slot_OpponentRight, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
            };
            burglar.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);
            burglar.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death)]);

            StatusEffect_ApplyByPrevious_Effect Malfunctionize = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            Malfunctionize._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");

            Ability malfunction = new Ability("Verb, 11 Down: To fail to function or work properly.", "AApocrypha_CrosswordMalfunction_A")
            {
                Description = "Apply Malfunction to the Left, Right and Opposing party members equal to half the length of this enemy's name, rounded down.",
                Cost = [Pigments.Grey],
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterGetNameLengthPercentageEffect>()),
                    Effects.GenerateEffect(Malfunctionize, 1, Targeting.Slot_FrontAndSides, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            malfunction.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Malfunction"]);

            StatusEffect_ApplyByPrevious_Effect Collapsify = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            Collapsify._Status = StatusField.GetCustomStatusEffect("Collapse_ID");

            Ability singularity = new Ability("Noun, 11 Down: A point at which space and time are infinitely distorted by gravitational forces.", "AApocrypha_CrosswordSingularity_A")
            {
                Description = "Apply Collapse to the Left, Right and Opposing party members equal to half the length of this enemy's name, rounded down.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterGetNameLengthPercentageEffect>()),
                    Effects.GenerateEffect(Collapsify, 1, Targeting.Slot_FrontAndSides, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            singularity.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Collapse"]);

            StatusEffect_ApplyByPrevious_Effect Atrophize = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            Atrophize._Status = StatusField.GetCustomStatusEffect("Atrophy_ID");

            Ability annihilate = new Ability("Verb, 10 Across: To do away with entirely so that nothing remains.", "AApocrypha_CrosswordAnnihilate_A")
            {
                Description = "Apply Atrophy to the Left, Right and Opposing party members equal to half the length of this enemy's name, rounded down.",
                Cost = [Pigments.Grey],
                Visuals = Visuals.Crush,
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterGetNameLengthPercentageEffect>()),
                    Effects.GenerateEffect(Atrophize, 1, Targeting.Slot_FrontAndSides, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            annihilate.AddIntentsToTarget(Targeting.Slot_FrontAndSides, ["Status_Atrophy"]);

            ExtraAbilityInfo burglarextra = new()
            {
                ability = burglar.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            crossword.AddPassive(Passives.BonusAttackGenerator(burglarextra));

            crossword.AddEnemyAbilities([
                malfunction.GenerateEnemyAbility(true),
                singularity.GenerateEnemyAbility(true),
                annihilate.GenerateEnemyAbility(true),
            ]);

            crossword.AddEnemy(false, false, false);
        }
    }
}
