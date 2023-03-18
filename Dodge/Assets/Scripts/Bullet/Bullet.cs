using Assets.Scripts.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Rigidbody _rigid;

    public ObjectPool<Bullet> Pool { private get; set; }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigid.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagLiteral.DEADZONE))
        {
            Pool.Release(this);
            gameObject.SetActive(false);
        }
    }
}
