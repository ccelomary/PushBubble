using UnityEngine;

public class RotateOppositeMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;
        direction.z = 0;

        // Calculate the angle to rotate the object
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 180; // Rotate to the opposite direction

        // Apply the rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}