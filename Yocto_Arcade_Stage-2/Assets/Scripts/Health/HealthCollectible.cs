using UnityEngine;



public class HealthCollectible : MonoBehaviour
{
    [SerializeField]private float healthValue;
    //private Animator anim;
    //private void Awake() // При старте делаем текущее здоровье равным исходному т.е. startingHealth
    //{
    //    anim = GetComponent<Animator>();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Если коллайдер пересекает объект с тэгом Player тогда взять компонент из <Health> и использовать <AddHealth> для healthValue
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false); // Отключение предмета после взаимодействия
        }
    }

}
