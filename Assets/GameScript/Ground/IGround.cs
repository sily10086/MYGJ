using System.Collections.Generic;
using cfg;
using GameScript.Card;
using UnityEngine;

namespace GameScript.Ground
{
    public interface IGround
    {
        GameObject IGetBuildGround();
        void IOpenOutLineGround();
        void ICloseOutLineGround();
        void IChangeGroundType(CardType type);
        GroundType IGetGroundType();
        int IGetGroundLevel();
        int IGetUseCardSlot();
        void IAddUsedCardSlot();
        void IAddCardToList(ICard card);
        List<ICard> IGetCards();
        int IGetBuildingLevel();
        void ISetBuildingLevel(int level);
    }
}