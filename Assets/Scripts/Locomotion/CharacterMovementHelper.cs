using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XROrigin))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerDriver))]
public class CharacterMovementHelper : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private XROrigin _XROrigin;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterControllerDriver _characterControllerDriver;

    //UNITY MESSAGES _____________________________________________________________
    private void Awake()
    {
        _XROrigin = GetComponent<XROrigin>();
        _characterController = GetComponent<CharacterController>();
        _characterControllerDriver = GetComponent<CharacterControllerDriver>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        UpdateCharacterController();
    }


    //CUSTOM METHODS ______________________________________________________________

    /// <summary>
    /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCharacterController() //Taken from CharacterControllerDriver
    {
        if (_XROrigin == null || _characterController == null)
            return;

        var height = Mathf.Clamp(_XROrigin.CameraInOriginSpaceHeight, _characterControllerDriver.minHeight, _characterControllerDriver.maxHeight);

        Vector3 center = _XROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + _characterController.skinWidth;

        _characterController.height = height;
        _characterController.center = center;
    }
}
