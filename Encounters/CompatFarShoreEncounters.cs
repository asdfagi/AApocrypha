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
            List<RandomEnemyGroup> flamingoMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_FlaMingGoa_Medium_EnemyBundle"))._enemyBundles)
            {
                new([
                   "FlaMinGoa_EN",
                   "MudLung_EN",
                   "FungusColumn_EN",
                ]),
                new([
                   "FlaMinGoa_EN",
                   "MunglingMudLung_EN",
                   "FungusColumn_EN",
                ]),
                new([
                   "FlaMinGoa_EN",
                   "MudLung_EN",
                   "SandSifter_EN",
                ]),
                new([
                   "FlaMinGoa_EN",
                   "Wringle_EN",
                   "SandSifter_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_FlaMingGoa_Medium_EnemyBundle"))._enemyBundles = flamingoMedium;
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                List<RandomEnemyGroup> draugrEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Draugr_Easy_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "Draugr_EN",
                       "FungusColumn_EN",
                    ]),
                    new([
                       "Draugr_EN",
                       "SandSifter_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Draugr_Easy_EnemyBundle"))._enemyBundles = draugrEasy;
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                List<RandomEnemyGroup> ufoMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_ToyUfo_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "ToyUfo_EN",
                       "JumbleGuts_Waning_EN",
                       "SandSifter_EN",
                    ]),
                    new([
                       "ToyUfo_EN",
                       "MudLung_EN",
                       "FungusColumn_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone01_ToyUfo_Medium_EnemyBundle"))._enemyBundles = ufoMedium;
            }
        }
    }
}
