using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RerollCombatEnvEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            OverworldCombatSharedDataSO current = CombatManager.Instance._informationHolder.CombatData;
            if (current.enemyBundle.SpecialEnvironment != "")
            {
                Debug.Log("special environment detected - skipping");
                return false;
            };

            //destroy old environment?
            //GameObject.Destroy(CombatManager.Instance._combatEnvHandler);
            CombatManager.Instance._combatEnvHandler.gameObject.SetActive(false);
            //CombatManager.Instance._combatEnvHandlr.

            //generate environment
            CombatManager.Instance.GenerateCombatEnvironment(current.combatEnvironmentPrefabName, current.enemyBundle.SpecialEnvironment);


            //ambiance
            /*if (!CombatManager.Instance._isGameRun)
            {
                if (!CombatManager.Instance._combatEnvHandler.HasExtraAmbience)
                {
                    MethodInfo method = typeof(AudioControllerSO).GetMethod(nameof(AudioControllerSO.ForceSetAmbience));

                    if (typeof(ZoneDataBaseSO).GetField("CombatAmbience") != null)
                        method.Invoke(CombatManager.Instance._soundManager, [typeof(ZoneDataBaseSO).GetField("CombatAmbience").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03"))]);
                    else if (typeof(ZoneDataBaseSO).GetField("m_AmbienceID") != null)
                        method.Invoke(CombatManager.Instance._soundManager, [typeof(ZoneDataBaseSO).GetField("m_AmbienceID").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03")), typeof(ZoneDataBaseSO).GetField("m_CombatAmbVarID").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03"))]);

                }
                else
                {
                    CombatManager.Instance._soundManager.StartExtraCombatAmbienceEvent(CombatManager.Instance._combatEnvHandler.ExtraAmbienceSound);
                }
            }
            else if (!CombatManager.Instance._combatEnvHandler.HasExtraAmbience)
            {
                //CombatManager.Instance._soundManager.TrySetAmbienceState(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03").CombatAmbience);

                MethodInfo method = typeof(AudioControllerSO).GetMethod(nameof(AudioControllerSO.TrySetAmbienceState));

                if (typeof(ZoneDataBaseSO).GetField("CombatAmbience") != null)
                    method.Invoke(CombatManager.Instance._soundManager, [typeof(ZoneDataBaseSO).GetField("CombatAmbience").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03"))]);
                else if (typeof(ZoneDataBaseSO).GetField("m_AmbienceID") != null)
                    method.Invoke(CombatManager.Instance._soundManager, [typeof(ZoneDataBaseSO).GetField("m_AmbienceID").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03")), typeof(ZoneDataBaseSO).GetField("m_CombatAmbVarID").GetValue(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03"))]);

            }
            else
            {
                CombatManager.Instance._soundManager.TryStopAmbience();
                CombatManager.Instance._soundManager.StartExtraCombatAmbienceEvent(CombatManager.Instance._combatEnvHandler.ExtraAmbienceSound);
            }*/

            //environmenet notifs
            CombatManager.Instance._combatEnvHandler.SetUpNotifications();
            CombatManager.Instance._combatEnvHandler.InitializeExtraData(CombatManager.Instance._informationHolder.Game);

            return true;
        }
    }
}
