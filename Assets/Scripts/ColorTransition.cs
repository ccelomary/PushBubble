using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public Material material;
    public Color startColor = Color.white;
    public Color safeColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color dangerColor = Color.red;
    public float maxForce = 15f;

    void Start()
    {
        material.color = startColor;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 startPos = transform.position;
            float distance = Vector3.Distance(startPos, mousePos);
            
            float forcePercentage = Mathf.Clamp01(distance / maxForce);
            
            // Three-color gradient
            if (forcePercentage <= 0.5f)
            {
                // Lerp between green and yellow (0 to 0.5)
                material.color = Color.Lerp(safeColor, warningColor, forcePercentage * 2f);
            }
            else
            {
               
                // Lerp between yellow and red (0.5 to 1)
                material.color = Color.Lerp(warningColor, dangerColor, (forcePercentage - 0.5f) * 2f);
            }
        }
        else
        {
            // Reset to white when not dragging
            material.color = startColor;
        }
    }
}