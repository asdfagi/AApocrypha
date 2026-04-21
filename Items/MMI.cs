using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class MMI
    {
        public static ExtraAbility_Wearable_SMS mmiDropWearable;
        public static void Add()
        {
            ExtraAbility_Wearable_SMS dropWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();

            CasterAddOrRemoveExtraAbilityEffect DropTake = ScriptableObject.CreateInstance<CasterAddOrRemoveExtraAbilityEffect>();
            DropTake._extraAbility = dropWear;
            DropTake._removeExtraAbility = true;
            Ability mmidrop = new Ability("Dispose", "AApocrypha_ItemMMIDrop_A")
            {
                Description = "Remove the equipped MMI's contained brain, allowing a new one to be inserted, and refresh this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemMMIDropAbility"),
                Cost = [],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MMIBrainResetEffect>()),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RefreshAbilityUseEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(DropTake, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.AbsurdlyRare,
                Priority = Priority.VeryFast
            };
            mmidrop.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            dropWear._extraAbility = mmidrop.GenerateCharacterAbility(true);

            mmiDropWearable = dropWear;

            MMI_Item manmachineinterface = new MMI_Item("MMI_ID", null, false)
            {
                Item_ID = "MMI_SW",
                Name = "MMI",
                Flavour = "\"The bland acronym obscures the true horror of this monstrosity.\"",
                Description = "When this party member kills something, insert the victim's brain into this item. Filled MMIs give their holder one of the victim's abilities.",
                IsShopItem = true,
                ShopPrice = 6,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaNaudiz4"),
                TriggerOn = TriggerCalls.OnKill,
                Effects =
                [
                ],
                SecondaryDoesPopUpInfo = false,
                SecondaryConsumeOnUse = false,
                SecondaryTriggerOn = [TriggerCalls.OnDeath, TriggerCalls.OnCombatEnd],
                SecondaryEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraLootDupeCasterItemEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Abstraction_ACH";
            string unlockID = "AApocrypha_Naudiz4_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(manmachineinterface.item, new ItemModdedUnlockInfo(manmachineinterface.Item_ID, ResourceLoader.LoadSprite("UnlockDoulaNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, manmachineinterface.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [manmachineinterface.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("MMI", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
