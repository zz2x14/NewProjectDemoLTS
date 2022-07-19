using System;
using System.Collections.Generic;
using UnityEngine;
using MyEventSpace;

public static class EventName
{
    public const string OnEnemyDeath = nameof(EventManager.OnEnemyDeath);
    public const string OnPlayerDeath = nameof(EventManager.OnPlayerDeath);
    public const string OnPlayerMenuOpen = nameof(EventManager.OnPlayerMenuOpen);
    public const string OnSceneTeleport = nameof(EventManager.OnSceneTeleport);
}

//Sign:一对多和多对一的使用情况
namespace MyEventSpace// TODO:做到外部调用不到委托
{
    public delegate void EventHandler(object sender, EventArgs e);
    
    [DefaultExecutionOrder(205)]
    public class EventManager : PersistentSingletonTool<EventManager>
    {
        private Dictionary<string, EventHandler> eventDic = new Dictionary<string, EventHandler>();
        
        internal EventHandler OnEnemyDeath;
        internal EventHandler OnPlayerDeath; //对委托做一个保护
        internal EventHandler OnPlayerMenuOpen;
        internal EventHandler OnSceneTeleport;
    
        private void OnEnable()
        {
            InitializeEventDic();
        }
    
        private void OnDisable()
        {
            ClearEventHandlerDic();
        }

        private void InitializeEventDic()
        {
            eventDic.Add(EventName.OnEnemyDeath,OnEnemyDeath);
            eventDic.Add(EventName.OnPlayerDeath,OnPlayerDeath);
            eventDic.Add(EventName.OnPlayerMenuOpen,OnPlayerMenuOpen);
            eventDic.Add(EventName.OnSceneTeleport,OnSceneTeleport);
        }
    
        public void AddEventHandlerListener(string eventName, EventHandler handler)
        {
            if (eventDic.ContainsKey(eventName))
            {
                eventDic[eventName] += handler;
            }
        }
        
        public void RemoveEventHandlerListener(string eventName, EventHandler handler)
        {
            if (eventDic.ContainsKey(eventName))
                eventDic[eventName] -= handler;
        }
    
        public void ClearEventHandlerDic()
        {
            eventDic.Clear();
        }
    
        public void EventHandlerTrigger(string eventName, object sender)
        {
            if (eventDic.ContainsKey(eventName))
                eventDic[eventName]?.Invoke(sender,EventArgs.Empty);
        }
        
        public void EventHandlerTrigger(string eventName, object sender,EventArgs e)
        {
            if (eventDic.ContainsKey(eventName))
                eventDic[eventName]?.Invoke(sender,e);
        }
    }
}



