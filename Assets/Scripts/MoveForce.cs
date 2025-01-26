using UnityEngine;
using System.Collections;

public class MoveForce : MonoBehaviour
{

    Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero;
    float distance, angle;
    Rigidbody2D rb;
    public float forceMagnitude = 10f;
    public float maxDistance = 15f; // Optional max distance limit
    public GameObject arrow;
    private float dangerTimer = 0f;
    private bool isInDangerZone = false;
    private float destroyTime = 1f;
    private bool endedDrag = false;
    private float oppositeAngle;

    private float floorTimer = 0f;
    private bool isOnFloor = false;
    private float maxFloorTime = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // During drag, update arrow scale

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition.z = 0;
            arrow.SetActive(true);

        }

        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPosition.z = 0;

            Vector3 direction = (endPosition - startPosition).normalized;

            // Calculate angle in the opposite direction
            oppositeAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
            endedDrag = true;
            arrow.SetActive(false);


        }


    }
    void FixedUpdate()
    {
        if (endedDrag)
        {
            // Convert angle to radians and calculate force vector
            rb.linearVelocity = Vector2.zero;
            float angleInRadians = oppositeAngle * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(
                Mathf.Cos(angleInRadians),
                Mathf.Sin(angleInRadians)
            );

            // Apply initial impulse force
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
            Vector3 startPos = transform.position;
            // Track initial position for distance calculation
            float traveledDistance = 0f;

            // // Continue until max distance is reached
            // while (traveledDistance < maxTravelDistance)
            // {
            //     traveledDistance = Vector3.Distance(startPos, transform.position);
            //     yield return null;
            // }

            // Stop the object
            // rb.linearVelocity = Vector2.zero;
            endedDrag = false;
        }

        if (isOnFloor)
        {
            floorTimer += Time.deltaTime;
            if (floorTimer >= maxFloorTime)
            {
                Destroy(gameObject);
            }
        }
    }



void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("flour"))
    {
        isOnFloor = true;
    }
    else if (collision.gameObject.CompareTag("obstacle"))
    {
        Destroy(gameObject);
    }
}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("flour"))
        {
            isOnFloor = false;
            floorTimer = 0f;
        }
    }
}