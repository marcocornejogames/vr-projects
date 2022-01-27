using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator _animator;

    [Header("Customization")]
    [SerializeField] private float _thumbInterpolationValue = 0.5f;
    [SerializeField] private float _indexInterpolationValue = 0.5f;
    [SerializeField] private float _gripInterpolationValue = 0.5f;

    //Unity Messages ______________________________
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //Custom Methods ______________________________
    public void SetGrip(float gripValue)
    {
        _animator.SetFloat("Grip", gripValue, _gripInterpolationValue, Time.deltaTime);
    }
    public void SetIndex(float indexValue)
    {
        _animator.SetFloat("Index", indexValue, _indexInterpolationValue, Time.deltaTime);
    }
    public void SetThumb(float thumbValue)
    {
        _animator.SetFloat("Thumb", thumbValue, _thumbInterpolationValue, Time.deltaTime);
    }
}
