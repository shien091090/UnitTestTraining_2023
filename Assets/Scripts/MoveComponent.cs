using UnityEngine;

public class MoveComponent : MonoBehaviour, IMoveComponent
{
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;

    public float RunningSpeed => runningSpeed;
    public float Speed => speed;
    private MoveComponentModel moveComponentModel;

    public void Translate(Vector3 moveVector)
    {
        transform.Translate(moveVector);
    }

    public void Start()
    {
        moveComponentModel = new MoveComponentModel(new InputController(), new DeltaTimeGetter(), this);
    }

    public void Update()
    {
        moveComponentModel.Update();
    }
}