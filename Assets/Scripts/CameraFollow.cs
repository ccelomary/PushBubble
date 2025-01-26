using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public float followSpeed = 5f;
    public Vector3 offset = Vector3.zero;

    [Header("Boundaries")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;
    public float boundaryBuffer = 1f;

    [Header("Camera Settings")]
    public float zoomSpeed = 2f;
    public float minZoom = 4f;
    public float maxZoom = 8f;
    public bool enablePrediction = false;
    public float predictionTime = 0.5f;

    private const float CAMERA_Z = -10f;
    private Vector2 velocity;
    private Camera cam;
    private float targetZoom;
    private float shakeIntensity;
    private float shakeDuration;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        offset.z = CAMERA_Z;
        transform.position = new Vector3(0, 0, CAMERA_Z);
    }
private void LateUpdate()
{
    if (target == null) return;

    // Calculate desired position with offset
    Vector3 desiredPosition = target.position;
    desiredPosition.z = CAMERA_Z;

    // Smooth movement
    Vector3 smoothedPosition = Vector3.Lerp(
        transform.position,
        desiredPosition,
        followSpeed * Time.deltaTime
    );

    // Apply boundaries
    ApplyBoundaries(ref smoothedPosition);
    
    // Apply screen shake if active
    ApplyScreenShake(ref smoothedPosition);
    
    // Update camera position
    transform.position = smoothedPosition;
    
    // Update zoom
    UpdateZoom();
}


    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPos = target.position + offset;
        
        if (enablePrediction && target.TryGetComponent<Rigidbody2D>(out var rb))
        {
            targetPos += (Vector3)rb.linearVelocity * predictionTime;
        }

        return targetPos;
    }

    private Vector3 SmoothFollow(Vector3 targetPosition)
    {
        float newX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocity.x, 1f / followSpeed);
        float newY = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref velocity.y, 1f / followSpeed);
        return new Vector3(newX, newY, CAMERA_Z);
    }


private void ApplyBoundaries(ref Vector3 position)
{
    position.x = Mathf.Clamp(position.x, minX + boundaryBuffer, maxX - boundaryBuffer);
    position.y = Mathf.Clamp(position.y, minY + boundaryBuffer, maxY - boundaryBuffer);
    position.z = CAMERA_Z;
}

    private void ApplyScreenShake(ref Vector3 position)
    {
        if (shakeDuration > 0)
        {
            position.x += Random.Range(-1f, 1f) * shakeIntensity;
            position.y += Random.Range(-1f, 1f) * shakeIntensity;
            shakeDuration -= Time.deltaTime;
        }
    }

    private void UpdateZoom()
    {
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize, 
            targetZoom, 
            Time.deltaTime * zoomSpeed
        );
    }

    public void AddScreenShake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
    }

    public void SetZoom(float zoom)
    {
        targetZoom = Mathf.Clamp(zoom, minZoom, maxZoom);
    }
}