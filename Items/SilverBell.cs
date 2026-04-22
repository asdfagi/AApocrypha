using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class SilverBell
    {
        public static void Add()
        {
            TargetSplitOrReplaceHealthEffect purplify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify._color = Pigments.Purple;
            purplify._colorBlacklist = [Pigments.Grey];
            purplify._transformBlacklist = true;

            TargetExtractHealthColorEffect DePurple = ScriptableObject.CreateInstance<TargetExtractHealthColorEffect>();
            DePurple._color = Pigments.Purple;
            DePurple._fallbackColors = [Pigments.Red, Pigments.Yellow, Pigments.Blue];

            SpecificAlliesByHealthColorTargeting ThePurples = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            ThePurples.slotOffsets = [0];
            ThePurples.targetUnitAllySlots = false;
            ThePurples._color = Pigments.Purple;
            ThePurples._contains = true;
            ThePurples.getAllUnitSelfSlots = false;
            ThePurples._excludeCaster = true;

            SpecificAlliesByHealthColorTargeting NThePurples = ScriptableObject.CreateInstance<SpecificAlliesByHealthColorTargeting>();
            NThePurples.slotOffsets = [0];
            NThePurples.targetUnitAllySlots = false;
            NThePurples._color = Pigments.Purple;
            NThePurples._contains = true;
            NThePurples.getAllUnitSelfSlots = false;
            NThePurples.blacklist = true;
            NThePurples._excludeCaster = true;

            RandomTargetPerformEffectViaSubaction PurplifyOne = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            PurplifyOne.effects = [
                Effects.GenerateEffect(purplify, 1, Targeting.Slot_SelfSlot),
            ];

            ExtraAbility_Wearable_SMS bellWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();

            Ability bellcommand = new Ability("Command", "AApocrypha_ItemSilverBellCommand_A")
            {
                Description = "Make All other party members with purple-containing health perform a random one of their abilities, then remove purple from All other party members' health color, randomizing it if it would be grey.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemSilverBellCommandAbility"),
                Cost = [Pigments.YellowPurple, Pigments.YellowPurple],
                AnimationTarget = Targeting.Slot_SelfSlot,
                Visuals = Visuals.Bell,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<PerformRandomAbilityEffect>(), 1, ThePurples),
                    Effects.GenerateEffect(DePurple, 1, ThePurples),
                ],
                Rarity = Rarity.AbsurdlyRare,
                Priority = Priority.VeryFast
            };
            bellcommand.AddIntentsToTarget(ThePurples, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Mana_Modify)]);

            bellWear._extraAbility = bellcommand.GenerateCharacterAbility(true);

            PerformEffect_Item silverBell = new PerformEffect_Item("SilverBell_ID")
            {
                Item_ID = "SilverBell_TW",
                Name = "Silver Bell",
                Flavour = "\"It radiates with an otherworldly presence.\"",
                Description = "At the start of each turn, split purple into a random other party member's health color. Grey health is instead replaced with purple." +
                "\nThis party member now has \"Command\" as an extra ability, letting them inspire those they have brought into the fold to action.",
                IsShopItem = false,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBossAmdusiasBonus"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects = [
                    Effects.GenerateEffect(PurplifyOne, 1, NThePurples),
                ],
                OnUnlockUsesTHE = true,
                EquippedModifiers = [bellWear],
            };

            string achievementID = "AApocrypha_Comedy_AmdusiasCrowd_ACH";
            string unlockID = "ComedyAmdusiasCrowd";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(silverBell.item, new ItemModdedUnlockInfo(silverBell.Item_ID, ResourceLoader.LoadSprite("UnlockBossAmdusiasBonusLocked", null, 32, null), achievementID));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, silverBell.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [silverBell.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Voluntold", "Exhaust Amdusias' supply of volunteers.", ResourceLoader.LoadSprite("AchievementComedyAmdusiasCrowd", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
