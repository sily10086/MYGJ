using System.Collections.Generic;
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

        private int _useArmCount = 0;
        
        
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
        public void IChangeGroundType(GroundType type)
        {
            _type = type;
        }
        public GroundType IGetGroundType()
        {
            return _type;
        }
        public int IGetGroundLevel()
        {
            return _currentLevel;
        }
        public int IGetUsePopulation()
        {
            return _useArmCount;
        }
        public void IAddPopulation()
        {
            _useArmCount++;
        }
        
        #endregion
    }
}