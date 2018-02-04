using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public float m_smoothing;

    private Vector2 m_origin;
    private Vector2 m_direction;
    private Vector2 m_smoothDirection;
    private bool m_touched;
    private int m_pointerId;

    void Awake()
    {
        m_direction = Vector2.zero;
        m_touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Set our start point
        if(!m_touched)
        {
            m_touched = true;
            m_pointerId = eventData.pointerId;
            m_origin = eventData.position;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.pointerId == m_pointerId)
        {
            // Compare the difference between our start and current pointer pos
            Vector2 current = eventData.position;
            m_direction = current - m_origin;
            m_direction = m_direction.normalized;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerId == m_pointerId)
        {
            // Reset everything
            m_origin = Vector2.zero;
            m_touched = false;
        }
    }
   
    public Vector2 GetDirection()
    {
        m_smoothDirection = Vector2.MoveTowards(m_smoothDirection, m_direction, m_smoothing);
        return m_smoothDirection;
    }
}
