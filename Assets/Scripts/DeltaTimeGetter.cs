using UnityEngine;

public class DeltaTimeGetter : IDeltaTimeGetter
{
    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }
}