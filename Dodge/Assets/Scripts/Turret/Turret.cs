using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector3 _direction;

    private float _angleDiff;
    private float _angularSpeed = 5.0f;

    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();
    }

    void RotateToTarget()
    {
        _direction = _target.position - transform.position;
        _direction.y = 0f;
        _angleDiff = Vector3.SignedAngle(transform.forward, _direction, Vector3.up);
        Quaternion end = transform.rotation * Quaternion.Euler(0f, _angleDiff, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, end, Time.deltaTime * _angularSpeed);
    }
}
