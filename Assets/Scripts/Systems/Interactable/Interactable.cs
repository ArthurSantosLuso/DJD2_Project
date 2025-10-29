using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private InteractMode interactMode;

    public void OnFocus()
    {
        return;
    }

    public void OnLoseFocus()
    {
        return;
    }

    public void Interact()
    {
        // Implement
        switch (interactMode)
        {
            case InteractMode.Collect:
                Debug.Log("Object was collected");
                break;

            case InteractMode.Inspect:
                Debug.Log("Object was Inspected");
                break;

            case InteractMode.Use:
                Debug.Log("Object was used");
                break;
        }
    }
}
