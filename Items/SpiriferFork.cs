using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace A_Apocrypha.Items
{
    public class SpiriferFork
    {
        public static void Add()
        {
            ModifyRunBoolDataEffect SpiriferForkReset = ScriptableObject.CreateInstance<ModifyRunBoolDataEffect>();
            SpiriferForkReset._data = "SpiriferForkUsed";
            SpiriferForkReset._isTrue = false;

            DoublePerformEffect_Item fork = new DoublePerformEffect_Item("SpirifersFork_ID", null, false)
            {
                Item_ID = "SpirifersFork_SW",
                Name = "Spirifer's Fork",
                Flavour = "\"If you'd never had it, you'd never miss it.\"",
                Description = "The first time this party member hits an enemy in a battle, permanently remove one of that enemy's abilities and remove its turns from the timeline. Does not work in boss encounters or on enemies with only one ability.",
                IsShopItem = true,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockKatalixiWhitlock"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<SpiriferForkHitEffectCondition>()],
                Effects = [],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [TriggerCalls.OnCombatStart],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(SpiriferForkReset, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Whitlock_Boundary_ACH";
            string unlockID = "AApocrypha_Whitlock_Boundary_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(fork.item, new ItemModdedUnlockInfo(fork.Item_ID, ResourceLoader.LoadSprite("UnlockKatalixiWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, fork.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [fork.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 16, null));
            unlockCheck.AddUnlockData("Whitlock_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Spirifer's Fork", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementKatalixiWhitlock", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }
    }
}
