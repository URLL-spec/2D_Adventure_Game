using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDelayGame : MonoBehaviour
{
    //Скрипт для начального ролика
    public PlayerDamage Damege;
    public PlayerMove Moving;
    public CameraShake Shake;
    void Start()
    {
        Shake.enabled = false;
        Damege.enabled = false;
        Moving.enabled = false;
        Invoke("Delay", 46f);
    }

    void Delay()
    {
        Shake.enabled = true;
        Damege.enabled = true;
        Moving.enabled = true;
    }

}
