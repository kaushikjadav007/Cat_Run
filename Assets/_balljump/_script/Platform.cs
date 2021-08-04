using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    public int m_mindistance;
    public int m_maxdistance;

    public int m_id;
    [Space]
    public int m_platformanimationid;
    [Space]
    public float m_mydelayedtime;
    [Space]
    public float m_blockaniamtiontime;
    public float m_blockaniamtionDelayedtime;
    [Space]
    public List<Transform> m_blockstoanimate;

    [Space]
    private List<Vector3> m_positionstored;

    [Space]
    public GameObject m_genrator;

    [Space]
    public Ease m_ease;

    private int m_blockscount;


    private bool m_storedpos;

    private void OnEnable()
    {
        m_genrator.SetActive(true);
        m_blockscount = m_blockstoanimate.Count;

        if (!m_storedpos)
        {

            if (m_blockscount>0)
            {
                m_positionstored = new List<Vector3>();

                for (int i = 0; i < m_blockscount; i++)
                {
                    m_positionstored.Add(m_blockstoanimate[i].transform.position);
                }
            }
        }

        StartCoroutine(_DoPlatformAnimation());
    }


    private void OnDisable()
    {
        if (m_blockscount > 0)
        {    
            for (int i = 0; i < m_blockscount; i++)
            {
                m_blockstoanimate[i].transform.position = m_positionstored[i];
            }
        }
    }

    IEnumerator _DoPlatformAnimation()
    {
        yield return new WaitForSecondsRealtime(m_mydelayedtime);

        switch (m_platformanimationid)
        {
            case 1:

                for (int i = 0; i < m_blockscount; i++)
                {          
                    m_blockstoanimate[i].transform.DOLocalMoveY(0f, m_blockaniamtiontime).SetEase(Ease.OutBack);
                    yield return new WaitForSeconds(m_blockaniamtionDelayedtime);
                }
                break;

            case 2:
                for (int i = 0; i < m_blockscount; i++)
                {
                    m_blockstoanimate[i].transform.DOLocalMoveZ(0f, m_blockaniamtiontime).SetEase(Ease.OutBack);
                    yield return new WaitForSeconds(m_blockaniamtionDelayedtime);
                }
                break;

            case 3:


                break;
        }

    }



}
