using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveManualRB : MonoBehaviour, IMove
{

    [Header("Speed Settings")]
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _maxTurnSpeed;


    [HideInInspector] public GameObject _moveEffect;
    [HideInInspector] public AudioClip _runSound;
    [HideInInspector] public AudioClip _walkSound;

    private Rigidbody _rb;

    public float MasterSpeed
    {
        get { return _accelerationSpeed; }
        set { _accelerationSpeed = value; }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void Move(Vector3 dir)
    {
        if (dir.magnitude > 1)
            dir = dir.normalized;

        _rb.AddForce(transform.forward * _accelerationSpeed * dir.magnitude);
    }
}
