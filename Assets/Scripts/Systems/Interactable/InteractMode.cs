using UnityEngine;

enum InteractMode
{
    // The interactable goes direct to inventory (it can also be inspected later). 
    // Ex: Bottle, Tag, Severed pieces
    Collect,
    // The interactable is inspected when interacted with.
    // Ex: Contracts, Clock
    Inspect,
    // The interactable is directly used when interacted with.
    // Ex: Door, Elevator, Button
    Use,
}
