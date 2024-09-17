using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2DMono_JoystickOnScreen : MonoBehaviour
{

    public RectTransform m_centerJoystick;
    [Range(-1, 1)]
    public float m_leftToRightPercent11;
    [Range(-1, 1)]
    public float m_downToUpPercent11;

    public float m_multiplicator = 1.0f;

    public void Update()
    {
        m_centerJoystick.anchoredPosition = new Vector2(
           ( m_leftToRightPercent11 * m_centerJoystick.sizeDelta.x / 2) * m_multiplicator, 
            (m_downToUpPercent11 * m_centerJoystick.sizeDelta.y / 2)*m_multiplicator);
        
    }

    public void SetJoystickHorizontalLeftRight(float value)
    {
        m_leftToRightPercent11 = Mathf.Clamp( value,-1,1);
    }
    public void SetJoystickVerticalDownUp(float value)
    {
        m_downToUpPercent11 = Mathf.Clamp(value, -1, 1);
    }
}
