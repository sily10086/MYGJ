using System;
using System.Collections.Generic;

namespace Common.Event
{
    public partial class EventUtility
    {
        #region 属性

        /// <summary>
        /// 用于存储事件监听器的字典，键为事件ID，值为事件监听器链表
        /// </summary>
        private readonly Dictionary<int, LinkedList<Action<IEventMessage>>> _listeners = new(1000);
        
        /// <summary>
        /// 用于存储延迟广播事件的列表
        /// </summary>
        private readonly List<PostWrapper> _postingList = new(1000);

        #endregion
    }
}