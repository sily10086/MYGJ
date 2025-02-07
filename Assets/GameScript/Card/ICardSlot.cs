using cfg;
using GameScript.Ground;

namespace GameScript.Card
{
    public interface ICardSlot
    {
        CardSlotType IGetSlotType();
        void ISetGround(IGround ground);
        IGround IGetGround();
        void ISetCard(ICard card);
    }
}