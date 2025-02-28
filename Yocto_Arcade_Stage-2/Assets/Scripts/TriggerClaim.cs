using UnityEngine;

public class TriggerClaim : MonoBehaviour
{

    PlayerMove CharControl;
    BoxCollider2D box;

    void Start()
    {
        CharControl = GetComponentInParent<PlayerMove>();
        box = GetComponents<BoxCollider2D>()[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 12) { box.enabled = false; } // Если пересекаемый объект находится на слое под номером X то мы отключаем маленький колладер 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12) { box.enabled = true; } // Если объект не пересекается тогда включаем его обратно для зацепа 
    }

    private void OnCollisionEnter2D(Collision2D collision) // Метод будет вызываться когда произойдёт столкновение маленького колладера с другими объектами
    {
        if (collision.gameObject.layer == 12 && CharControl.rb.velocity.y <= 0) { CharControl.StartAnimLeadge(); } // Если объект с которым произошло столкновение находится под номером x
                                                                                                                  // И находится в состоянии падения т.е. CharControl.rb.velocity.y <= 0
                                                                                                                  // ТО запуск CharControl.StartAnimLeadge
    }

}
