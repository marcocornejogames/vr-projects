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


    //Unity Calls _______________________________________________________
    private void Awake()
    {
        _actionBasedController = GetComponent<ActionBasedController>();
        _handAnimation = GetComponentInChildren<HandAnimation>();
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
        Debug.Log($"{_thumbInputReference.action.ReadValue<float>()}, {gameObject.name}");
    }

}
