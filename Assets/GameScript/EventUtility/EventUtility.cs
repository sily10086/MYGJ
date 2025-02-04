using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Event
{
    //================================================
    /*
     * 事件管理工具。
     *
     * --------------------用法--------------------
     *
        public struct EventA : IEventMessage
        {
            public int _a;
        }
        public void EventAAction(IEventMessage message)
        {
            if(message is not EventA eventA) return;
            
            LogKit.Log(eventA._a);
        }
        1.添加
        this.GetUtility<IEventUtility>().AddListener<EventA>(EventAAction);
        2.广播
        实时
        this.GetUtility<IEventUtility>().SendMessage(new EventA{ a = 1 });
        延迟
        this.GetUtility<IEventUtility>().PostMessage(new EventA{ a = 1 });
        3.移除
        this.GetUtility<IEventUtility>().RemoveListener<EventA>(EventAAction);
     *
     *
     */
    //================================================
    
    /// <summary>
    /// 事件管理工具
    /// </summary>
    public partial class EventUtility : IEventUtility
    {
        #region 生命周期
        
        /// <summary>
        /// 更新事件系统，处理延迟广播事件
        /// </summary>
        public void OnUpdate()
        {
            // 从列表末尾向前遍历，确保在移除元素时不会影响遍历
            for (int i = _postingList.Count - 1; i >= 0; i--)
            {
                var wrapper = _postingList[i];
                
                // 如果当前帧数大于等于延迟帧数，则发送事件
                if (Time.frameCount >= wrapper.PostFrame)
                {
                    SendMessage(wrapper.EventID, wrapper.Message);
                    
                    // 移除已处理的延迟事件
                    _postingList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 销毁事件系统，清理所有资源
        /// </summary>
        public void OnDestroy(UnityAction callback = null)
        {
            // 移除所有事件监听器
            RemoveAllListener();
            
            // 记录日志信息
            Debug.Log($"{nameof(EventUtility)} destroy all !");
        }

        #endregion

        #region 添加事件

        /// <summary>
        /// 添加事件监听器，使用泛型事件类型
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="listener">事件监听器</param>
        public void AddListener<TEvent>(Action<IEventMessage> listener) where TEvent : class, IEventMessage
        {
            // 获取事件类型的哈希码作为事件ID
            Type eventType = typeof(TEvent);
            int eventId = eventType.GetHashCode();
            AddListener(eventId, listener);
        }

        /// <summary>
        /// 添加事件监听器，使用事件类型
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="listener">事件监听器</param>
        public void AddListener(Type eventType, Action<IEventMessage> listener)
        {
            // 获取事件类型的哈希码作为事件ID
            int eventId = eventType.GetHashCode();
            AddListener(eventId, listener);
        }

        /// <summary>
        /// 添加事件监听器，使用事件ID
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="listener">事件监听器</param>
        public void AddListener(int eventId, Action<IEventMessage> listener)
        {
            // 如果字典中没有该事件ID，则添加一个新的链表
            if (!_listeners.ContainsKey(eventId))
                _listeners.Add(eventId, new LinkedList<Action<IEventMessage>>());
            // 如果链表中没有该监听器，则添加到链表末尾
            if (!_listeners[eventId].Contains(listener))
                _listeners[eventId].AddLast(listener);
        }

        #endregion

        #region 广播事件

        /// <summary>
        /// 实时广播事件，使用事件消息对象
        /// </summary>
        /// <param name="message">事件消息对象</param>
        public void SendMessage(IEventMessage message)
        {
            // 获取事件消息对象的类型哈希码作为事件ID
            int eventId = message.GetType().GetHashCode();
            SendMessage(eventId, message);
        }

        /// <summary>
        /// 实时广播事件，使用事件ID和事件消息对象
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="message">事件消息对象</param>
        public void SendMessage(int eventId, IEventMessage message)
        {
            // 如果字典中没有该事件ID，则直接返回
            if (!_listeners.TryGetValue(eventId, out var listeners))
                return;

            // 如果监听器链表中有监听器，则从末尾向前遍历并调用每个监听器
            if (listeners.Count > 0)
            {
                var currentNode = listeners.Last;
                while (currentNode != null)
                {
                    currentNode.Value.Invoke(message);
                    currentNode = currentNode.Previous;
                }
            }
        }

        /// <summary>
        /// 延迟广播事件，使用事件消息对象
        /// </summary>
        /// <param name="message">事件消息对象</param>
        public void PostMessage(IEventMessage message)
        {
            // 获取事件消息对象的类型哈希码作为事件ID
            int eventId = message.GetType().GetHashCode();
            PostMessage(eventId, message);
        }

        /// <summary>
        /// 延迟广播事件，使用事件ID和事件消息对象
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="message">事件消息对象</param>
        public void PostMessage(int eventId, IEventMessage message)
        {
            // 将事件信息添加到延迟广播列表中
            _postingList.Add(new PostWrapper(Time.frameCount, eventId, message));
        }

        #endregion

        #region 移除监听

        /// <summary>
        /// 移除事件监听器，使用泛型事件类型
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener<TEvent>(Action<IEventMessage> listener) where TEvent : struct, IEventMessage
        {
            // 获取事件类型的哈希码作为事件ID
            Type eventType = typeof(TEvent);
            int eventId = eventType.GetHashCode();
            RemoveListener(eventId, listener);
        }

        /// <summary>
        /// 移除事件监听器，使用事件类型
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener(Type eventType, Action<IEventMessage> listener)
        {
            // 获取事件类型的哈希码作为事件ID
            int eventId = eventType.GetHashCode();
            RemoveListener(eventId, listener);
        }

        /// <summary>
        /// 移除事件监听器，使用事件ID
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener(int eventId, Action<IEventMessage> listener)
        {
            // 如果字典中有该事件ID，并且链表中有该监听器，则移除监听器
            if (_listeners.ContainsKey(eventId))
            {
                if (_listeners[eventId].Contains(listener))
                    _listeners[eventId].Remove(listener);
            }
        }
        
        /// <summary>
        /// 清空所有事件监听器
        /// </summary>
        private void RemoveAllListener()
        {
            // 清空所有监听器链表
            foreach (int eventId in _listeners.Keys)
            {
                _listeners[eventId].Clear();
            }
            // 清空监听器字典和延迟广播列表
            _listeners.Clear();
            _postingList.Clear();
        }

        #endregion
    }
}