using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items.TradeItems
{
    public class SeekerBarMarrow
    {
        public static void Add()
        {
            FullHealthDetectionEffectorCondition Injured = ScriptableObject.CreateInstance<FullHealthDetectionEffectorCondition>();
            Injured.checkFullHealth = false;

            ExtraLootOptionsEffect mmmYummy = ScriptableObject.CreateInstance<ExtraLootOptionsEffect>();
            mmmYummy._itemName = "SeekerMarrow_ExtraW";

            RemovePassiveEffect UnSeek = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            UnSeek.m_PassiveID = "MarrowSeeker_PA";

            PerformEffectPassiveAbility marrowSeeker = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            marrowSeeker.name = "Item_MarrowSeeker_PA";
            marrowSeeker._passiveName = "Marrow Seeker";
            marrowSeeker.m_PassiveID = "MarrowSeeker_PA";
            marrowSeeker.passiveIcon = ResourceLoader.LoadSprite("IconMarrowSeeker");
            marrowSeeker._characterDescription = "The first time this party member kills something, add \"Bloody Marrow\" as a combat reward and remove this passive.";
            marrowSeeker._enemyDescription = "unaccountably peckish";
            marrowSeeker._triggerOn = [TriggerCalls.OnKill];
            marrowSeeker.doesPassiveTriggerInformationPanel = true;
            marrowSeeker.effects =
            [
                Effects.GenerateEffect(mmmYummy, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(UnSeek, 1, Targeting.Slot_SelfSlot),
            ];

            AddPassiveEffect instillHunger = ScriptableObject.CreateInstance<AddPassiveEffect>();
            instillHunger._passiveToAdd = marrowSeeker;

            PerformEffect_Item marrow = new PerformEffect_Item("SeekerMarrow_ID", null, false)
            {
                Item_ID = "SeekerMarrow_ExtraW",
                Name = "Bloody Marrow",
                Flavour = "\"Her strength came from us, and will return to us.\"",
                Description = "At the start of combat, if this party member is not at full health, heal them 6 health and consume this item." +
                "\nThe first time this party member kills something this combat, receive a new copy of this item.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("SeekerBarItemMarrow"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [Injured],
                ConsumeOnUse = true,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 6, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(instillHunger, 1, Targeting.Slot_SelfSlot),
                ],
            };

            marrow.item._ItemTypeIDs =
            [
                "FoodID",
                "Meat",
            ];

            ItemUtils.JustAddItemSoItCanBeLoaded(marrow.item);
        }
    }
}
