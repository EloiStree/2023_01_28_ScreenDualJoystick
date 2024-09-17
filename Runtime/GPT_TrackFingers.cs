using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThreeFingerTracker : MonoBehaviour
{
    // This will hold references to the 3 tracked fingers
    private Vector2[] fingerPositions = new Vector2[3];

    void Update()
    {
        // Check if we have at least 3 touches
        if (Touchscreen.current.touches.Count >= 3)
        {
            // Loop through the first 3 touches
            for (int i = 0; i < 3; i++)
            {
                // Check if the touch is active
                if (Touchscreen.current.touches[i].press.isPressed)
                {
                    // Get the touch position
                    fingerPositions[i] = Touchscreen.current.touches[i].position.ReadValue();
                }
            }

            // You now have the positions of the first 3 fingers
            Debug.Log($"Finger 1: {fingerPositions[0]}, Finger 2: {fingerPositions[1]}, Finger 3: {fingerPositions[2]}");
        }
    }
}


