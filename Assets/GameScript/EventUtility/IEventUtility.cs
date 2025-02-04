
namespace Common.Event
{
    public interface IEventUtility
    {
        /// <summary>
        /// 添加事件监听器，使用泛型事件类型
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="listener">事件监听器</param>
        public void AddListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : class, IEventMessage;

        /// <summary>
        /// 添加事件监听器，使用事件类型
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="listener">事件监听器</param>
        public void AddListener(System.Type eventType, System.Action<IEventMessage> listener);

        /// <summary>
        /// 添加事件监听器，使用事件ID
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="listener">事件监听器</param>
        public void AddListener(int eventId, System.Action<IEventMessage> listener);
        
        /// <summary>
        /// 实时广播事件，使用事件消息对象
        /// </summary>
        /// <param name="message">事件消息对象</param>
        public void SendMessage(IEventMessage message);

        /// <summary>
        /// 实时广播事件，使用事件ID和事件消息对象
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="message">事件消息对象</param>
        public void SendMessage(int eventId, IEventMessage message);

        /// <summary>
        /// 延迟广播事件，使用事件消息对象
        /// </summary>
        /// <param name="message">事件消息对象</param>
        public void PostMessage(IEventMessage message);

        /// <summary>
        /// 延迟广播事件，使用事件ID和事件消息对象
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="message">事件消息对象</param>
        public void PostMessage(int eventId, IEventMessage message);

        /// <summary>
        /// 移除事件监听器，使用泛型事件类型
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : struct, IEventMessage;
        
        /// <summary>
        /// 移除事件监听器，使用事件类型
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener(System.Type eventType, System.Action<IEventMessage> listener);
        
        /// <summary>
        /// 移除事件监听器，使用事件ID
        /// </summary>
        /// <param name="eventId">事件ID</param>
        /// <param name="listener">事件监听器</param>
        public void RemoveListener(int eventId, System.Action<IEventMessage> listener);
    }
}