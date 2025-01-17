using UnityEngine;

public abstract class InputControllerBase : MonoBehaviour
{
	public abstract Vector2 GetMovementVector();
	public abstract void FireEvent();
}