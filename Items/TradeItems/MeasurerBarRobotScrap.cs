using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items.TradeItems
{
    public class MeasurerBarRobotScrap
    {
        public static void Add()
        {
            PerformEffect_Item scrap1 = new PerformEffect_Item("AA_RobotBarLoot1_ID", null, false)
            {
                Item_ID = "AA_RobotBarLoot1_ExtraW",
                Name = "Scrap",
                Flavour = "\"Property of the Institute.\"",
                Description = "Useless scrap." +
                "\nMaybe someone else will be interested in this?",
                IsShopItem = false,
                ShopPrice = 1,
                DoesPopUpInfo = false,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("RobotQuestLoot1"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [],
                ConsumeOnUse = false,
                Effects =
                [
                ],
            };

            scrap1.item._ItemTypeIDs =
            [
                "Measurer_ScrapMetal",
            ];

            ItemUtils.JustAddItemSoItCanBeLoaded(scrap1.item);
        }
    }
}
