using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyExitDamageWolf : MonoBehaviour
{
    // Скрипт для немгновенного поворота волка к персонажу 
    public AIPath aiPath;
    public EnemyReflectWolf enemyreflectwolf;
    public Animator anim;
    public BoxCollider2D enemydamagewolf;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemydamagewolf.enabled = false;
            anim.SetTrigger("wolfexitdamage");
            StartCoroutine(timeObj1());
            aiPath.enabled = false;
            enemyreflectwolf.enabled = false;
        }
        IEnumerator timeObj1() // Метод где описывается корутин
        {
            yield return new WaitForSeconds(1f); // Время спустя которое волк снова обратит внимание на игрока 
            enemydamagewolf.enabled = true;
            aiPath.enabled = true; // Включение аи
            enemyreflectwolf.enabled = true; // Включение поворота волка
        }
    }
    
}
