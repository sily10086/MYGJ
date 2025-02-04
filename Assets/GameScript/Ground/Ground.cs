using System;
using System.Collections.Generic;
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
        [SerializeField] private GroundType _type;
        [SerializeField] private SimpleOutline _outline;
        
        [Header("等级")]
        [SerializeField] private int _intiLevel;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _maxLevel;
        
        
        private void Awake()
        {
            _buildGround = null;
            if(_type==GroundType.None) _voidGround.SetActive(true);
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
        #endregion
        
    }
}