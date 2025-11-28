using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class CranesSavesTheRun
    {
        public static void Add()
        {
            DirectDeathEffect vaporize = ScriptableObject.CreateInstance<DirectDeathEffect>();
            vaporize._obliterationDeath = true;

            CasterTransformationEffect becomecranes = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            becomecranes._characterTransformation = "Cranes_CH";
            becomecranes._maintainMaxHealth = false;

            ExtraAbility_Wearable_SMS craneswearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();

            Ability cranesability = new Ability("Save The Run", "AApocrypha_SaveTheRun_A")
            {
                Description = "Thank you, Cranes.",
                AbilitySprite = ResourceLoader.LoadSprite("ItemCranesAbility"),
                Visuals = CustomVisuals.CranesHeavenVisualsSO,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Cost = [],
                Effects =
                    [
                        Effects.GenerateEffect(becomecranes, 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(vaporize, 1, Targeting.Slot_SelfSlot),
                    ],
                Rarity = Rarity.VeryRare,
                Priority = Priority.Normal,
            };
            cranesability.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_Death)]);

            craneswearable._extraAbility = cranesability.GenerateCharacterAbility(true);

            PerformEffect_Item cranes = new PerformEffect_Item("CranesSavesTheRun_ID", null, false)
            {
                Item_ID = "CranesSavesTheRun_ExtraW",
                Name = "Cranes Saves The Run",
                Flavour = "\"Thanks, Cranes.\"",
                Description = "Adds the ability \"Save The Run\" to this party member, a sacrifice.",
                IsShopItem = false,
                ShopPrice = 1,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("CranesSavesTheRunItem"),
                EquippedModifiers = [craneswearable],
            };

            ItemUtils.JustAddItemSoItCanBeLoaded(cranes.item);
        }
    }
}
