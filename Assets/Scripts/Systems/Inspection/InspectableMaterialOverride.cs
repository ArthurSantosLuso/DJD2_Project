using UnityEngine;

public class InspectableMaterialOverride : MonoBehaviour
{
    [SerializeField] private Material materialOverride;

    public Material InspectionMaterial => materialOverride;
}
