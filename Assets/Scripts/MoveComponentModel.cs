using UnityEngine;

public class MoveComponentModel
{
    private readonly IInputController inputController;
    private readonly IDeltaTimeGetter deltaTimeGetter;
    private readonly IMoveComponent moveComponent;
    private float moveSpeed;

    public MoveComponentModel(IInputController inputController, IDeltaTimeGetter deltaTimeGetter, IMoveComponent moveComponent)
    {
        this.inputController = inputController;
        this.deltaTimeGetter = deltaTimeGetter;
        this.moveComponent = moveComponent;
    }

    public void Update()
    {
        moveSpeed = inputController.IsRunningKeyDown() ?
            moveComponent.RunningSpeed :
            moveComponent.Speed;

        float horizontalAxis = inputController.GetHorizontalAxis();
        float verticalAxis = inputController.GetVerticalAxis();

        if (horizontalAxis == 0 && verticalAxis == 0)
            return;

        Vector3 moveVector = new Vector3(horizontalAxis, verticalAxis, 0) * deltaTimeGetter.GetDeltaTime() * moveSpeed;
        moveComponent.Translate(moveVector);
    }
}