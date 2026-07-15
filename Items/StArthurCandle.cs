using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class StArthurCandle
    {
        public static void Add()
        {
            AnimationVisualsEffect shankL = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            shankL._visuals = Visuals.Shank;
            shankL._animationTarget = Targeting.Slot_AllyLeft;

            AnimationVisualsEffect shankR = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            shankR._visuals = shankL._visuals;
            shankR._animationTarget = Targeting.Slot_AllyRight;

            Ability betrayAbil = new Ability("Betrayal", "AApocrypha_ItemStArthurBetray_A")
            {
                Description = "Deal 4 damage to the Left or Right party member.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemShankAbility"),
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<LeftOrRightChanceForNextEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shankL, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_AllyLeft, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(shankR, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<DamageEffect>(), 4, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 4)),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VeryFast
            };
            betrayAbil.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Damage_3_6)]);

            ExtraAbility_Wearable_SMS betrayWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            betrayWear._extraAbility = betrayAbil.GenerateCharacterAbility(true);

            DoublePerformEffect_Item starthur = new DoublePerformEffect_Item("StArthurCandle_ID", null, false)
            {
                Item_ID = "StArthurCandle_TW",
                Name = "St Arthur's Candle",
                Flavour = "\"It reeks of betrayal.\"",
                Description = "When this party member deals damage to an ally, heal this party member for half of the damage dealt, rounded down." +
                "\nAdds the ability \"Betrayal\" to this party member, letting them harm their allies.",
                IsShopItem = false,
                ShopPrice = 7, //seven is the number
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("StArthurCandle"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<StArthurCandleHitEffectCondition>()],
                Effects = [],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [],
                SecondaryEffects =
                [],
                OnUnlockUsesTHE = false,
                EquippedModifiers = [betrayWear],
            };

            string achievementID = "AApocrypha_Tragedy_StArthur_ACH";
            string unlockID = "AApocrypha_Tragedy_StArthur_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(starthur.item, new ItemModdedUnlockInfo(starthur.Item_ID, ResourceLoader.LoadSprite("CandleItemLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, starthur.Item_ID);
            
            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [starthur.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Dreams of Dark Water", "Make a most unwise decision.", ResourceLoader.LoadSprite("AchievementTragedyStArthur", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
