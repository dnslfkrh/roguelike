using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    public int initialWeaponCount;

    public GameObject[] weaponPrefabs;
    private List<GameObject> weapons = new List<GameObject>();

    public void InitializeWeapons(Transform playerTransform)
    {
        for (int i = 0; i < initialWeaponCount; i++)
        {
            CreateWeapon(i, playerTransform);
        }
    }

    public void CreateWeapon(int index, Transform playerTransform)
    {
        int prefabIndex = index % weaponPrefabs.Length;
        GameObject weapon = Instantiate(weaponPrefabs[prefabIndex]);

        float angle = 360f / initialWeaponCount * index;
        float distanceFromPlayer = 1.0f;

        weapon.transform.SetParent(transform);
        Weapon weaponComponent = weapon.GetComponent<Weapon>();
        weaponComponent.SetPlayer(playerTransform);
        weaponComponent.SetInitialAngle(angle);

        weapons.Add(weapon);
    }

    public void ChangeWeapon(int index, int weaponPrefabIndex)
    {
        if (index < weapons.Count && weaponPrefabIndex < weaponPrefabs.Length)
        {
            Destroy(weapons[index]);
            CreateWeapon(weaponPrefabIndex, weapons[index].GetComponent<Weapon>().player);
        }
    }

    public void IncreaseWeaponCount()
    {
        CreateWeapon(weapons.Count, weapons[0].GetComponent<Weapon>().player);
    }

    public void DecreaseWeaponCount()
    {
        if (weapons.Count > 0)
        {
            Destroy(weapons[weapons.Count - 1]);
            weapons.RemoveAt(weapons.Count - 1);
        }
    }
}
