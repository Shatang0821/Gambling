using Framework.Aduio;
using FrameWork.Utils;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : UnitySingleton<EnemyManager>
{
    [SerializeField]
    GameObject[] EnemyPrefab;
    public AudioData bgm;
    public static int enemycount = 1;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(AudioManager.Instance.FadeInBGM(bgm));
    }

    public void SpawnEnemy()
    {
        if (enemycount < EnemyPrefab.Length)
        {
            GameObject enemy = Instantiate(EnemyPrefab[enemycount], new Vector3(4, -1.2f, 0), Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
        else
        {
            Debug.LogWarning("No more enemy prefabs to spawn.");
            GameManager.Instance.isClear = true;
        }
    }

    public void destroyEnemy()
    {
        if (spawnedEnemies.Count > 0)
        {
            GameObject enemyToDestroy = spawnedEnemies[0];
            spawnedEnemies.RemoveAt(0);

            Destroy(enemyToDestroy); // ƒV[ƒ““à‚Ì“G‚ğíœ
            enemycount++;
            GameManager.Instance.isResult = true;
        }
        else
        {
            Debug.LogWarning("No enemies to destroy.");
            
        }
    }
}
