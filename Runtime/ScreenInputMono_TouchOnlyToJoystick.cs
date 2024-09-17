using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ScreenInputMono_TouchOnlyToJoystick : MonoBehaviour
{


    public VirtualScreenJoystickState[] m_fingers = new VirtualScreenJoystickState [10]; 
    public int m_fingerCount = 0;

    public UnityEvent<string> m_debugAsString;




    [System.Serializable]
    public class VirtualScreenJoystickState
    {
        public bool m_isDown;
        public Vector2 m_startPositionInPixel;
        public Vector2 m_currentPositionInPixel;
        public Vector2 m_percenteState;
        public int m_touchIndex;
        public bool m_isLeft;
    }


    public float m_widthPercentOfRadius = 0.1f;
    public float m_screenWidth = 1920;
    public float m_screenHalfWidth = 1080;
    public float m_radiusPixelToGeneratePercent;


    void Update()
    {
        m_screenWidth = Screen.width;
        m_screenHalfWidth = m_screenWidth / 2;
        m_radiusPixelToGeneratePercent = m_screenWidth * m_widthPercentOfRadius;

        m_fingerCount = Touchscreen.current.touches.Count;
        for ( int i = 0; i < m_fingers.Length; i++)
        {
            if(i<m_fingerCount)
            {
                bool isPressed = Touchscreen.current.touches[i].press.isPressed;
                Vector2 touchPosition = Touchscreen.current.touches[i].position.ReadValue();

                bool isNew = !m_fingers[i].m_isDown && Touchscreen.current.touches[i].press.isPressed;
                if (isNew)
                {
                    m_fingers[i].m_isDown = true;
                    m_fingers[i].m_startPositionInPixel = touchPosition;
                    m_fingers[i].m_currentPositionInPixel = m_fingers[i].m_startPositionInPixel;
                    m_fingers[i].m_percenteState = Vector2.zero;
                    m_fingers[i].m_touchIndex = i;
                }
                

                if (Touchscreen.current.touches[i].press.isPressed)
                {
                    m_fingers[i].m_isDown = true;
                    m_fingers[i].m_currentPositionInPixel = touchPosition;
                    m_fingers[i].m_percenteState = 
                        (m_fingers[i].m_currentPositionInPixel -
                        m_fingers[i].m_startPositionInPixel)
                        / m_radiusPixelToGeneratePercent;
                    m_fingers[i].m_isLeft = m_fingers[i].m_currentPositionInPixel.x < m_screenHalfWidth;
                }

                if (!isPressed) {

                    if (m_fingers[i].m_isDown)
                    {

                        m_fingers[i].m_isDown = false;
                        m_fingers[i].m_startPositionInPixel = Vector2.zero;
                        m_fingers[i].m_currentPositionInPixel = Vector2.zero;
                        m_fingers[i].m_percenteState = Vector2.zero;
                        m_fingers[i].m_touchIndex = i;
                    }
                    m_fingers[i].m_isDown = false;
                }
            }
            else
            {
                if (m_fingers[i].m_isDown) { 
                
                    m_fingers[i].m_isDown = false;
                    m_fingers[i].m_startPositionInPixel = Vector2.zero;
                    m_fingers[i].m_currentPositionInPixel = Vector2.zero;
                    m_fingers[i].m_percenteState = Vector2.zero;
                    m_fingers[i].m_touchIndex = i;
                }
                m_fingers[i].m_isDown = false;
            }

            m_fingers[i].m_percenteState = (m_fingers[i].m_currentPositionInPixel - m_fingers[i].m_startPositionInPixel) / m_radiusPixelToGeneratePercent;
            
        }

        string debugString = "Finger Count "+ m_fingerCount+"\n";
        for (int i = 0; i < m_fingers.Length; i++)
        {
            debugString += string.Format("{0} \n{1} {2} \n{3} {4} \n{5}\n", m_fingers[i].m_isDown,
                m_fingers[i].m_startPositionInPixel.x, m_fingers[i].m_startPositionInPixel.y,
                m_fingers[i].m_currentPositionInPixel.x, m_fingers[i].m_currentPositionInPixel.y,
                m_fingers[i].m_percenteState);
        }
        m_debugAsString.Invoke(debugString);

       
    }

}
