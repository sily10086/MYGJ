namespace Common.Event
{
    /// <summary>
    /// 事件发布包装器，用于封装事件发布时的相关信息。
    /// </summary>
    internal readonly struct PostWrapper
    {
        /// <summary>
        /// 事件发布时的帧数。
        /// </summary>
        private readonly int _postFrame;
            
        /// <summary>
        /// 获取事件发布时的帧数。
        /// </summary>
        internal int PostFrame => _postFrame;
            
        /// <summary>
        /// 事件的唯一标识符。
        /// </summary>
        private readonly int _eventID;
            
        /// <summary>
        /// 获取事件的唯一标识符。
        /// </summary>
        internal int EventID => _eventID;
            
        /// <summary>
        /// 事件消息对象。
        /// </summary>
        private readonly IEventMessage _message;
            
        /// <summary>
        /// 获取事件消息对象。
        /// </summary>
        internal IEventMessage Message => _message;

        /// <summary>
        /// 初始化一个事件发布包装器。
        /// </summary>
        /// <param name="postFrame">事件发布时的帧数。</param>
        /// <param name="eventID">事件的唯一标识符。</param>
        /// <param name="message">事件消息对象。</param>
        internal PostWrapper(int postFrame, int eventID, IEventMessage message)
        {
            _postFrame = postFrame;
            _eventID = eventID;
            _message = message;
        }
    }
}