using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UIManager          _uiManager;
    [SerializeField] private InspectionSystem _inspectionSystem;
    [SerializeField] private List<Interactive> _startingItems;

    private PlayerInteractor    _playerInteractor;
    private List<GameObject>    _inventory;
    private int                 _selectedSlotIndex;

    void Start()
    {
        _playerInteractor  = GetComponent<PlayerInteractor>();
        _inventory          = new List<GameObject>();
        AddStartingItems(_startingItems);
        _selectedSlotIndex  = 0;
    }

    public void Add(Interactive item)
    {
        _inventory.Add(item.gameObject);

        _uiManager.ShowInventoryIcon(_inventory.Count - 1, item.GetComponent<Interactive>().inventoryIcon);

        if (_selectedSlotIndex == -1)
            SelectInventorySlot(0);
    }

    public void Remove(Interactive item)
    {
        _inventory.Remove(item.gameObject);

        _uiManager.HideInventoryIcons();

        for (int i = 0; i < _inventory.Count; ++i)
            _uiManager.ShowInventoryIcon(i, _inventory[i].GetComponent<Interactive>().inventoryIcon);

        if (_selectedSlotIndex == _inventory.Count)
            SelectInventorySlot(_selectedSlotIndex - 1);
    }

    public bool Contains(GameObject item)
    {
        return _inventory.Contains(item);
    }

    public bool IsFull()
    {
        return _inventory.Count == _uiManager.GetInventorySlotCount();
    }

    private void SelectInventorySlot(int index)
    {
        _selectedSlotIndex = index;

        _uiManager.SelectInventorySlot(index);

        _playerInteractor.RefreshCurrentInteractive();
    }

    public string GetSelectedInteractionMessage()
    {
        return _inventory[_selectedSlotIndex].GetComponent<Interactive>().GetInteractionMessage();
    }

    public bool IsSelected(Interactive item)
    {
        return GetSelected() == item;
    }

    public Interactive GetSelected()
    {
        return _selectedSlotIndex != -1 ? _inventory[_selectedSlotIndex].GetComponent<Interactive>() : null;
    }

    void Update()
    {
        CheckForPlayerSlotSelection();

        if (_selectedSlotIndex != -1
            && Input.GetButtonDown("Inspect"))
        {
            _inspectionSystem.InspectObject(GetSelected().gameObject, true);
        }
    }

    private void CheckForPlayerSlotSelection()
    {
        for (int i = 0; i < _inventory.Count; ++i)
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && i != _selectedSlotIndex)
                SelectInventorySlot(i);
    }

    private void AddStartingItems(List<Interactive> startingItems)
    {
        foreach (Interactive interactive in startingItems)
        {
            Add(interactive);
        }
    }
}
