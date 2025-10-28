using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatSirenEncounters
    {
        public static void Add()
        {
            List<RandomEnemyGroup> boilerEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerEasy"))._enemyBundles)
            {
                new([
                   "Boiler_EN",
                   "Boiler_EN",
                   "SandSifter_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerEasy"))._enemyBundles = boilerEasy;
        }
    }
}
