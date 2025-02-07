using System.Collections.Generic;
using cfg;
using GameScript.Card;
using GameScript.Luban;
using UnityEngine;

namespace GameScript.Ground
{
    /// <summary>
    /// 地块逻辑
    /// </summary>
    public class Ground : MonoBehaviour,IGround
    {
        [SerializeField] private GameObject _voidGround;
        [SerializeField] private GameObject _buildGround;
        [SerializeField] private GameObject _buildingGroundChild;
        [SerializeField] private GroundType _type;
        [SerializeField] private SimpleOutline _outline;
        
        [Header("等级")]
        [SerializeField] private int _intiLevel;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _maxLevel;

        [Header("地块")] 
        [SerializeField] private List<GameObject> _groundListLV1 = new();
        [SerializeField] private List<GameObject> _groundListLV2 = new();
        [SerializeField] private List<GameObject> _groundListLV3 = new();
        
        private int _useSlotCount;
        private readonly List<ICard> _cards = new();
        
        private void Awake()
        {
            _buildingGroundChild = null;
            if (_type == GroundType.None)
            {
                _voidGround.SetActive(true);
                _groundListLV1.ForEach(obj=>obj.SetActive(false));
                _groundListLV2.ForEach(obj=>obj.SetActive(false));
                _groundListLV3.ForEach(obj=>obj.SetActive(false));
            }
            else
            {
                _voidGround.SetActive(false);
                _groundListLV1.ForEach(obj=>obj.SetActive(false));
                _groundListLV2.ForEach(obj=>obj.SetActive(false));
                _groundListLV3.ForEach(obj=>obj.SetActive(false));
            }
            _outline.enabled = false;
        }

        private GroundType GroundTypeMapping(CardType type)
        {
            return type switch
            {
                CardType.Technology => GroundType.Technology,
                CardType.Food => GroundType.Food,
                CardType.Gold => GroundType.Gold,
                CardType.Wood => GroundType.Wood,
                CardType.Iron => GroundType.Iron,
                CardType.Stone => GroundType.Stone,
                CardType.WeaponsBuilding => GroundType.Weapons,
                _ => GroundType.None
            };
        }
        
        #region 接口

        public GameObject IGetBuildGround()
        {
            return _buildGround;
        }

        public void IOpenOutLineGround()
        {
            _outline.enabled = true;
        }

        public void ICloseOutLineGround()
        {
            _outline.enabled = false;
        }

        public void IChangeGroundType(CardType type)
        {
            _type = GroundTypeMapping(type);
            if(_type!=GroundType.None) _voidGround.SetActive(false);
            if (_buildingGroundChild)
            {
                _buildingGroundChild.SetActive(false);
                _buildingGroundChild = null;
            }
            if (_currentLevel == 1)
            {
                foreach (var obj in _groundListLV1)
                {
                    if (obj.GetComponent<GroundMeshType>().GetGroundType() == _type)
                    {
                        obj.gameObject.SetActive(true);
                        _buildingGroundChild = obj;
                    }
                }
            }
            if (_currentLevel == 2)
            {
                foreach (var obj in _groundListLV2)
                {
                    if (obj.GetComponent<GroundMeshType>().GetGroundType() == _type)
                    {
                        obj.gameObject.SetActive(true);
                        _buildingGroundChild = obj;
                    }
                }
            }
            if (_currentLevel == 3)
            {
                foreach (var obj in _groundListLV3)
                {
                    if (obj.GetComponent<GroundMeshType>().GetGroundType() == _type)
                    {
                        obj.gameObject.SetActive(true);
                        _buildingGroundChild = obj;
                    }
                }
            }
        }
        
        public GroundType IGetGroundType()
        {
            return _type;
        }

        public int IGetGroundLevel()
        {
            return _currentLevel;
        }

        public int IGetUseCardSlot()
        {
            return _useSlotCount;
        }

        public void IAddUsedCardSlot()
        {
            _useSlotCount++;
        }

        public void IAddCardToList(ICard card)
        {
            _cards.Add(card);
        }

        public List<ICard> IGetCards()
        {
            return _cards;
        }
        
        public int IGetBuildingLevel()
        {
            return _currentLevel;
        }

        public void ISetBuildingLevel(int level)
        {
            _currentLevel = level;
        }
        #endregion
        
    }
}