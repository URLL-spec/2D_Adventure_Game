using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaggeActionStarting : MonoBehaviour
{
    public GameObject MessageStarting;

    // Start is called before the first frame update
    void Start()
    {
        MessageStarting.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MessageStarting.SetActive(true);
        }
    }
}
