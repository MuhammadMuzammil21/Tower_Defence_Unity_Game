using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = new Vector3(5, 5, 0);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GameManager.Instance.EnemyReachedBase();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameManager.Instance.EnemyDefeated();
            SoundManager.Instance.PlayDeath();
            Destroy(gameObject);
        }
    }
}
