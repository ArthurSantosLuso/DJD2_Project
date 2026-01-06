using Unity.VisualScripting;
using UnityEngine;

public class BodyPiece : MonoBehaviour
{
    [SerializeField] private int pieceOrder;
    [SerializeField] private GameObject bodyPartToActivate;
    [SerializeField] private Material newTagMaterial;
    [SerializeField] private GameObject footTag;

    public int Order => pieceOrder;
    public GameObject BodyPart => bodyPartToActivate;

    

    public void ClearTag()
    {
        if (footTag != null)
        {
            footTag.GetComponent<MeshRenderer>().materials[0] = newTagMaterial;
        }
    }
}
