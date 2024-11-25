using UnityEngine;

public class Player : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Vector2 inputVec;
    public float speed;
    public float attackDamage;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        attackDamage = 20.0f;
    }
    private void Start()
    {
        weaponManager.InitializeWeapons(transform);
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    public void ChangeAttackDamage(string type, int value)
    {
        Debug.Log("변경 전 공격력: " + attackDamage);

        if (type == "+")
        {
            attackDamage += value;
        }
        else if (type == "*")
        {
            attackDamage *= value;
        }

        // 최소 공격력
        if (attackDamage <= 0)
        {
            attackDamage = 10;
        }

        Debug.Log("공격력 변경: " + attackDamage);
    }

    public void IncreaseMoveSpeed(int value)
    {
        Debug.Log("증가 전 이동 속도: " + speed);
        speed += value;
        Debug.Log("이동 속도 증가: " + speed);
    }
}
