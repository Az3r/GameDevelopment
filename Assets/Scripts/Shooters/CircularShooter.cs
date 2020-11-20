using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Control movement of bullets in chunk
public class CircularShooter : MonoBehaviour
{
    public GameObject prefab;
    public int total;
    public int reload;
    public int chunk;
    public float rotate;
    public float modifier;

    private int _fireIn;
    private float _firingAngle;

    private float _angleOffset;
    private int _remain;
    // Start is called before the first frame update
    void Start()
    {
        _angleOffset = 360f / chunk;
        _remain = total;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireIn <= 0 && _remain > 0)
        {
            int i = 0;
            foreach (var bullet in BulletPool.Instance.Request(chunk))
            {
                LinearBullet behavior = bullet.GetComponent<LinearBullet>();
                behavior.angle = _firingAngle + _angleOffset * i + Random.Range(-modifier, modifier);
                behavior.speed = 32f;
                behavior.OnSpawn(gameObject);

                ++i;
            }

            _firingAngle += rotate;
            _fireIn = reload;
            _remain -= chunk;
        }
        else --_fireIn;
    }
}
