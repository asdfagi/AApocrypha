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
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                List<RandomEnemyGroup> tanehineriEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Tanehineri_Easy_EnemyBundle"))._enemyBundles)
                {
                    new([
                        "Tanehineri_EN",
                        "Tanehineri_EN",
                        "MachineGnomes_EN",
                        "MachineGnomes_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Tanehineri_Easy_EnemyBundle"))._enemyBundles = tanehineriEasy;
                List<RandomEnemyGroup> wrkMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_WRK_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "WRK_EN",
                       "MachineGnomes_EN",
                       "MachineGnomes_EN",
                    ]),
                    new([
                       "WRK_EN",
                       "WRK_EN",
                       "MachineGnomes_EN",
                       "MachineGnomes_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_WRK_Medium_EnemyBundle"))._enemyBundles = wrkMedium;
                List<RandomEnemyGroup> kcolclockHard = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Kcolclock_Hard_EnemyBundle"))._enemyBundles)
                {
                    new([
                        "Kcolclock_EN",
                        "CrimsonLogos_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Kcolclock_Hard_EnemyBundle"))._enemyBundles = kcolclockHard;
            }
        }
    }
}
