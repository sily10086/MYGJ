using cfg;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameScript.Card
{
    public interface ICard
    {
        CardType IGetCardType();
        int IGetCardLever();
        void IInitCard(CardType cardType, int cardLevel,int cardIndex);
        GameObject IGetGameObject();
        int IGetCardIndex();
        Sprite IGetCardSprite();
        string IGetCardName();
        int GetCardLevel();
        void IOnClickCard();
        void IOffClickCard();
        void IOnPlaceCard(Vector2 screenPosition); // 新增放置方法
    }
}