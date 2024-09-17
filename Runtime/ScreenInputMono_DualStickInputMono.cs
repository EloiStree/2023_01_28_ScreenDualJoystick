using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ScreenInputMono_DualStickInputMono : MonoBehaviour
{

    public UnityEvent<float> m_onLeftJoystickHorizontalChanged;
    public UnityEvent<float> m_onLeftJoystickVerticalChanged;
    public UnityEvent<float> m_onRightJoystickHorizontalChanged;
    public UnityEvent<float> m_onRightJoystickVerticalChanged;

    public UnityEvent<string> m_debugAsString;

    public VirtualScreenJoystickState m_left;
    public VirtualScreenJoystickState m_right;


  


     float m_previousLeftHorizontal = 0.0f;
     float m_previousLeftVertical = 0.0f;
     float m_previousRightHorizontal = 0.0f;
     float m_previousRightVertical = 0.0f;

    [System.Serializable]
    public class VirtualScreenJoystickState { 
        public bool m_isDown;
        public Vector2 m_startPositionInPixel;
        public Vector2 m_currentPositionInPixel;
        public Vector2 m_percenteState;
        public ScreenInputTracked m_source;
        public void ComputerPercentState(float pixelRadius)
        {
            if(pixelRadius == 0.0f || !m_isDown)
            {
                m_percenteState= Vector2.zero;
                return;
            }



            Vector2 delta = m_currentPositionInPixel - m_startPositionInPixel;
            m_percenteState = delta / pixelRadius;

        }
    }


    public float m_joystickScreenWidthPercent = 0.1f;
    [Header("Debug")]
    public float m_width = Screen.width;
    public float m_halfWidth = Screen.width;
    public float m_pixelRadius = 0.0f;


    private void Update()
    {

        m_width = Screen.width;
        m_halfWidth = m_width / 2;
        m_pixelRadius = m_joystickScreenWidthPercent * m_width;

        if(m_previousLeftHorizontal != m_left.m_percenteState.x)
        {
            m_previousLeftHorizontal = m_left.m_percenteState.x;
            m_onLeftJoystickHorizontalChanged.Invoke(m_left.m_percenteState.x);
        }
        if (m_previousLeftVertical != m_left.m_percenteState.y)
        {
            m_previousLeftVertical = m_left.m_percenteState.y;
            m_onLeftJoystickVerticalChanged.Invoke(m_left.m_percenteState.y);
        }
        if (m_previousRightHorizontal != m_right.m_percenteState.x)
        {
            m_previousRightHorizontal = m_right.m_percenteState.x;
            m_onRightJoystickHorizontalChanged.Invoke(m_right.m_percenteState.x);
        }
        if (m_previousRightVertical != m_right.m_percenteState.y)
        {
            m_previousRightVertical = m_right.m_percenteState.y;
            m_onRightJoystickVerticalChanged.Invoke(m_right.m_percenteState.y);
        }

        string debug = string.Format("Left: \n{0} {1} {2}\n{8} {9}\n {3}\nRight: \n{4} {5} {6}\n {10} {11}\n {7}",
            m_left.m_isDown, m_left.m_startPositionInPixel.x, m_left.m_startPositionInPixel.y, m_left.m_percenteState,
            m_right.m_isDown, m_right.m_startPositionInPixel.x, m_right.m_startPositionInPixel.y, m_right.m_percenteState
            
            ,m_left.m_currentPositionInPixel.x, m_left.m_currentPositionInPixel.y
            ,m_right.m_currentPositionInPixel.x, m_right.m_currentPositionInPixel.y
            );
        m_debugAsString.Invoke(debug);

    }

   
    public void ReceivedScreenInfoContext(ScreenInputTracked onRecevied)
    {

        bool left = onRecevied.m_screenStartPosition.x < m_halfWidth;
        if (left)
        {
            m_left.m_isDown = onRecevied.m_isPressing;
            m_left.m_startPositionInPixel = onRecevied.m_screenStartPosition;
            m_left.m_currentPositionInPixel = onRecevied.m_screenCurrentPosition;
            m_left.m_source = onRecevied;
            m_left.ComputerPercentState(m_pixelRadius);
        }
        else
        {
            m_right.m_isDown = onRecevied.m_isPressing;
            m_right.m_startPositionInPixel = onRecevied.m_screenStartPosition;
            m_right.m_currentPositionInPixel = onRecevied.m_screenCurrentPosition;
            m_right.m_source = onRecevied;
            m_right.ComputerPercentState(m_pixelRadius);
        }
    }
    public void ReceivedScreenInfoEndContext(ScreenInputTracked onEnd)
    {
        bool left = onEnd.m_screenStartPosition.x < m_halfWidth;
        if (left)
        {
            m_left.m_isDown = false;
            m_left.m_startPositionInPixel = Vector2.zero;
            m_left.m_currentPositionInPixel = Vector2.zero;
            m_left.m_percenteState = Vector2.zero;
            m_right.m_source = onEnd;
            m_left.ComputerPercentState(m_pixelRadius);
        }
        else
        {
            m_right.m_isDown = false;
            m_right.m_startPositionInPixel = Vector2.zero;
            m_right.m_currentPositionInPixel = Vector2.zero;
            m_right.m_percenteState = Vector2.zero;
            m_right.m_source = onEnd;
            m_right.ComputerPercentState(m_pixelRadius);
        }
    }


}
