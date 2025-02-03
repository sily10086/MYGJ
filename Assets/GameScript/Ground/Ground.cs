using System;
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
        [SerializeField] private GameObject _outLineGround;
        [SerializeField] private GroundEnum _type;

        private void Awake()
        {
            _buildGround = null;
            if(_type==GroundEnum.None) _voidGround.SetActive(true);
            _outLineGround.SetActive(false);
        }

        #region 接口

        public GameObject IGetBuildGround()
        {
            return _buildGround;
        }

        public void IOpenOutLineGround()
        {
            _outLineGround.SetActive(true);
        }

        public void ICloseOutLineGround()
        {
            _outLineGround.SetActive(false);
        }

        public void IChangeGroundType(GroundEnum type)
        {
            _type = type;
        }
        
        public GroundEnum IGetGroundType()
        {
            return _type;
        }

        #endregion
        
    }
}