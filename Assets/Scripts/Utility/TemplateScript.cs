using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Marco Cornejo, 2022
public class TemplateScript : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private MonoBehaviour _component;

    [Header("Customization")]
    [SerializeField] private float _float;

    [Header("Feedback")]
    [SerializeField] private bool _bool;

    //UNITY MESSAGES _______________________________________________________
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //CUSTOM METHODS _______________________________________________________
    private void CustomMethod()
    {

    }
}
