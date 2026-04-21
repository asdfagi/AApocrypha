using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class SerpentEffigy
    {
        public static void Add()
        {
            string text = "Kneynsberg_EffigyHate_Dialogue";
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/KneynsbergEffigyScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            DialogueSO dialogueObject = Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Kneynsberg.Effigy");
            
            StartDialogueConversationEffect KneynsbergEffigyInit = ScriptableObject.CreateInstance<StartDialogueConversationEffect>();
            KneynsbergEffigyInit._dialogue = dialogueObject;

            PercentageEffectCondition OneInTen = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            OneInTen.percentage = 10;

            PercentageEffectorCondition OneIn5_2 = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            OneIn5_2.triggerPercentage = 20;

            PercentageEffectorCondition TwoInThree = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            TwoInThree.triggerPercentage = 66;

            IsSpecificCharacterEffectorCondition IsKneynsberg = ScriptableObject.CreateInstance<IsSpecificCharacterEffectorCondition>();
            IsKneynsberg._passIfTrue = true;
            IsKneynsberg._character = "Kneynsberg_CH";

            IsSpecificCharacterEffectorCondition IsntKneynsberg = ScriptableObject.CreateInstance<IsSpecificCharacterEffectorCondition>();
            IsntKneynsberg._passIfTrue = false;
            IsntKneynsberg._character = "Kneynsberg_CH";

            RunBoolDataComparatorEffect FirstWarningChecker = ScriptableObject.CreateInstance<RunBoolDataComparatorEffect>();
            FirstWarningChecker._data = "KneynsbergEffigyReaction";

            ModifyRunBoolDataEffect dataFirstStrike = ScriptableObject.CreateInstance<ModifyRunBoolDataEffect>();
            dataFirstStrike._data = "KneynsbergEffigyReaction";

            ModifyRunBoolDataEffect dataSecondStrike = ScriptableObject.CreateInstance<ModifyRunBoolDataEffect>();
            dataSecondStrike._data = "KneynsbergEffigyRepeatReaction";

            PerformEffectViaSubaction firstStrikeSub = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            firstStrikeSub.effects = [Effects.GenerateEffect(dataFirstStrike)];

            PerformEffectViaSubaction secondStrikeSub = ScriptableObject.CreateInstance<PerformEffectViaSubaction>();
            secondStrikeSub.effects = [Effects.GenerateEffect(dataSecondStrike)];

            StatusEffect_Apply_Effect RupturedApplyToRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApplyToRandom._Status = StatusField.Ruptured;
            RupturedApplyToRandom._JustOneRandomTarget = true;

            StatusEffect_Apply_Effect PoisonApplyToRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            PoisonApplyToRandom._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            PoisonApplyToRandom._JustOneRandomTarget = true;

            StatusEffect_ApplyPermanent_Effect RadiationForeverToRandom = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanent_Effect>();
            RadiationForeverToRandom._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");
            RadiationForeverToRandom._JustOneRandomTarget = true;

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;
            OilApply._JustOneRandomTarget = false;

            AddPassiveEffect AddLeaky = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddLeaky._passiveToAdd = Passives.Leaky1;

            AddPassiveEffect AddSlippery = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddSlippery._passiveToAdd = Passives.Slippery;

            PerformRandomEffectViaSubaction RandomBullshitGo = ScriptableObject.CreateInstance<PerformRandomEffectViaSubaction>();
            RandomBullshitGo.effects = [
                [
                    Effects.GenerateEffect(RupturedApplyToRandom, 5, Targeting.Unit_AllOpponents),
                ],
                [
                    Effects.GenerateEffect(RadiationForeverToRandom, 1, Targeting.Unit_AllOpponents),
                ],
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateUnitTarget_Specific_Health(true, false, false, true)),
                ],
                [
                    Effects.GenerateEffect(OilApply, 3, Targeting.Unit_AllOpponents),
                ],
                [
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Unit_AllOpponents),
                ],
                [
                    Effects.GenerateEffect(AddSlippery, 1, Targeting.Unit_AllOpponents),
                ],
                [
                    Effects.GenerateEffect(PoisonApplyToRandom, 5, Targeting.Unit_AllOpponents),
                ],
            ];

            DoublePerformEffect_Item serpenteffigy = new DoublePerformEffect_Item("EffigyOfASerpent_ID", null, false)
            {
                Item_ID = "EffigyOfASerpent_TW",
                Name = "Effigy of a Serpent",
                Flavour = "\"ONCE UPON A TIME there was a little snake, no bigger than your finger, who lived behind the mirror.\"",
                Description = "Occasionally performs random effects, most of them good, at the start of each turn.", //My sssecrets shall be revealed in time. Worry not, sssweetling. You will be sssafe.
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenKneynsberg"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Conditions = [TwoInThree, IsntKneynsberg],
                Effects =
                [
                    Effects.GenerateEffect(RandomBullshitGo, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RandomBullshitGo, 1, Targeting.Slot_SelfSlot, OneInTen),
                ],
                OnUnlockUsesTHE = true,
                SecondaryTriggerOn = [TriggerCalls.OnCombatStart],
                SecondaryConditions = [IsKneynsberg],
                SecondaryDoesPopUpInfo = false,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(FirstWarningChecker),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(KneynsbergEffigyInit),
                    Effects.GenerateEffect(secondStrikeSub, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(firstStrikeSub, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ]
            };

            serpenteffigy.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(serpenteffigy.item, new ItemModdedUnlockInfo("EffigyOfASerpent_TW", ResourceLoader.LoadSprite("UnlockHeavenKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Divine_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Divine_ACH", "EffigyOfASerpent_TW");

            UnlockableModData kneynsbergHeavenUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Divine_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Divine_ACH",
                hasItemUnlock = true,
                items = ["EffigyOfASerpent_TW"],
            };

            FinalBossCharUnlockCheck UnlockDivineKneynsberg = Unlocks.GetUnlock_HeavenFinalBoss();
            UnlockDivineKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergHeavenUnlockData);

            ModdedAchievements kneynsbergheavenachievement = new ModdedAchievements("Effigy of a Serpent", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Divine_ACH");
            kneynsbergheavenachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
