using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public abstract void Interact();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}
