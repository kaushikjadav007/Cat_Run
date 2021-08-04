using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float m_movespeed;
    [Space]
    public float m_jumpspeed;

    private Rigidbody m_rb;
    public float m_fallMultiple = 3.55f;
    public float m_jumpMultiple = 2.5f;

    public bool m_onground;

    [Space]
    public Animator m_cat;


    [Space]
    public bool m_gamestarted;

    private float m_taptimer=0f;

    public bool m_jump = false;

    public static Player Instance;

    void Start()
    {

        if (Instance==null)
        {
            Instance = this;
        }

        m_rb = gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        //Move ahed if game is started
        if (!m_gamestarted)
        {
            return;
        }


#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.Space))
        {
//            Debug.Log("WORKED");
            m_taptimer += Time.deltaTime;

            if (m_taptimer > 0.1f && !m_jump)
            {
//                Debug.Log("long jump");
                _Jump();
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (m_taptimer<= 0.1f && !m_jump)
            {
//                Debug.Log("small jump");
                _Jump();
            }
            m_taptimer = 0f;
        }


#endif

        if (Input.touchCount > 0)
        {
            if (Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                //Debug.Log("WORKED");
                m_taptimer += Time.deltaTime;

                if (m_taptimer > 0.1f && !m_jump)
                {
                    //Debug.Log("long jump");
                    _Jump();
                }
            }

            if (Input.GetTouch(0).phase==TouchPhase.Ended)
            {
                if (m_taptimer <= 0.1f && !m_jump)
                {
                    _Jump();
                }
                m_taptimer = 0f;
            }
        }


        if (!m_onground)
        {
            if (m_rb.velocity.y < 0)
            {
                m_rb.velocity += Vector3.up * Physics.gravity.y * (m_fallMultiple - 1) * Time.deltaTime;
            }
            else if (m_rb.velocity.y > 0)
            {
                m_rb.velocity += Vector3.up * Physics.gravity.y * (m_jumpMultiple - 1) * Time.deltaTime;
            }
        }

    }

    private void FixedUpdate()
    {
        //Move ahed if game is started
        if (!m_gamestarted)
        {
            return;
        }

        _Move();
    }

    /// <summary>
    /// MOving Player
    /// </summary>
    void _Move()
    {
        m_rb.velocity = new Vector3(m_rb.velocity.x,m_rb.velocity.y, m_movespeed);
    }

    /// <summary>
    /// For Jump of the player
    /// </summary>
    void _Jump()
    {
        m_jump = true;

        if (!_NewGroundCheck())
        {
            Debug.Log("Returned");
        }

        m_cat.SetInteger("state",2);

        Vector3 v = m_rb.velocity;
        v.y = 0;
        m_rb.velocity = v;


//        Debug.Log(m_taptimer);

        if (m_taptimer>0.1f)
        {
            m_jumpspeed = 13f;
        }
        else
        {
            m_jumpspeed = 10f;
        }

        m_rb.AddForce(Vector3.up * m_jumpspeed,ForceMode.Impulse);
        StartCoroutine(_SetanimationtoRuning());

    }

    public void _SetAnimationToRun()
    {
        m_cat.SetInteger("state", 1);
    }

    IEnumerator _SetanimationtoRuning()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        m_cat.SetInteger("state",1);
    }

    bool _NewGroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }


    private void OnCollisionEnter(Collision col)
    {
        m_onground = true;
        m_jump = false;

        if (col.gameObject.CompareTag("Finish"))
        {
            m_gamestarted = false;
            m_rb.velocity = Vector3.zero;
            UiManager.Instance._GameoOver();
        }

    }

    private void OnCollisionExit(Collision col)
    {
        m_onground = false;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Genrator"))
        {
            LevelHandller.Instance._GenratePlatform();
        }

    }

}
