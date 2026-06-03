using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace A_Apocrypha.CustomStatusField
{
    public class CustomStatus
    {
        public static void Add()
        {
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Poisoned_ID"))
            {
                StatusEffectInfoSO PoisonedInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                PoisonedInfo._statusName = "Poisoned";
                PoisonedInfo._description = "Receive indirect damage between 1 and the amount of Poisoned on turn end.\nPoisoned is reduced by 25-75% at the end of each turn.";
                PoisonedInfo.icon = ResourceLoader.LoadSprite("IconPoisoned");

                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Scars_ID", out StatusEffect_SO scars);
                StatusEffectInfoSO baseinfo = scars.EffectInfo;

                PoisonedInfo._applied_SE_Event = baseinfo._applied_SE_Event;
                PoisonedInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                PoisonedInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                Poisoned poisoned = ScriptableObject.CreateInstance<Poisoned>();
                poisoned._StatusID = "Poisoned_ID";
                poisoned._EffectInfo = PoisonedInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(poisoned, true);

                IntentInfoBasic PoisonedIntent = new()
                {
                    _color = Color.white,
                    _sprite = PoisonedInfo.icon,
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Poisoned", PoisonedIntent);

                IntentInfoBasic PoisonedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = PoisonedInfo.icon,
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Poisoned", PoisonedRemIntent);
            }

            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Irradiated_ID"))
            {
                StatusEffectInfoSO IrradiatedInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                IrradiatedInfo._statusName = "Irradiated";
                IrradiatedInfo._description = "On performing an ability, reduce this unit's max health by 2. Irradiated is reduced by 1 at the end of each turn.";
                IrradiatedInfo.icon = ResourceLoader.LoadSprite("IconIrradiated");

                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Scars_ID", out StatusEffect_SO scars);
                StatusEffectInfoSO baseinfo = scars.EffectInfo;

                IrradiatedInfo._applied_SE_Event = "event:/AASFX/RadiationApply";
                IrradiatedInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                IrradiatedInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                Irradiated irradiated = ScriptableObject.CreateInstance<Irradiated>();
                irradiated._StatusID = "Irradiated_ID";
                irradiated._EffectInfo = IrradiatedInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(irradiated, true);

                IntentInfoBasic IrradiatedIntent = new()
                {
                    _color = Color.white,
                    _sprite = IrradiatedInfo.icon,
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Irradiated", IrradiatedIntent);

                IntentInfoBasic IrradiatedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = IrradiatedInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Irradiated", IrradiatedRemIntent);
            }

            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Hexed_ID"))
            {
                StatusEffectInfoSO HexedInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                HexedInfo._statusName = "Hexed";
                HexedInfo._description = "While Hexed, this party member's abilities (except their basic ability) cost 1 additional Purple Pigment. Hexed is reduced by 1 at the end of each turn.";
                HexedInfo.icon = ResourceLoader.LoadSprite("IconCometGaze");

                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Cursed_ID", out StatusEffect_SO cursed);
                StatusEffectInfoSO baseinfo = cursed.EffectInfo;

                HexedInfo._applied_SE_Event = baseinfo._applied_SE_Event;//"event:/AASFX/RadiationApply";
                HexedInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                HexedInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                Hexed hexed = ScriptableObject.CreateInstance<Hexed>();
                hexed._StatusID = "Hexed_ID";
                hexed._EffectInfo = HexedInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(hexed, true);

                IntentInfoBasic HexedIntent = new()
                {
                    _color = Color.white,
                    _sprite = HexedInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Hexed", HexedIntent);

                IntentInfoBasic HexedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = HexedInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Hexed", HexedRemIntent);
            }

            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Smouldering_ID"))
            {
                StatusEffectInfoSO SmoulderingInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                SmoulderingInfo._statusName = "Smouldering";
                SmoulderingInfo._description = "At the end of each round, take fire damage equal to the amount of Smouldering. Smouldering does not decrease over time. Smouldering is capped at 7, with any Smouldering applied above 7 instead being converted to indirect fire damage.";
                SmoulderingInfo.icon = ResourceLoader.LoadSprite("IconSmouldering2");

                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Scars_ID", out StatusEffect_SO scars);
                StatusEffectInfoSO baseinfo = scars.EffectInfo;

                SmoulderingInfo._applied_SE_Event = baseinfo._applied_SE_Event;
                SmoulderingInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                SmoulderingInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                Smouldering smouldering = ScriptableObject.CreateInstance<Smouldering>();
                smouldering._StatusID = "Smouldering_ID";
                smouldering._EffectInfo = SmoulderingInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(smouldering, true);

                IntentInfoBasic SmoulderingIntent = new()
                {
                    _color = Color.white,
                    _sprite = SmoulderingInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Smouldering", SmoulderingIntent);

                IntentInfoBasic SmoulderingRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = SmoulderingInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Smouldering", SmoulderingRemIntent);
            }

            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Overclock_ID"))
            {
                StatusEffectInfoSO OverclockInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                OverclockInfo._statusName = "Overclock";
                OverclockInfo._description = "While Overclocked, all direct damage dealt is doubled." +
                    "\nPerforming an ability reduces Overclock by 1.";
                OverclockInfo.icon = ResourceLoader.LoadSprite("IconOverclock");

                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Linked_ID", out StatusEffect_SO linked);
                StatusEffectInfoSO baseinfo = linked.EffectInfo;

                OverclockInfo._applied_SE_Event = "event:/AASFX/OverclockApply";
                OverclockInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                OverclockInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                Overclock overclock = ScriptableObject.CreateInstance<Overclock>();
                overclock._StatusID = "Overclock_ID";
                overclock._EffectInfo = OverclockInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(overclock, true);

                IntentInfoBasic OverclockIntent = new()
                {
                    _color = Color.white,
                    _sprite = OverclockInfo.icon,
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Overclock", OverclockIntent);

                IntentInfoBasic OverclockRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = OverclockInfo.icon,
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Overclock", OverclockRemIntent);
            }

            // Into The Abyss - Petrified
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Petrified_ID"))
            {
                StatusEffectInfoSO ModuInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                ModuInfo._statusName = "Petrified";
                ModuInfo._description = "If this unit is not already Inanimate, they gain Inanimate until they lose this status. 1 Petrified is lost at the beginning of each turn.";
                ModuInfo.icon = ResourceLoader.LoadSprite("status_petrified.png");


                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(StatusField.DivineProtection._StatusID, out StatusEffect_SO frail);
                StatusEffectInfoSO baseinfo = frail.EffectInfo;

                ModuInfo._applied_SE_Event = "event:/AASFX/ITA/Petrified";
                ModuInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                ModuInfo._updated_SE_Event = baseinfo._updated_SE_Event;
                
                Petrified modu = ScriptableObject.CreateInstance<Petrified>();
                modu._StatusID = "Petrified_ID";
                modu._EffectInfo = ModuInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(modu, true);

                IntentInfoBasic PetrifiedIntent = new()
                {
                    _color = Color.white,
                    _sprite = ModuInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Petrified", PetrifiedIntent);

                IntentInfoBasic PetrifiedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ModuInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Petrified", PetrifiedRemIntent);
            }

            // Into The Abyss - Celerity
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Celerity_ID"))
            {
                string statID = "Celerity_ID";
                string intentID = "Status_Celerity";

                StatusEffectInfoSO ModuInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                ModuInfo._statusName = "Celerity";
                ModuInfo._description = "For each point of Celerity, this unit gets to perform an extra ability next turn, after which 1 point of Celerity is lost." +
                    "\nOn characters, on using an ability, refresh after performing the ability and reduce Celerity by 1.";
                ModuInfo.icon = ResourceLoader.LoadSprite("Celerity.png");


                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(StatusField.Focused._StatusID, out StatusEffect_SO focus);
                StatusEffectInfoSO baseinfo = focus.EffectInfo;

                ModuInfo._applied_SE_Event = baseinfo._applied_SE_Event;//"event:/AASFX/ITA/Petrified";
                ModuInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                ModuInfo._updated_SE_Event = baseinfo._updated_SE_Event;
                
                CeleritySE_SO modu = ScriptableObject.CreateInstance<CeleritySE_SO>();
                modu._StatusID = statID;
                modu._EffectInfo = ModuInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(modu, true);

                IntentInfoBasic CelerityIntent = new()
                {
                    _color = Color.white,
                    _sprite = ModuInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent(intentID, CelerityIntent);

                IntentInfoBasic CelerityRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ModuInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_" + intentID, CelerityRemIntent);
            }
        }
    }
}
