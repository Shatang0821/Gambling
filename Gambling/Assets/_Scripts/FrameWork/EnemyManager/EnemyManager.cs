using System;
using Framework.Aduio;
using FrameWork.Utils;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using Framework.Entity;
using FrameWork.EventCenter;
using Game.Component;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // 敵のプレハブリスト
    public AudioData bgm;

    private EntityObject spawnedEnemy; // 生成済みの敵リスト
    private int _index = 0;
    private void Start()
    {
        StartCoroutine(AudioManager.Instance.FadeInBGM(bgm));
    }

    private void OnEnable()
    {
        EventCenter.AddListener(GameState.Result, DestroyEnemy);
        
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(GameState.Result, DestroyEnemy);
    }

    /// <summary>
    /// 敵を生成する
    /// </summary>
    public void SpawnEnemy(Vector3 spawnPosition)
    {
        if (spawnedEnemy != null)
        {
            DestroySpecificEnemy(spawnedEnemy.gameObject);
        }
        // _indexをインクリメントしてリストの範囲内に収める
        _index = (_index + 1) % enemyPrefabs.Count;
        spawnedEnemy = Instantiate(enemyPrefabs[_index], spawnPosition, quaternion.identity).GetComponent<EntityObject>();
        var healthComponent = spawnedEnemy.GetEntityComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.AddDeathAction(WhenEnemyDeath);
        }
    }

    /// <summary>
    /// 敵を削除する
    /// </summary>
    public void DestroyEnemy()
    {
        if (spawnedEnemy)
        {
            Destroy(spawnedEnemy.gameObject);
            spawnedEnemy = null;
        }
    }

    /// <summary>
    /// 特定の敵を削除する
    /// </summary>
    public void DestroySpecificEnemy(GameObject targetEnemy)
    {
        if (targetEnemy)
        {
            Destroy(targetEnemy.gameObject);
        }
    }

    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    public void WhenEnemyDeath()
    {
        DestroyEnemy();
        GameManager.Instance.IsGameWin = true;
        GameManager.Instance.ChangeState(GameState.Result);
    }

}
