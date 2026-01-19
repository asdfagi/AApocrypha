using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class CustomField
    {
        public static void Add()
        {
            if (!LoadedDBsHandler.StatusFieldDB.FieldEffects.ContainsKey("Hoarfrost_ID"))
            {
                SlotStatusEffectInfoSO HoarfrostInfo = ScriptableObject.CreateInstance<SlotStatusEffectInfoSO>();
                HoarfrostInfo._fieldName = "Hoarfrost";
                HoarfrostInfo._description = "Receive 2-4 direct damage on performing an ability in Hoarfrost.\nHoarfrost is reduced by 1 at the end of each turn.";//\nHoarfrost and Fire override each other if one is applied to a position occupied by the other.";
                HoarfrostInfo.icon = ResourceLoader.LoadSprite("IconHoarfrost");

                LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Constricted_ID", out FieldEffect_SO constricted);
                SlotStatusEffectInfoSO baseinfo = constricted.EffectInfo;

                HoarfrostInfo._applied_SE_Event = "event:/AASFX/HoarfrostApply";
                HoarfrostInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                HoarfrostInfo._updated_SE_Event = baseinfo._updated_SE_Event;
                //HoarfrostInfo.m_CharacterLayoutTemplate = baseinfo.m_CharacterLayoutTemplate;
                //HoarfrostInfo.m_EnemyLayoutTemplate = baseinfo.m_EnemyLayoutTemplate;
                GameObject HoarfrostCharacter = AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_FieldEffects/Hoarfrost_Character_FEL.prefab");
                HoarfrostInfo.m_CharacterLayoutTemplate = HoarfrostCharacter.GetComponent<CharacterFieldEffectLayout>();

                GameObject HoarfrostEnemy = AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_FieldEffects/Hoarfrost_Enemy_FEL.prefab");
                HoarfrostInfo.m_EnemyLayoutTemplate = HoarfrostEnemy.GetComponent<EnemyFieldEffectLayout>();

                Hoarfrost hoarfrost = ScriptableObject.CreateInstance<Hoarfrost>();
                hoarfrost._FieldID = "Hoarfrost_ID";
                hoarfrost._EffectInfo = HoarfrostInfo;

                LoadedDBsHandler.StatusFieldDB.AddNewFieldEffect(hoarfrost, true);

                IntentInfoBasic HoarfrostIntent = new()
                {
                    _color = Color.white,
                    _sprite = ResourceLoader.LoadSprite("IconHoarfrost")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Field_Hoarfrost", HoarfrostIntent);

                IntentInfoBasic HoarfrostRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("IconHoarfrost")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Field_Hoarfrost", HoarfrostRemIntent);
            }
        }
    }
}
