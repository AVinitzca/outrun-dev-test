using Assets._Outrun.Components;
using Assets._Outrun.Components.CarEngineState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacterController : MonoBehaviour, IResetable
{
    public CarEngine CarEngine;

    private bool mouseDown;
    private Vector2 mouseDownPosition;

    void Update()
    {
        Touches();

        MouseDrags();

        KeyboardPresses();
    }

    private void Touches()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SetSteering(touch.deltaPosition.x);
            }
        }
    }
    private void MouseDrags()
    {
        if (Input.mousePresent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
                mouseDownPosition = Input.mousePosition;
            }
            if(Input.GetMouseButtonUp(0) && mouseDown)
            {
                float x = Input.mousePosition.x - mouseDownPosition.x;
                if(Mathf.Abs(x) > 100.0f)
                    SetSteering(x);
            }
        }
        else
            mouseDown = false;
    }
    private void KeyboardPresses()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetSteering(Input.GetAxis("Horizontal"));
        }
    }

    // This should be a whole event dispatcher
    private void SetSteering(float deltaX)
    {
        CarEngineStateSteering.SteeringDirection direction = deltaX > 0.0f ?
                    CarEngineStateSteering.SteeringDirection.RIGHT :
                    CarEngineStateSteering.SteeringDirection.LEFT;
        CarEngine.SetSteering(direction);
    }

    public void Reset()
    {
        enabled = !enabled;
    }
}
