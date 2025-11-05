using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineInteractable : MonoBehaviour
{
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void ActivateOutline()
    {
        outline.enabled = true;
    }

    public void RemoveOutline()
    {
        outline.enabled = false;
    }
}
