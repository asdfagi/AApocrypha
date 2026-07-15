using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AcolyteFish
    {
        public static void Add()
        {
            AddPassiveEffect Zelators = ScriptableObject.CreateInstance<AddPassiveEffect>();
            Zelators._passiveToAdd = Passives.GetCustomPassive("Zelator_PA");

            StatusEffect_Apply_Effect HexedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            HexedApply._Status = StatusField.GetCustomStatusEffect("Hexed_ID");
            HexedApply._JustOneRandomTarget = true;

            DoublePerformEffect_Item watercultist = new DoublePerformEffect_Item("Aqualite_ID", null, false)
            {
                Item_ID = "Aqualite_FishW",
                Name = "Aqualite",
                Flavour = "\"You caught a... Cultist? 55cm.\"",
                Description = "At the start of combat, apply Zelator as a passive to all party members, increasing the damage they deal to Hexed units." +
                "\nAt the start of each turn, apply 2 Hexed to a random enemy. 15% chance to be destroyed upon activation.",
                IsShopItem = false,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyHaborym"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(HexedApply, 2, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(15)),
                ],
                EquippedModifiers = [],
                SecondaryTriggerOn = [TriggerCalls.OnCombatStart],
                SecondaryEffects = [
                    Effects.GenerateEffect(Zelators, 1, Targeting.Unit_AllAllies),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Haborym_Dreamer_ACH";
            string unlockID = "AApocrypha_Haborym_Dreamer_Unlock";

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(watercultist.item, "Fish", "Fish", new ItemModdedUnlockInfo(watercultist.Item_ID, ResourceLoader.LoadSprite("UnlockBlueSkyHaborymLocked", null, 32, null), achievementID));
            ItemUtils.AddItemFishingRodPool(watercultist.item, 2, watercultist.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(watercultist.item, 2, watercultist.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, watercultist.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [watercultist.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Haborym_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Aqualite", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyHaborym", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
