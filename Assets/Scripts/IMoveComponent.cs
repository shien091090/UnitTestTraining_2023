using UnityEngine;

public interface IMoveComponent
{
    float RunningSpeed { get; }
    float Speed { get; }
    void Translate(Vector3 moveVector);
}