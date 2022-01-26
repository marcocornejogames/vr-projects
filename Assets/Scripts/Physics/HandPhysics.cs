using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Rigidbody _rigidBody;
    public Transform FollowTarget;
    private Transform _originalFollowTarget;

    [Header("Customization")]
    [SerializeField] private float _followSpeed = 30f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _maxAngularVelocity = 20f;
    [SerializeField] private Vector3 _rotationOffset;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _timeScale = 5f;




    //Unity Messages ____________________________
    private void Awake()
    {
        _originalFollowTarget = FollowTarget;

        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.maxAngularVelocity = _maxAngularVelocity;

        _rigidBody.position = FollowTarget.position;
        _rigidBody.rotation = FollowTarget.rotation;

        

        
    }

    private void FixedUpdate()
    {
        MoveHands();
    }
    void Update()
    {
        
    }

    //Custom Methods ___________________________
    private void MoveHands()
    {
        //Position
        Vector3 targetPosition = FollowTarget.TransformPoint(_positionOffset);
        float distance = Vector3.Distance(targetPosition, transform.position);
        _rigidBody.velocity = (targetPosition - transform.position).normalized * (_followSpeed * distance) * (Time.deltaTime * _timeScale);

        //Rotation
        Quaternion targetRotation = FollowTarget.rotation * Quaternion.Euler(_rotationOffset);
        Quaternion rotationDifference = targetRotation * Quaternion.Inverse(_rigidBody.rotation);
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
        _rigidBody.angularVelocity = axis * (angle * Mathf.Deg2Rad * _rotationSpeed) * (Time.deltaTime * _timeScale);
    }

    public void ResetFollowTarget()
    {
        FollowTarget = _originalFollowTarget;
    }
}
