using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class AigulSpine
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect weakerer = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            weakerer._Status = StatusField.GetCustomStatusEffect("Weakness_ID");

            DamageIntModCasterStatusAndSecondaryEffect_Item aigul = new DamageIntModCasterStatusAndSecondaryEffect_Item("SpineOfRegret_ID", 2, true, false, true, "Weakness_ID")
            {
                Item_ID = "SpineOfRegret_TW",
                Name = "Spine of Regret",
                Flavour = "\"He would give up anything, except us.\"",
                Description = "Weakness increases damage dealt by this party member instead of decreasing it." +
                "\nAt the start of each turn and when getting a kill, apply 1 Weakness to this party member.",
                IsShopItem = false,
                ShopPrice = 9,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyVaughan"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryDoesPopUpInfo = true,
                SecondaryConsumeOnUse = false,
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart, TriggerCalls.OnKill],
                SecondaryConditions = [],
                SecondaryEffects = [
                    Effects.GenerateEffect(weakerer, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Vaughan_Forgotten_ACH";
            string unlockID = "AApocrypha_Vaughan_Forgotten_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(aigul.item, new ItemModdedUnlockInfo(aigul.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, aigul.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [aigul.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Spine of Regret", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
