using UnityEngine;
using System.Collections;

public class BubbleMovement : MonoBehaviour
{
    Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero;
    float distance, angle;
    Rigidbody2D rb;
    public float appliedForce = 10f;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
                

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition.z = 0; // Ensure z is zero since we're in 2D
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPosition.z = 0; // Ensure z is zero since we're in 2D
            distance = Vector3.Distance(startPosition, endPosition);
            Vector3 direction = (endPosition - startPosition).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            StartCoroutine(SmoothMove(distance, angle));
            startPosition = Vector3.zero;
            endPosition = Vector3.zero;
        }
    }

    private IEnumerator SmoothMove(float distance, float angle)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition - new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance;
        float elapsedTime = 0f;
        float duration = distance / appliedForce; // Adjust duration based on distance and applied force

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is set
    }
}