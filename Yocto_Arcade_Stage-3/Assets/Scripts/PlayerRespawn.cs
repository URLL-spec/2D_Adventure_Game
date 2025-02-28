using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint; //объявление переменной, которая будет хранить позицию последнего тронутого чекпоинта
    private Health playerHealth; //объявление переменной, которая будет ссылаться на скрипт "Health", отвечающий за здоровье игрока

    private void Awake() //метод, который вызывается до работы метода Start() и используется для инициализации объекта в момент создания
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn() //метод, который вызывается при переходе игрока через чекпоинт;
    {
        transform.position = currentCheckpoint.position; // Перемещает игрока на позицию чекпоинта
        // Восстанавливает здоровья игрока и сбрасывает анимации
        playerHealth.Respawn();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //Последний тронутый чекпоинт сохраняется для дальнейшего использования
            collision.GetComponent<Collider2D>().enabled = false; //После активации бокс выключается
        }    
    }

}
