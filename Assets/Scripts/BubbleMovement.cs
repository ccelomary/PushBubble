using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField]
    float appliedForce = 1000f;
    // Update is called once per frame
    Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero, differencePosition;
    float distance, angle;

    Rigidbody2D rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            distance = Vector3.Distance(startPosition, endPosition);
            rb.AddRelativeForce((startPosition - endPosition) * Time.deltaTime * appliedForce);
            startPosition = Vector3.zero;
            endPosition = Vector3.zero;
        }
    }
}
