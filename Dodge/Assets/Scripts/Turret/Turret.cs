using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Pool;

public class Turret : MonoBehaviour
{
    enum Parts
    {
        Base,
        Barral,
        FirePosition
    }

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _firePosition;

    [SerializeField]
    private Bullet _bullet;

    [Range(5, 100)][SerializeField]
    private int _defaultCapacity = 10;

    private ObjectPool<Bullet> _bulletPool;

    private Vector3 _direction;

    const int FIREPOSITION = (int)Parts.FirePosition;
    

    private float _elapsedTime;
    private float _fireCoolTime;
    private float _minFireCoolTime = 2.0f;
    private float _maxFireCoolTime = 4.0f;
    private float _angleDiff;
    private float _angularSpeed = 5.0f;

    private void Awake()
    {
        _target = GameObject.Find("Player").transform;
        _firePosition = transform.GetChild(FIREPOSITION);
        _bulletPool = new ObjectPool<Bullet>(GenerateBullet, _defaultCapacity);
    }

    // Start is called before the first frame update
    void Start()
    {
        _fireCoolTime = Random.Range(_minFireCoolTime, _maxFireCoolTime);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();
        Shoot();
    }

    void RotateToTarget()
    {
        _direction = _target.position - transform.position;
        _direction.y = 0f;
        _angleDiff = Vector3.SignedAngle(transform.forward, _direction, Vector3.up);
        Quaternion end = transform.rotation * Quaternion.Euler(0f, _angleDiff, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, end, Time.deltaTime * _angularSpeed);
    }

    void Shoot()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _fireCoolTime)
        {
            _elapsedTime = 0f;
            Bullet bullet = GetBulletFromPool();
            bullet.transform.position = _firePosition.position;
            bullet.transform.rotation = _firePosition.rotation;
        }
    }

    Bullet GetBulletFromPool()
    {
        if (_bulletPool.CountInactive > 0)
        {
            Bullet bullet = _bulletPool.Get();
            bullet.gameObject.SetActive(true);
            bullet.Pool = _bulletPool;
            return bullet;
        }
        else
        {
            Bullet bullet = GenerateBullet();
            bullet.Pool = _bulletPool;
            return bullet;
        }
    }

    Bullet GenerateBullet()
    {
        return Instantiate(_bullet);
    }
}
