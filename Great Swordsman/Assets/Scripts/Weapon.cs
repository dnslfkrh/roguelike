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
    private float distanceFromPlayer = 2f;
    public WeaponType weaponType;
    public Transform player;
    public float rotationSpeed = 100f;
    private float startAngle;

    private void Update()
    {
        if (player != null)
        {
            float angle = startAngle + (Time.time * rotationSpeed);
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromPlayer, Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromPlayer, 0);
            transform.position = player.position + offset;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void ChangeRotationSpeed(string type, int value) // 무기마다 실행
    {
        if (type == "+")
        {
            rotationSpeed += value;
            Debug.Log("무기 회전 속도 증가: " + rotationSpeed);

        }
        else if (type == "/")
        {
            rotationSpeed /= value;
            Debug.Log("무기 회전 속도 감소: " + rotationSpeed);
        }
    }

    public void ChangeDistanceFromPlayer(float value)
    {
        distanceFromPlayer += value;
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
