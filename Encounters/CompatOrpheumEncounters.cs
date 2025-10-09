using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatOrpheumEncounters
    {
        public static void Add()
        {
            List<RandomEnemyGroup> musicmanEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_MusicMan_Easy_EnemyBundle"))._enemyBundles)
            {
                new([
                   "MusicMan_EN",
                   "Acolyte_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_MusicMan_Easy_EnemyBundle"))._enemyBundles = musicmanEasy;
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                List<RandomEnemyGroup> frostbiteMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("FrostbiteMed"))._enemyBundles)
                {
                    new([
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "SculptorBirdSculpture_EN",
                    ]),
                    new([
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "BloatfingerHiddenOrpheum_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("FrostbiteMed"))._enemyBundles = frostbiteMedium;
            }
        }
    }
}
