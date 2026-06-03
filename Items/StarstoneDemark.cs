using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class StarstoneDemark
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearableInferno = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearableInferno._extraPassiveAbility = Passives.Inferno;

            FieldEffectCheckEffect iDoBelieveIAmOnFire = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            iDoBelieveIAmOnFire._fields = [StatusField.OnFire];

            StatusEffect_Apply_Effect Smoulder = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            Smoulder._Status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            DamageIntModCasterStatusAndSecondaryEffect_Item demark = new DamageIntModCasterStatusAndSecondaryEffect_Item("StarstoneDemark_ID", 2, true, false, true, "Smouldering_ID")
            {
                Item_ID = "StarstoneDemark_TW",
                Name = "Starstone Demark",
                Flavour = "\"A legal token, punctuation, protection. It's an odd language.\"",
                Description = "This party member now has Inferno (1) as a passive." +
                "\nBefore performing an ability while standing in Fire, apply 1 Smouldering to this party member." + 
                "\nIncrease direct damage dealt by this party member by twice the amount of Smouldering they have.",
                IsShopItem = false,
                ShopPrice = 9,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchAmbrose"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryDoesPopUpInfo = true,
                SecondaryConsumeOnUse = false,
                SecondaryTriggerOn = [TriggerCalls.OnAbilityWillBeUsed],
                SecondaryConditions = [],
                SecondaryEffects = [
                    Effects.GenerateEffect(iDoBelieveIAmOnFire, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Smoulder, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                OnUnlockUsesTHE = true,
                EquippedModifiers = [wearableInferno],
            };

            string achievementID = "AApocrypha_Ambrose_Inevitable_ACH";
            string unlockID = "AApocrypha_Ambrose_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(demark.item, new ItemModdedUnlockInfo(demark.Item_ID, ResourceLoader.LoadSprite("UnlockMarchAmbroseLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, demark.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [demark.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Ambrose_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Starstone Demark", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchAmbrose", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
