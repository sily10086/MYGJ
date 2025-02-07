using cfg;
using GameScript.Ground;
using UnityEngine;

namespace GameScript.Card
{
    public class CardSlot:MonoBehaviour,ICardSlot
    {
        [SerializeField] private CardSlotType _slotType;
        private IGround _ground;

        public CardSlotType IGetSlotType()
        {
            return _slotType;
        }

        public void ISetGround(IGround ground)
        {
            _ground = ground;
        }
        
        public IGround IGetGround()
        {
            return _ground;
        }

        public void ISetCard(ICard card)
        {
            _ground.IAddUsedCardSlot();
            _ground.IAddCardToList(card);
        }
    }
}