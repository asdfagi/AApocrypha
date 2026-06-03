using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public class MainCharacterBonusCardPatch
    {
        // borrowed from Ruinful Revelry github (AddAccordio.cs)
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZoneBGDataBaseSO), nameof(ZoneBGDataBaseSO.TryGenerateNewCard))]
        public static bool CardGenPatch(ZoneBGDataBaseSO __instance, CardInfo info)
        {
            //SingleUnlockCheck check;
            //LoadedDBsHandler.UnlockablesDB.TryGetMiscUnlockCheck("Lucina", out check);
            string loadedMC = LoadedDBsHandler.InfoHolder.Run.inGameData.GetStringData("AA_MainCharacter");
            //Debug.Log("cardType: " + info.cardType.ToString() + ", loadedMC: " + loadedMC);
            if (info.cardType == ForceAddType && loadedMC == "Whitlock_CH")
            {
                Debug.Log("whitlock bonus card generated");
                GenerateWhitlockBonusCard(__instance, info);
                return false;
            }
            //Debug.Log("no bonus card generated");
            return true;
        }

        public static CardTypeInfo bonusCard;
        public static BasicEncounterSO bonusEncounter;

        public static CardType ForceAddType => (CardType)787;

        public static void AddWhitlockBonusEncounter(BasicEncounterSO BasicEncounter, string zoneDB_ID)
        {
            if (!LoadedAssetsHandler.LoadedBasicEncounters.TryGetValue(BasicEncounter.name, out BasicEncounterSO _))
                LoadedAssetsHandler.LoadedBasicEncounters.Add(BasicEncounter.name, BasicEncounter);

            CardTypeInfo FWCard = new CardTypeInfo();
            FWCard._cardInfo = new CardInfo();
            FWCard._cardInfo.cardType = ForceAddType;
            FWCard._cardInfo.pilePosition = PilePositionType.Any;
            FWCard._usePercentage = true;
            FWCard._percentage = 75;
            bonusCard = FWCard;
            bonusEncounter = BasicEncounter;

            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB(zoneDB_ID) as ZoneBGDataBaseSO;
            List<CardTypeInfo> addonCards = zoneBGDataBaseSO._deckInfo._possibleCards.ToList<CardTypeInfo>();
            addonCards.Add(FWCard);
            zoneBGDataBaseSO._deckInfo._possibleCards = addonCards.ToArray();
        }

        public static void GenerateWhitlockBonusCard(ZoneBGDataBaseSO zone, CardInfo info)
        {
            string[] encounterEntityIDs = bonusEncounter.encounterEntityIDs;
            foreach (string item in encounterEntityIDs)
            {
                zone._EntitiesInRun.Add(item);
            }
            TalkingEntityContentData newEntity = new TalkingEntityContentData(bonusEncounter.DialoguePath);
            int idInfo = zone._zoneData.AddDialoguePathData(newEntity);
            Card card = new Card(zone._zoneData.CardCount, idInfo, CardType.Quest, info.pilePosition, bonusEncounter.signID, bonusEncounter.encounterRoom);
            zone._zoneData.AddCard(card);
        }
    }
}
