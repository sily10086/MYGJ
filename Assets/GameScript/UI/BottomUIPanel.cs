using GameScript.Card;
using UnityEngine;
using UnityEngine.UI;

public class BottomUIPanel : MonoBehaviour
{
    [SerializeField] private CardManager _cardManager;
    
    [Header("Button")]
    [SerializeField] private Button _cardManagerController;
    [Header("Text")]
    [SerializeField] private Text _cardManagerControllerText;

    private void Awake()
    {
        _cardManagerController.onClick.AddListener(CardManagerController);
        _cardManager.gameObject.SetActive(false);
        _cardManagerControllerText.text = "չ��";
    }

    private void CardManagerController()
    {
        if (_cardManager.gameObject.activeSelf)
        {
            _cardManager.gameObject.SetActive(false);
            _cardManagerControllerText.text = "չ��";
        }
        else
        {
            _cardManager.gameObject.SetActive(true);
            _cardManagerControllerText.text = "�ջ�";
        }
    }
}
