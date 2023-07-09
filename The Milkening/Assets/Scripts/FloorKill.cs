using UnityEngine;

public class FloorKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<MilkLevel>().UpdateMilkContents(-100);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>().Damage(100);
        }
    }
}
