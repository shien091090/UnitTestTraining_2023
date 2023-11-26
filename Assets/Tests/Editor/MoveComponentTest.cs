using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class MoveComponentTest
{
    private MoveComponentModel moveComponentModel;
    private IInputController inputController;
    private IDeltaTimeGetter deltaTimeGetter;
    private IMoveComponent moveComponent;

    [SetUp]
    public void Setup()
    {
        inputController = Substitute.For<IInputController>();
        deltaTimeGetter = Substitute.For<IDeltaTimeGetter>();
        moveComponent = Substitute.For<IMoveComponent>();

        GivenDeltaTime(1);
        GivenSpeed(1);

        moveComponentModel = new MoveComponentModel(inputController, deltaTimeGetter, moveComponent);
    }

    [Test]
    [TestCase(1, 3)]
    [TestCase(-0.5f, -1.5f)]
    //按左右時角色平行移動
    public void character_move_horizontally_when_left_or_right_key_pressed(float horizontalAxis, float expectedMoveHorizontalValue)
    {
        GivenSpeed(3);
        GivenHorizontalAxis(horizontalAxis);

        moveComponentModel.Update();

        ShouldCallTranslate(new Vector3(expectedMoveHorizontalValue, 0, 0));
    }

    [Test]
    //沒有按任何按鍵時, 角色不移動
    public void character_not_move_when_no_key_pressed()
    {
        GivenHorizontalAxis(0);

        moveComponentModel.Update();

        ShouldNotCallTranslate();
    }

    [Test]
    [TestCase(0.8f, 1.6f)]
    [TestCase(-0.4f, -0.8f)]
    //按上下時角色垂直移動
    public void character_move_vertically_when_up_or_down_key_pressed(float verticalAxis, float expectedMoveVerticalValue)
    {
        GivenVerticalAxis(verticalAxis);
        GivenSpeed(2);

        moveComponentModel.Update();

        ShouldCallTranslate(new Vector3(0, expectedMoveVerticalValue, 0));
    }

    [Test]
    [TestCase(0.5f, -0.8f, 3, -4.8f)]
    [TestCase(1, 0.7f, 6, 4.2f)]
    [TestCase(0.4f, -0.1f, 2.4f, -0.6f)]
    [TestCase(-0.4f, 0.7f, -2.4f, 4.2f)]
    //同時按平行和垂直時角色斜向移動
    public void character_move_diagonally_when_horizontal_and_vertical_key_pressed(float horizontalAxis, float verticalAxis, float expectedMoveHorizontalValue,
        float expectedMoveVerticalValue)
    {
        GivenHorizontalAxis(horizontalAxis);
        GivenVerticalAxis(verticalAxis);
        GivenSpeed(6);

        moveComponentModel.Update();

        ShouldCallTranslate(new Vector3(expectedMoveHorizontalValue, expectedMoveVerticalValue, 0));
    }

    [Test]
    //僅按下跑步鍵時, 角色不移動
    public void character_not_move_when_only_running_key_pressed()
    {
        GivenRunningKeyDown();

        moveComponentModel.Update();

        ShouldNotCallTranslate();
    }

    [Test]
    //按下跑步鍵時, 角色移動速度加快
    public void character_move_faster_when_running_key_pressed()
    {
        GivenRunningKeyDown();
        GivenSpeed(2);
        GivenRunningSpeed(10);
        GivenHorizontalAxis(1);
        GivenVerticalAxis(-0.5f);

        moveComponentModel.Update();

        ShouldCallTranslate(new Vector3(10, -5, 0));
    }

    [Test]
    //按下跑步鍵再放開, 角色移動恢復到原速度
    public void character_move_back_to_normal_speed_when_running_key_released()
    {
        GivenSpeed(2);
        GivenRunningSpeed(10);
        GivenHorizontalAxis(-0.8f);
        GivenVerticalAxis(0.6f);

        GivenRunningKeyDown();
        moveComponentModel.Update();

        GivenRunningKeyUp();
        moveComponentModel.Update();

        ShouldCallTranslate(new Vector3(-1.6f, 1.2f, 0));
    }

    private void GivenRunningKeyUp()
    {
        inputController.IsRunningKeyUp().Returns(true);
        inputController.IsRunningKeyDown().Returns(false);
    }

    private void GivenRunningSpeed(float speed)
    {
        moveComponent.RunningSpeed.Returns(speed);
    }

    private void GivenRunningKeyDown()
    {
        inputController.IsRunningKeyDown().Returns(true);
        inputController.IsRunningKeyUp().Returns(false);
    }

    private void GivenVerticalAxis(float verticalAxis)
    {
        inputController.GetVerticalAxis().Returns(verticalAxis);
    }

    private void GivenHorizontalAxis(float horizontalAxis)
    {
        inputController.GetHorizontalAxis().Returns(horizontalAxis);
    }

    private void GivenSpeed(float speed)
    {
        moveComponent.Speed.Returns(speed);
    }

    private void GivenDeltaTime(int deltaTime)
    {
        deltaTimeGetter.GetDeltaTime().Returns(deltaTime);
    }

    private void ShouldNotCallTranslate()
    {
        moveComponent.DidNotReceive().Translate(Arg.Any<Vector3>());
    }

    private void ShouldCallTranslate(Vector3 moveVector, int callTimes = 1)
    {
        moveComponent.Received(callTimes).Translate(moveVector);
    }
}