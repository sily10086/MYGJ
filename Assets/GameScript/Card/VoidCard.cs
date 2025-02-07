using cfg;
using GameScript.Ground;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.Card
{
    public class VoidCard:MonoBehaviour
    {
        [SerializeField] private Image _cardImage;
        [SerializeField] private Text _cardNameText;
        [SerializeField] private Text _cardLevelText;
        [SerializeField] private RectTransform _rectTransform;
        
        public void Init(GroundType groundType)
        {
            
        }
    }
}