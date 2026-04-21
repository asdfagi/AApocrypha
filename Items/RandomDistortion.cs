using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Assets;
using BrutalAPI;
using BrutalAPI.Items;
using static A_Apocrypha.Items.RandomDistortion;

namespace A_Apocrypha.Items
{
    public class RandomDistortion
    {
        public static void Add()
        {
            // INITIAL VALUES
            int complexity = UnityEngine.Random.Range(0, 11);
            int itemtype = UnityEngine.Random.Range(1, 4); // 1 = bad for enemies | 2 = good for allies | 3 = untargeted nonsense
            /*if (UnityEngine.Random.Range(0, 1) == 0)
            {
                itemtype = UnityEngine.Random.Range(1, 3);
            }*/

            // DEFINE EFFECTS
            IncreaseStatusEffectsEffect IncreaseBadStatus = ScriptableObject.CreateInstance<IncreaseStatusEffectsEffect>();
            IncreaseBadStatus._increasePositives = false;

            StatusEffect_Apply_Effect FrailApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            FrailApply._Status = StatusField.Frail;

            StatusEffect_Apply_Effect ScarsApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ScarsApply._Status = StatusField.Scars;

            StatusEffect_Apply_Effect RupturedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApply._Status = StatusField.Ruptured;

            FieldEffect_Apply_Effect HoarfrostApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            HoarfrostApply._Field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

            FieldEffect_Apply_Effect ShieldApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldApply._Field = StatusField.Shield;

            SwapToOneSideEffect SwapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapLeft._swapRight = false;

            SwapToOneSideEffect SwapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            SwapRight._swapRight = true;

            DamageEffect IndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            IndirectDamage._indirect = true;

            HealEffect IndirectHeal = ScriptableObject.CreateInstance<HealEffect>();
            IndirectHeal._directHeal = false;

            RandomTargetPerformEffectViaSubaction TheRupturer = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            TheRupturer.effects = [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<SpawnCasterGibsEffect>()),
                Effects.GenerateEffect(RupturedApply, 2, Targeting.Slot_SelfSlot),
            ];

            GainLootOneOfCustomCharactersEffect GnomeReward = ScriptableObject.CreateInstance<GainLootOneOfCustomCharactersEffect>();
            GnomeReward._characterCopies = ["Gnome_CH", "GnomePurple_CH", "GnomeBlue_CH", "GnomeGreen_CH"];
            GnomeReward._rank = 0;
            GnomeReward._nameAddition = new NameAdditionLocID();

            // COMPOSE EFFECT AND TRIGGER LISTS
            List<DistortionEffect> effectList = new List<DistortionEffect>();

            List<DistortionTrigger> triggerList = new List<DistortionTrigger>();

