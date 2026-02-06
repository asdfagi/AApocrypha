using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Moondial
    {
        public static void Add()
        {
            var MoonInfo = Moon.Now(AApocrypha.hemisphere.Value ? "south" : "north");
            string spriteID = $"moondial{MoonInfo.Visual}";
            int fullness = 0;
            int emptyness = 0;
            switch (MoonInfo.Visual)
            {
                case "new":
                    fullness = 0;
                    emptyness = 8;
                    break;
                case "crescentleft":
                    fullness = 2;
                    emptyness = 6;
                    break;
                case "crescentright":
                    fullness = 2;
                    emptyness = 6;
                    break;
                case "halfleft":
                    fullness = 4;
                    emptyness = 4;
                    break;
                case "halfright":
                    fullness = 4;
                    emptyness = 4;
                    break;
                case "gibbousleft":
                    fullness = 6;
                    emptyness = 2;
                    break;
                case "gibbousright":
                    fullness = 6;
                    emptyness = 2;
                    break;
                case "full":
                    fullness = 8;
                    emptyness = 0;
                    break;
                default:
                    fullness = 4;
                    emptyness = 4;
                    break;
            }

            FieldEffect_Apply_Effect ShieldApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldApply._Field = StatusField.Shield;

            PerformEffect_Item evilhoney = new PerformEffect_Item("Moondial_ID", null, false)
            {
                Item_ID = "Moondial_TW",
                Name = "Moondial",
                Flavour = "\"Counting the Days is increasing...\"",
                Description = $"At the end of each turn, Apply {emptyness} Shields to this party member's position and heal them {fullness} health.\nThe amount of Shields applied and health restored by this item is determined by the phase of the moon.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite(spriteID),
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(ShieldApply, emptyness, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), fullness, Targeting.Slot_SelfSlot),
                ]
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(evilhoney.item, new ItemModdedUnlockInfo("Moondial_TW", ResourceLoader.LoadSprite("UnlockNobodyKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Forgotten_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Forgotten_ACH", "Moondial_TW");

            UnlockableModData kneynsbergNobodyUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Forgotten_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Forgotten_ACH",
                hasItemUnlock = true,
                items = ["Moondial_TW"],
            };

            FinalBossCharUnlockCheck UnlockForgottenKneynsberg = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            UnlockForgottenKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergNobodyUnlockData);

            ModdedAchievements kneynsbergnobodyachievement = new ModdedAchievements("Moondial", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Forgotten_ACH");
            kneynsbergnobodyachievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
