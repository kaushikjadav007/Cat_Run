using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour 
{
	
	[Space]
	public Player m_player;
    public Vector3 offset;

	private Vector3 m_pos;


    // Update is called once per frame
    void Update ()
    {
		_CameraFollow ();
	}

	void _CameraFollow()
	{
		m_pos = m_player.transform.position;
		m_pos.y = 0f;
		transform.position = m_pos + offset;
	}
}
