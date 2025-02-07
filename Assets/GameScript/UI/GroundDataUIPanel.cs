using System;
using System.Collections.Generic;
using cfg;
using Common.Event;
using GameScript.Card;
using GameScript.Ground;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VHierarchy.Libs;

namespace GameScript.UI
{
    public class GroundDataUIPanel:MonoBehaviour
    {
        [Header("Other")]
        [SerializeField] private Text _groundTypeText;
        [SerializeField] private Text _buildLevelText;
        [SerializeField] private GameObject _buildCardSlot;
        
        [Header("劳力")]
        [SerializeField] private Text _populationTipsText;
        [SerializeField] private Image _populationCardSlot;
        [SerializeField] private int _canPopulationNumber;
        [SerializeField] private int _usePopulationNumber;
        
        [Header("按钮")]
        [SerializeField] private Button _populationCardSlotButton;
        [SerializeField] private Button _mainPanelButton;
        [SerializeField] private Button _armButton;
        [SerializeField] private Button _getCardButton;
        
        [Header("军队")]
        [SerializeField] private GameObject _armPanel;
        [SerializeField] private Image _armCardSlot;
        [SerializeField] private Text _armTipsText;
        
        [Header("GetCard")]
        [SerializeField] private GameObject _getCardPanel;
        
        [Header("Tips")]
        [SerializeField] private Text _groundTipsText;
        
        private int _currentLevel;
        private IGround _groundSC;

        private readonly List<VoidCard> _voidCards = new();
        private GameObject _buildCardObj;
        
        private void Awake()
        {
            _populationCardSlotButton.onClick.AddListener(OpenPopulationCardSlot);
            _mainPanelButton.onClick.AddListener(OpenMainPanel);
            _armButton.onClick.AddListener(OpenArmPanel);
            _getCardButton.onClick.AddListener(OpenGetCardPanel);
        }

        public void UpdateUseCardSlot(IGround ground)
        {
            if(_groundSC!=ground||ground==null) return;
            var list = _groundSC.IGetCards();
            if(list.Count <= 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var obj = VoidCardPool.Instance.GetVoidCard();
                obj.transform.SetParent(_armCardSlot.transform.parent.gameObject.activeSelf?
                    _armCardSlot.transform:_populationCardSlot.transform);
                var sc = obj.GetComponent<VoidCard>();
                sc.Init(_groundSC.IGetGroundType());
                _voidCards.Add(sc);
            }
            _usePopulationNumber = _groundSC.IGetUseCardSlot();
            _populationTipsText.text = $"劳动力: {_usePopulationNumber}/{_canPopulationNumber}";
            ArrangeCards();
        }
        
        public void UpdateGroundDataUI(IGround groundSC)
        {
            if(_groundSC!=groundSC || groundSC==null) return;
            DespawnGroundTypeCard();
            _groundTypeText.text = GroundTypeMapping(groundSC.IGetGroundType());
            _buildLevelText.text = "等级: " + groundSC.IGetGroundLevel();
            _currentLevel = groundSC.IGetGroundLevel();
            LoadGroundTypeCard(groundSC.IGetGroundType());
        }
        
        public void InitGroundDataUI(IGround groundSC)
        {
            DespawnGroundTypeCard();
            _groundSC = groundSC;
            _groundTypeText.text = GroundTypeMapping(groundSC.IGetGroundType());
            _buildLevelText.text = "等级: " + groundSC.IGetGroundLevel();
            _currentLevel = groundSC.IGetGroundLevel();
            _groundTipsText.text = InitGroundTips(groundSC.IGetGroundType());
            _usePopulationNumber = groundSC.IGetUseCardSlot();
            _armButton.gameObject.SetActive(groundSC.IGetGroundType() == GroundType.Center);
            _getCardButton.gameObject
                .SetActive(groundSC.IGetGroundType() == GroundType.Center);
            _populationCardSlotButton.gameObject
                .SetActive(groundSC.IGetGroundType() != GroundType.Center);
            _buildCardSlot.GetComponent<ICardSlot>().ISetGround(groundSC);
            LoadGroundTypeCard(groundSC.IGetGroundType());
            OpenMainPanel();
        }

        #region 私有方法

        private string GroundTypeMapping(GroundType groundType)
        {
            return groundType switch
            {
                GroundType.Center => "城镇中心",
                GroundType.Wood => "伐木场",
                GroundType.Stone => "采石场",
                GroundType.Gold => "市场",
                GroundType.Technology => "学校",
                GroundType.Food => "农田",
                GroundType.Weapons => "武器坊",
                _ => "普通地块",
            };
        }

