using cfg;
using UnityEngine;

namespace GameScript.UI
{
    public interface ICard
    {
        CardType IGetCardType();
        int IGetCardLever();
        void IInitCard(CardType cardType, int cardLevel);
        GameObject IGetGameObject();
        int IGetCardIndex();
    }
}