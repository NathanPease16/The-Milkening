using UnityEngine;

public class MilkAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject hurt;
    private MilkMovement movement;
    private MilkLevel milkLevel;
    public float damage;

    private void Awake()
    {
        movement = GetComponent<MilkMovement>();
        milkLevel = transform.parent.GetComponent<MilkLevel>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAI ai = other.gameObject.GetComponent<EnemyAI>();

            if (!ai.isLunging && movement.isLunging)
            {
                ai.Damage(damage);
                Instantiate(hurt, ai.transform.position, Quaternion.identity);
            }
            else if (!movement.isLunging)
                milkLevel.UpdateMilkContents(-ai.damage);
            else if (movement.isLunging && ai.isLunging)
            {
                Vector3 playerToEnemy = (ai.transform.position - transform.parent.position).normalized;
                Vector3 enemyToPlayer = (transform.parent.position - ai.transform.position).normalized;

                float playerDot = Vector3.Dot(playerToEnemy, movement.lungeDir.normalized);
                float enemyDot = Vector3.Dot(enemyToPlayer, ai.lungeDir.normalized);

                if (enemyDot < playerDot)
                {
                    ai.Damage(damage);
                    Instantiate(hurt, ai.transform.position, Quaternion.identity);
                    milkLevel.UpdateMilkContents(-ai.damage * .3f);
                }
                else if (playerDot < enemyDot)
                {
                    ai.Damage(damage * .3f);
                    Instantiate(hurt, ai.transform.position, Quaternion.identity);
                    milkLevel.UpdateMilkContents(-ai.damage);
                }
                else
                {
                    ai.Damage(damage);
                    Instantiate(hurt, ai.transform.position, Quaternion.identity);
                    milkLevel.UpdateMilkContents(-ai.damage);
                }

            }
        }
    }
}
