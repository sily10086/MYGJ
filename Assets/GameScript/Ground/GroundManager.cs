using System;
using System.Collections.Generic;
using Common.Event;
using Cysharp.Threading.Tasks;
using GameScript.UI;
using UnityEngine;

namespace GameScript.Ground
{
    public class GroundManager : MonoBehaviour
    {
        private static GroundManager _instance;
        public static GroundManager Instance => _instance;

        private readonly Dictionary<int, List<GameObject>> _groundDic = new();
        private readonly Dictionary<GameObject,IGround> _groundSCDic = new();
        
        [SerializeField] private GameObject groundPrefab;
        [SerializeField] private GameObject startGround;
        [SerializeField] private int maxGroundLayer;
        [SerializeField] private int currentGroundLayer = 0;
        [SerializeField] private int initGroundLayer = 0;
        [SerializeField] private List<Vector3> rayDirection;
        
        [SerializeField] private GameObject currentGround;
        
        #region 生命周期

        private void Awake()
        {
            _instance = this;
            _groundDic.Add(currentGroundLayer, new List<GameObject> { startGround });
            _groundSCDic.Add(startGround,startGround.GetComponent<IGround>());
            InitGrounds();
        }

        private void Start()
        {
            WhileCreateGround();
            OpenGroundLayer(initGroundLayer);
        }

        #endregion

        #region 私有方法

        private void InitGrounds()
        {
            for (int i = 1; i <= maxGroundLayer; i++)
            {
                _groundDic.Add(i, new List<GameObject>());
            }
        }

        private void WhileCreateGround()
        {
            for (int i = 0; i < maxGroundLayer; i++)
            {
                CreateGround(i);
            }

            // DebugDic();
        }

        private void CreateGround(int layer)
        {
            currentGroundLayer = layer + 1;
            var currentList = _groundDic[layer];
            List<GameObject> tempNextList = new List<GameObject>(); // 临时集合

            foreach (var lastObj in currentList)
            {
                foreach (var target in rayDirection)
                {
                    var startPos = lastObj.transform.position;
                    var direction = new Vector3(startPos.x + target.x,
                        startPos.y + target.y, startPos.z + target.z);
                    Ray ray = new Ray(startPos, target * 10f);

                    if (!Physics.Raycast(ray, out RaycastHit hit, 100f))
                    {
                        //Debug.Log("射线路径上没有物体");
                        // 生成物体
                        var obj = Instantiate(groundPrefab, transform, true);
                        obj.transform.position = direction;
                        obj.name = $"Ground.Layer{layer + 1}";
                        tempNextList.Add(obj); // 添加到临时集合
                        _groundSCDic.Add(obj,obj.GetComponent<IGround>());
                        Physics.SyncTransforms();
                    }
                }
            }
            
            _groundDic[layer + 1] = tempNextList;
        }
        
        /// <summary>
        /// 查看字典里面是否存在内容
        /// </summary>
        private void DebugDic()
        {
            foreach (var val in _groundSCDic)
            {
                Debug.Log($"name：{val.Key}//SC：{val.Value}");
            }
            foreach (var val in _groundDic)
            {
                foreach (var list in val.Value)
                {
                    Debug.LogError($"layer：{val.Key}///Pos:{list.transform.position}");
                }
            }
        }
        
        #endregion

        #region 公有方法
        
        /// <summary>
        /// 打开多少层地块
        /// </summary>
        /// <param name="layer"></param>
        public void OpenGroundLayer(int layer)
        {
            //if(initGroundLayer>=layer) return;
            foreach (var val in _groundDic)
            {
                if(val.Key <= layer) val.Value.ForEach(obj=>obj.SetActive(true));
                else val.Value.ForEach(obj=>obj.SetActive(false));
            }
            currentGroundLayer = layer;
        }
        
        /// <summary>
        /// 设置被选中的地块
        /// </summary>
        /// <param name="ground"></param>
        public void SetCurrentGround(GameObject ground)
        {
            if(currentGround) _groundSCDic[currentGround]?.ICloseOutLineGround();
            currentGround = ground;
            var openLeftUIEvent = UIEventManager.GetEvent<OpenLeftUIEvent>();
            openLeftUIEvent.groundSC = currentGround?.GetComponent<IGround>();
            EventManager.Instance.GetEventUtility().SendMessage(openLeftUIEvent);
            if(!currentGround) return;
            _groundSCDic[currentGround]?.IOpenOutLineGround();
        }
        
        /// <summary>
        /// 获取被选中的地块
        /// </summary>
        /// <returns></returns>
        public GameObject GetCurrentGround()
        {
            return currentGround;
        }
        
        /// <summary>
        /// 获取被选中的地块的接口
        /// </summary>
        /// <returns></returns>
        public IGround GetCurrentGroundSC()
        {
            return _groundSCDic[currentGround];
        }
        
        #endregion
        
    }
}

