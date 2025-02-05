using System;
using System.Collections.Concurrent;
using Common.Event;
using GameScript.Ground;
using UnityEngine;

namespace GameScript.UI
{
    public static class UIEventManager
    {
        // 使用线程安全的ConcurrentDictionary存储事件消息实例
        private static readonly ConcurrentDictionary<Type, IEventMessage> _eventMessages = 
            new ();

        /// <summary>
        /// 获取指定类型的事件消息单例（如果不存在则创建新实例）
        /// </summary>
        /// <typeparam name="T">需要获取的事件消息类型</typeparam>
        /// <returns>事件消息单例</returns>
        public static T GetEvent<T>() where T : class, IEventMessage, new()
        {
            Type type = typeof(T);
        
            // 使用GetOrAdd确保线程安全，当类型不存在时使用new T()创建实例
            return (T)_eventMessages.GetOrAdd(type, _ => new T());
        }
    }
    public class UpdateTopUIEvent:IEventMessage
    {
        /// <summary>
        /// 金币
        /// </summary>
        public int gold;
        /// <summary>
        /// 木材
        /// </summary>
        public int wood;
        /// <summary>
        /// 石材
        /// </summary>
        public int stone;
        /// <summary>
        /// 铁
        /// </summary>
        public int iron;
        /// <summary>
        /// 食物
        /// </summary>
        public int food;
        /// <summary>
        /// 科技点
        /// </summary>
        public int technology;
    }

    public class OpenLeftUIEvent : IEventMessage
    {
        public IGround groundSC = null;
    }

    public class CardUIEvent : IEventMessage
    {
        public GameObject card = null;
    }
}