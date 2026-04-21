using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class BridgeCircuit
    {
        public static void Add()
        {
            MassSwapZoneEffect Shuffle = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            StatusEffect_Apply_Effect AddScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddScars._Status = StatusField.Scars;

            PassiveLockingEffect AnchoredLock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AnchoredLock._lock = true;
            AnchoredLock.m_PassiveIDs = [Passives.Anchored.m_PassiveID];

            PassiveLockingEffect AnchoredUnlock = ScriptableObject.CreateInstance<PassiveLockingEffect>();
            AnchoredUnlock._lock = false;
            AnchoredUnlock.m_PassiveIDs = [Passives.Anchored.m_PassiveID];

            Ability warpAbil = new Ability("Warp Protocol", "AApocrypha_ItemWarpProtocol_A")
            {
                Description = "Deal 1-3 damage to all units, then randomly move all units. This ignores Anchored." +
                "\nApply 1 random positive status effect to all party members and 1 random negative status effect to all enemies." +
                "\nApply 1 Scar to this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemBridgeCircuitAbility"),
                Cost = [Pigments.BlueYellow, Pigments.YellowBlue],
                Visuals = CustomVisuals.StaticVisualsSO,
                AnimationTarget = Targeting.AllUnits,
                Effects =
                [
                    Effects.GenerateEffect(AnchoredLock),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<RandomDamageBetweenPreviousAndEntryEffect>(), 3, Targeting.AllUnits),
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_AllyAllSlots),
                    Effects.GenerateEffect(Shuffle, 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<StatusEffect_ApplyRandom_PositiveEffect>(), 1, Targeting.Slot_AllyAllSlots),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<StatusEffect_ApplyRandom_NegativeEffect>(), 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(AddScars, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(AnchoredUnlock),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.VeryFast
            };
            warpAbil.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Swap_Mass)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Damage_3_6)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Damage_3_6)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, [nameof(IntentType_GameIDs.Misc_Hidden)]);
            warpAbil.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars)]);

            ExtraAbility_Wearable_SMS warpWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            warpWear._extraAbility = warpAbil.GenerateCharacterAbility(true);

            // Add to Construct Pool (probably)
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, warpWear];

            PerformEffect_Item bridgeCircuit = new PerformEffect_Item("FaultyBridgeCircuit_ID", null, false)
            {
                Item_ID = "FaultyBridgeCircuit_TW",
                Name = "Faulty Bridge Circuit",
                Flavour = "\"An ongoing failure.\"",
                Description = "Adds the ability \"Warp Protocol\" to this party member, an unwieldy distortion of reality that damages and shuffles everything.",
                IsShopItem = false,
                ShopPrice = 4,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchNaudiz4"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                ],
                EquippedModifiers = [warpWear],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Inevitable_ACH";
            string unlockID = "AApocrypha_Naudiz4_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(bridgeCircuit.item, new ItemModdedUnlockInfo(bridgeCircuit.Item_ID, ResourceLoader.LoadSprite("UnlockMarchNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, bridgeCircuit.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [bridgeCircuit.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Faulty Bridge Circuit", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
