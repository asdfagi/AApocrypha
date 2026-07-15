using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class MemoryOfChange
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

            IsSpecificCharacterEffectorCondition notNymph = ScriptableObject.CreateInstance<IsSpecificCharacterEffectorCondition>();
            notNymph._character = "TeneralNymph_CH";
            notNymph._passIfTrue = false;

            CasterTransformationEffect becomeNymph = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            becomeNymph._maintainMaxHealth = false;
            becomeNymph._fullyHeal = true;
            becomeNymph._characterTransformation = "TeneralNymph_CH";

            SpawnEnemyAnywhereEffect shedExuvia = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            shedExuvia.enemy = LoadedAssetsHandler.GetEnemy("DiscardedExuviae_EN");
            shedExuvia.givesExperience = false;
            shedExuvia._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffect_Item memory = new PerformEffect_Item("MemoryOfChange_ID", null, false)
            {
                Item_ID = "MemoryOfChange_TW",
                Name = "Memory of Change",
                Flavour = "\"Shoot Here!\"",
                Description = "Prevent this party member's death through ecdysis, leaving behind a pile of Discarded Exuviae on the enemy side. At the end of combat, this party member recovers. If either the Nymph or Exuviae are killed, this item is destroyed." +
                "\nThis item does nothing if this party member has Dying or Inanimate as passives or all enemy positions are full.",
                IsShopItem = false,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyVaughan"),
                TriggerOn = TriggerCalls.OnDamaged,
                Conditions = [ScriptableObject.CreateInstance<UnitDeadEffectorCondition>(), notNymph, NotDying, NotStatue, ScriptableObject.CreateInstance<EmptyEnemyPositionsEffectorCondition>()],
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(becomeNymph, 1),
                    Effects.GenerateEffect(shedExuvia, 1),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Vaughan_Forgotten_ACH";
            string unlockID = "AApocrypha_Vaughan_Forgotten_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(memory.item, new ItemModdedUnlockInfo(memory.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, memory.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [memory.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Memory of Change", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
