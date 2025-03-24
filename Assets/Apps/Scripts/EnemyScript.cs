using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float AvoidanceRadius = 1.5f; // How far they avoid each other
    [SerializeField] private float AvoidanceForce = 2f; // Strength of avoidance movement

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // Find the player dynamically
    }

    private void Update()
    {
        if (Player == null) return; // Prevent errors if player is missing

        Vector2 direction = (Player.transform.position - transform.position).normalized;
        direction.Normalize();

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, MoveSpeed * Time.deltaTime);

        // Rotate to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    
}
