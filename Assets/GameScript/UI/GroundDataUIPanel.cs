using System;
using System.Collections.Generic;
using GameScript.Ground;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.UI
{
    public class GroundDataUIPanel:MonoBehaviour
    {
        [Header("Other")]
        [SerializeField] private Text _groundTypeText;
        [SerializeField] private Text _buildLevelText;
        [SerializeField] private Image _buildCardSlotDataImage;
        
        [Header("劳力")]
        [SerializeField] private Text _populationTipsText;
        [SerializeField] private List<Image> _populationCardSlotImages;
        [SerializeField] private List<Image> _closePopulationCardSlotImages;
        [SerializeField] private int _canPopulationNumber;
        [SerializeField] private int _usePopulationNumber;
        
        [Header("按钮")]
        [SerializeField] private Button _populationCardSlotButton;
        [SerializeField] private Button _mainPanelButton;
        [SerializeField] private Button _armButton;
        
        [Header("军队")]
        [SerializeField] private GameObject _armPanel;
        [SerializeField] private Text _armTipsText;
        [SerializeField] private List<Image> _armCardSlotImages;
        [SerializeField] private List<Image> _closeArmCardSlotImages;
        
        [Header("Tips")]
        [SerializeField] private Text _groundTipsText;
        
        private int _currentLevel;
        
        private void Awake()
        {
            _populationCardSlotButton.onClick.AddListener(OpenPopulationCardSlot);
            _mainPanelButton.onClick.AddListener(OpenMainPanel);
            _armButton.onClick.AddListener(OpenArmPanel);
            
        }

        public void InitGroundDataUI(IGround groundSC)
        {
            _groundTypeText.text = GroundTypeMapping(groundSC.IGetGroundType());
            _buildLevelText.text = "等级: " + groundSC.IGetGroundLevel();
            _currentLevel = groundSC.IGetGroundLevel();
            _groundTipsText.text = InitGroundTips(groundSC.IGetGroundType());
            _usePopulationNumber = groundSC.IGetUsePopulation();
            _armButton.gameObject.SetActive(groundSC.IGetGroundType() == GroundType.Center);
            _populationCardSlotButton.gameObject.SetActive(groundSC.IGetGroundType() != GroundType.Center);
            OpenMainPanel();
        }

        #region 私有方法

        private string GroundTypeMapping(GroundType groundType)
        {
            return groundType switch
            {
                GroundType.Center => "城镇中心",
                GroundType.House => "房屋",
                _ => "普通地块",
            };
        }
        private void OpenPopulationCardSlot()
        {
            CloseMainPanel();
            _populationCardSlotImages.ForEach(image => image.gameObject.SetActive(true));
            _populationTipsText.gameObject.SetActive(true);
            switch (_currentLevel)
            {
                case 3:
                    _closePopulationCardSlotImages[0].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[1].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(false);
                    _canPopulationNumber = 4;
                    break;
                case 2:
                    _closePopulationCardSlotImages[0].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[1].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    _canPopulationNumber = 2;
                    _populationTipsText.text = "劳动力: "+_usePopulationNumber+"/"+_canPopulationNumber;
                    break;
                case 1:
                    _closePopulationCardSlotImages[0].gameObject.SetActive(false);
                    _closePopulationCardSlotImages[1].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    _canPopulationNumber = 1;
                    _populationTipsText.text = "劳动力: "+_usePopulationNumber+"/"+_canPopulationNumber;
                    break;
                case 0:
                    _closePopulationCardSlotImages[0].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[1].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    _canPopulationNumber = 0;
                    _usePopulationNumber = 0;
                    _populationTipsText.text = "劳动力: 0/0";
                    break;
            }
        }
        private void ClosePopulationCardSlot()
        {
            _populationCardSlotImages.ForEach(image => image.gameObject.SetActive(false));
            _closePopulationCardSlotImages.ForEach(image => image.gameObject.SetActive(false));
            _populationTipsText.gameObject.SetActive(false);
        }
        private void OpenMainPanel()
        {
            ClosePopulationCardSlot();
            CloseArmPanel();
            _buildCardSlotDataImage.gameObject.SetActive(true);
            _groundTypeText.gameObject.SetActive(true);
            _buildLevelText.gameObject.SetActive(true);
            _groundTipsText.gameObject.SetActive(true);
        }
        private void CloseMainPanel()
        {
            _buildCardSlotDataImage.gameObject.SetActive(false);
            _groundTypeText.gameObject.SetActive(false);
            _buildLevelText.gameObject.SetActive(false);
            _groundTipsText.gameObject.SetActive(false);
        }
        private void OpenArmPanel()
        {
            CloseMainPanel();
            _armPanel.SetActive(true);
            _armTipsText.gameObject.SetActive(true);
            switch (_currentLevel)
            {
                case 1:
                    for (int i = 0; i < _armCardSlotImages.Count; i++)
                    {
                        _closeArmCardSlotImages[i].gameObject.SetActive(i>1);
                    }
                    _armTipsText.text = "军队: "+_usePopulationNumber+"/2";
                    break;
                case 2:
                    for (int i = 0; i < _armCardSlotImages.Count; i++)
                    {
                        _closeArmCardSlotImages[i].gameObject.SetActive(i>3);
                    }
                    _armTipsText.text = "军队: "+_usePopulationNumber+"/4";
                    break;
                case 3:
                    for (int i = 0; i < _armCardSlotImages.Count; i++)
                    {
                        _closeArmCardSlotImages[i].gameObject.SetActive(false);
                    }
                    _armTipsText.text = "军队: "+_usePopulationNumber+"/8";
                    break;
            }
        }
        private void CloseArmPanel()
        {
            _armPanel.SetActive(false);
            _armTipsText.gameObject.SetActive(false);
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

        #endregion
        
    }
}