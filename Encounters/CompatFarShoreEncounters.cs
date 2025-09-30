using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatFarShoreEncounters
    {
        public static void Add()
        {
            List<RandomEnemyGroup> mudLungEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_MudLung_Easy_EnemyBundle"))._enemyBundles)
            {
                new([
                   "MudLung_EN",
                   "SandSifter_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_MudLung_Easy_EnemyBundle"))._enemyBundles = mudLungEasy;
            List<RandomEnemyGroup> flarbHard = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle"))._enemyBundles)
            {
                new([
                   "Flarb_EN",
                   "SandSifter_EN",
                ]),
                new([
                   "Flarb_EN",
                   "TearDrinker_EN",
                   "Flarblet_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Flarb_Hard_EnemyBundle"))._enemyBundles = flarbHard;
        }
    }
}
