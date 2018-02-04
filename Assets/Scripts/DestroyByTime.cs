using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float m_lifetime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, m_lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
