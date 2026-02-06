using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using Utility.SerializableCollection;

namespace A_Apocrypha.Items
{
    public class HyperdimensionalPearl
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveOmnichromia = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveOmnichromia._extraPassiveAbility = Passives.GetCustomPassive("Omnichromia_PA");

            ExtraPassiveAbility_Wearable_SMS wearablePassiveLeaky = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveLeaky._extraPassiveAbility = Passives.Leaky1;

            PerformEffect_Item hyperdimensionalPearl = new PerformEffect_Item("HyperdimensionalPearl_ID", null, false)
            {
                Item_ID = "HyperdimensionalPearl_TW",
                Name = "Hyperdimensional Pearl",
                Flavour = "\"It looks wrsrihoginhngty...\"",
                Description = "This party member now has Omnichromia and Leaky as passives. Before performing an ability, this party member generates 1 Pigment of their health color.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMinibossRift"),
                TriggerOn = TriggerCalls.OnAbilityWillBeUsed,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<GenerateCasterHealthManaEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [wearablePassiveOmnichromia, wearablePassiveLeaky],
                OnUnlockUsesTHE = true,
            };

            hyperdimensionalPearl.item._ItemTypeIDs =
            [];

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(hyperdimensionalPearl.item, new ItemModdedUnlockInfo("HyperdimensionalPearl_TW", ResourceLoader.LoadSprite("UnlockMinibossRiftLocked", null, 32, null), "AApocrypha_Miniboss_Rift_ACH"));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Miniboss_Rift_ACH", "HyperdimensionalPearl_TW");

            UnlockableModData riftMinibossUnlockData = new UnlockableModData("MinibossRift")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Miniboss_Rift_ACH",
                hasItemUnlock = true,
                items = ["HyperdimensionalPearl_TW"],
            };

            EnemyDeathUnlockCheck RiftDeathUnlock = ScriptableObject.CreateInstance<EnemyDeathUnlockCheck>();
            RiftDeathUnlock.usesSimpleDeathData = true;
            RiftDeathUnlock.enemyID = "RiftMiniboss_EN";
            RiftDeathUnlock.simpleDeathData = riftMinibossUnlockData;
            RiftDeathUnlock.specialDeathData = new SerializableDictionary<string, UnlockableModData>();

            ModdedAchievements riftminibossachievement = new ModdedAchievements("Wound Tender", "Mend a Hyperdimensional Fracture.", ResourceLoader.LoadSprite("AchievementMinibossRift", null, 32, null), "AApocrypha_Miniboss_Rift_ACH");
            riftminibossachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_EnemyDeath(RiftDeathUnlock);
        }
    }
}
