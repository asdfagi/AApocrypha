using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Dustwine
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveShy = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveShy._extraPassiveAbility = Passives.GetCustomPassive("Shy_PA");

            FullHealthDetectionEffectorCondition Injured = ScriptableObject.CreateInstance<FullHealthDetectionEffectorCondition>();
            Injured.checkFullHealth = false;

            StatusEffect_Apply_Effect RandomPoisoned = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RandomPoisoned._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            RandomPoisoned._RandomBetweenPrevious = true;

            PerformEffect_Item dustwine = new PerformEffect_Item("CupOfDustwine_ID", null, false)
            {
                Item_ID = "CupOfDustwine_SW",
                Name = "Cup Of Dustwine",
                Flavour = "\"Addles the mind. Tastes of roses.\"",
                Description = "This party member now has Shy as a passive.\nUpon this party member moving, heal them 0-2 health and apply 0-2 Poisoned to the Left and Right enemies.",
                IsShopItem = true,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockDoulaKneynsberg"),
                TriggerOn = TriggerCalls.OnMoved,
                EquippedModifiers = [wearablePassiveShy],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomHealBetweenPreviousAndEntryEffect>(), 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RandomPoisoned, 2, Targeting.Slot_OpponentSides),
                ],
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(dustwine.item, new ItemModdedUnlockInfo("CupOfDustwine_SW", ResourceLoader.LoadSprite("UnlockDoulaKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Abstraction_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Abstraction_ACH", "CupOfDustwine_SW");

            UnlockableModData kneynsbergDoulaUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Abstraction_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Abstraction_ACH",
                hasItemUnlock = true,
                items = ["CupOfDustwine_SW"],
            };

            FinalBossCharUnlockCheck UnlockAbstractionKneynsberg = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            UnlockAbstractionKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergDoulaUnlockData);

            ModdedAchievements kneynsbergdoulaachievement = new ModdedAchievements("Cup Of Dustwine", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementDoulaKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Abstraction_ACH");
            kneynsbergdoulaachievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
