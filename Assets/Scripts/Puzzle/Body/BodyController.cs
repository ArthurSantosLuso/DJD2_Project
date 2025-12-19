using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private int totalPieces;
    [SerializeField] private PlayerInventory playerInventory;

    private List<BodyPiece> placedPieces = new List<BodyPiece>();

    public void PlacePiece(BodyPiece piece)
    {
            placedPieces.Add(piece);
        piece.BodyPart.SetActive(true);

        if (placedPieces.Count == totalPieces)
            ValidadePuzzle();
    }

    private void ValidadePuzzle()
    {
        bool correct = true;

        for (int i = 0; i < placedPieces.Count; i++)
        {
            if (placedPieces[i].Order != i + 1)
            {
                correct = false;
                break;
            }
        }

        if (correct)
            CompletePuzzle();
        else ResetPuzzle();
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle Completed!");
    }

    private void ResetPuzzle()
    {
        foreach (BodyPiece piece in placedPieces)
            piece.BodyPart.SetActive(false);

        ReturnPiecesToInventory(playerInventory);

        placedPieces.Clear();
    }

    private void ReturnPiecesToInventory(PlayerInventory inventory)
    {
        foreach (BodyPiece piece in placedPieces)
        {
            GameObject obj = piece.gameObject;
            obj.SetActive(true);
            inventory.Add(obj.GetComponent<Interactive>());
        }
    }

    public List<BodyPiece> GetPlacedPieces() => placedPieces;


}
