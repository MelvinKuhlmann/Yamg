using System.Collections.Generic;
using UnityEngine;

public class MoneyPool : ObjectPool<MoneyPool, MoneyObject, Vector2>
{
    public float rangeMin = -5f;
    public float rangeMax = 5f;
    public float forceMin = 5f;
    public float forceMax = 10f;

    static protected Dictionary<GameObject, MoneyPool> s_PoolInstances = new Dictionary<GameObject, MoneyPool>();

    private void Awake()
    {
        //This allow to make Pool manually added in the scene still automatically findable & usable
        if (prefab != null && !s_PoolInstances.ContainsKey(prefab))
            s_PoolInstances.Add(prefab, this);
    }

    private void OnDestroy()
    {
        s_PoolInstances.Remove(prefab);
    }

    //initialPoolCount is only used when the objectpool don't exist
    static public MoneyPool GetObjectPool(GameObject prefab, int initialPoolCount = 10)
    {
        MoneyPool objPool = null;
        if (!s_PoolInstances.TryGetValue(prefab, out objPool))
        {
            GameObject obj = new GameObject(prefab.name + "_Pool");
            objPool = obj.AddComponent<MoneyPool>();
            objPool.prefab = prefab;
            objPool.initialPoolCount = initialPoolCount;

            s_PoolInstances[prefab] = objPool;
        }

        return objPool;
    }

    public void Spawn()
    {
        MoneyObject mo = Pop(new Vector2(transform.position.x, (transform.position.y + 2)));
        mo.rigidbody2D.velocity = new Vector2(Random.Range(rangeMin, rangeMax), Random.Range(forceMin, forceMax));
    }

    public void SpawnAmount(int amountToSpawn)
    {
        int i = 0;
        while(i < amountToSpawn)
        {
            i++;
            Spawn();
        }
    }
}
public class MoneyObject : PoolObject<MoneyPool, MoneyObject, Vector2>
{
    public Transform transform;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    protected override void SetReferences()
    {
        transform = instance.transform;
        rigidbody2D = instance.GetComponent<Rigidbody2D>();
        spriteRenderer = instance.GetComponent<SpriteRenderer>();
    }

    public override void WakeUp(Vector2 position)
    {
        transform.position = position;
        instance.SetActive(true);
    }

    public override void Sleep()
    {
        instance.SetActive(false);
    }
}