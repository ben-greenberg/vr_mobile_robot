using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // Use default line or distance
        float targetLength = m_DefaultLength;
        
        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // Or based on hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position of the dot
        m_Dot.transform.position = endPosition;

        // Set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);

        /*Vector3 test1;
        test1.x = -222f;
        test1.y = 88f;
        test1.z = -77f;

        Vector3 test2;
        test2.x = -225f;
        test2.y = 90f;
        test2.z = -73f;

        m_LineRenderer.SetPosition(0, test1);
        m_LineRenderer.SetPosition(1, test2);*/
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}
