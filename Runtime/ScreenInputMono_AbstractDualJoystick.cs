using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenInputMono_AbstractDualJoystick : MonoBehaviour
{

    public UnityEvent<float> m_onJoystickLeftHorizontal;
    public UnityEvent<float> m_onJoystickLeftVertical;
    public UnityEvent<float> m_onJoystickRightHorizontal;
    public UnityEvent<float> m_onJoystickRightVertical;
    public UnityEvent<Vector2, Vector2> m_onDualJoystick;

    public float m_joystickLeftHorizontalValuePrevious;
    public float m_joystickLeftVerticalValuePrevious;
    public float m_joystickRightHorizontalValuePrevious;
    public float m_joystickRightVerticalValuePrevious;


    public void SetJoystickLeftHorizontal(float value)
    {
        if (value != m_joystickLeftHorizontalValuePrevious)
        {
            m_joystickLeftHorizontalValuePrevious = value;
            m_onJoystickLeftHorizontal.Invoke(value);
            PushChangedDualJoystickInfo();
         }
    }

    private bool m_hadChanged = true;
    public void LateUpdate()
    {
        CheckChange();
    }

    private void CheckChange()
    {
        if (m_hadChanged)
        {
            m_hadChanged = false;
            PushChangedDualJoystickInfo();
        }
    }

    private void PushChangedDualJoystickInfo()
    {
        m_onDualJoystick.Invoke(new Vector2(m_joystickLeftHorizontalValuePrevious, m_joystickLeftVerticalValuePrevious), new Vector2(m_joystickRightHorizontalValuePrevious, m_joystickRightVerticalValuePrevious));
    }

    public void SetJoystickLeftVertical(float value)
    {
        if (value != m_joystickLeftVerticalValuePrevious)
        {
            m_joystickLeftVerticalValuePrevious = value;
            m_onJoystickLeftVertical.Invoke(value);
        }
    }
    public void SetJoystickRightHorizontal(float value)
    {
        if (value != m_joystickRightHorizontalValuePrevious)
        {
            m_joystickRightHorizontalValuePrevious = value;
            m_onJoystickRightHorizontal.Invoke(value);
        }
    }
    public void SetJoystickRightVertical(float value)
    {
        if (value != m_joystickRightVerticalValuePrevious)
        {
            m_joystickRightVerticalValuePrevious = value;
            m_onJoystickRightVertical.Invoke(value);
        }
    }


    public void SetJoysticks(Vector2 left, Vector2 right)
    {
        SetJoystickLeftHorizontal(left.x);
        SetJoystickLeftVertical(left.y);
        SetJoystickRightHorizontal(right.x);
        SetJoystickRightVertical(right.y);
        CheckChange();
    }
    
}
