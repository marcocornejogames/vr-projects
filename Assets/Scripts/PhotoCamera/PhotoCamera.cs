using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour, IInteractable
{
    [Header("Component References")]
    [SerializeField] private PhotoCameraLens _lens;
    [SerializeField] private SDCard _sdCard;

    private void Awake()
    {
        _lens = GetComponentInChildren<PhotoCameraLens>();
    }
    public void Interact()
    {
        VRDebugTools.Instance.PlaySound(VRDebugTools.SoundBitType.Low);
        _sdCard.AddPhoto(_lens.TakePicture());
    }
}
