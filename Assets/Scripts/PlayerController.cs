using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public enum ControlType { TouchPad, Accelerometer };

public class PlayerController : MonoBehaviour {

    public float m_speed = 10f;
    public float m_tilt = 2f;
    public Boundary m_boundary;
    public GameObject m_shot;
    public Transform m_shotSpawn;
    public float m_fireDelta = 0.5f;
    public SimpleTouchPad m_touchPad;
    public SimpleTouchAreaButton m_touchAreaButton;
    public GameObject m_settingsButton;

    private Rigidbody m_rb;
    private AudioSource m_audioSrc;
    private float m_nextFire = 0.5f;
    private float m_time = 0f;
    private Quaternion m_calibrationQuaternion;
    private ControlType m_controlType;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_audioSrc = GetComponent<AudioSource>();
        CalibrateAccelerometer();
        // Type of control : touchpad or accelerometer
        m_controlType = ControlType.TouchPad;
    }


    // MOBILE CALBRATION

    // Used to calibrate the Input.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = 
            Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), 
                                      accelerationSnapshot);
        m_calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    // Get the calibrated value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = m_calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
	

	// FixedUpdate is called once per frame for physics
	void FixedUpdate () {
        // Translation
        Vector3 movement = new Vector3();
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        // PC
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0f, moveVertical);

#elif UNITY_IOS || UNITY_ANDROID
        // MOBILE
        if(m_controlType == ControlType.Accelerometer)
        {
            // Accelerometer
            Vector3 accelerationRaw = Input.acceleration;
            Vector3 acceleration = FixAcceleration(accelerationRaw);
            movement = new Vector3(acceleration.x, 0f, acceleration.y);
        }
        else
        {
            // touchPad
            Vector2 direction = m_touchPad.GetDirection();
            movement = new Vector3(direction.x, 0f, direction.y);
        } 
        
#endif

        // Speed
        m_rb.velocity = movement * m_speed;

        // Clamp to Boundary
        m_rb.position = new Vector3(
            Mathf.Clamp(m_rb.position.x, m_boundary.xMin, m_boundary.xMax),
            0f,
            Mathf.Clamp(m_rb.position.z, m_boundary.zMin, m_boundary.zMax)
        );

        // Rotation
        m_rb.rotation = Quaternion.Euler(0f, 0f, -m_tilt * m_rb.velocity.x);
	}


    // Update is called once per frame
    void Update()
    {
        m_time = m_time + Time.deltaTime;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetButton("Fire1") && m_time > m_nextFire)
#elif UNITY_IOS || UNITY_ANDROID
	    if (m_touchAreaButton.CanFire() && m_time > m_nextFire)   
#endif
        {
            m_nextFire = m_time + m_fireDelta;
            Instantiate(m_shot, m_shotSpawn.position, m_shotSpawn.rotation);

            m_audioSrc.Play();
            
            m_nextFire = m_nextFire - m_time;
            m_time = 0f;
        }
    }

    // Switch control between TouchPad and Accelerometer
    public void ChangeControlType()
    {
        if (m_controlType == ControlType.TouchPad)
            m_controlType = ControlType.Accelerometer;
        else
            m_controlType = ControlType.TouchPad;
    }
}
