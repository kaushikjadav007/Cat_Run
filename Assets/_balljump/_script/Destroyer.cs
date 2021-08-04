using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.root.CompareTag("Platform"))
        {
            Debug.Log(other.transform.root.gameObject.name);

            other.transform.root.gameObject.SetActive(false);
        }

    }
}
