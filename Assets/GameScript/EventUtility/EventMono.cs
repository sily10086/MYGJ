using System;
using UnityEngine;

namespace Common.Event
{
    public class EventMono:MonoBehaviour
    {
        private static EventMono _instance;
        public static EventMono Instance{get{return _instance;}}
        private EventUtility eventUtility;

        private void Awake()
        {
            eventUtility = new EventUtility();
        }

        public EventUtility GetEventUtility()
        {
            return eventUtility;
        }
    }
}