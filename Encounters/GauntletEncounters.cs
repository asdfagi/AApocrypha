using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BepInEx.Logging;

namespace A_Apocrypha.Encounters
{
    public static class GauntletEncounters
    {
        public static List<string> enemyList_Shore_Normal;
        public static List<string> enemyList_Shore_Hard;
        public static List<string> enemyList_Shore_Size2;
        public static List<string> enemyList_Orpheum_Normal;
        public static List<string> enemyList_Orpheum_Hard;
        public static List<string> enemyList_Orpheum_Size2;
        public static List<string> enemyList_Siren_Normal;
        public static List<string> enemyList_Siren_Hard;
        public static List<string> enemyList_Siren_Size2;
        public static List<string> enemyList_Garden_Normal;
        public static List<string> enemyList_Garden_Hard;
        public static List<string> enemyList_Garden_Size2;

        public static DebugCommand generateAndAddGauntletEncounter;

        public static int incrementation = 0;

        public static void Add()
        {
            Portals.AddPortalSign("GauntletSimulation_Sign", ResourceLoader.LoadSprite("GauntletIcon", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            OverworldRooms.Prepare_Enemy_RoomPrefab("Assets/Apocrypha_Rooms/GauntletFight.prefab", "GauntletFight", AApocrypha.assetBundle);

            generateAndAddGauntletEncounter = new DebugCommand("addgauntletencounter", "generate and add a gauntlet encounter to the end of the current zone.", new List<DebugCommandArgument>
            {
                new StringCommandArgument("zone", new AutocompletionGroup(ZoneStrings)),
                new BoolCommandArgumnt("hard"),
            }, delegate (List<FilledCommandArgument> args)
            {
                Debug.Log($"this command was executed with parameters zone: {args[0].Read<string>()}, hard: {args[1].Read<string>()}");
                string area = args[0].Read<string>();
                bool hard = args[1].Read<bool>();
                RunDataSO run = LoadedDBsHandler.InfoHolder.Run;
                if (run == null)
                {
                    DebugController.Instance.WriteLine("No active run.", LogLevel.Error);
                }
                else if (run.CurrentZoneID >= run.zoneData.Count)
                {
                    DebugController.Instance.WriteLine("No zone.", LogLevel.Error);
                }
                else
                {
                    RunZoneData runZoneData = run.zoneData[run.CurrentZoneID];
                    ZoneDataBaseSO zoneDataBaseSO = runZoneData.LoadZoneDB();
                    if (zoneDataBaseSO is ZoneBGDataBaseSO zone)
                    {
                        zone._zoneData = runZoneData;
                        zone.GenerateGauntletEncounterAndCard(area, hard);
                    }
                    else
                    {
                        DebugController.Instance.WriteLine("Invalid zone.", LogLevel.Error);
                    }
                }
            });

            DebugController.Commands.children.Add(generateAndAddGauntletEncounter);

            enemyList_Shore_Normal.AddRange([
                "Mung_EN",
                "Mungie_EN",
                "MudLung_EN",
                "Keko_EN",
                "FlaMinGoa_EN",
                "JumbleGuts_Clotted_EN",
                "JumbleGuts_Waning_EN",
            ]);
            enemyList_Shore_Hard.AddRange([
                "MunglingMudLung_EN",
                "Flarblet_EN",
                "JumbleGuts_Hollowing_EN",
                "JumbleGuts_Flummoxing_EN",
            ]);
            enemyList_Shore_Size2.AddRange([
                "Flarb_EN",
                "Voboola_EN",
                "Kecastle_EN",
            ]);
        }

        public static void GenerateGauntletEncounterAndCard(this ZoneBGDataBaseSO self, string zone, bool hard)
        {
            if (self._zoneData == null) Debug.LogWarning("Gauntlet | warning - zonedata is null!");
            else if (self._zoneData.ZonePiles == null) Debug.LogWarning("Gauntlet | warning - zonepiles is null!");
            if (self._zoneData.ZonePiles.Length <= 0) return;

            string encounterID = $"Gauntlet_{self.ZoneName}_{zone}Sim_{(hard ? "Hard" : "Normal")}_{incrementation}_EnemyBundle";
            incrementation++;

            EnemyEncounter_API gauntletEncounter = new EnemyEncounter_API(0, encounterID, "GauntletSimulation_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/DoubleTap",
                RoarEvent = "event:/AASFX/DX/gauntlet-terminal-dx",
            };
            //gauntletEncounter.AddCustomOverworldRoom("GauntletFight");
            if (hard)
            {
                gauntletEncounter.CreateNewEnemyEncounterData(
                [
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            }
            else
            {
                gauntletEncounter.CreateNewEnemyEncounterData(
                [
                    "Mung_EN",
                ], null);
            }
            gauntletEncounter.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(encounterID, 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyCombatBundle selectedBundle = LoadedAssetsHandler.GetEnemyBundle(encounterID).GetEnemyBundle(BundleDifficulty.Medium, self.EnemyEncounterData.m_MediumSelector._defaultRoomPrefab/*"GauntletFight"*/);

            int idInfo = self._zoneData.AddEnemyBundle(selectedBundle);
            Card card = new Card(self._zoneData.CardCount, idInfo, CardType.EnemyMedium, PilePositionType.Any, selectedBundle.SignID, selectedBundle.RoomPrefabName);
            self._zoneData.AddCard(card);

            int pileID = UnityEngine.Random.Range(0, self._zoneData.ZonePiles.Length);
            Card[] pile = self._zoneData.ZonePiles[pileID]._cards;
            List<Card> temp = new List<Card>();
            bool added = false;
            foreach (Card item in pile)
            {
                if (item.PilePosition != PilePositionType.End)
                {
                    temp.Add(item);
                }
                else
                {
                    if (!added) temp.Add(card);
                    added = true;
                    temp.Add(item);
                }
            }
            self._zoneData.ZonePiles[pileID]._cards = temp.ToArray();
            DebugController.Instance.WriteLine("gauntlet encounter added successfully");
            Debug.Log($"Gauntlet | encounter Gauntlet_{self.ZoneName}_{zone}Sim_{(hard ? "Hard" : "Normal")}_EnemyBundle added");
        }

        public static IEnumerable<string> ZoneStrings()
        {
            List<string> processed = new List<string>();

            if (LoadedDBsHandler.InfoHolder.Run == null || LoadedDBsHandler.InfoHolder.Run.CurrentZoneDB == null) yield break;

            yield return "farshore";
            processed.Add("farshore");
            yield return "orpheum";
            processed.Add("orpheum");
            yield return "garden";
            processed.Add("garden");
            if (AApocrypha.CrossMod.Siren)
            {
                yield return "siren";
                processed.Add("siren");
            }
        }
    }
}
