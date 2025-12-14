using UnityEngine;

public class InspectableMaterialOverride : MonoBehaviour
{
    [SerializeField] private ContractData contractData;

    public bool HasOverride => contractData != null;
    public Material InspectionMaterial => contractData.inspectionMaterial;
}
