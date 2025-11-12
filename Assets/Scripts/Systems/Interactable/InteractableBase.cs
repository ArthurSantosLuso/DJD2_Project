using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    protected void OnFocus(OutlineInteractable interactable)
    {
        interactable.ActivateOutline();
    }

    protected void OnLoseFocus(OutlineInteractable interactable)
    {
        interactable.RemoveOutline();
    }
}
