using UnityEngine;

public class EffectController : MonoBehaviour
{
    public bool CanRotation = true;
    private Animator animator;

    private void Awake()
    {
        // アニメータを取得
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on EffectController.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        // 回転値をランダムに取る
        if (CanRotation)
        {
            float randomZRotation = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomZRotation);
        }
        
        // アニメーションを再生
        if (animator != null)
        {
            animator.Play(0, -1, 0); // 最初から再生
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delay"></param>
    public void DeactivateAfter(float delay)
    {
        Invoke(nameof(Deactivate), delay);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}