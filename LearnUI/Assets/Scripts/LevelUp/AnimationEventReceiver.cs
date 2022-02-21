using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnTriggerAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        Debug.Log(gameObject.name);
    }
}
