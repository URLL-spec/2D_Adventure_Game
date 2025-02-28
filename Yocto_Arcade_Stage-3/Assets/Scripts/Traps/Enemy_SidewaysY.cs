using UnityEngine;


public class Enemy_SidewaysY : MonoBehaviour
{
    [SerializeField] private float damage; // [SerializeField]. Позволяет сериализовать переменные вне зависимости от их области видимости.
                                           //                   Очень полезный атрибут, который позволяет сделать все переменные класса приватными, 
                                           //                   но настраиваемыми в инспекторе.
    [SerializeField] private float movementDistance;   // Расстояние движения 
    [SerializeField] private float speed;
    private bool movingleft;
    private float leftEdge;
    private float rightEdge;
    private void Awake()
    {
        leftEdge = transform.position.y - movementDistance; // Как ось координат, левые значение означают что объект, условно, на -5,
        rightEdge = transform.position.y + movementDistance; // а правае значения в плюсе т.е. просто 5
    }
    // Всё зависит от нынешнего положения объекта и переменной movementDistance т.е. переменной, которая задаёт на сколько пунктов нужно сдинутся
    private void Update()
    {
        if (movingleft) // Если он движется вправо тогда мы проверим 
        {
            if (transform.position.y > leftEdge) // больше ли позиция объекта чем левый край (Нынешняя позиция объекта это значение по X в строке Position в инспекторе Unity)
            {
                transform.position = new Vector3(transform.position.x , transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else // иначе объект не двигается влево 
                movingleft = false;
        }
        else
        {
            if (transform.position.y < rightEdge) // Тоже самое только с правым краем
            {
                transform.position = new Vector3(transform.position.x , transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                movingleft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
