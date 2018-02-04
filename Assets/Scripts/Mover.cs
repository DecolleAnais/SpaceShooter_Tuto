using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float m_speed = 20f;

    private Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_rb.velocity = transform.forward * m_speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
