using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool m_touched;
    private int m_pointerId;
    private bool m_canFire;

    void Awake()
    {
        m_touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Set our start point
        if (!m_touched)
        {
            m_touched = true;
            m_pointerId = eventData.pointerId;
            m_canFire = true;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == m_pointerId)
        {
            // Reset everything
            m_touched = false;
            m_canFire = false;
        }
    }

    public bool CanFire()
    {
        return m_canFire;
    }
}
