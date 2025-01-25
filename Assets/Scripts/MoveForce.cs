using UnityEngine;
using System.Collections;

public class MoveForce : MonoBehaviour 
{
    
    Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero;
    float distance, angle;
    Rigidbody2D rb;
    public float forceMagnitude = 10f;
    public float maxDistance = 5f; // Optional max distance limit
  [SerializeField] private GameObject arrow; // Reference to arrow object
    private Vector3 arrowOriginalScale;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arrowOriginalScale = arrow.transform.localScale;
    }

    void Update()
    {
                        // During drag, update arrow scale
  
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition.z = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPosition.z = 0;

            // Calculate distance and direction
            distance = Mathf.Min(Vector3.Distance(startPosition, endPosition));
            Vector3 direction = (endPosition - startPosition).normalized;
            
            // Calculate angle in the opposite direction
            float oppositeAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
            
            StartCoroutine(ApplyForceAndStop(distance, oppositeAngle));
        }
    }

    private IEnumerator ApplyForceAndStop(float maxTravelDistance, float angleInDegrees)
    {
        // Convert angle to radians and calculate force vector
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(
            Mathf.Cos(angleInRadians), 
            Mathf.Sin(angleInRadians)
        );

        // Apply initial impulse force
        rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);

        // Track initial position for distance calculation
        Vector3 startPos = transform.position;
        float traveledDistance = 0f;

        // Continue until max distance is reached
        while (traveledDistance < maxTravelDistance)
        {
            traveledDistance = Vector3.Distance(startPos, transform.position);
            yield return null;
        }

        // Stop the object
        rb.linearVelocity = Vector2.zero;
    }
}