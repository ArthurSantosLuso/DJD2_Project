using UnityEngine;

public class FootController : MonoBehaviour
{
    [SerializeField] private Interactive objectClean;
    [SerializeField] private Interactive sink;

    private void ClearObject()
    {
        InteractionManager.instance.playerInventory.Add(objectClean);
        sink.isOn = false;
    }
}
