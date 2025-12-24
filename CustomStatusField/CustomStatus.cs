using System;
using System.Collections.Generic;
using System.Text;
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
                    _sprite = ResourceLoader.LoadSprite("IconPoisoned")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Poisoned", PoisonedIntent);

                IntentInfoBasic PoisonedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("IconPoisoned")
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
                    _sprite = ResourceLoader.LoadSprite("IconIrradiated")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Irradiated", IrradiatedIntent);

                IntentInfoBasic IrradiatedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("IconIrradiated")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Irradiated", IrradiatedRemIntent);
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
                //ModuInfo.
                Petrified modu = ScriptableObject.CreateInstance<Petrified>();
                modu._StatusID = "Petrified_ID";
                modu._EffectInfo = ModuInfo;
                //modu.IsPositive = false;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(modu, true);

                IntentInfoBasic PetrifiedIntent = new()
                {
                    _color = Color.white,
                    _sprite = ResourceLoader.LoadSprite("status_petrified.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Petrified", PetrifiedIntent);

                IntentInfoBasic PetrifiedRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("status_petrified.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Petrified", PetrifiedRemIntent);
            }
        }
    }
}