            if (itemtype == 1)
            {
                // Effects
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 3, Targeting.Slot_Front),
                    ],
                    "deal 3 damage to the Opposing enemy."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentLeft),
                    ],
                    "deal 5 damage to the Left enemy."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 5, Targeting.Slot_OpponentRight),
                    ],
                    "deal 5 damage to the Right enemy."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScarsApply, 1, Targeting.Slot_OpponentSides),
                    ],
                    "apply 1 Scar to the Left and Right enemies."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScarsApply, 2, Targeting.Slot_OpponentLeft),
                    ],
                    "apply 2 Scars to the Left enemy."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(FrailApply, 2, Targeting.Slot_OpponentRight),
                    ],
                    "apply 2 Frail to the Right enemy."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(SwapLeft, 1, Targeting.Slot_SelfSlot),
                    ],
                    "move this party member and the Opposing enemy to the Left."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 1, Targeting.Slot_Front),
                    ],
                    "deal 1 damage to the Opposing enemy 4 times."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(HoarfrostApply, 1, Targeting.GenerateSlotTarget([0, -1], false)),
                    ],
                    "apply 1 Hoarfrost to the Opposing and Left enemy positions."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<PerformRandomAbilityFromCharacterEffect>(), 1, Targeting.Slot_SelfSlot),
                    ],
                    "make this party member perform a random fool ability."
                ));

                // Triggers
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnDamaged,
                    "When this party member takes damage, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnAbilityUsed,
                    "When this party member uses an ability, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnTurnFinished,
                    "On turn end, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnAnyoneBeingHealed,
                    "When any unit is healed, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnAbilityWillBeUsed,
                    "Before this party member uses an ability, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnDirectHealed,
                    "When this party member is directly healed, "
                ));
            }
            else if (itemtype == 2)
            {
                // Effects
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(IndirectHeal, 2, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<FuckingAliveEffectCondition>()),
                    ],
                    "indirectly heal this party member 2 health."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(IndirectHeal, 2, Targeting.Slot_AllyLeft, ScriptableObject.CreateInstance<FuckingAliveEffectCondition>()),
                    ],
                    "indirectly heal the Left party member 2 health."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(IndirectHeal, 2, Targeting.Slot_AllyFarSides, ScriptableObject.CreateInstance<FuckingAliveEffectCondition>()),
                    ],
                    "indirectly heal the Far Left and Far Right party members 2 health."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<PerformRandomAbilityFromCharacterEffect>(), 1, Targeting.Slot_SelfSlot),
                    ],
                    "make this party member perform a random fool ability."
                ));

                // Triggers
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnDamaged,
                    "When this party member takes damage, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnAbilityUsed,
                    "When this party member uses an ability, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnTurnFinished,
                    "On turn end, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnDirectHealed,
                    "When this party member is directly healed, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnAbilityWillBeUsed,
                    "Before this party member uses an ability, "
                ));
                triggerList.Add(new DistortionTrigger(
                    TriggerCalls.OnDidApplyDamage,
                    "When this party member deals damage, "
                ));
            }
            else if (itemtype == 3)
            {
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(TheRupturer, 1, Targeting.AllUnits),
                    ],
                    "make a random unit apply 2 Ruptured to itself."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<LuckyBlueAmountSetEffect>(), 1, Targeting.Slot_SelfSlot),
                    ],
                    "increase the amount of pigment gained from Lucky Pigment by 1."
                ));
                effectList.Add(new DistortionEffect(
                    true,
                    [
                        Effects.GenerateEffect(GnomeReward, 1, Targeting.Slot_SelfSlot),
                    ],
                    "add a Gnome to this combat's rewards."
                ));
            }

            string[] names1 = [
                "{0}",
                "{0}",
                "{0}",
                "{0}",
                "Tainted {0}",
                "{0} of Doom",
                "!!{0}!!",
                "My {0} is full of eels.",
                "{0}{0}",
                "Liver of {0}",
                "{0}^2",
                "█████ {0}",
                "\"{0}\"",
                "<color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">{0}</color>",
            ];
            string[] names1double = [
                "{0}{1}",
                "{1}{0}",
                "My {0} is sometimes {1}",
            ];
            string[] names2 = [
                "Distortion",
                "Distortortortor",
                "noitrotsiD",
                "DisDisDison",
                "The",
                "AnomalolamonA",
                "_",
                "sometimes, I wish",
                "cement",
                "Insider",
                "███████",
                "{0}",
                "Infested?",
                "S AND FLIES",
                "E",
            ];

            string name = (UnityEngine.Random.Range(0, 5) == 0 ? string.Format(names1double[UnityEngine.Random.Range(0, names1.Length)], names2[UnityEngine.Random.Range(0, names2.Length)], names2[UnityEngine.Random.Range(0, names2.Length)]) : string.Format(names1[UnityEngine.Random.Range(0, names1.Length)], names2[UnityEngine.Random.Range(0, names2.Length)]));

            string[] descriptions = [
                "ing.Format(names1double[UnityEngine.Rand",
                "ity.ToHtmlStringRGB(Color.green) + \">",
                "erformEffect(CombatStats stats, IUnit c",
                "opPrice = UnityEngine.Random.Ra",
                "dAddCustom_DialogueSO(text, yarnProgra",
                "void TellMeDamnYou(IMinimalZoneInfoD",
                "ption = \"Remove all Shield from th",
                "obias/PhobiasRoar\"; string deat",
                "ing.Slot_SelfSlot, Effects.CheckMulti",
            ];

            DistortionEffect effect1 = effectList[UnityEngine.Random.Range(0, effectList.Count)];
            DistortionTrigger trigger1 = triggerList[UnityEngine.Random.Range(0, triggerList.Count)];

            DistortionEffect effect2 = effectList[UnityEngine.Random.Range(0, effectList.Count)];
            DistortionTrigger trigger2 = triggerList[UnityEngine.Random.Range(0, triggerList.Count)];

            Dictionary<int, WearableStaticModifierSetterSO> modifiers = new Dictionary<int, WearableStaticModifierSetterSO>();
            Dictionary<int, string> modifierTexts = new Dictionary<int, string>();

            AddPassiveModifier(modifiers, modifierTexts, Passives.Skittish.name, "This party member now has Skittish as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.Slippery.name, "This party member now has Slippery as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.Construct.name, "This party member now has Construct as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.Inferno.name, "This party member now has Inferno as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.BoneSpurs3.name, "This party member now has Bone Spurs (3) as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.Unstable.name, "This party member now has Unstable as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, Passives.Withering.name, "This party member now has Withering as a passive.");
            AddHealthColorModifier(modifiers, modifierTexts, Pigments.Red.name, "This party member's health is now red.");
            AddHealthColorModifier(modifiers, modifierTexts, "Broken", "This party member's health is now broken.");
            AddPassiveModifier(modifiers, modifierTexts, "Omnichromia_PA", "This party member now has Omnichromia as a passive.");
            AddPassiveModifier(modifiers, modifierTexts, "JollyJoker_PA", "This party member is feeling rather jolly.");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                AddPassiveModifier(modifiers, modifierTexts, "Vandal_PA", "This party member now has Vandal as a passive.");
                AddHealthColorModifier(modifiers, modifierTexts, "Clusterfuck", "This party member's health is now clusterfuck.");
                AddHealthColorModifier(modifiers, modifierTexts, "Iridescent", "This party member's health is now iridescent.");
            }

            DoublePerformEffect_Item distortion = new DoublePerformEffect_Item("RandomDistortion_ID", null, false)
            {
                Item_ID = "RandomDistortion_SW",
                Name = name,
                Flavour = "\"" + descriptions[UnityEngine.Random.Range(0, descriptions.Length)] + "\"",
                Description = trigger1.Description + effect1.Description,
                IsShopItem = true,
                ShopPrice = UnityEngine.Random.Range(1, 11),
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockKatalixiNaudiz4_" + UnityEngine.Random.Range(1, 8)),
                DoesPopUpInfo = effect1.Popup,
                TriggerOn = trigger1.Trigger,
                Effects = effect1.Effects,
                EquippedModifiers = [],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [],
                SecondaryEffects = [],
                OnUnlockUsesTHE = true,
            };
            if (complexity >= 7)
            {
                distortion.item._description += "\n" + trigger2.Description + effect2.Description;
                distortion.item._secondDoesPerformItemPopUp = effect2.Popup;
                distortion.item._secondPerformTriggersOn = [trigger2.Trigger];
                distortion.item._secondEffects = effect2.Effects;
            }
            if (complexity <= 5)
            {
                int modIndex = UnityEngine.Random.Range(0, modifiers.Count);
                distortion.item._description += "\n" + modifierTexts[modIndex];
                distortion.EquippedModifiers = [modifiers[modIndex]];
            }

            Debug.Log($"Distortion Data | {distortion.Item._itemName} (Price: {distortion.Item.shopPrice}) - \"{distortion.Item._flavourText}\" - {distortion.Item._description} - Generated with itemtype {itemtype} at complexity {complexity}");

            string achievementID = "AApocrypha_Naudiz4_Boundary_ACH";
            string unlockID = "AApocrypha_Naudiz4_Boundary_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(distortion.item, new ItemModdedUnlockInfo(distortion.Item_ID, ResourceLoader.LoadSprite("UnlockKatalixiNaudiz4Locked", null, 32, null), achievementID));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, distortion.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [distortion.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Reality Distortion", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementKatalixiNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }

        public class DistortionEffect
        {
            public DistortionEffect(/*int value, */bool popup, EffectInfo[] effects, string description)
            {
                //Value = value;
                Popup = popup;
                Effects = effects;
                Description = description;
            }
            //public int Value { get; }
            public bool Popup { get; }
            public EffectInfo[] Effects { get; }
            public string Description { get; }
        }

        public class DistortionTrigger
        {
            public DistortionTrigger(TriggerCalls trigger, string description)
            {
                Trigger = trigger;
                Description = description;
            }
            public TriggerCalls Trigger { get; }
            public string Description { get; }
        }

        public static void AddModifier(Dictionary<int, WearableStaticModifierSetterSO> modifiersList, Dictionary<int, string> modifiersTextList, WearableStaticModifierSetterSO wearable, string desc)
        {
            int counter = modifiersList.Count;
            modifiersList.Add(counter, wearable);
            modifiersTextList.Add(counter, desc);
        }

        public static void AddPassiveModifier(Dictionary<int, WearableStaticModifierSetterSO> modifiersList, Dictionary<int, string> modifiersTextList, string passiveID, string desc)
        {
            if (Passives.GetCustomPassive(passiveID) == null) { return; }
            ExtraPassiveAbility_Wearable_SMS wearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearable._extraPassiveAbility = Passives.GetCustomPassive(passiveID);
            AddModifier(modifiersList, modifiersTextList, wearable, desc);
        }

        public static void AddHealthColorModifier(Dictionary<int, WearableStaticModifierSetterSO> modifiersList, Dictionary<int, string> modifiersTextList, string color, string desc)
        {
            if (LoadedDBsHandler.PigmentDB.GetPigment(color) == null) { return; }
            HealthColorChange_Wearable_SMS wearable = ScriptableObject.CreateInstance<HealthColorChange_Wearable_SMS>();
            wearable._healthColor = LoadedDBsHandler.PigmentDB.GetPigment(color);
            AddModifier(modifiersList, modifiersTextList, wearable, desc);
        }
    }
}
