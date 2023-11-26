using UnityEngine;

public class InputController : IInputController
{
    public bool IsRunningKeyUp()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }

    public bool IsRunningKeyDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
}