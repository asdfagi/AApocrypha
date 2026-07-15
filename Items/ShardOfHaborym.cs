using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class ShardOfHaborym
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearableFreezent = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearableFreezent._extraPassiveAbility = Passives.GetCustomPassive("Antifreeze_PA");

            HealthColorChange_Wearable_SMS wearableBlue = ScriptableObject.CreateInstance<HealthColorChange_Wearable_SMS>();
            wearableBlue._healthColor = Pigments.Blue;

            FieldEffect_Apply_Effect AddHoarfrost = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            AddHoarfrost._Field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

            PerformEffect_Item haborymdead = new PerformEffect_Item("ShardOfHaborym_ID", null, false)
            {
                Item_ID = "ShardOfHaborym_TW",
                Name = "Shard of Haborym",
                Flavour = "\"Cold-hearted & Cold-blooded\"",
                Description = "This party member has blue health, is unaffected by Hoarfrost and immune to frost damage." +
                "\nAt the start of each turn, apply 1 Hoarfrost to this party member's position and 2 Hoarfrost to the Opposing position.",
                IsShopItem = false,
                ShopPrice = 4,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockTragedyHaborymPoke"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Conditions = [],
                Effects = [
                    Effects.GenerateEffect(AddHoarfrost, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AddHoarfrost, 2, Targeting.Slot_Front),
                ],
                EquippedModifiers = [wearableFreezent, wearableBlue],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Tragedy_HaborymPoke_ACH";
            string unlockID = "AApocrypha_Tragedy_HaborymPoke_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(haborymdead.item, new ItemModdedUnlockInfo(haborymdead.Item_ID, ResourceLoader.LoadSprite("UnlockTragedyHaborymPokeLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, haborymdead.Item_ID);
            
            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [haborymdead.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Shattered Lives", "Be careless with a frozen corpse.", ResourceLoader.LoadSprite("AchievementTragedyHaborymPoke", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
