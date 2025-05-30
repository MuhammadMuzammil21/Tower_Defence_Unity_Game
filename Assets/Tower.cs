using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackInterval = 1f;
    public int baseDamage = 1;
    public int upgradeCount = 0;

    private float attackTimer;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            AttackEnemyInRange();
        }
    }

    void AttackEnemyInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(GetDamage());
                    break;
                }
            }
        }
    }

    public int GetDamage()
    {
        float multiplier = 1f + (upgradeCount * 0.05f);
        SoundManager.Instance.PlayShoot();
        return Mathf.CeilToInt(baseDamage * multiplier);
    }

    public int GetUpgradeCost()
    {
        return 2 * (upgradeCount + 1);
    }

    public void Upgrade()
    {
        upgradeCount++;
        Debug.Log($"{gameObject.name} upgraded! New damage: {GetDamage()}");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
