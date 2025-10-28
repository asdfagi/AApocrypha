using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatGardenEncounters
    {
        public static void Add()
        {
            List<RandomEnemyGroup> ministerMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_GigglingMinister_Medium_EnemyBundle"))._enemyBundles)
            {
                new([
                   "GigglingMinister_EN",
                   "MachineGnomes_EN",
                   "MachineGnomes_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_GigglingMinister_Medium_EnemyBundle"))._enemyBundles = ministerMedium;
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                List<RandomEnemyGroup> chaliceMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("ChaliceMed"))._enemyBundles)
                {
                    new([
                       "GodsChalice_EN",
                       "MachineGnomes_EN",
                       "MachineGnomes_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("ChaliceMed"))._enemyBundles = chaliceMedium;
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                List<RandomEnemyGroup> metatronHard = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("MetatronHard"))._enemyBundles)
                {
                    new([
                       "Metatron_EN",
                       "MachineGnomes_EN",
                       "MachineGnomes_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("MetatronHard"))._enemyBundles = metatronHard;
            }
        }
    }
}
