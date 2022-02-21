using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventReceiver : MonoBehaviour
{
    public UnityEvent<string> animEventTrigger = new UnityEvent<string>();

    public void OnTriggerAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        animEventTrigger.Invoke(eventName);
    }
}
