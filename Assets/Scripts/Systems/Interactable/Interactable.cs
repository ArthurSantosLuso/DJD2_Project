using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(OutlineInteractable))]
public class Interactable : InteractableBase
{
    [SerializeField]
    private InteractMode interactMode;

    private OutlineInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<OutlineInteractable>();
    }

    public override void OnFocus()
    {
        interactable.ActivateOutline();
    }

    public override void OnLoseFocus()
    {
        interactable.RemoveOutline();
    }

    public override void Interact()
    {
        // Implement
        switch (interactMode)
        {
            case InteractMode.Collect:
                Debug.Log("Object was collected");
                break;

            case InteractMode.Inspect:
                Debug.Log("Object was inspected");
                break;

            case InteractMode.Use:
                Debug.Log("Object was used");
                break;
        }
    }
}
