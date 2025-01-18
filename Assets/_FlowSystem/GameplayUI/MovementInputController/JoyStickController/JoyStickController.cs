using UnityEngine;
using UnityEngine.Events;

public class JoyStickerController : InputControllerBase
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private UnityEvent<Vector2> onJoyStickerTriggered;


	public override void FireEvent()
	{
		print("Movement vector" + GetMovementVector().ToString());
		onJoyStickerTriggered?.Invoke(GetMovementVector());
	}

	public override Vector2 GetMovementVector()
	{
		return new Vector2(_joystick.Horizontal, _joystick.Vertical);
	}

	public void SetEnable(bool enable)
	{
		_joystick.enabled = enable;
	}

}