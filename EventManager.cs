using System;
using Godot;
using Godot.Collections;

public static class EventManager
{
    // Stores completed events
    private static Dictionary<string,Resource> completedEvents = new Dictionary<string,Resource>();

    // Event listeners
    private static Dictionary<string, Callable> eventListeners = new Dictionary<string, Callable>();

    // Mark an event as completed
    public static void CompleteEvent(string eventName,Resource resource = null)
    {
        if (!completedEvents.ContainsKey(eventName))
        {
            completedEvents.Add(eventName,resource);
            GD.Print($"Event Completed: {eventName}");

            // Notify listeners
            if (eventListeners.ContainsKey(eventName))
            {
                eventListeners[eventName].Call();
            }
        }
    }

    // Check if an event is completed
    public static bool IsEventCompleted(string eventName)
    {
        return completedEvents.ContainsKey(eventName);
    }

    // Add a listener for an event
    public static void AddListener(string eventName, Callable callback)
    {
        eventListeners[eventName] = callback;
    }

    // Remove a listener for an event
    public static void RemoveListener(string eventName, Action callback)
    {
       eventListeners.Remove(eventName);
    }
}
