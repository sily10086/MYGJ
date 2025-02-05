using System;
using System.Collections.Generic;
using cfg;
using UnityEngine;

namespace GameScript.UI
{
    public  class CardManager : MonoBehaviour
    {
        private static CardManager _instance;
        public static CardManager Instance => _instance;
        
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private Vector3 _cardStartPos;
        [SerializeField] private Vector3 _cardIntervalPos;

        private List<ICard> _cardList = new();

        private void Start()
        {
            CreateCard(CardType.Food);
            CreateCard(CardType.Technology);
            CreateCard(CardType.Iron);
            CreateCard(CardType.Wood);
            CreateCard(CardType.Weapons);
            CreateCard(CardType.Gold);
            CreateCard(CardType.House);
            CreateCard(CardType.People);
        }

        public void CreateCard(CardType cardType,int carLevel = 1)
        {
            var obj = Instantiate(_cardPrefab, _cardStartPos, Quaternion.identity);
            var sc = obj.GetComponent<ICard>();
            sc.IInitCard(cardType, carLevel);
            obj.transform.SetParent(transform);
            AddCard(sc);
        }

        public void SubCard(ICard cardSC)
        {
            _cardList.Remove(cardSC);
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