using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class MMIWearable : BaseWearableSO
    {
        [Header("Wearable Effects")]
        public bool _firstImmediateEffect;

        public EffectInfo[] _firstEffects;

        [Header("Wearable Secondary data")]
        public TriggerCalls[] _secondPerformTriggersOn;

        public EffectorConditionSO[] _secondPerformConditions;

        public bool _secondDoesPerformItemPopUp = true;

        [Header("Secondary Consume Data")]
        public bool _GetsConsumedOnSecondaryUse;

        [Header("Wearable Effects")]
        public bool _secondImmediateEffect;

        public EffectInfo[] _secondEffects;

        public List<string> _blacklist = [];

        public override bool IsItemImmediate => _firstImmediateEffect;

        public override bool DoesItemTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            IUnit caster = sender as IUnit;
            if (args is IUnit == false)
            {
                Debug.LogWarning("MMI | MMI wearable called with incorrect Trigger!");
                return; 
            }

            IUnit victim = args as IUnit;
            if (victim == null)
            {
                Debug.LogWarning("MMI | MMI wearable called with invalid Unit!");
                return;
            }
            AbilitySO chosenAbility = null;
            string abilID = "";
            if (victim.IsUnitCharacter)
            {
                CharacterCombat victimCH = victim as CharacterCombat;
                List<AbilitySO> charAbilities = new List<AbilitySO>();
                foreach (CombatAbility charAbil in victimCH.CombatAbilities) 
                { 
                    if (_blacklist.Contains(charAbil.ability.name) || _blacklist.Contains(charAbil.ability._abilityName))
                    {
                        continue;
                    }
                    charAbilities.Add(charAbil.ability);
                }
                if (charAbilities.Count > 1)
                {
                    int randomIndex = UnityEngine.Random.Range(0, charAbilities.Count);
                    chosenAbility = charAbilities[randomIndex];
                }
                else if (charAbilities.Count == 1)
                {
                    chosenAbility = charAbilities[0];
                }
                else
                {
                    Debug.LogWarning("MMI | No valid abilities found!");
                    return;
                }
            }
            else if (!victim.IsUnitCharacter)
            {
                EnemyCombat victimEN = victim as EnemyCombat;
                List<AbilitySO> enemAbilities = new List<AbilitySO>();
                foreach (CombatAbility enemAbil in victimEN.Abilities)
                {
                    if (_blacklist.Contains(enemAbil.ability.name) || _blacklist.Contains(enemAbil.ability._abilityName))
                    {
                        continue;
                    }
                    enemAbilities.Add(enemAbil.ability);
                }
                if (enemAbilities.Count > 1)
                {
                    int randomIndex = UnityEngine.Random.Range(0, enemAbilities.Count);
                    chosenAbility = enemAbilities[randomIndex];
                }
                else if (enemAbilities.Count == 1)
                {
                    chosenAbility = enemAbilities[0];
                }
                else
                {
                    Debug.LogWarning("MMI | No valid abilities found!");
                    return;
                }
            }
            abilID = chosenAbility.name;
            string itemInternalID = "MMI_" + abilID + "_ID";
            string itemID = "MMI_" + abilID + "_ExtraW";
            if (LoadedAssetsHandler.LoadedWearables.ContainsKey(itemID))
            {
                Debug.Log("MMI | Matching MMI instance for " + itemID + " found! retrieving...");
                LoadedAssetsHandler.LoadedWearables.TryGetValue(itemID, out BaseWearableSO item);
                TransformBrain(caster, item);
            }
            else
            {
                Debug.Log("MMI | No matching MMI instance for " + itemID + ", generating...");

                CharacterAbility mmiExtra = new()
                {
                    ability = chosenAbility,
                    cost = [Pigments.Yellow, Pigments.RedBlue],
                };

                ExtraAbility_Wearable_SMS stolenWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
                stolenWear._extraAbility = mmiExtra;

                CasterAddExtraAbilityIfMissingEffect DropAdd = ScriptableObject.CreateInstance<CasterAddExtraAbilityIfMissingEffect>();
                DropAdd._extraAbility = MMI.mmiDropWearable;

                DoublePerformEffect_Item mmiFill = new DoublePerformEffect_Item(itemInternalID, null, false)
                {
                    Item_ID = itemID,
                    Name = "MMI (" + victim.Name + ")",
                    Flavour = "\"" + FlavorTextHandler(victim) + "\"",
                    Description = "This party member now has \"" + chosenAbility._abilityName + "\" and \"Dispose\" as extra abilities. Dispose resets this item to its unfilled state." +
                    "\n<color=#" + ColorUtility.ToHtmlStringRGB(Color.yellow) + ">Lost when the game is closed!</color>",
                    IsShopItem = true,
                    ShopPrice = 6,
                    StartsLocked = true,
                    DoesPopUpInfo = false,
                    Icon = ResourceLoader.LoadSprite("UnlockDoulaNaudiz4" + ImageHandler(victim)),
                    TriggerOn = TriggerCalls.OnTurnStart,
                    Effects =
                    [
                        Effects.GenerateEffect(DropAdd, 1, Targeting.Slot_SelfSlot),
                    ],
                    EquippedModifiers = [stolenWear],//, MMI.mmiDropWearable],
                    SecondaryDoesPopUpInfo = false,
                    SecondaryConsumeOnUse = false,
                    SecondaryTriggerOn = [TriggerCalls.OnDeath, TriggerCalls.OnCombatEnd],
                    SecondaryEffects = [
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraLootDupeCasterItemEffect>(), 1, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                    ],
                };
                ItemUtils.JustAddItemSoItCanBeLoaded(mmiFill.item);
                TransformBrain(caster, mmiFill.item);
            }
        }

        public static void TransformBrain(IUnit caster, BaseWearableSO item)
        {
            caster.TrySetUpNewItem(item);
            CasterAddExtraAbilityIfMissingEffect DropAdd = ScriptableObject.CreateInstance<CasterAddExtraAbilityIfMissingEffect>();
            DropAdd._extraAbility = MMI.mmiDropWearable;
            CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(DropAdd)], caster));
        }

        public static string FlavorTextHandler(IUnit victim)
        {
            if (victim.IsUnitCharacter)
            {
                switch (victim.EntityID)
                {
                    case ("Pinec_CH"):
                        return "You know what, " + victim.Name + " probably deserves it.";
                    case ("Nowak_CH"):
                        return "The funny thing is, he's probably still in the party. How does that even work?";
                    case ("Gospel_CH"):
                        return "...";
                    case ("ContainmentBlock_CH"):
                        return "...";
                    default:
                        return "How very cruel, exposing " + victim.Name + " to the insensate horror of an MMI.";
                }
            }
            return "The bland acronym obscures the true horror of this monstrosity.";
        }

        public static string ImageHandler(IUnit victim)
        {
            //SPECIFIC UNITS

            //TYPE LISTS
            List<string> _typeListStone = ["ContainmentBlock_CH", "MarbleMaw_EN", "Euryale_EN", "UpStairs_EN", "DownStairs_EN", "Soothsayer_EN", "Platitude_EN", "Ridicule_EN"];

            List<string> _typeListStoneGospel = ["Gospel_CH", "MortalSpoggle_EN", "ColophonImmaculate_EN"];

            List<string> _typeListGlitchStack = ["Unmung_EN", "Malachai_EN"];

            if (_typeListStoneGospel.Contains(victim.EntityID)) { return "_StoneGospel"; }
            if (_typeListStone.Contains(victim.EntityID)) { return "_Stone"; }
            if (_typeListGlitchStack.Contains(victim.EntityID)) { return "_GlitchStack"; }

            //UNIT TYPES
            foreach (string unitType in victim.UnitTypes)
            {
                if (unitType == "Robot")
                {
                    return "_Robot";
                }
                if (unitType == "Anomaly")
                {
                    return "_Anomaly";
                }
            }

            //DEFAULT
            return "_Default";
        }

        public override void CustomOnTriggerAttached(IWearableEffector caller)
        {
            TriggerCalls[] secondPerformTriggersOn = _secondPerformTriggersOn;
            for (int i = 0; i < secondPerformTriggersOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondPerformTriggersOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.AddObserver(TryPerformWearable, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IWearableEffector caller)
        {
            TriggerCalls[] secondPerformTriggersOn = _secondPerformTriggersOn;
            for (int i = 0; i < secondPerformTriggersOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondPerformTriggersOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.RemoveObserver(TryPerformWearable, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void FinalizeCustomTriggerItem(object sender, object args, int triggerID)
        {
            if (sender is IWearableEffector wearableEffector && !wearableEffector.Equals(null) && !wearableEffector.IsWearableConsumed)
            {
                bool itemConsumed = false;
                if (_GetsConsumedOnSecondaryUse)
                {
                    itemConsumed = true;
                    wearableEffector.ConsumeWearable();
                }

                if (_secondDoesPerformItemPopUp)
                {
                    CombatManager.Instance.AddUIAction(new ShowItemInformationUIAction(wearableEffector.ID, GetItemLocData().text, itemConsumed, wearableImage));
                }

                IUnit caster = sender as IUnit;
                if (_secondImmediateEffect)
                {
                    CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction(_secondEffects, caster));
                }
                else
                {
                    CombatManager.Instance.AddSubAction(new EffectAction(_secondEffects, caster));
                }
            }
        }

        public void TryPerformWearable(object sender, object args)
        {
            if (!(sender is IWearableEffector wearableEffector) || wearableEffector.Equals(null) || !wearableEffector.CanWearableTrigger)
            {
                return;
            }

            if (_secondPerformConditions != null && !_secondPerformConditions.Equals(null))
            {
                EffectorConditionSO[] secondPerformConditions = _secondPerformConditions;
                for (int i = 0; i < secondPerformConditions.Length; i++)
                {
                    if (!secondPerformConditions[i].MeetCondition(wearableEffector, args))
                    {
                        return;
                    }
                }
            }

            if (_secondImmediateEffect)
            {
                CombatManager.Instance.ProcessImmediateAction(new PerformItemCustomImmediateAction(this, sender, args, 0));
            }
            else
            {
                CombatManager.Instance.AddSubAction(new PerformItemCustomAction(this, sender, args, 0));
            }
        }
    }
}
