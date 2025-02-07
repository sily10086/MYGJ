using Common.Event;
using UnityEngine;
using UnityEngine.UI;

namespace GameScript.UI
{
    public class GameDataPanel:MonoBehaviour
    {
        [SerializeField]private Text _goldText;
        [SerializeField]private Text _woodText;
        [SerializeField]private Text _stoneText;
        [SerializeField]private Text _ironText;
        [SerializeField]private Text _technologyText;
        [SerializeField]private Text _foodText;

        private void Awake()
        {
            Init();
            EventManager.Instance?.GetEventUtility().AddListener<UpdateTopUIEvent>(UpdateGameData);
        }

        private void Init()
        {
            
        }

        private void UpdateGameData(IEventMessage eventMessage)
        {
            if(eventMessage is not UpdateTopUIEvent message) return;
            _goldText.text = "金币: " + message.gold.ToString();
            _woodText.text = "木头: " + message.wood.ToString();
            _stoneText.text = "石头: " + message.stone.ToString();
            _ironText.text = "铁矿: " + message.iron.ToString();
            _technologyText.text = "科技: " + message.technology.ToString();
            _foodText.text = "食物: " + message.food.ToString();
        }
    }
}