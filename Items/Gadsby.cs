using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class Gadsby
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS warablPassivGadsby = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            warablPassivGadsby._extraPassiveAbility = Passives.GetCustomPassive("Gadsby_PA");

            DamagePercentageModifier_Item gadsbyBook = new DamagePercentageModifier_Item("EyelessSkull_ID", 25, true, false, true)
            {
                Item_ID = "Gadsby_SW",
                Name = "Unusual Book",
                Flavour = "\"Lipogram omitting fifth symbol. Astounding!\"",
                Description = "This unit inflicts 25% additional harm against any combatant with an alias containing that fifth symbol.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("GadsbyItem"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<TargetContainsECondition>()],
                EquippedModifiers = [warablPassivGadsby],
                AffectDamageDealtInsteadOfReceived = true,
                UseSimpleIntegerInsteadOfDamage = false,
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(gadsbyBook.item, new ItemModdedUnlockInfo("Gadsby_SW", ResourceLoader.LoadSprite("GadsbyItemLocked", null, 32, null), "AApocrypha_Misc_Gadsby_ACH"));
        }
    }
}
