using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Vector2 inputVec;
    public float speed;
    public float attackDamage;
    public bool isVampiric = false;
    public bool extraExp = false;
    private Vector2 lastNonZeroInputVec;
    public Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        attackDamage = 10.0f;
    }

    private void Start()
    {
        weaponManager.InitializeWeapons(transform);
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (inputVec != Vector2.zero)
        {
            lastNonZeroInputVec = inputVec.normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    public void ChangeAttackDamage(string type, int value)
    {
        Debug.Log("���� �� ���ݷ�: " + attackDamage);

        if (type == "+")
        {
            attackDamage += value;
        }
        else if (type == "*")
        {
            attackDamage *= value;
        }

        if (attackDamage <= 0)
        {
            attackDamage = 10;
        }

        Debug.Log("���ݷ� ����: " + attackDamage);
    }

    public void IncreaseMoveSpeed(int value)
    {
        Debug.Log("���� �� �̵� �ӵ�: " + speed);
        speed += value;
        Debug.Log("�̵� �ӵ� ����: " + speed);
    }

    public void Dash(float distance)
    {
        Vector2 dashDirection = inputVec == Vector2.zero ? lastNonZeroInputVec : inputVec.normalized;
        Vector3 dashVector = new Vector3(dashDirection.x, dashDirection.y, 0) * distance;
        Vector3 newPosition = transform.position + dashVector;

        StartCoroutine(PerformDash(newPosition));
    }

    private IEnumerator PerformDash(Vector3 targetPosition)
    {
        float dashDuration = 0.2f;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dashDuration;

            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.position = currentPosition;

            yield return null;
        }

        transform.position = targetPosition;
    }

    public void ChangeVampiric()
    {
        if (!isVampiric)
        {
            isVampiric = true;
        }
        else
        {
            isVampiric = false;
        }
    }

    public void GetExpFromOption()
    {
        GameManager.Instance.exp += 3;
        Debug.Log("����ġ ����");

        GameManager.Instance.CheckLevelUp();
    }

    public void ChangeExtraExp()
    {
        extraExp = true;
        Debug.Log("���ݺ��� �߰� ����ġ");
    }
}
