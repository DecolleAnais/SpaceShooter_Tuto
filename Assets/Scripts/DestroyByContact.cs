using UnityEngine;
using System;

public class DestroyByContact : MonoBehaviour {

    public GameObject m_explosion;
    public GameObject m_playerExplosion;
    public int m_scoreValue = 10;

    //public delegate void DestroyAction(int scoreValue);
    //public static event DestroyAction OnModifyScore;
    public static Action<int> OnModifyScore;
    public static Action OnGameOver;

    private void OnTriggerEnter(Collider other)
    {
        // ignore the collisions with the boundary or between ennemies
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
        // player destruction
        if(other.CompareTag("Player"))
        {
            Instantiate(m_playerExplosion, other.transform.position, other.transform.rotation);
            if(OnGameOver != null)
            {
                OnGameOver();
            }
        } else
        {
            // add score, only if it's an hazard who has been destroyed
            if (OnModifyScore != null)
            {
                OnModifyScore(m_scoreValue);
            }
        }
        // asteroid explosion
        if(m_explosion != null)
            Instantiate(m_explosion, transform.position, transform.rotation);

        // objects destruction
        Destroy(other.gameObject);
        Destroy(gameObject);

    }
}
