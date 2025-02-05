using cfg;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.UI
{
    public class CardMono:MonoBehaviour,ICard
    {
        private int _cardLevel;
        
        [SerializeField] private Image _cardImage;
        [SerializeField] private Text _cardLevelText;
        [SerializeField] private Text _cardNameText;
        [SerializeField] private int _cardIndex;
        [SerializeField] private CardType _cardType;
        
        public void IInitCard(CardType cardType,int cardLevel)
        {
            _cardType = cardType;
            _cardLevel = cardLevel;
            _cardNameText.text = CardTypeMapping(cardType);
            _cardLevelText.text = "等级: " + cardLevel;
        }
        
        public CardType IGetCardType()
        {
            return _cardType;
        }

        public int IGetCardLever()
        {
            return _cardLevel;
        }

        public GameObject IGetGameObject()
        {
            return gameObject;
        }

        public int IGetCardIndex()
        {
            return _cardIndex;
        }
        
        private string CardTypeMapping(CardType cardType)
        {
            return cardType switch
            {
                CardType.House => "房屋",
                CardType.Food => "农场",
                CardType.Iron => "冶金坊",
                CardType.Wood=>"伐木场",
                CardType.Stone=>"采石场",
                CardType.Technology=>"学校",
                CardType.Weapons=>"武器坊",
                CardType.People => "人口",
                CardType.Gold => "市场",
                _=>"",
            };
        }
    }
}