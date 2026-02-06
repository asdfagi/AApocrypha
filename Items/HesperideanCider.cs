using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class HesperideanCider
    {
        public static void Add()
        {
            ExtraAbility_Wearable_SMS sipwearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();

            CasterAddOrRemoveExtraAbilityEffect CiderAdd = ScriptableObject.CreateInstance<CasterAddOrRemoveExtraAbilityEffect>();
            CiderAdd._extraAbility = sipwearable;
            CiderAdd._removeExtraAbility = false;

            CasterAddOrRemoveExtraAbilityEffect CiderRemove = ScriptableObject.CreateInstance<CasterAddOrRemoveExtraAbilityEffect>();
            CiderRemove._extraAbility = sipwearable;
            CiderRemove._removeExtraAbility = true;

            Ability cidersip = new Ability("Sip of Gold", "AApocrypha_CiderSip_A")
            {
                Description = "Heal this party member 10 health.\nRemove this ability from this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemCiderAbility"),
                Cost = [],
                Visuals = Visuals.Relapse,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                    [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CiderRemove, 1, Targeting.Slot_SelfSlot),
                    ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            cidersip.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_5_10)]);

            sipwearable._extraAbility = cidersip.GenerateCharacterAbility(true);

            PerformEffect_Item cider = new PerformEffect_Item("HesperideanCider_ID", null, false)
            {
                Item_ID = "HesperideanCider_TW",
                Name = "Hesperidean Cider",
                Flavour = "\"SO SHALL HE NEVER DIE\"",
                Description = "Adds the ability \"Sip of Gold\" to this party member, a powerful self-healing move that can be used once every battle.",
                IsShopItem = false,
                ShopPrice = 160000,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenWhitlock"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(CiderAdd, 1, Targeting.Slot_SelfSlot),
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(cider.item, new ItemModdedUnlockInfo("HesperideanCider_TW", ResourceLoader.LoadSprite("UnlockHeavenWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Divine_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Divine_ACH", "HesperideanCider_TW");

            UnlockableModData whitlockHeavenUnlockData = new UnlockableModData("AApocrypha_Whitlock_Divine_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Divine_ACH",
                hasItemUnlock = true,
                items = ["HesperideanCider_TW"],
            };

            FinalBossCharUnlockCheck UnlockDivineWhitlock = Unlocks.GetUnlock_HeavenFinalBoss();
            UnlockDivineWhitlock.AddUnlockData("Whitlock_CH", whitlockHeavenUnlockData);

            ModdedAchievements whitlockheavenachievement = new ModdedAchievements("Hesperidean Cider", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenWhitlock", null, 32, null), "AApocrypha_Whitlock_Divine_ACH");
            whitlockheavenachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
