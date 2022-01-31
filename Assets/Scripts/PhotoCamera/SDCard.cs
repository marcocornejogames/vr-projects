using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/SD Card")]
public class SDCard : ScriptableObject
{
    [SerializeField] private List<Photo> _storedPhotos;

    private void Awake()
    {
        _storedPhotos = new List<Photo>();
    }
    public void AddPhoto(Photo photo)
    {
        _storedPhotos.Add(photo);
    }
}
