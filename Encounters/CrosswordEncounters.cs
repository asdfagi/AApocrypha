using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CrosswordEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("CrosswordSign", ResourceLoader.LoadSprite("CrosswordTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API crosswordEasy = new EnemyEncounter_API(0, Abyss.H.Crossword.Easy, "CrosswordSign")
                {
                    MusicEvent = "event:/AAMusic/ProjectMoon/RunBongyRun",
                    RoarEvent = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,//"event:/AASFX/Nothing_SFX",//"event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 1, "BasicElemental_EN");
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 1, "BFElemental_EN", 1, "Wug_EN");
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 2, "Wug_EN");
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 1, "WanderFellow_EN");
                crosswordEasy.SimpleAddEncounter(1, "Crossword_EN", 1, "Sycophant_EN", 1, "Streetlight_EN");
                crosswordEasy.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Crossword.Easy, 5, "TheAbyss_Zone3", BundleDifficulty.Easy);
            }
        }
    }
}
