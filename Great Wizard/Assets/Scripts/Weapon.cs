using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Basic,
    Ice,
    Fire,
}

public class Weapon : MonoBehaviour
{
    private float distanceFromPlayer;
    public WeaponType weaponType;
    public Transform player;
    public float rotationSpeed = 100f;
    private float startAngle;

    private void Start()
    {
        distanceFromPlayer = 2f;
    }

    private void Update()
    {
        if (player != null)
        {
            float angle = startAngle + (Time.time * rotationSpeed);
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromPlayer, Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromPlayer, 0);
            transform.position = player.position + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(GameManager.Instance.player.attackDamage);
            }
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetInitialAngle(float angle)
    {
        startAngle = angle;
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromPlayer, Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromPlayer, 0);
        transform.position = player.position + offset;
    }
}
