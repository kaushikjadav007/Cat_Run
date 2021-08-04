using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{

    public RectTransform m_mainmenu;
    public RectTransform m_gameover;

    [Space]
    public TextMeshProUGUI m_score;

    private int m_coincount;

    public static UiManager Instance;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void _GameoOver()
    {
        m_gameover.gameObject.SetActive(true);
    }

    public void _RePlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void _AddScore()
    {

        m_coincount += 1;

        Debug.Log(m_coincount);
        m_score.text = m_coincount.ToString();
    }


    public void _Play()
    {
        Player.Instance.m_gamestarted = true;
        m_mainmenu.gameObject.SetActive(false);
        Player.Instance._SetAnimationToRun();
    }
}
