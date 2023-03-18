using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _input;
    private Rigidbody _rigid;

    [SerializeField]
    private float _speed = 5.0f;

    private Vector3 _velocity;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = new Vector3(_input.Horizontal, 0f, _input.Vertical);
        _rigid.velocity = _velocity * _speed;
    }
}
