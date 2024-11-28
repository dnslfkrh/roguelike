using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    public int initialWeaponCount;

    public GameObject[] weaponPrefabs;
    private List<GameObject> weapons = new List<GameObject>();

    public float knockbackForce = 5;

    public void InitializeWeapons(Transform playerTransform)
    {
        for (int i = 0; i < initialWeaponCount; i++)
        {
            float angleStep = 360f / initialWeaponCount;
            float angle = angleStep * i;

            CreateWeapon(angle, playerTransform);
        }
    }

    public void CreateWeapon(float angle, Transform playerTransform)
    {
        int prefabIndex = weapons.Count % weaponPrefabs.Length;
        if (weaponPrefabs.Length == 0 || weaponPrefabs[prefabIndex] == null)
        {
            return;
        }

        GameObject weapon = Instantiate(weaponPrefabs[prefabIndex]);

        weapon.transform.SetParent(transform);
        Weapon weaponComponent = weapon.GetComponent<Weapon>();
        if (weaponComponent == null)
        {
            Destroy(weapon);
            return;
        }

        weaponComponent.SetPlayer(playerTransform);
        weaponComponent.SetInitialAngle(angle); 

        weapons.Add(weapon);
    }

    public void IncreaseWeaponCount(float value) // 버그 수정 필요
    {
        Weapon playerWeapon = weapons.Count > 0 ? weapons[0].GetComponent<Weapon>() : null;
        Transform playerTransform = playerWeapon != null ? playerWeapon.player : null;

        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }

        float totalWeapons = weapons.Count + value;
        float angleStep = 360f / totalWeapons;

        for (int i = 0; i < totalWeapons; i++)
        {
            float angle = angleStep * i;
            CreateWeapon(angle, playerTransform);
        }
    }

    public void DecreaseWeaponCount()
    {
        if (weapons.Count > 0)
        {
            Destroy(weapons[weapons.Count - 1]);
            weapons.RemoveAt(weapons.Count - 1);
        }
    }


    public void ChangeWeapon(int index, int weaponPrefabIndex)
    {
        if (index < weapons.Count && weaponPrefabIndex < weaponPrefabs.Length)
        {
            Destroy(weapons[index]);
            CreateWeapon(weaponPrefabIndex, weapons[index].GetComponent<Weapon>().player);
        }
    }

    public void ChangeRotationSpeed(string type, int value)
    {
        foreach (GameObject weaponObj in weapons)
        {
            Weapon weapon = weaponObj.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.ChangeRotationSpeed(type, value);
            }
        }
    }

    public void ChangeDistanceFromPlayer(float value)
    {
        foreach (GameObject weaponObj in weapons)
        {
            Weapon weapon = weaponObj.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.ChangeDistanceFromPlayer(value);
            }
        }
    }

    public void IncreaseKnockbackForce()
    {
        knockbackForce += 5;
        Debug.Log("이제 적을 더 멀리 밀어냅니다: " + knockbackForce);
    }

    public void ChangeRotationDirection()
    {
        foreach (GameObject weaponObj in weapons)
        {
            Weapon weapon = weaponObj.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.ChangeRotationDirection();
            }
        }
    }
}
