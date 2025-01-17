using UnityEngine;

namespace FrameWork.Utils
{
    public static class ErrorCheck
    {
        /// <summary>
        /// Unity のコンポーネントまたは自作コンポーネントが空であるかを確認し、自動でエラーを報告します。
        /// </summary>
        /// <typeparam name="T">コンポーネントのタイプ</typeparam>
        /// <param name="component">確認するコンポーネント</param>
        /// <param name="context">このメソッドを呼び出したオブジェクト（エラー報告の位置特定用）</param>
        public static void Check<T>(T component, Object context = null) where T : class
        {
            bool isNull = component is UnityEngine.Object unityObject
                ? unityObject == null
                : component == null; 

            if (isNull)
            {   
                string componentType = typeof(T).Name;
                string contextName = context != null ? context.name : "Unknown Context";

                Debug.LogError($"[ErrorCheck] Missing required component of type {componentType} in {contextName}.", context);
            }
        }
    }
}