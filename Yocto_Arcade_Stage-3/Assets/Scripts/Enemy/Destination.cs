using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Destination : MonoBehaviour
{
    //—крипт, который назначает место назначени€ пути
    public Transform target; // ÷ель 1 т.е. точка где находитс€ спот противника (место его изначального положени€)
    public Transform target2; // ÷ель 2 т.е. сам персонаж за который будет происходить преследование 
    IAstarAI ai; //используетс€ дл€ управлени€ движением противника;
    public bool Checkbox = false;

    void OnEnable() //јктивирован 
    {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update; //  огда активирован каждый кадр будет следовать
    }

    void OnDisable() //Ќе активирован 
    {
        if (ai != null) ai.onSearchPath -= Update; //  огда не активирован каждый кадр будет сто€ть
    }

    void Update()
    {
        if (!Checkbox) ai.destination = target.position; // ≈сли checkbox = false тогда следовать за целью 1 иначе следовать за целью 2
        else ai.destination = target2.position;
    }
}
