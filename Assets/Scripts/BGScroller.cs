using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float m_scrollSpeed;
    public float m_tileSizeZ;

    private Vector3 m_startPosition;

	// Use this for initialization
	void Start () {
        m_startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float newPosition = Mathf.Repeat(Time.time * m_scrollSpeed, m_tileSizeZ);
        transform.position = m_startPosition + Vector3.forward * newPosition;
	}
}
