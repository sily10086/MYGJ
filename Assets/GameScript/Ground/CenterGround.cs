using System.Collections.Generic;
using cfg;
using GameScript.Card;
using UnityEngine;

namespace GameScript.Ground
{
    public class CenterGround:MonoBehaviour,IGround
    {
        [SerializeField] private GameObject _buildGround;
        [SerializeField] private GroundType _type;
        [SerializeField] private SimpleOutline _outline;
        
        [Header("等级")]
        [SerializeField] private int _intiLevel;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _maxLevel;
        [SerializeField] private List<GameObject> _centerBuild;
        
        private int _useSlotCount = 0;
        private readonly List<ICard> _cards = new ();
        
        
        private void Awake()
        {
            _centerBuild.ForEach(obj => obj.SetActive(false));
            _centerBuild[_intiLevel-1].SetActive(true);
            _currentLevel = _intiLevel;
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

        public ICard IGetBuildCard()
        {
            return null;
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