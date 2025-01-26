using UnityEngine;

public class TowerPush : MonoBehaviour
{
    [Header("Air Force Settings")]
    [Tooltip("قوة الدفع التي يتم تطبيقها على اللاعب")]
    public float pushForce = 10f; // قوة الدفع

    private Rigidbody2D playerRigidbody; // مرجع لجسم اللاعب

    private void OnTriggerEnter2D(Collider2D other)
    {
        // التحقق من أن الكائن الذي دخل المنطقة هو اللاعب
        if (other.CompareTag("Player"))
        {
            playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                // تطبيق دفعة أولى عند الدخول
                ApplyForce(playerRigidbody, pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // الاستمرار في دفع اللاعب أثناء وجوده في المنطقة
        if (playerRigidbody != null && other.CompareTag("Player"))
        {
            ApplyForce(playerRigidbody, pushForce * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // إزالة تأثير المنطقة عند خروج اللاعب
        if (other.CompareTag("Player") && playerRigidbody != null)
        {
            playerRigidbody = null;
        }
    }

    /// <summary>
    /// دالة لتطبيق القوة على الجسم
    /// </summary>
    /// <param name="rb">مرجع Rigidbody2D</param>
    /// <param name="force">مقدار القوة</param>
    /// <param name="mode">طريقة تطبيق القوة</param>
    private void ApplyForce(Rigidbody2D rb, float force, ForceMode2D mode)
    {
        rb.AddForce(Vector2.up * force, mode);
    }
}