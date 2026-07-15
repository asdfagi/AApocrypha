using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class OccultistsTablets
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect Overclock = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Overclock._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            PerformEffect_Item tablets = new PerformEffect_Item("OccultistsTablets_ID", null, false)
            {
                Item_ID = "OccultistsTablets_SW",
                Name = "Occultist's Tablets",
                Flavour = "\"Who knows what these might summon?\"",
                Description = "At the start of combat, if there is room on the enemy side, apply 3 Overclocked to this party member and summon a random small enemy.",
                IsShopItem = true,
                ShopPrice = 5,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaHaborym"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [ScriptableObject.CreateInstance<EmptyEnemyPositionsEffectorCondition>()],
                Effects =
                [
                    Effects.GenerateEffect(Overclock, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(LoadedAssetsHandler.GetEnemyAbility("Regurgitate_A").effects[0].effect, 1),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Haborym_Abstraction_ACH";
            string unlockID = "AApocrypha_Haborym_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(tablets.item, new ItemModdedUnlockInfo(tablets.Item_ID, ResourceLoader.LoadSprite("UnlockDoulaHaborymLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, tablets.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [tablets.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Haborym_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Occultist's Tablets", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaHaborym", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
