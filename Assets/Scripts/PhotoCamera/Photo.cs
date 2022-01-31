using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo
{
    [Header("Feedback")]
    [SerializeField] private string _pngFileName;
    public Photo(string filePath)
    {
        _pngFileName = filePath;
    }
}
