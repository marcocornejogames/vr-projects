using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(TeleportationProvider))]
[RequireComponent(typeof(ActionBasedContinuousMoveProvider))]
[RequireComponent(typeof(ActionBasedSnapTurnProvider))]
[RequireComponent(typeof(ActionBasedContinuousTurnProvider))]
public class LocomotionMethodToggle : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private InputActionReference _movementToggleInput;
    [SerializeField] private InputActionReference _turnToggleInput;
    [SerializeField] private TeleportationProvider _teleportation;
    [SerializeField] private ActionBasedContinuousMoveProvider _continuousMove;
    [SerializeField] private ActionBasedSnapTurnProvider _snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider _continuousTurn;

    [Header("Customization")]
    [SerializeField] private bool _setTeleportAsDefault = true;
    [SerializeField] private bool _setSnapTurnAsDefault = true;

    [Header("Debug")]
    [SerializeField] private bool _playSoundOnToggle = true;

    //UNITY MESSAGES __________________________________________________
    private void Awake()
    {
        _teleportation = GetComponent<TeleportationProvider>();
        _continuousMove = GetComponent<ActionBasedContinuousMoveProvider>();
        _snapTurn = GetComponent<ActionBasedSnapTurnProvider>();
        _continuousTurn = GetComponent<ActionBasedContinuousTurnProvider>();

        _teleportation.enabled = _setTeleportAsDefault;
        _continuousMove.enabled = !_setTeleportAsDefault;

        _snapTurn.enabled = _setSnapTurnAsDefault;
        _continuousTurn.enabled = !_setSnapTurnAsDefault;


        _movementToggleInput.action.started += ToggleMoveMode;
        _turnToggleInput.action.started += ToggleTurnMode;


    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    //CUSTOM METHODS __________________________________________________
    private void ToggleMoveMode(InputAction.CallbackContext context)
    {
        _teleportation.enabled = !_teleportation.isActiveAndEnabled;
        _continuousMove.enabled = !_continuousMove.isActiveAndEnabled;

        if(_playSoundOnToggle) DebugTools.Instance.PlaySound(_teleportation.isActiveAndEnabled ? DebugTools.SoundBitType.High : DebugTools.SoundBitType.Low);
    }
    private void ToggleTurnMode(InputAction.CallbackContext context)
    {
        _snapTurn.enabled = !_snapTurn.isActiveAndEnabled;
        _continuousTurn.enabled = !_continuousTurn.isActiveAndEnabled;

        if (_playSoundOnToggle) DebugTools.Instance.PlaySound(_snapTurn.isActiveAndEnabled ? DebugTools.SoundBitType.High : DebugTools.SoundBitType.Low);
    }


}
