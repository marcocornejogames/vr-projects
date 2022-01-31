using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private InputActionReference _thumbInputReference;
    [SerializeField] private ActionBasedController _actionBasedController;
    [SerializeField] private HandAnimation _handAnimation;
    [SerializeField] private Grabbing _grabbing;


    //Unity Calls _______________________________________________________
    private void Awake()
    {
        _actionBasedController = GetComponent<ActionBasedController>();
        _handAnimation = GetComponentInChildren<HandAnimation>();
        _grabbing = GetComponentInChildren<Grabbing>();


        _actionBasedController.selectAction.action.started += TryGrab;
        _actionBasedController.selectAction.action.canceled += ReleaseGrab;

        _actionBasedController.activateAction.action.performed += TryInteract;
    }



    private void Update()
    {
        UpdateAnimations();
    }

    //Custom Methods
    private void UpdateAnimations()
    {
        _handAnimation.SetGrip(_actionBasedController.selectAction.action.ReadValue<float>());
        _handAnimation.SetIndex(_actionBasedController.activateAction.action.ReadValue<float>());
        _handAnimation.SetThumb(_thumbInputReference.action.ReadValue<float>()); //TODO: Find a way to read Thumb value
    }


    //___________________________________ GRABBING & RELEASING
    private void TryGrab(InputAction.CallbackContext context)
    {
        _grabbing.Grab();
    }

    private void ReleaseGrab(InputAction.CallbackContext context)
    {
        _grabbing.Release();
    }

    //______________________________________ INTERACTING
    private void TryInteract(InputAction.CallbackContext context)
    {
        if (_grabbing.GetHeldObject() == null) return;
        _grabbing.GetHeldObject().TryGetComponent<IInteractable>(out IInteractable interactable);

        if (interactable == null)
        {
            VRDebugTools.Instance.DisplayMessage("Tried to interact but no interactable was found");
            return;
        }

        interactable.Interact();
    }
}
