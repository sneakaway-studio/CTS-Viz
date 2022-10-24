using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

/** 
 *  LISTEN for event => Set an object active/inactive
 */

public class Listener_SetActive : MonoBehaviour
{
    [Tooltip("Object to affect")]
    [SerializeField] GameObject obj;

    [Tooltip("The event that will trigger this listener")]
    [SerializeField] string eventName = "";

    [Tooltip("Before event")]
    [SerializeField] bool initialState = false;

    [Tooltip("After event")]
    [SerializeField] bool triggeredState = false;

    [Tooltip("Event has been received")]
    [SerializeField] bool triggered = false;

    [Tooltip("Delay in seconds")]
    [SerializeField] float delay;

    void Awake()
    {
        // initial state
        obj.SetActive(initialState);
    }

    private void OnEnable()
    {
        EventManager.StartListening(eventName, Trigger);
    }
    private void OnDisable()
    {
        EventManager.StopListening(eventName, Trigger);
    }

    void Trigger()
    {
        // only once
        if (!triggered)
        {
            triggered = true;

            // after delay
            Timer.Register(delay, () =>
            {
                // hide overlay
                obj.SetActive(triggeredState);
            });
        }
    }

}
