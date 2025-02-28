using UnityEngine;
//Этот код отвечает за эффект параллакса и движение фона относительно камеры в игре Unity 2D. 
//Это позволяет создать впечатление движения объекта с разной скоростью, что даёт эффект приемлемой глубины в игре.
public class Parallax : MonoBehaviour
{
    public GameObject cam; //определяет камеру, относительно которой будет двигаться задний план.

    float stratRox;
    public float parallax; //определяет уровень параллакса и насколько быстро будет двигаться задний план по отношению к камере.
    float startPosX; //задает изначальную позицию объекта по оси X

    void Start()
    {
        startPosX = transform.position.x;
    }

    void Update()
    {
        float distX = (cam.transform.position.x * (1 - parallax)); //вычисляется как произведение текущей позиции камеры на (1 - parallax),
                                                                   //то есть, чем больше значение parallax, тем меньше параллакс эффект,
                                                                   //а задний план будет двигаться медленнее относительно камеры.
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z); //Затем transform.position задает новую позицию объекта
                                                                                                         //относительно координат начальной позиции по оси X. 

    }
}
