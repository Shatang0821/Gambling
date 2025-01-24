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
    [SerializeField] private List<GameObject> enemyPrefabs; // “G‚ÌƒvƒŒƒnƒuƒŠƒXƒg
    public AudioData bgm;

    private EntityObject spawnedEnemy; // ¶¬Ï‚İ‚Ì“GƒŠƒXƒg

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
    /// “G‚ğ¶¬‚·‚é
    /// </summary>
    public void SpawnEnemy(Vector3 spawnPosition)
    {
        if (spawnedEnemy != null)
        {
            DestroySpecificEnemy(spawnedEnemy.gameObject);
        }
        spawnedEnemy = Instantiate(enemyPrefabs[0], spawnPosition, quaternion.identity).GetComponent<EntityObject>();
        var healthComponent = spawnedEnemy.GetEntityComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.AddDeathAction(WhenEnemyDeath);
        }
    }

    /// <summary>
    /// “G‚ğíœ‚·‚é
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
    /// “Á’è‚Ì“G‚ğíœ‚·‚é
    /// </summary>
    public void DestroySpecificEnemy(GameObject targetEnemy)
    {
        if (targetEnemy)
        {
            Destroy(targetEnemy.gameObject);
        }
    }

    /// <summary>
    /// ƒQ[ƒ€I—¹‚Ìˆ—
    /// </summary>
    public void WhenEnemyDeath()
    {
        DestroyEnemy();
        GameManager.Instance.IsGameWin = true;
        GameManager.Instance.ChangeState(GameState.Result);
    }

}
