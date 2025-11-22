using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Emergence
    {
        public static void Add()
        {
            CurrentHealthPercentageEffectorCondition CheckWillDie = ScriptableObject.CreateInstance<CurrentHealthPercentageEffectorCondition>();
            CheckWillDie.healthPercentageThreshold = 1;
            CheckWillDie.healthUnderThreshold = true;

            ContainsPassiveAbilityCondition NotDying = ScriptableObject.CreateInstance<ContainsPassiveAbilityCondition>();
            NotDying.useNotContains = true;
            NotDying.m_PassiveID = Passives.Dying.m_PassiveID.ToString();

            ContainsPassiveAbilityCondition NotStatue = ScriptableObject.CreateInstance<ContainsPassiveAbilityCondition>();
            NotStatue.useNotContains = true;
            NotStatue.m_PassiveID = Passives.Inanimate.m_PassiveID.ToString();

            HealEffect HealByPercentage = ScriptableObject.CreateInstance<HealEffect>();
            HealByPercentage.entryAsPercentage = true;

            StatusEffect_ApplyPermanent_Effect RupturedRestrictorApply = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanent_Effect>();
            RupturedRestrictorApply._Status = StatusField.Ruptured;

            ExtraLootOptionsEffect Moth = ScriptableObject.CreateInstance<ExtraLootOptionsEffect>();
            Moth._itemName = "Emergence_TW";

            PerformEffect_Item emergence = new PerformEffect_Item("Emergence_ID", null, false)
            {
                Item_ID = "Emergence_TW",
                Name = "Emergence",
                Flavour = "\"Death need not be the only end.\"",
                Description = "Prevent this party member's death once. Heal this party member by half of their maximum health and permanently Rupture them.\nThis item is destroyed upon activation, but will return at the end of combat.\nThis item does nothing if this party member has Dying or Inanimate as passives.",
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchKneynsberg"),
                TriggerOn = TriggerCalls.OnDamaged,
                Conditions = [CheckWillDie, NotDying, NotStatue],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HealByPercentage, 50, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(RupturedRestrictorApply, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(Moth),
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(emergence.item, new ItemModdedUnlockInfo("Emergence_TW", ResourceLoader.LoadSprite("UnlockMarchKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Inevitable_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Inevitable_ACH", "Emergence_TW");

            UnlockableModData kneynsbergMarchUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Inevitable_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Inevitable_ACH",
                hasItemUnlock = true,
                items = ["Emergence_TW"],
            };

            FinalBossCharUnlockCheck UnlockInevitableKneynsberg = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            UnlockInevitableKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergMarchUnlockData);

            ModdedAchievements kneynsbergmarchachievement = new ModdedAchievements("Emergence", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Inevitable_ACH");
            kneynsbergmarchachievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
