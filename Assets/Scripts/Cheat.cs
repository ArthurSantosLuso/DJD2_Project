using UnityEngine;
using System.Collections.Generic;

public class Cheat : MonoBehaviour
{
    public PlayerInventory inventory;
    public List<Interactive> bodyPieces;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (var piece in bodyPieces)
            {
                inventory.Add(piece);
            }
        }
    }

}
