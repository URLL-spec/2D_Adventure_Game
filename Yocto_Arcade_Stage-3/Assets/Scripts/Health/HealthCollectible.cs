using UnityEngine;



public class HealthCollectible : MonoBehaviour
{
    [SerializeField]private float healthValue;
    //private Animator anim;
    //private void Awake() // ��� ������ ������ ������� �������� ������ ��������� �.�. startingHealth
    //{
    //    anim = GetComponent<Animator>();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // ���� ��������� ���������� ������ � ����� Player ����� ����� ��������� �� <Health> � ������������ <AddHealth> ��� healthValue
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false); // ���������� �������� ����� ��������������
        }
    }

}
