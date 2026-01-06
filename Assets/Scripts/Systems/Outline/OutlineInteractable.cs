using UnityEngine;

[RequireComponent(typeof(OutlineMeshCombiner))]
public class OutlineInteractable : MonoBehaviour
{
    private OutlineMeshCombiner outline;

    private void Awake()
    {
        outline = GetComponent<OutlineMeshCombiner>();
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
