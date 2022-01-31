using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PhotoCameraLens : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Camera _photoCamera;

    [Header("Feedback")]
    [SerializeField] private int _resWidth = 1080;
    [SerializeField] private int _resHeight = 1080;

    private void Awake()
    {
        _photoCamera = GetComponent<Camera>();
        if(_photoCamera.targetTexture == null)
        {
            Debug.Log("Critical Reference Missing: The camera attached to Photo Camera Lens is missing a target textuxture (Render Texture)");
            VRDebugTools.Instance.DisplayMessage("Camera missing Render Texture");
        }
        else
        {
            _resHeight = _photoCamera.targetTexture.height;
            _resWidth = _photoCamera.targetTexture.width;
        }
    }

    public Photo TakePicture()
    {

        Texture2D image = new Texture2D(_resWidth, _resHeight, TextureFormat.RGB24, false);
        _photoCamera.Render();
        RenderTexture.active = _photoCamera.targetTexture;
        image.ReadPixels(new Rect(0, 0, _resWidth, _resHeight), 0, 0);

        byte[] bytes = image.EncodeToPNG();
        string fileName = GenerateFileName();
        System.IO.File.WriteAllBytes(fileName, bytes);

        Debug.Log("Snapshot taken!");
        VRDebugTools.Instance.DisplayMessage("Snapshot taken!");

        return new Photo(fileName);
    }

    private string GenerateFileName()
    {
        return string.Format("{0}/Photos/photo_{1}x{2}_{3}.png", 
            Application.dataPath, 
            _resWidth, 
            _resHeight, 
            System.DateTime.Now.ToString("yyy-mm-dd_HH-mm-ss"));
    }
}
