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
    private bool isIceUnlocked = false;
    private bool isFireUnlocked = false;

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

    public void ChangeRotationSpeed(string type, int value)
    {
        if (type == "+")
        {
            rotationSpeed += value;

        }
        else if (type == "/")
        {
            rotationSpeed /= value;
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

                if (isIceUnlocked && weaponType == WeaponType.Ice)
                {
                    enemy.SetState(new IceEffect(2f, 3f));
                }
                else if (isFireUnlocked && weaponType == WeaponType.Fire)
                {
                    enemy.SetState(new FireEffect(3f, 5f));
                }
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            BossEnemy boss = collision.GetComponent<BossEnemy>();
            if (boss != null)
            {
                boss.TakeDamage(GameManager.Instance.player.attackDamage);
            }
        }
    }

    public void UnlockIce()
    {
        isIceUnlocked = true;
    }

    public void UnlockFire()
    {
        isFireUnlocked = true;
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

    public void ChangeRotationDirection()
    {
        rotationSpeed = -rotationSpeed;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector3 scale = spriteRenderer.transform.localScale;
            scale.y = -scale.y;
            spriteRenderer.transform.localScale = scale;
        }
    }
}
