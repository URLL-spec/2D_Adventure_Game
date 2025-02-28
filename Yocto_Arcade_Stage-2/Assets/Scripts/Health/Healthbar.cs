// Скрипт отвечает за обновление изображении, которые отображают здоровье. 
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // Ссылка на скрипт ХП игрока 
    [SerializeField] private Image totalhealthBar; // Общее
    [SerializeField] private Image currenthealthBar; // Текущее 

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 5;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 5; // Проще говоря, здесь создание формулы благодаря которой можно будет отображать текущее состояние ХП
                                                                       // уже в самой игре 
    }
}
 