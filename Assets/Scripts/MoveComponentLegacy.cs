using UnityEngine;

public class MoveComponentLegacy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;
    private bool isRunning;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            if (isRunning)
                transform.Translate(new Vector3(horizontalAxis, verticalAxis, 0) * Time.deltaTime * runningSpeed);
            else
                transform.Translate(new Vector3(horizontalAxis, verticalAxis, 0) * Time.deltaTime * speed);
        }
    }
}