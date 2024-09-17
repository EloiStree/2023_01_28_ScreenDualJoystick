using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenInputMono_TouchOnlyToDualJoystick : MonoBehaviour
{

    public ScreenInputMono_TouchOnlyToJoystick m_observed;
    public Vector2 m_leftJoystick;
    public Vector2 m_rightJoystick;

    public UnityEvent<string> m_debugAsString;


    public UnityEvent<float> m_onJoystickLeftHorizontal;
    public UnityEvent<float> m_onJoystickLeftVertical;
    public UnityEvent<float> m_onJoystickRightHorizontal;
    public UnityEvent<float> m_onJoystickRightVertical;
    public UnityEvent<Vector2, Vector2> m_onDualJoystick;

    public float m_joystickLeftHorizontalValuePrevious;
    public float m_joystickLeftVerticalValuePrevious;
    public float m_joystickRightHorizontalValuePrevious;
    public float m_joystickRightVerticalValuePrevious;


    void Update()
    {

        m_leftJoystick = Vector2.zero;
        m_rightJoystick = Vector2.zero;
        foreach(var touch in m_observed.m_fingers)
        {
            
            if(touch.m_isDown)
            {
                if (touch.m_isLeft)
                    m_leftJoystick = touch.m_percenteState;
            }
        }

        foreach (var touch in m_observed.m_fingers)
        {
            if (touch.m_isDown)
            {
                if (!touch.m_isLeft)
                    m_rightJoystick = touch.m_percenteState;
            }
        }

        bool hadChanged = false; ;
        if(m_leftJoystick.x != m_joystickLeftHorizontalValuePrevious)
        {
            m_joystickLeftHorizontalValuePrevious = m_leftJoystick.x;
            m_onJoystickLeftHorizontal.Invoke(m_leftJoystick.x);
            hadChanged = true;
        }
        if (m_leftJoystick.y != m_joystickLeftVerticalValuePrevious)
        {
            m_joystickLeftVerticalValuePrevious = m_leftJoystick.y;
            m_onJoystickLeftVertical.Invoke(m_leftJoystick.y);
            hadChanged = true;
        }
        if (m_rightJoystick.x != m_joystickRightHorizontalValuePrevious)
        {
            m_joystickRightHorizontalValuePrevious = m_rightJoystick.x;
            m_onJoystickRightHorizontal.Invoke(m_rightJoystick.x);
            hadChanged = true;
        }
        if (m_rightJoystick.y != m_joystickRightVerticalValuePrevious)
        {
            m_joystickRightVerticalValuePrevious = m_rightJoystick.y;
            m_onJoystickRightVertical.Invoke(m_rightJoystick.y);
            hadChanged = true;
        }
        if(hadChanged)
            m_onDualJoystick.Invoke(m_leftJoystick, m_rightJoystick);


        m_debugAsString.Invoke("Left: " + m_leftJoystick + "\n Right: " + m_rightJoystick);
        
    }
}
