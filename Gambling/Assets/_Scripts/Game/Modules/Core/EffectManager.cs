using System.Collections;
using System.Collections.Generic;
using FrameWork.Pool;
using FrameWork.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectManager : UnitySingleton<EffectManager>
{

    [SerializeField] private UnityObjectPool[] effectPools;
    
    private void Start()
    {
        InitializePools();
    }

    private void OnDestroy()
    {
        ClearPools();
    }

    public void InitializePools()
    {
        foreach (var pool in effectPools)
        {
            // EffectManager の子として新しいオブジェクトを作成
            var poolParent = new GameObject("Pool:" + pool.Prefab.name);
            poolParent.transform.SetParent(transform);
            
            PoolManager.InitializePool(pool, poolParent.transform);
        }
    }

    public void ClearPools()
    {
        foreach (var pool in effectPools)
        {
            PoolManager.DestroyPool(pool.Prefab);
        }
    }

    public void TrimPools(int targetSize)
    {
        foreach (var pool in effectPools)
        {
            PoolManager.TrimPool(pool.Prefab, targetSize);
        }
    }

    public GameObject SpawnEffect(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return PoolManager.Release(prefab, position, rotation);
    }
    
}
