using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class BonusCardHandler
    {
        public static void Add()
        {
            CardInfo info = new CardInfo()
            {
                pilePosition = PilePositionType.Any,
                cardType = (CardType)787
            };
            CardTypeInfo card = new CardTypeInfo();
            card._cardInfo = info;
            card._minimumAmount = 1;
            card._maximumAmount = 1;
            (LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray(); ;
        }
    }
}
