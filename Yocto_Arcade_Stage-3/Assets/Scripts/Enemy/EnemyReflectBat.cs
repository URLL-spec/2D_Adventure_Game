using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyReflectBat : MonoBehaviour
{
    public AIPath aiPath;
    //public AIDestinationSetter Ab;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f) // Если аи путь врага движется в положительных значениях 
        {
            transform.localScale = new Vector3(3.5f, 3.5f, 3.5f); // тогда поворачивать спрайт влево
        }
        else if (aiPath.desiredVelocity.x <= -0.01f) // иначе наоборот поворачивать вправо
        {
            transform.localScale = new Vector3(-3.5f, 3.5f, 3.5f);
        }


    }
}
