using cfg;
using Common.Event;
using GameScript.Luban;
using GameScript.UI;

namespace GameScript.GameData
{
    public static class GameData
    {
        #region 物资数据

        private static int _gold;
        private static int _wood;
        private static int _food;
        private static int _stone;
        private static int _iron;
        private static int _technology;

        #endregion

        #region 卡牌数据

        

        #endregion
        
        public static void Init()
        {
            _gold = LuBanData.tables.TbGameData.Get(GameDataType.Gold).InitData;
            _wood = LuBanData.tables.TbGameData.Get(GameDataType.Wood).InitData;
            _food = LuBanData.tables.TbGameData.Get(GameDataType.Food).InitData;
            _stone = LuBanData.tables.TbGameData.Get(GameDataType.Stone).InitData;
            _iron = LuBanData.tables.TbGameData.Get(GameDataType.Iron).InitData;
            _technology = LuBanData.tables.TbGameData.Get(GameDataType.Technology).InitData;
            
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.gold = _gold;
            updateTopUIEvent.stone = _stone;
            updateTopUIEvent.iron = _iron;
            updateTopUIEvent.technology = _technology;
            updateTopUIEvent.food = _food;
            updateTopUIEvent.wood = _wood;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }

        public static void AddGold(int amount)
        {
            _gold += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.gold = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }

        public static void AddWood(int amount)
        {
            _wood += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.wood = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }
        
        public static void AddStone(int amount)
        {
            _stone += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.stone = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }
        
        public static void AddFood(int amount)
        {
            _food += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.food = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }
        
        public static void AddTechnology(int amount)
        {
            _technology += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.technology = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }
        
        public static void AddIron(int amount)
        {
            _iron += amount;
            var updateTopUIEvent = UIEventManager.GetEvent<UpdateTopUIEvent>();
            updateTopUIEvent.iron = _gold;
            EventManager.Instance.GetEventUtility().SendMessage(updateTopUIEvent);
        }
    }
}