using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LinearBullet : MonoBehaviour, IPoolObject
{
    /// speed per second
    public float speed;
    /// angle in radian
    public float angle;

    private Renderer _renderer;

    public void OnSpawn(GameObject spawner)
    {
        transform.position = spawner.transform.position;
        transform.eulerAngles = new Vector3(0f, 0f, angle + 90f);
        if (_renderer == null) _renderer = GetComponent<Renderer>();
    }
    void Update()
    {
        if (!_renderer.isVisible) BulletPool.Instance.Collect(gameObject);
        float delta = speed * Time.deltaTime;
        float radAngle = angle * Mathf.Deg2Rad;
        transform.position += new Vector3(delta * Mathf.Cos(radAngle), delta * Mathf.Sin(radAngle));
    }

}
