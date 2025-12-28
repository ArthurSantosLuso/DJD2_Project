using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutlineInteractable))]
public class Interactive : InteractableBase
{
    [SerializeField] private InteractiveData    _interactiveData;
    [SerializeField] private AudioClip          interactionSound;
    [SerializeField] private Transform          _focusPoint; // <-- Change this later. Not every interactive should have a focus point.

    private InteractionManager  _interactionManager;
    private PlayerInventory     _playerInventory;
    private OutlineInteractable interactable;
    private List<Interactive>   _requirements;
    private List<Interactive>   _dependents;
    private Animator            _animator;
    private bool                _requirementsMet;
    private int                 _interactionCount;

    public bool isOn;
    public InteractiveData interactiveData => _interactiveData;
    public string inventoryName => _interactiveData.inventoryName;
    public Sprite inventoryIcon => _interactiveData.inventoryIcon;

    void Awake()
    {
        _interactionManager = InteractionManager.instance;
        _playerInventory = _interactionManager.playerInventory;
        _requirements = new List<Interactive>();
        _dependents = new List<Interactive>();
        _animator = GetComponent<Animator>();
        _requirementsMet = _interactiveData.requirements.Length == 0;
        _interactionCount = 0;
        isOn = _interactiveData.startsOn;
        interactable = GetComponent<OutlineInteractable>();

        _interactionManager.RegisterInteractive(this);
    }

    public void AddRequirement(Interactive requirement)
    {
        _requirements.Add(requirement);
    }

    public void AddDependent(Interactive dependent)
    {
        _dependents.Add(dependent);
    }

    private bool IsType(InteractiveData.Type type)
    {
        return _interactiveData.type == type;
    }

    public string GetInteractionMessage()
    {
        if (IsType(InteractiveData.Type.Pickable) && !_playerInventory.Contains(gameObject) && _requirementsMet)
            return _interactionManager.GetPickMessage(_interactiveData.inventoryName);
        else if (!_requirementsMet)
        {
            if (PlayerHasRequirementSelected())
                return _playerInventory.GetSelectedInteractionMessage();
            else
                return _interactiveData.requirementsMessage;
        }
        else if (interactiveData.interactionMessages.Length > 0)
            return _interactionManager.GetInteractionMessage(interactiveData.interactionMessages[_interactionCount % _interactiveData.interactionMessages.Length]);
        else
            return null;
    }

    private bool PlayerHasRequirementSelected()
    {
        foreach (Interactive requirement in _requirements)
            if (_playerInventory.IsSelected(requirement))
                return true;

        return false;
    }

    public void Interact()
    {
        if (_requirementsMet)
            InteractSelf(true);
        else if (PlayerHasRequirementSelected())
            UseRequirementFromInventory();
    }

    private void InteractSelf(bool direct)
    {
        if (direct && IsType(InteractiveData.Type.Indirect))
            return;
        else if (IsType(InteractiveData.Type.Pickable) && !_playerInventory.IsFull())
            PickUpInteractive();
        else if (IsType(InteractiveData.Type.InteractOnce) || IsType(InteractiveData.Type.InteractMulti))
            DoDirectInteraction();
        else if (IsType(InteractiveData.Type.Indirect))
            PlayAnimation(_interactionManager.interactAnimationName);
        else if (IsType(InteractiveData.Type.PickInspect))
            TiggerInspection();
        else if (IsType(InteractiveData.Type.Focusable))
            TriggerCameraFocus();
    }

    private void PickUpInteractive()
    {
        _playerInventory.Add(gameObject.GetComponent<Interactive>());
        PlayInteractionAudio();
        gameObject.SetActive(false);
    }

    private void DoDirectInteraction()
    {

        if (TryGetComponent(out DrawerController drawer))
        {
            if (drawer.IsLocked)
            {
                drawer.TryPlayLockedSound();
                return;
            }
        }

        ++_interactionCount;

        if (IsType(InteractiveData.Type.InteractOnce))
            isOn = false;

        CheckDependentsRequirements();
        DoIndirectInteractions();

        PlayAnimation(_interactionManager.interactAnimationName);
    }

    private void CheckDependentsRequirements()
    {
        foreach (Interactive dependent in _dependents)
            dependent.CheckRequirements();
    }

    private void CheckRequirements()
    {
        foreach (Interactive requirement in _requirements)
        {
            if (!requirement._requirementsMet ||
               (!requirement.IsType(InteractiveData.Type.Indirect) && requirement._interactionCount == 0))
            {
                _requirementsMet = false;
                return;
            }
        }

        _requirementsMet = true;
        PlayAnimation(_interactionManager.awakeAnimationName);

        CheckDependentsRequirements();
    }

    private void DoIndirectInteractions()
    {
        foreach (Interactive dependent in _dependents)
            if (dependent.IsType(InteractiveData.Type.Indirect) && dependent._requirementsMet)
                dependent.InteractSelf(false);
    }

    private void PlayAnimation(string animation)
    {
        if (_animator != null)
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(animation);
        }
    }

    private void UseRequirementFromInventory()
    {
        GameObject requirement = _playerInventory.GetSelected().gameObject;

        BodyPiece piece = requirement.GetComponent<BodyPiece>();
        if (piece != null)
        {
            BodyController body = GetComponent<BodyController>();
            body.PlacePiece(piece);

            requirement.SetActive(false);
            return;
        }

        _playerInventory.Remove(requirement.GetComponent<Interactive>());

        ++requirement.GetComponent<Interactive>()._interactionCount;

        requirement.GetComponent<Interactive>().PlayAnimation(_interactionManager.interactAnimationName);

        CheckRequirements();
    }

    private void TiggerInspection()
    {
        InteractionManager.instance.inspectionSystem.InspectObject(gameObject, false);
        PlayInteractionAudio();
    }

    private void TriggerCameraFocus()
    {
        if(_focusPoint != null)
        {
            if (!InteractionManager.instance.cameraFocusController.IsFocusing)
            {
                InteractionManager.instance.cameraFocusController.EnterFocus(_focusPoint);
                PlayInteractionAudio();
            }
            else 
            {
                InteractionManager.instance.cameraFocusController.ExitFocus();
            }

            PlayAnimation(_interactionManager.interactAnimationName);
        }
    }

    private void PlayInteractionAudio()
    {
        if (interactionSound != null)
            AudioManager.Instance.PlaySound(interactionSound);
    }

    public void ApplyFocus()
    {
        OnFocus(interactable);
    }

    public void LoseFocus()
    {
        OnLoseFocus(interactable);
    }
}
