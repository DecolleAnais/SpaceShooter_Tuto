using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float m_tumble = 5f;

    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_rb.angularVelocity = Random.insideUnitSphere * m_tumble;
	}
}
