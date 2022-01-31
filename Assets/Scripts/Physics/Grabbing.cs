using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform _palmTransform;
    [SerializeField] private HandPhysics _handPhysics;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Customization")]
    [SerializeField] private LayerMask _grabbableLayer;
    [SerializeField] private float _reachDistance = 0.1f;
    [SerializeField] private float _joinDistance = 0.075f;

    [Header("Feedback")]
    [SerializeField] private bool _isGrabbing;
    [SerializeField] private Transform _grabPoint;
    [SerializeField] private GameObject _heldObject;
    [SerializeField] private FixedJoint _joint1;
    [SerializeField] private FixedJoint _joint2;

    // UNITY MESSAGES ________________________________
    private void Awake()
    {
        _handPhysics = GetComponent<HandPhysics>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    void Update()
    {
    }

    //CUSTOM METHODS ____________________________________
    public void Grab()
    {
        if (_isGrabbing) return;

        Collider[] grabbableColliders = Physics.OverlapSphere(_palmTransform.position, _reachDistance, _grabbableLayer);
        if (grabbableColliders.Length < 1)
        {
            return;
        }

        if (!grabbableColliders[0].TryGetComponent<Rigidbody>(out Rigidbody objectRigidbody)) return;

        _heldObject = grabbableColliders[0].transform.gameObject;
        StartCoroutine(GrabObject(grabbableColliders[0], objectRigidbody));
    }

    public void Release()
    {
        if (!_isGrabbing) return;

        if (_joint1 != null) Destroy(_joint1);
        if (_joint2 != null) Destroy(_joint2);
        if (_grabPoint != null) Destroy(_grabPoint.gameObject);

        if(_heldObject != null)
        {
            Rigidbody objectRigidbody = _heldObject.GetComponent<Rigidbody>();
            objectRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            objectRigidbody.interpolation = RigidbodyInterpolation.None;

            _heldObject = null;
        }

        _handPhysics.ResetFollowTarget();
        _isGrabbing = false;
    }

    //______________________________________Coroutines

    private IEnumerator GrabObject(Collider collider, Rigidbody objectRigidbody)
    {
        _isGrabbing = true;

        Transform originalFollowTarget = _handPhysics.FollowTarget;

        //Create grab point
        _grabPoint = new GameObject().transform;
        _grabPoint.position = collider.ClosestPoint(_palmTransform.position);
        _grabPoint.parent = _heldObject.transform;

        //Move hand to grab point
        _handPhysics.FollowTarget = _grabPoint;

        //Wait for hand to reach grab point
        while(_grabPoint != null && Vector3.Distance( _grabPoint.position, _palmTransform.position) > _joinDistance && _isGrabbing)
        {
            yield return new WaitForEndOfFrame();
        }

        //Freeze hand and object motion
        objectRigidbody.velocity = Vector3.zero;
        objectRigidbody.angularVelocity = Vector3.zero;

        objectRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        objectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        //Attach joints
        _joint1 = gameObject.AddComponent<FixedJoint>();
        _joint1.connectedBody = objectRigidbody;
        _joint1.breakForce = float.PositiveInfinity;
        _joint1.breakTorque = float.PositiveInfinity;

        _joint1.connectedMassScale = 1;
        _joint1.massScale = 1;
        _joint1.enableCollision = false;
        _joint1.enablePreprocessing = false;

        _joint2 = _heldObject.AddComponent<FixedJoint>();
        _joint2.connectedBody = _rigidbody;
        _joint2.breakForce = float.PositiveInfinity;
        _joint2.breakTorque = float.PositiveInfinity;
              
        _joint2.connectedMassScale = 1;
        _joint2.massScale = 1;
        _joint2.enableCollision = false;
        _joint2.enablePreprocessing = false;


        //Reset follow target
        _handPhysics.FollowTarget = originalFollowTarget;
    }

    //Interaction References ______________________________________

    public GameObject GetHeldObject()
    {
        return _heldObject;
    }
}
