using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject m_shot;
    public Transform m_shotSpawn;
    public float m_fireRate;
    public float m_delay;

    private AudioSource m_audioSrc;

	// Use this for initialization
	void Start () {
        m_audioSrc = GetComponent<AudioSource>();
        InvokeRepeating("Fire", m_delay, m_fireRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fire()
    {
        Instantiate(m_shot, m_shotSpawn.position, m_shotSpawn.rotation);
        m_audioSrc.Play();
    }
}
