using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://www.willrmiller.com/?p=87
// http://wiki.unity3d.com/index.php/CSharpEventManager

public class GameEvent
{
}

public class EventManager : MonoSingleton<EventManager>
{
    public delegate void EventDelegate<T>(T e) where T : GameEvent;
    private delegate void EventDelegate(GameEvent e);

    private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

    private List<GameEvent> eventQueue = new List<GameEvent>(10);

    public static void Subscribe<T>(EventDelegate<T> del) where T : GameEvent
    {
        if (Instance.delegateLookup.ContainsKey(del))
            return;

        EventDelegate internalDelegate = (e) => del((T)e);
        Instance.delegateLookup[del] = internalDelegate;

        EventDelegate tempDel;
        if (Instance.delegates.TryGetValue(typeof(T), out tempDel))
        {
            Instance.delegates[typeof(T)] = tempDel += internalDelegate;
        }
        else
        {
            Instance.delegates[typeof(T)] = internalDelegate;
        }
    }

    public static void UnSubscribe<T>(EventDelegate<T> del) where T : GameEvent
    {
        if (Instance == null) return;
        
        EventDelegate internalDelegate;
        if (Instance.delegateLookup.TryGetValue(del, out internalDelegate))
        {
            EventDelegate tempDel;
            if (Instance.delegates.TryGetValue(typeof(T), out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    Instance.delegates.Remove(typeof(T));
                }
                else
                {
                    Instance.delegates[typeof(T)] = tempDel;
                }
            }

            Instance.delegateLookup.Remove(del);
        }
    }

    public static void TriggerEvent(GameEvent e)
    {
        EventDelegate del;
        if (Instance.delegates.TryGetValue(e.GetType(), out del))
            del.Invoke(e);

#if USE_WILD_EVENT
        if (Instance.delegates.TryGetValue(typeof(GameEvent), out del))
            del.Invoke(e);
#endif
    }

    public static void QueueEvent(GameEvent e)
    {
        Instance.eventQueue.Add(e);
    }

    /// Need processing limit
    void Update()
    {
        while (Instance.eventQueue.Count > 0)
        {
            GameEvent e = Instance.eventQueue[0];
            TriggerEvent(e);
            
            Instance.eventQueue.RemoveAt(0);
        }
    }

    protected EventManager () {}
}
