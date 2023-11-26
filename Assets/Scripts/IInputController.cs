public interface IInputController
{
    bool IsRunningKeyUp();
    bool IsRunningKeyDown();
    float GetVerticalAxis();
    float GetHorizontalAxis();
}