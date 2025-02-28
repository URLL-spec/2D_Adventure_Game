using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FButton : MonoBehaviour
{
    public GameObject Wall;
    public GameObject TriggerAnimExit;
    public GameObject SpriteF;
    public GameObject Notice;

    public GameObject start;
    public PlayerMove Animation;

    private UnityEngine.Object explosion;
    [SerializeField] private AudioSource Tap;

    private bool checkactive = false;

    void Start()
    {
        explosion = Resources.Load("Explosion");
        start.SetActive(false);
    }

    void Update()
    {
        Active();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpriteF.SetActive(true);
            checkactive = true;
        }
    }

    void Active()
    {
        if (Input.GetKeyDown(KeyCode.F) && checkactive)
        {
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Animation.StartAnimDialoge();
            TriggerAnimExit.SetActive(true);
            Wall.SetActive(false);
            Tap.Play();
            start.SetActive(true);
            Notice.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            SpriteF.SetActive(false);
            checkactive = false;
        }
    }
}
