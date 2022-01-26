using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovementHelper : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private XROrigin _XROrigin;
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private CharacterControllerDriver _CharacterControllerDriver;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();
    }

    /// <summary>
    /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCharacterController()
    {
        if (_XROrigin == null || _CharacterController == null)
            return;

        var height = Mathf.Clamp(_XROrigin.CameraInOriginSpaceHeight, _CharacterControllerDriver.minHeight, _CharacterControllerDriver.maxHeight);

        Vector3 center = _XROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + _CharacterController.skinWidth;

        _CharacterController.height = height;
        _CharacterController.center = center;
    }
}
