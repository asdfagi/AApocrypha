using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class SerpentEffigy
    {
        public static void Add()
        {
            PercentageEffectCondition OneInTen = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            OneInTen.percentage = 10;

            PercentageEffectorCondition OneIn5_2 = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            OneIn5_2.triggerPercentage = 20;

            StatusEffect_Apply_Effect RupturedApplyToRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            RupturedApplyToRandom._Status = StatusField.Ruptured;
            RupturedApplyToRandom._JustOneRandomTarget = true;

            StatusEffect_Apply_Effect PoisonApplyToRandom = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            PoisonApplyToRandom._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");
            PoisonApplyToRandom._JustOneRandomTarget = true;

            StatusEffect_ApplyPermanent_Effect RadiationForeverToRandom = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanent_Effect>();
            RadiationForeverToRandom._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");
            RadiationForeverToRandom._JustOneRandomTarget = true;

            StatusEffect_Apply_Effect OilApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            OilApply._Status = StatusField.OilSlicked;
            OilApply._JustOneRandomTarget = false;

            AddPassiveEffect AddLeaky = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddLeaky._passiveToAdd = Passives.Leaky1;

            AddPassiveEffect AddSlippery = ScriptableObject.CreateInstance<AddPassiveEffect>();
            AddSlippery._passiveToAdd = Passives.Slippery;

            PerformEffect_Item serpenteffigy = new PerformEffect_Item("EffigyOfASerpent_ID", null, false)
            {
                Item_ID = "EffigyOfASerpent_TW",
                Name = "Effigy of a Serpent",
                Flavour = "\"ONCE UPON A TIME there was a little snake, no bigger than your finger, who lived behind the mirror.\"",
                Description = "Occasionally performs random effects, most of them good, at the start of each turn.", //My sssecrets shall be revealed in time. Worry not, sssweetling. You will be sssafe.
                IsShopItem = false,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenKneynsberg"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Conditions = [OneIn5_2],
                Effects =
                [
                    Effects.GenerateEffect(RupturedApplyToRandom, 5, Targeting.Unit_AllOpponents, OneInTen),
                    Effects.GenerateEffect(RupturedApplyToRandom, 5, Targeting.Unit_AllOpponents, OneInTen),
                    Effects.GenerateEffect(RadiationForeverToRandom, 1, Targeting.Unit_AllOpponents, OneInTen),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateUnitTarget_Specific_Health(true, false, false, true), OneInTen),
                    Effects.GenerateEffect(OilApply, 3, Targeting.Unit_AllOpponents, OneInTen),
                    Effects.GenerateEffect(AddLeaky, 1, Targeting.Unit_AllOpponents, OneInTen),
                    Effects.GenerateEffect(AddSlippery, 1, Targeting.Unit_AllOpponents, OneInTen),
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(serpenteffigy.item, new ItemModdedUnlockInfo("EffigyOfASerpent_TW", ResourceLoader.LoadSprite("UnlockHeavenKneynsbergLocked", null, 32, null), "AApocrypha_Kneynsberg_Divine_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Kneynsberg_Divine_ACH", "EffigyOfASerpent_TW");

            UnlockableModData kneynsbergHeavenUnlockData = new UnlockableModData("AApocrypha_Kneynsberg_Divine_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Kneynsberg_Divine_ACH",
                hasItemUnlock = true,
                items = ["EffigyOfASerpent_TW"],
            };

            FinalBossCharUnlockCheck UnlockDivineKneynsberg = Unlocks.GetUnlock_HeavenFinalBoss();
            UnlockDivineKneynsberg.AddUnlockData("Kneynsberg_CH", kneynsbergHeavenUnlockData);

            ModdedAchievements kneynsbergheavenachievement = new ModdedAchievements("Effigy of a Serpent", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenKneynsberg", null, 32, null), "AApocrypha_Kneynsberg_Divine_ACH");
            kneynsbergheavenachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
