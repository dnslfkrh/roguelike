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

    public void IncreaseAttackDamage(float value)
    {
        attackDamage += value;
        Debug.Log("공격력 증가: " + attackDamage);
    }

    public void IncreaseMoveSpeed(float value)
    {
        Debug.Log("증가 전 이동 속도: " + speed);
        speed += value;
        Debug.Log("이동 속도 증가: " +  speed);
    }
}
