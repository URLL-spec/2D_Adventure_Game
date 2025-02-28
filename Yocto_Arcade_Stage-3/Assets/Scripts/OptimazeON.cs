using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimazeON : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    public GameObject obj8;
    public GameObject obj9;
    public GameObject obj10;
    public GameObject obj11;
    public GameObject obj12;
    public GameObject obj13;
    public GameObject obj14;
    public GameObject obj15;
    public GameObject obj16;
    public GameObject obj17;
    public GameObject obj18;
    public GameObject obj19;
    public GameObject obj20;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            obj.SetActive(true);
            obj1.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(true);
            obj4.SetActive(true);
            obj5.SetActive(true);
            obj6.SetActive(true);
            obj7.SetActive(true);
            obj8.SetActive(true);
            obj9.SetActive(true);
            obj10.SetActive(true);
            obj11.SetActive(true);
            obj12.SetActive(true);
            obj13.SetActive(true);
            obj14.SetActive(true);
            obj15.SetActive(true);
            obj16.SetActive(true);
            obj17.SetActive(true);
            obj18.SetActive(true);
            obj19.SetActive(true);
            obj20.SetActive(true);
        }
    }
}
