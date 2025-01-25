using UnityEngine;
using System.Collections.Generic;
public class ArrowPath : MonoBehaviour
{
    [SerializeField]
    GameObject BubbleEffect;

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;
    List<Vector2> CollectPoints = new List<Vector2>();
    List<GameObject> points = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CollectPoints.Contains(Input.mousePosition))
            {
                CollectPoints.Add(Input.mousePosition);
                Instantiate(BubbleEffect, Input.mousePosition, Quaternion.identity);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
           foreach (GameObject point in points)
            {

                Destroy(point);
            }
           points.Clear();
            CollectPoints.Clear();
        }
    }
}
