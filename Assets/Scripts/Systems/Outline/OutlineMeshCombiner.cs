using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OutlineMeshCombiner : MonoBehaviour
{

    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;

    private GameObject outlineObject;
    private Mesh combinedMesh;

    private void Awake()
    {
        CreateOutlineMesh();
        

        outlineMaskMaterial = Instantiate(Resources.Load<Material>("Materials/Outline/OutlineMask"));
        outlineFillMaterial = Instantiate(Resources.Load<Material>("Materials/Outline/OutlineFill"));
        enabled = false;
    }

    private void OnEnable()
    {
        if (outlineObject != null)
            outlineObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (outlineObject != null)
            outlineObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (combinedMesh != null)
            Destroy(combinedMesh);

        if (outlineObject != null)
            Destroy(outlineObject);
    }

    private void CreateOutlineMesh()
    {
        var meshFilters = GetComponentsInChildren<MeshFilter>();

        var combines = new List<CombineInstance>();

        foreach (var mf in meshFilters)
        {
            if (mf.sharedMesh == null)
                continue;

            // ignora o próprio outline
            if (mf.transform == transform)
                continue;

            combines.Add(new CombineInstance
            {
                mesh = mf.sharedMesh,
                transform = transform.worldToLocalMatrix * mf.transform.localToWorldMatrix
            });
        }

        if (combines.Count == 0)
            return;

        combinedMesh = new Mesh
        {
            name = $"{gameObject.name}_OutlineCombinedMesh"
        };

        combinedMesh.CombineMeshes(combines.ToArray(), true, true);

        outlineObject = new GameObject("OutlineMesh");
        outlineObject.transform.SetParent(transform, false);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one;

        var mfOutline = outlineObject.AddComponent<MeshFilter>();
        mfOutline.sharedMesh = combinedMesh;

        var mrOutline = outlineObject.AddComponent<MeshRenderer>();
        mrOutline.sharedMaterials = new[]
        {
            outlineMaskMaterial,
            outlineFillMaterial
        };

        mrOutline.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mrOutline.receiveShadows = false;
        mrOutline.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        mrOutline.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

        outlineObject.layer = gameObject.layer;
    }
}
