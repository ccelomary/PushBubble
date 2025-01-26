using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform bubble;
    [SerializeField]
    float Speed = 10f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bubble.position , Speed * Time.deltaTime);
    }
}
