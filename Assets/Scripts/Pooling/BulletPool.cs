using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject prefab;
    public int chunk = 1000;

    public static BulletPool Instance;

    private Queue<GameObject> _store;
    private void Awake()
    {
        _store = new Queue<GameObject>(chunk);
        for (int i = 0; i < chunk; i++)
        {
            var bullet = Instantiate(prefab);
            bullet.SetActive(false);
            _store.Enqueue(bullet);
        }
        Instance = this;
    }

    public IEnumerable<GameObject> Request(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return GetOrCreate();
        }
    }

    private GameObject Create() => Instantiate(prefab);

    private GameObject GetOrCreate()
    {
        if (_store.Count > 0)
        {
            var bullet = _store.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        return Create();
    }
    public void Collect(GameObject bullet)
    {
        bullet.SetActive(false);
        _store.Enqueue(bullet);
    }
    public void Collect(IEnumerable<GameObject> bullets)
    {
        foreach (var bullet in bullets)
        {
            Collect(bullet);
        }
    }
}
