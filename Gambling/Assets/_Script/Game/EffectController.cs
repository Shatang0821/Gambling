using UnityEngine;

public class EffectController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on EffectController.");
        }
    }

    /// <summary>
    /// 激活效果
    /// </summary>
    private void OnEnable()
    {
        // 随机调整 Z 轴旋转
        float randomZRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomZRotation);

        // 播放动画
        if (animator != null)
        {
            animator.Play(0, -1, 0); // 播放第一个动画从头开始
        }
    }

    /// <summary>
    /// 可选：自动禁用对象（例如效果完成后）
    /// </summary>
    /// <param name="delay">延迟禁用的时间</param>
    public void DeactivateAfter(float delay)
    {
        Invoke(nameof(Deactivate), delay);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}