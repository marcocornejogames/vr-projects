using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Marco Cornejo, 2022
[RequireComponent(typeof(AudioSource))]
public class DebugTools : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private AudioSource _audioSource;
    public static DebugTools Instance;

    [Header("Customization")]
    [SerializeField] private AudioClip _lowBeep;
    [SerializeField] private AudioClip _highBeep;
    public enum SoundBitType { Low, High}



    [Header("Feedback")]
    [SerializeField] private bool _bool;

    //UNITY MESSAGES _______________________________________________________
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //CUSTOM METHODS _______________________________________________________
    public void PlaySound(SoundBitType soundBitType)
    {
        AudioClip soundToPlay = null;

        switch(soundBitType)
        {
            case SoundBitType.High:
                soundToPlay = _highBeep;
                break;
            case SoundBitType.Low:
                soundToPlay = _lowBeep;
                break;
        }

        _audioSource.clip = soundToPlay;
        _audioSource.Play();
    }
}
