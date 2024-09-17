using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class ScreenInputMono_TrackedInputActionRef : MonoBehaviour
{
    public List<ScreenInputTracked> m_foundAndTracked = new List<ScreenInputTracked>();
    public UnityEvent< ScreenInputTracked> m_onStartReceived;
    public UnityEvent< ScreenInputTracked> m_onContextReceived;
    public UnityEvent< ScreenInputTracked> m_onEndReceived;


    public UnityEvent<string> m_debugListAsString;
    public void Update()
    {
        string debugString = "";
        foreach (var tracked in m_foundAndTracked)
        {
            debugString += string.Format("{0} {1} {2}\n", tracked.m_isPressing,
                tracked.m_screenCurrentPosition.x, tracked.m_screenCurrentPosition.y);
        }
        m_debugListAsString.Invoke(debugString);
    }

    public void OnEnable()
    {
        foreach (var tracked in m_foundAndTracked)
        {   
            tracked.m_screenPositionVector2.action.Enable();
            tracked.m_screenPositionVector2.action.performed += tracked.PerformGivenPosition;
            tracked.m_screenPositionVector2.action.canceled += tracked.PerformCancelPosition;
            tracked.m_isPressingBool.action.Enable();
            tracked.m_isPressingBool.action.performed += tracked.PreformGivenPressing;
            tracked.m_isPressingBool.action.canceled += tracked.PreformCancelPressing;

            tracked.m_events.m_onStartReceived.AddListener(m_onStartReceived.Invoke);
            tracked.m_events.m_onContextReceived.AddListener(m_onContextReceived.Invoke);
            tracked.m_events.m_onEndReceived.AddListener(m_onEndReceived.Invoke);


        }
    }
    public void OnDisable()
    {
        foreach (var tracked in m_foundAndTracked) {
            
            tracked.m_screenPositionVector2.action.performed -= tracked.PerformGivenPosition;
            tracked.m_screenPositionVector2.action.canceled -= tracked.PerformCancelPosition;
            tracked.m_isPressingBool.action.performed -= tracked.PreformGivenPressing;
            tracked.m_isPressingBool.action.canceled -= tracked.PreformCancelPressing;

            tracked.m_events.m_onStartReceived.RemoveListener(m_onStartReceived.Invoke);
            tracked.m_events.m_onContextReceived.RemoveListener(m_onContextReceived.Invoke);
            tracked.m_events.m_onEndReceived.RemoveListener(m_onEndReceived.Invoke);
        }
    }

}
