using System;
using Common.Event;
using UnityEngine;

namespace GameScript.UI
{
    public class UIManager:MonoBehaviour
    {
        [SerializeField] private GameObject _top;
        [SerializeField] private GameObject _left;
        [SerializeField] private GameObject _bottom;
        [SerializeField] private GameObject _right;
        
        [SerializeField] private GroundDataUIPanel _groundDataUIPanel;
        
        private static UIManager _instance;
        public static UIManager Instance{get{return _instance;}}

        #region 生命周期

        private void Awake()
        {
            _instance = this;
            _top.SetActive(true);
            _left.SetActive(false);
            _bottom.SetActive(true);
            _right.SetActive(true);
        }

        private void Start()
        {
            InitTopUI();
            InitBottomUI();
            InitRightUI();
            InitLeftUI();
            AddListenerForUIEvent();
        }

        private void AddListenerForUIEvent()
        {
            EventManager.Instance.GetEventUtility()
                .AddListener<UpdateTopUIEvent>(UpdateTopUI);
            EventManager.Instance.GetEventUtility()
                .AddListener<OpenLeftUIEvent>(OpenLeftUI);
        }

        #endregion

        #region 初始化

        private void InitTopUI()
        {
            
        }

        private void InitLeftUI()
        {
            
        }

        private void InitBottomUI()
        {
            
        }

        private void InitRightUI()
        {
            
        }
        
        #endregion

        #region 更新

        private void OpenLeftUI(IEventMessage eventMessage)
        {
            if(eventMessage is not OpenLeftUIEvent message) return;
            _left.SetActive(message.groundSC!=null);
            if(message.groundSC!=null) _groundDataUIPanel.InitGroundDataUI(message.groundSC);
        }
        
        private void UpdateTopUI(IEventMessage eventMessage)
        {
            if(eventMessage is not UpdateTopUIEvent) return;
        }
        
        private void UpdateBottomUI(IEventMessage eventMessage)
        {
            
        }
        
        private void UpdateLeftUI(IEventMessage eventMessage)
        {
            
        }
        
        private void UpdateRightUI(IEventMessage eventMessage)
        {
            
        }

        #endregion
        
    }
}