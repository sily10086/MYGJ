using System;
using UnityEngine;

namespace Common.Event
{
    public class EventManager:MonoBehaviour
    {
        private static EventManager _instance;
        public static EventManager Instance{get{return _instance;}}
        private EventUtility eventUtility;

        private void Awake()
        {
            _instance = this;
            eventUtility = new EventUtility();
        }

        public EventUtility GetEventUtility()
        {
            return eventUtility;
        }
    }
}