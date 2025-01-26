using UnityEngine;

public class BallLauncher2D : MonoBehaviour
{
    private Vector2 dragStartPos;    // موقع بداية السحب
    private Vector2 dragCurrentPos; // موقع السحب الحالي
    private bool isDragging = false;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    [Header("Settings")]
    public float launchForce = 10f;       // قوة الإطلاق
    public float decelerationRate = 0.98f; // معامل التباطؤ (قيمة بين 0 و 1)
    public float minSpeed = 0.1f;         // أقل سرعة قبل التوقف

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // إخفاء الخط في البداية
    }

    void Update()
    {
        // بداية السحب
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = GetMouseWorldPosition();
            lineRenderer.positionCount = 2; // تفعيل الخط
            lineRenderer.SetPosition(0, dragStartPos); // النقطة الأولى
        }

        // أثناء السحب
        if (Input.GetMouseButton(0) && isDragging)
        {
            dragCurrentPos = GetMouseWorldPosition();
            lineRenderer.SetPosition(1, dragCurrentPos); // تحديث النقطة الثانية
        }

        // نهاية السحب والإطلاق
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            lineRenderer.positionCount = 0; // إخفاء الخط

            Vector2 dragEndPos = GetMouseWorldPosition();
            Vector2 launchDirection = (dragStartPos - dragEndPos).normalized; // الاتجاه المعاكس للسحب
            float dragDistance = Vector2.Distance(dragStartPos, dragEndPos);  // المسافة المسحوبة

            rb.isKinematic = false; // تفعيل الفيزياء
            rb.linearVelocity = launchDirection * dragDistance * launchForce; // تطبيق السرعة بدلاً من AddForce
        }

        // تخفيض السرعة تدريجيًا
        if (rb.linearVelocity.magnitude > minSpeed)
        {
            rb.linearVelocity *= decelerationRate; // تقليل السرعة تدريجيًا
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // إيقاف الحركة تمامًا عند الوصول للحد الأدنى
        }
    }

    // تحويل موضع الماوس إلى موضع في العالم
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition; // موقع الماوس على الشاشة
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // تحويل إلى موضع في العالم
        return mousePos;
    }
}