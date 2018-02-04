using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float m_dodge;
    public float m_smoothing;
    public float m_tilt;
    public Vector2 m_startWait;
    public Vector2 m_maneuverTime;
    public Vector2 m_maneuverWait;
    public Boundary m_boundary;

    private float m_targetManeuver;
    private float m_currentSpeed;
    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_currentSpeed = m_rb.velocity.z;
        StartCoroutine(Evade());
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
        // translation
        float newManeuver = Mathf.MoveTowards(m_rb.velocity.x,
                                              m_targetManeuver,
                                              Time.deltaTime * m_smoothing);
        m_rb.velocity = new Vector3(newManeuver, 0f, m_currentSpeed);
        m_rb.position = new Vector3(
            Mathf.Clamp(m_rb.position.x, m_boundary.xMin, m_boundary.xMax),
            0f,
            Mathf.Clamp(m_rb.position.z, m_boundary.zMin, m_boundary.zMax)
        );
        // rotation
        m_rb.rotation = Quaternion.Euler(0f, 0f, -m_tilt * m_rb.velocity.x);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(m_startWait.x, 
                                                    m_startWait.y));

        while(true)
        {
            // to dodge to the right side (the center)
            m_targetManeuver = Random.Range(1, m_dodge) * 
                                -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(m_maneuverTime.x,
                                                        m_maneuverTime.y));
            m_targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(m_maneuverWait.x,
                                                        m_maneuverWait.y));
        }
    }
}
