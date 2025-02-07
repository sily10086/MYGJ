using System;
using System.Collections.Generic;
using Common.Event;
using UnityEngine;

namespace GameScript.Card
{
    public class VoidCardPool:MonoBehaviour
    {
        private static VoidCardPool _instance;
        public static VoidCardPool Instance => _instance;
        
        private readonly Queue<GameObject> _cardPool = new ();
        
        private readonly List<GameObject> _cards = new ();
        
        private void Awake()
        {
            _instance = this;
            Init();
        }
        
        public GameObject GetVoidCard()
        {
            var obj = _cardPool.Dequeue();
            obj.SetActive(true);
            _cards.Add(obj);
            return obj;
        }

        private void DespawnVoidCard(GameObject card)
        {
            card.SetActive(false);
            card.transform.SetParent(transform);
            _cardPool.Enqueue(card);
        }

        public void DespawnVoidCard()
        {
            foreach (var obj in _cards)
            {
                DespawnVoidCard(obj);
            }
            _cards.Clear();
        }
        
        private void Init()
        {
            for (int i = 0; i < 100; i++)
            {
                var obj = Resources.Load<GameObject>("Prefabs/VoidCard");
                obj = Instantiate(obj,transform);
                obj.SetActive(false);
                DespawnVoidCard(obj);
            }
        }
    }
}