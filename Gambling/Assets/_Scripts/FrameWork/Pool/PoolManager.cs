using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.Pool
{
    public class PoolManager : MonoBehaviour
    {

        // プレハブとそれに対応するプールのリファレンスを格納する辞書
        static Dictionary<GameObject, UnityObjectPool> dictionary;
        
        protected void Awake()
        {
            Debug.Log("Init PoolManager");
            dictionary = new Dictionary<GameObject, UnityObjectPool>();
        }

        // Unityエディタでのみ実行されるデストラクタ。各プールのサイズを検証。
        // 実際のゲームプレイでは実行されない。
#if UNITY_EDITOR
        void OnDestroy()
        {
            //プールサイズが正しいかをチェックする

            //例
            //CheckPoolSize(enemyPools);
        }
#endif

        /// <summary>
        /// 各プールが指定されたサイズを超えていないかを確認し、超過している場合は警告を表示
        /// </summary>
        /// <param name="pools">指定プール</param>
        void CheckPoolSize(UnityObjectPool[] pools)
        {
            foreach (var pool in pools)
            {
                if (pool.RuntimeSize > pool.Size)
                {
                    Debug.LogWarning(
                        string.Format("Pool:{0}has a runtime size {1} bigger than its initial size{2}!",
                            pool.Prefab.name,
                            pool.RuntimeSize,
                            pool.Size));
                }
            }
        }

        /// <summary>
        /// 動的にプールを初期化
        /// </summary>
        public static void InitializePool(UnityObjectPool pool, Transform parent = null)
        {
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogWarning($"Pool for {pool.Prefab.name} already exists.");
                return;
            }

            var poolParent = parent != null ? parent : new GameObject("Pool:" + pool.Prefab.name).transform;
            pool.Initialize(poolParent);

            dictionary.Add(pool.Prefab, pool);
        }
        
        /// <summary>
        /// 動的にプールを削除
        /// </summary>
        public static void DestroyPool(GameObject prefab)
        {
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogWarning($"Pool for {prefab.name} does not exist.");
                return;
            }

            dictionary[prefab].Clear(); // プール内のオブジェクトを削除
            dictionary.Remove(prefab);
        }
        
        /// <summary>
        /// プールのサイズを指定サイズに調整
        /// </summary>
        public static void TrimPool(GameObject prefab, int targetSize)
        {
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogWarning($"Pool for {prefab.name} does not exist.");
                return;
            }

            dictionary[prefab].TrimToSize(targetSize);
        }

        #region Release

        /// <summary>
        /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
        /// </summary>
        /// <param name="prefab">
        /// <para>指定されたプレハブ</para>
        /// </param>
        /// <returns>
        /// <para>プール内に準備できたゲームオブジェクト</para>
        /// </returns>
        public static GameObject Release(GameObject prefab)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

                return null;
            }
#endif
            return dictionary[prefab].preparedObject();
        }

        /// <summary>
        /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
        /// </summary>
        /// <param name="prefab">
        /// <para>指定されたプレハブ</para>
        /// </param>
        /// <param name="position">
        /// <para>指定された生成位置</para>
        /// </param>
        /// <returns></returns>
        public static GameObject Release(GameObject prefab, Vector3 position)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

                return null;
            }
#endif
            return dictionary[prefab].preparedObject(position);
        }

        /// <summary>
        /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
        /// </summary>
        /// <param name="prefab">
        /// <para>指定されたプレハブ</para>
        /// </param>
        /// <param name="position">
        /// <para>指定された生成位置</para>
        /// </param>
        /// <param name="rotation">
        /// <para>指定された回転</para>
        /// </param>
        /// <returns></returns>
        public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

                return null;
            }
#endif
            return dictionary[prefab].preparedObject(position, rotation);
        }

        /// <summary>
        /// <para>プール内に指定された<paramref name="prefab"></paramref>をゲームオブジェクトに返す。</para>
        /// </summary>
        /// <param name="prefab">
        /// <para>指定されたプレハブ</para>
        /// </param>
        /// <param name="position">
        /// <para>指定された生成位置</para>
        /// </param>
        /// <param name="rotation">
        /// <para>指定された回転</para>
        /// </param>
        /// <param name="localScale">
        /// <para>指定された拡大・縮小</para>
        /// </param>
        /// <returns></returns>
        public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

                return null;
            }
#endif
            return dictionary[prefab].preparedObject(position, rotation, localScale);
        }

        #endregion
    }
}