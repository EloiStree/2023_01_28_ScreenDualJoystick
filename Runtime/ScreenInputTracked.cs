using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class ScreenInputTracked
{

    public InputActionReference m_isPressingBool;
    public InputActionReference m_screenPositionVector2;
    public bool m_isPressing;
    public Vector2 m_screenStartPosition;
    public Vector2 m_screenCurrentPosition;
    public Vector2 m_screenEndPosition;

    public Events m_events;
    [System.Serializable]
    public class Events { 
        public UnityEvent<ScreenInputTracked> m_onStartReceived = new UnityEvent<ScreenInputTracked>();
        public UnityEvent<ScreenInputTracked> m_onContextReceived = new UnityEvent<ScreenInputTracked>();
        public UnityEvent<ScreenInputTracked> m_onEndReceived = new UnityEvent<ScreenInputTracked>();
    }

    public void PreformGivenPressing(InputAction.CallbackContext ctx)
    {
        ValuePressingReceived(ctx.ReadValue<float>() > 0.5f);
    }

   

    public void PreformCancelPressing(InputAction.CallbackContext ctx)
    {
        ValuePressingReceived(ctx.ReadValue<float>() > 0.5f);
    }

    private void ValuePressingReceived(bool value)
    {
        bool previousValue = m_isPressing;
        bool newValue = value;
        if (previousValue != newValue)
        {
            m_isPressing = newValue;
            if (m_isPressing)
            {
                m_screenStartPosition = m_screenCurrentPosition;
                m_events.m_onStartReceived.Invoke(this);
            }


            if (!m_isPressing)
            {
                m_screenEndPosition = m_screenCurrentPosition;
                m_events.m_onEndReceived.Invoke(this);
            }
        }
    }
    public void PerformGivenPosition(InputAction.CallbackContext ctx)
    {
        m_screenCurrentPosition = ctx.ReadValue<Vector2>();
        m_events.m_onContextReceived.Invoke(this);
    }
    public void PerformCancelPosition(InputAction.CallbackContext ctx)
    {
        m_screenEndPosition = ctx.ReadValue<Vector2>();
        m_events.m_onContextReceived.Invoke(this);
    }
}