        private void LoadGroundTypeCard(GroundType groundType)
        {
            if (groundType == GroundType.Center)
            {
                var obj = Resources.Load<GameObject>("Prefabs/CenterCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Food)
            {
                var obj = Resources.Load<GameObject>("Prefabs/FoodCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Iron)
            {
                var obj = Resources.Load<GameObject>("Prefabs/IronCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Technology)
            {
                var obj = Resources.Load<GameObject>("Prefabs/TechnologyCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Stone)
            {
                var obj = Resources.Load<GameObject>("Prefabs/StoneCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Gold)
            {
                var obj = Resources.Load<GameObject>("Prefabs/GoldCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Wood)
            {
                var obj = Resources.Load<GameObject>("Prefabs/WoodCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
            if (groundType == GroundType.Weapons)
            {
                var obj = Resources.Load<GameObject>("Prefabs/WeaponsBuildingCard");
                _buildCardObj = Instantiate(obj, _buildCardSlot.transform);
                obj.transform.localPosition = Vector3.zero;
            }
        }

        private void DespawnGroundTypeCard()
        {
            if(!_buildCardObj) return;
            Destroy(_buildCardObj);
            _buildCardObj = null;
        }
        private void OpenPopulationCardSlot()
        {
            CloseMainPanel();
            _populationTipsText.gameObject.SetActive(true);
            _populationCardSlot.gameObject.SetActive(true);
            _populationCardSlot.GetComponent<ICardSlot>().ISetGround(_groundSC);
            switch (_groundSC.IGetBuildingLevel())
            {
                case 1:
                    _canPopulationNumber = 1;
                    break;
                case 2:
                    _canPopulationNumber = 2;
                    break;
                case 3:
                    _canPopulationNumber = 4;
                    break;
            }
            _usePopulationNumber = _groundSC.IGetUseCardSlot();
            _populationTipsText.text = $"劳动力: {_usePopulationNumber}/{_canPopulationNumber}";
            GetUseSlotCard();
        }

        private void ClosePopulationCardSlot()
        {
            CloseMainPanel();
            _populationCardSlot.gameObject.SetActive(false);
            _populationTipsText.gameObject.SetActive(false);
            DespawnAllCards();
        }
        private void OpenMainPanel()
        {
            CloseArmPanel();
            ClosePopulationCardSlot();
            CloseGetCardPanel();
            _buildCardSlot.gameObject.SetActive(true);
            _groundTypeText.gameObject.SetActive(true);
            _buildLevelText.gameObject.SetActive(true);
            _groundTipsText.gameObject.SetActive(true);
            GetMainBuildCard();
        }
        private void CloseMainPanel()
        {
            _buildCardSlot.gameObject.SetActive(false);
            _groundTypeText.gameObject.SetActive(false);
            _buildLevelText.gameObject.SetActive(false);
            _groundTipsText.gameObject.SetActive(false);
        }
        private void OpenArmPanel()
        {
            CloseMainPanel();
            CloseGetCardPanel();
            _armPanel.SetActive(true);
            _armTipsText.gameObject.SetActive(true);
            _armCardSlot.GetComponent<ICardSlot>().ISetGround(_groundSC);
            
            // 根据当前等级获取最大人口容量
            int maxPopulation = GetMaxPopulationByLevel(_currentLevel);
    
            // 更新提示文本
            _armTipsText.text = $"军队: {_usePopulationNumber}/{maxPopulation}";

            GetUseSlotCard();
        }
        // 根据等级返回最大人口容量
        private int GetMaxPopulationByLevel(int level) => level switch
        {
            1 => 2,
            2 => 4,
            3 => 8,
            _ => 0 // 默认值或错误处理
        };
        private void CloseArmPanel()
        {
            _armPanel.SetActive(false);
            _armTipsText.gameObject.SetActive(false);
            DespawnAllCards();
        }
        private void OpenGetCardPanel()
        {
            CloseMainPanel();
            _getCardPanel.SetActive(true);
        }
        private void CloseGetCardPanel()
        {
            _getCardPanel.SetActive(false);
        }
        private string InitGroundTips(GroundType groundType)
        {
            return groundType switch
            {
                GroundType.Center => "",
                GroundType.House => "",
                GroundType.None => "",
                _ => ""
            };
        }
        
        private void GetMainBuildCard()
        {
            
        }
        
        private void GetUseSlotCard()
        {
            var list = _groundSC.IGetCards();
            if(list.Count <= 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var obj = VoidCardPool.Instance.GetVoidCard();
                var sc = obj.GetComponent<VoidCard>();
                sc.Init(_groundSC.IGetGroundType());
                _voidCards.Add(sc);
            }
            ArrangeCards();
        }

        private void DespawnAllCards()
        {
            VoidCardPool.Instance.DespawnVoidCard();
        }
        
        // 调整卡片位置
        public void ArrangeCards()
        {
            int count = _voidCards.Count;

            if (count < 1)
            {
                Debug.LogWarning("No cards to arrange!");
                return;
            }

            if (count > 8)
            {
                Debug.LogWarning("Card limit reached (max 8)!");
                return;
            }

            float totalRange = 130 * 2; // 区间总范围
            float initialInterval = 100; // 初始间隔
            float interval = Mathf.Min(initialInterval, totalRange / (count - 1)); // 动态调整间隔

            List<Vector3> positions = new List<Vector3>();

            for (int i = 0; i < count; i++)
            {
                // 计算每个卡片的位置，y 坐标固定为 35
                float step = i - (count - 1) * 0.5f; // 计算相对位置
                positions.Add(new Vector3(step * interval, 35, 0));
            }

            // 更新卡片位置
            for (int i = 0; i < count; i++)
            {
                VoidCard card = _voidCards[i];
                card.transform.localPosition = positions[i];
            }
        }
        
        #endregion

        
        
    }
}