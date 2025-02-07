using System;
using System.Collections.Generic;
using cfg;
using UnityEngine;

namespace GameScript.Card
{
    public  class CardManager : MonoBehaviour
    {
        private static CardManager _instance;
        public static CardManager Instance => _instance;
        
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private Vector3 _cardStartPos;
        [SerializeField] private Vector3 _cardIntervalPos;

        private readonly List<ICard> _cardList = new();

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            CreateCard(CardType.Food);
            CreateCard(CardType.Technology);
            CreateCard(CardType.Iron);
            CreateCard(CardType.Wood);
            CreateCard(CardType.WeaponsBuilding);
            CreateCard(CardType.Gold);
            CreateCard(CardType.People);
        }

        public void CreateCard(CardType cardType,int carLevel = 1)
        {
            var obj = Instantiate(_cardPrefab, _cardStartPos, Quaternion.identity);
            var sc = obj.GetComponent<ICard>();
            sc.IInitCard(cardType, carLevel,_cardList.Count);
            obj.transform.SetParent(transform);
            AddCard(sc);
        }

        public void SubCard(ICard cardSC)
        {
            _cardList.Remove(cardSC);
            SortCardList();
        }
        
        private void AddCard(ICard cardSC)
        {
            if(_cardList.Count>=10) return;
            _cardList.Add(cardSC);
            SortCardList();
        }

        private void SortCardList()
        {
            for (int i = 0; i < _cardList.Count; i++)
            {
                _cardList[i].IGetGameObject().transform.localPosition
                    = _cardStartPos + (_cardIntervalPos * i);
            }
        }
    }
}