using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class HumanHeart
    {
        public static void Add()
        {
            AnimationVisualsEffect CopyAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            CopyAnim._animationTarget = Targeting.Slot_SelfSlot;
            CopyAnim._visuals = CustomVisuals.StaticColorVisualsSO;

            CopyThatItemEffect CopyEffect = ScriptableObject.CreateInstance<CopyThatItemEffect>();

            PerformEffect_Item humanHeart = new PerformEffect_Item("HumanHeart_ID", null, false)
            {
                Item_ID = "HumanHeart_TW",
                Name = "A Human Heart",
                Flavour = "\"Could you explain again this desire to become human?\"",
                Description = "At the start of each turn, this party member copies an ability from and the health color of a random living enemy, overriding previous ones if necessary.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockComedySimulacrumKillSelf"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(CopyAnim, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CopyEffect, 1, Targeting.Unit_AllOpponents),
                ],
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(humanHeart.item, new ItemModdedUnlockInfo("HumanHeart_TW", ResourceLoader.LoadSprite("UnlockComedySimulacrumKillSelfLocked", null, 32, null), "AApocrypha_Comedy_SimulacrumKillSelf_ACH"));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Comedy_SimulacrumKillSelf_ACH", "HumanHeart_TW");

            UnlockableModData simulacrumComedyUnlockData = new UnlockableModData("ComedySimulacrumKillSelf")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Comedy_SimulacrumKillSelf_ACH",
                hasItemUnlock = true,
                items = new string[] { "HumanHeart_TW" },
            };

            ModdedAchievements simulacrumcomedykillselfachievement = new ModdedAchievements("Maladapted", "Trick a Simulacrum into killing itself.", ResourceLoader.LoadSprite("AchievementComedySimulacrumKillSelf", null, 32, null), "AApocrypha_Comedy_SimulacrumKillSelf_ACH");
            simulacrumcomedykillselfachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_ByID(simulacrumComedyUnlockData);
        }
    }
}
