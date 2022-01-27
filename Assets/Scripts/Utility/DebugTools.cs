using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Marco Cornejo, 2022
[RequireComponent(typeof(AudioSource))]
public class DebugTools : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _debugMessage;
    public static DebugTools Instance;

    [Header("Customization")]
    [SerializeField] private int _maxDebugMessages = 3;
    [SerializeField] private AudioClip _lowBeep;
    [SerializeField] private AudioClip _highBeep;

    [Header("Feedback")]
    [SerializeField] private List<string> _currentDebugMessages;
    public enum SoundBitType { Low, High}

    //UNITY MESSAGES _______________________________________________________
    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _currentDebugMessages = new List<string>();
        
    }
    void Start()
    {
        if (_debugMessage == null) Debug.Log("Missing Critical Reference: Debug tools missing a reference to display text");
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

    public void DisplayMessage(string message)
    {
        if (_debugMessage == null) return;
        _currentDebugMessages.Add(message);
        if (_currentDebugMessages.Count > _maxDebugMessages) _currentDebugMessages.RemoveAt(0);

        string newDisplayMessage = "";
        foreach (string debugMessage in _currentDebugMessages)
        {
            newDisplayMessage = newDisplayMessage + debugMessage + "\n";
        }

        _debugMessage.text = newDisplayMessage;
    }
}
