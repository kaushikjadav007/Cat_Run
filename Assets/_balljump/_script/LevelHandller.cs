using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandller : MonoBehaviour
{
    public List<GameObject> m_platformprefabs;

    [Space]
    public List<GameObject> m_pooedobjects;

    private GameObject m_curruntplatform;
    [Space]
    public Vector3 m_lastplatformpos;

    [Space]
    public int m_objecttogenrate;

    [Space]
    public bool m_randmized;

    private float m_safedistance=30f;
    private float m_yposition=0f;

    private int m_zmin=30;
    private int m_zmax=30;
    private int m_randomnumber;


    private GameObject m_tempobject;


    public static LevelHandller Instance;



    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        for (int i = 0; i < m_platformprefabs.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                m_tempobject = Instantiate(m_platformprefabs[i]);
                m_tempobject.transform.position = Vector3.zero;
                m_tempobject.transform.rotation = Quaternion.identity;
                m_pooedobjects.Add(m_tempobject);
            }
        }
    }



    public void _GenratePlatform()
    {

        if (m_randmized)
        {
            m_curruntplatform = Instantiate(m_platformprefabs[m_objecttogenrate]);
        }
        else
        {

            m_curruntplatform = _GetDesieredGamobject();
        }

        //Get Z pos
        m_safedistance = _RandomizedZpos();


        m_curruntplatform.transform.position = new Vector3(0f, m_yposition, m_lastplatformpos.z+m_safedistance);
        m_curruntplatform.transform.rotation = Quaternion.identity;
        m_lastplatformpos = m_curruntplatform.transform.position;
        m_curruntplatform.SetActive(true);

        //Get Distance For next platform
        Platform p = m_curruntplatform.GetComponent<Platform>();
        m_zmax = m_curruntplatform.GetComponent<Platform>().m_maxdistance;
        m_zmin = m_curruntplatform.GetComponent<Platform>().m_mindistance;

    }

    private GameObject _GetDesieredGamobject()
    {

        m_randomnumber = Random.Range(0,m_pooedobjects.Count);

        if (!m_pooedobjects[m_randomnumber].activeSelf)
        {
            return m_pooedobjects[m_randomnumber];
        }


        return _InstantiateObject();
    }

    GameObject _InstantiateObject()
    {
        m_tempobject = Instantiate(m_platformprefabs[Random.RandomRange(0,m_platformprefabs.Count)]);
        m_tempobject.transform.position = Vector3.zero;
        m_tempobject.transform.rotation = Quaternion.identity;
        m_pooedobjects.Add(m_tempobject);
        return m_tempobject;
    }



    private int _RandomizedZpos()
    {
        return Random.Range(m_zmin, m_zmax);
    }

}
