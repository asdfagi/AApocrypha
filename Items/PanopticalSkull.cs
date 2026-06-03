using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class PanopticalSkull
    {
        public static void Add()
        {
            AddPassiveEffect abomimate = ScriptableObject.CreateInstance<AddPassiveEffect>();
            abomimate._passiveToAdd = Passives.Abomination1;

            RemovePassiveEffect rember = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            rember.m_PassiveID = Passives.Forgetful.m_PassiveID;

            DamageOfTypeEffect indirectOw = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
            indirectOw._indirect = true;
            indirectOw._DamageTypeID = CombatType_GameIDs.Dmg_Linked.ToString();

            TargetPerformEffectByTimelineAbilityAmountViaSubaction timelinePain = ScriptableObject.CreateInstance<TargetPerformEffectByTimelineAbilityAmountViaSubaction>();
            timelinePain.effects = [
                Effects.GenerateEffect(indirectOw, 3, Targeting.Slot_SelfSlot),
            ];

            PerformEffect_Item panopticalSkull = new PerformEffect_Item("PanopticalSkull_ID", null, false)
            {
                Item_ID = "PanopticalSkull_TW",
                Name = "Panoptical Skull",
                Flavour = "\"What has it seen? Why won't it stop seeing?\"",
                Description = "At the end of each turn, give the Opposing enemy Abomination and remove Forgetful from them, then deal 3 indirect damage to the Opposing enemy for each of their abilities in the timeline.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchVaughan"),
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(abomimate, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(rember, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(timelinePain, 1, Targeting.Slot_Front),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Vaughan_Inevitable_ACH";
            string unlockID = "AApocrypha_Vaughan_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(panopticalSkull.item, new ItemModdedUnlockInfo(panopticalSkull.Item_ID, ResourceLoader.LoadSprite("UnlockMarchWhitlockLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, panopticalSkull.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [panopticalSkull.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Panoptical Skull", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
