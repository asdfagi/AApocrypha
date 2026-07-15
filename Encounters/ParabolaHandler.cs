using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ParabolaHandler
    {
        // GENERAL
        public static string _parabolaID_SmokingShore = "Parabola_SmokingShore";
        public static string _parabolaID_DrownedForest = "Parabola_DrownedForest";
        public static string _parabolaID_PillaredCoast = "Parabola_PillaredCoast";
        public static string _parabolaID_SeaSpines = "Parabola_SeaOfSpines";
        // ENV IDs
        public static string _parabolaID_SmokingShore_Env = _parabolaID_SmokingShore + "_Env";
        public static string _parabolaID_DrownedForest_Env = _parabolaID_DrownedForest + "_Env";
        public static string _parabolaID_PillaredCoast_Env = _parabolaID_PillaredCoast + "_Env";
        public static string _parabolaID_SeaSpines_Env = _parabolaID_SeaSpines + "_Env";
        public static void AddEnvironments()
        {
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Parabola/Parabola_SmokingShore_CombatEnv.prefab", _parabolaID_SmokingShore_Env, AApocrypha.assetBundle);
            EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Parabola/Parabola_DrownedForest_CombatEnv.prefab", _parabolaID_DrownedForest_Env, AApocrypha.assetBundle);
            //EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Parabola/Parabola_PillaredCoast_CombatEnv.prefab", _parabolaID_PillaredCoast_Env, AApocrypha.assetBundle);
            //EnvironmentTools.PrepareCombatEnvPrefab("Assets/Apocrypha_Environments/Parabola/Parabola_SeaSpines_CombatEnv.prefab", _parabolaID_SeaSpines_Env, AApocrypha.assetBundle);
        }
    }
}
