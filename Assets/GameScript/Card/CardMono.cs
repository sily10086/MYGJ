using System;
using System.Collections.Generic;
using cfg;
using Common.Event;
using Cysharp.Threading.Tasks;
using GameScript.Ground;
using GameScript.UI;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.Card
{
    public class CardMono:MonoBehaviour,ICard
    {
        [SerializeField] private CardType _cardType;
        [SerializeField] private int _cardIndex;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Text _cardLevelText;
        [SerializeField] private Text _cardNameText;
        [SerializeField] private BoxCollider2D _cardCollider2D;
        
        private Sprite _cardSprite;
        private int _cardLevel;
        private string _cardName;
        
        private GameObject _unrealCard;

        #region 接口

        public void IInitCard(CardType cardType,int cardLevel,int cardIndex)
        {
            if(_cardType!=CardType.UnrealCard) _cardType = cardType;
            
            _cardLevel = cardLevel;
            _cardName = CardTypeMapping(cardType);
            
            _cardIndex = cardIndex;
            
            _cardNameText.text = _cardName;
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

        public void IOnClickCard()
        {
            if(_cardType==CardType.UnrealCard) return;
            _unrealCard = UnrealCardPool.Instance.GetUnrealCard();
            _unrealCard.GetComponent<ICard>().IInitCard(_cardType,_cardLevel,_cardIndex);
            _cardCollider2D.enabled = true;
            _unrealCard.transform.localPosition = gameObject.transform.localPosition;
        }

        public void IOffClickCard()
        {
            if (_unrealCard)
            {
                transform.localPosition = _unrealCard.gameObject.transform.localPosition;
                _cardCollider2D.enabled = false;
                UnrealCardPool.Instance.DespawnUnrealCard(_unrealCard);
                _unrealCard = null;
            }
        }

        public void IOnPlaceCard(Vector2 screenPosition)
        {
            transform.position = screenPosition;
            _cardCollider2D.enabled = true;
            CheckForCollisions();
        }

        public Sprite IGetCardSprite()
        {
            return _cardSprite;
        }

        public string IGetCardName()
        {
            return _cardName;
        }

        public int GetCardLevel()
        {
            return _cardLevel;
        }
        #endregion

        private bool CardMapping(GroundType groundType)
        {
            if (groundType == GroundType.None)
            {
                return true;
            }
            if (_cardType == CardType.Food && groundType == GroundType.Food)
            {
                return true;
            }
            if (_cardType == CardType.Technology && groundType == GroundType.Technology)
            {
                return true;
            }
            if (_cardType == CardType.Gold && groundType == GroundType.Gold)
            {
                return true;
            }
            if (_cardType == CardType.Wood && groundType == GroundType.Wood)
            {
                return true;
            }
            if (_cardType == CardType.Iron && groundType == GroundType.Iron)
            {
                return true;
            }
            if (_cardType == CardType.Stone && groundType == GroundType.Stone)
            {
                return true;
            }
            if (_cardType == CardType.WeaponsBuilding && groundType == GroundType.Weapons)
            {
                return true;
            }

            return false;
        }
        
        private string CardTypeMapping(CardType cardType)
        {
            return cardType switch
            {
                CardType.Food => "农场",
                CardType.Iron => "冶金坊",
                CardType.Wood=>"伐木场",
                CardType.Stone=>"采石场",
                CardType.Technology=>"学校",
                CardType.WeaponsBuilding=>"武器坊",
                CardType.People => "人口",
                CardType.Gold => "市场",
                _=>"",
            };
        }
        
        
        private void CheckForCollisions()
        {
            // 获取所有与当前Collider2D接触的Collider2D
            List<Collider2D> colliders = new List<Collider2D>();
            _cardCollider2D.Overlap(transform.position, 0, colliders);

            if (colliders.Count>0)
            {
                // 遍历所有接触的Collider2D
                foreach (var col2d in colliders)
                {
                    // 检查目标Collider的Tag
                    if (col2d.CompareTag("Card") || col2d.CompareTag("CardSlot"))
                    {
                        Debug.Log("Detected collision with an object tagged as Card or CardSlot: " 
                                  + col2d.name);
                        // 在这里执行你的逻辑
                        // 例如：调用一个方法处理碰撞事件
                        HandleCollision(col2d);
                    }
                }
            } else
            {
                IOffClickCard();
            }
        }

        private async void HandleCollision(Collider2D col2d)
        {
            // 在这里实现你的逻辑，例如：
            // 如果是卡片，执行某种操作
            if (col2d.CompareTag("Card"))
            {
                Debug.Log("Handling Card collision");
                
                var type = col2d.gameObject.GetComponent<ICard>().IGetCardType();
                
                if (type == CardType.People && _cardType == CardType.Weapons)
                {
                    
                }
                if (_cardType == CardType.People && type == CardType.Weapons)
                {
                    
                }
            }
            // 如果是卡槽，执行另一种操作
            else if (col2d.CompareTag("CardSlot"))
            {
                Debug.Log("Handling CardSlot collision");
                var sc = col2d.gameObject.GetComponent<ICardSlot>();
                var type = sc.IGetSlotType();
                
                if(_cardType==CardType.Weapons) return;
                
                var groundSC = sc.IGetGround();
                
                if (_cardType == CardType.People && type == CardSlotType.PopulationSlot)
                {
                    sc.ISetCard(this);
                    
                    var eventSC = UIEventManager.GetEvent<UpdateUseCardSlot>();
                    eventSC.groundSC = groundSC;
                    EventManager.Instance.GetEventUtility().SendMessage(eventSC);
                    
                    UnrealCardPool.Instance.DespawnUnrealCard(_unrealCard);
                    CardManager.Instance.SubCard(this);
                    await UniTask.Yield();
                    
                    _unrealCard = null;
                    CardManager.Instance.SubCard(this);
                    Destroy(gameObject);
                }
                if (_cardType == CardType.Arm && type == CardSlotType.ArmSlot)
                {
                    sc.ISetCard(this);
                    
                    var eventSC = UIEventManager.GetEvent<UpdateUseCardSlot>();
                    eventSC.groundSC = groundSC;
                    EventManager.Instance.GetEventUtility().SendMessage(eventSC);
                    
                    UnrealCardPool.Instance.DespawnUnrealCard(_unrealCard);
                    CardManager.Instance.SubCard(this);
                    await UniTask.Yield();
                    
                    _unrealCard = null;
                    Destroy(gameObject);
                }
                
                if (CardMapping(groundSC.IGetGroundType()))
                {
                    groundSC.ISetBuildingLevel(_cardLevel);
                    groundSC.IChangeGroundType(_cardType);

                    var eventSC = UIEventManager.GetEvent<UpdateLeftUIEvent>();
                    eventSC.groundSC = groundSC;
                    EventManager.Instance.GetEventUtility().SendMessage(eventSC);
                    
                    UnrealCardPool.Instance.DespawnUnrealCard(_unrealCard);
                    CardManager.Instance.SubCard(this);
                    await UniTask.Yield();
                    _unrealCard = null;
                    Destroy(gameObject);
                }
            }
            IOffClickCard();
        }
    }
}