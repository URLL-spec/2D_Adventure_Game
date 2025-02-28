using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyReflectWolf : MonoBehaviour
{
    public AIPath aiPath;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f) // Если аи путь врага движется в положительных значениях 
        {
            transform.localScale = new Vector3(2f, 2f, 1f); // тогда поворачивать спрайт влево
        }
        else if (aiPath.desiredVelocity.x <= -0.01f) // иначе наоборот поворачивать вправо
        {
            transform.localScale = new Vector3(-2f, 2f, 1f);
        }
    }
}
