using Unity.VisualScripting;
using UnityEngine;

public class BodyPiece : MonoBehaviour
{
    [SerializeField] private int pieceOrder;
    [SerializeField] private GameObject bodyPartToActivate;

    public int Order => pieceOrder;
    public GameObject BodyPart => bodyPartToActivate;

   
}
