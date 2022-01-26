using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private InputActionReference _activateAction;
    [SerializeField] private InputActionReference _cancelAction;
    [SerializeField] private InputActionReference _thumbstickAction;
    [SerializeField] private XRRayInteractor _rayInteractor;
    [SerializeField] private TeleportationProvider _teleportationProvider;

    [Header("Feedback")]
    [SerializeField] private bool _isActive;

    //Unity Messages ________________________________________
    private void Awake()
    {
        //Enable all actions
        _activateAction.action.Enable();
        _activateAction.action.performed += OnTeleportActivate;

        _cancelAction.action.Enable();
        _cancelAction.action.performed += OnTeleportCancel;

        _thumbstickAction.action.Disable();

        //Find components
        _rayInteractor = GetComponent<XRRayInteractor>();
        _rayInteractor.enabled = false;

        if (_teleportationProvider == null) Debug.LogError($"MISSING CRITICAL REFERENCE: TeleportationManager on {gameObject.name} requires an external reference to TeleportationProvider.");
    }
    void Start()
    {

    }
    void Update()
    {
        TryTeleport();
    }

    //Custom Methods ________________________________________
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        ToggleTeleportation(true);
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        ToggleTeleportation(false);
    }

    private void ToggleTeleportation(bool isOn)
    {
        Debug.Log("Toggle Teleport = " + isOn);
        _rayInteractor.enabled = isOn;
        _isActive = isOn;
    }
    private void TryTeleport()
    {
        if (!_isActive || _thumbstickAction.action.triggered) return;
        if (!_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            ToggleTeleportation(false);
            return;
        }

        TeleportRequest request = new TeleportRequest();
        request.destinationPosition = hit.point;

        _teleportationProvider.QueueTeleportRequest(request);
        ToggleTeleportation(false);
    }
}
