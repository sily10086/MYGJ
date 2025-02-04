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
        [SerializeField] private List<Image> _populationCardSlotImages;
        [SerializeField] private List<Image> _closePopulationCardSlotImages;
        
        [Header("按钮")]
        [SerializeField] private Button _populationCardSlotButton;
        [SerializeField] private Button _mainPanelButton;
        
        [Header("Tips")]
        [SerializeField] private Text _groundTipsText;
        
        private int _currentLevel;
        
        private void Awake()
        {
            _populationCardSlotButton.onClick.AddListener(OpenPopulationCardSlot);
            _mainPanelButton.onClick.AddListener(OpenMainPanel);
            
        }

        public void InitGroundDataUI(IGround groundSC)
        {
            _groundTypeText.text = GroundTypeMapping(groundSC.IGetGroundType());
            _buildLevelText.text = "等级: " + groundSC.IGetGroundLevel().ToString();
            _currentLevel = groundSC.IGetGroundLevel();
            _groundTipsText.text = InitGroundTips(groundSC.IGetGroundType());
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
            switch (_currentLevel)
            {
                case 2:
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    break;
                case 1:
                    _closePopulationCardSlotImages[1].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    break;
                case 0:
                    _closePopulationCardSlotImages[0].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[1].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[2].gameObject.SetActive(true);
                    _closePopulationCardSlotImages[3].gameObject.SetActive(true);
                    break;
            }
        }
        private void ClosePopulationCardSlot()
        {
            _populationCardSlotImages.ForEach(image => image.gameObject.SetActive(false));
            _closePopulationCardSlotImages.ForEach(image => image.gameObject.SetActive(false));
        }
        private void OpenMainPanel()
        {
            ClosePopulationCardSlot();
            _buildCardSlotDataImage.gameObject.SetActive(true);
        }
        private void CloseMainPanel()
        {
            _buildCardSlotDataImage.gameObject.SetActive(false);
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