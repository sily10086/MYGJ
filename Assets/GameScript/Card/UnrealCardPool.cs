using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameScript.Card
{
    public class UnrealCardPool:MonoBehaviour
    {
        private static UnrealCardPool _instance;
        public static UnrealCardPool Instance => _instance;
        
        private readonly Queue<GameObject> _cardPool = new ();
        
        private void Awake()
        {
            _instance = this;
            Init();
        }

        public GameObject GetUnrealCard()
        {
            var obj = _cardPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void DespawnUnrealCard(GameObject card)
        {
            card.SetActive(false);
            _cardPool.Enqueue(card);
        }

        private void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                var obj = Resources.Load<GameObject>("Prefabs/UnrealCard");
                obj = Instantiate(obj,transform);
                obj.SetActive(false);
                DespawnUnrealCard(obj);
            }
        }
    }
}