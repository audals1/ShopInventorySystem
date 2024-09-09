using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Return
    }
    //한글주석테스트
    public float speed = 1.0f;
    public float detectionRange = 6.0f;
    public float returnRange = 10.0f;
    public float stopDistance = 2.0f;
    public Transform player;

    private MonsterState currentState = MonsterState.Idle;
    private Vector3 originalPosition;

    private Animator animator;

    void Start()
    {
        originalPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MonsterBehaviour();
        DetectPlayer();
    }

    void MonsterBehaviour()
    {
        switch (currentState)
        {
            case MonsterState.Idle:
                animator.SetBool("IsMove", false);
                break;
            case MonsterState.Chase:
                ChasePlayer();
                break;
            case MonsterState.Return:
                ReturnToOriginalPosition();
                break;
            default:
                break;
        }
    }

    void DetectPlayer()
    {
        float sqrDistanceToPlayer = (transform.position - player.position).sqrMagnitude;

        if (sqrDistanceToPlayer <= detectionRange * detectionRange)
        {
            currentState = MonsterState.Chase;
        }
        else if (sqrDistanceToPlayer > returnRange * returnRange)
        {
            currentState = MonsterState.Return;
        }
        else
        {
            currentState = MonsterState.Idle;
        }
    }

    void ChasePlayer()
    {
        float sqrDistanceToPlayer = (transform.position - player.position).sqrMagnitude;

        if (sqrDistanceToPlayer > stopDistance * stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += direction * speed * Time.deltaTime;
            animator.SetBool("IsMove", true);
        }
        else
        {
            currentState = MonsterState.Idle;
            animator.SetBool("IsMove", false);
        }
    }

    void ReturnToOriginalPosition()
    {
        Vector3 direction = (originalPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += direction * speed * Time.deltaTime;
        animator.SetBool("IsMove", true);

        if ((transform.position - originalPosition).sqrMagnitude < 0.1f * 0.1f)
        {
            transform.position = originalPosition;
            currentState = MonsterState.Idle;
            animator.SetBool("IsMove", false);
        }
    }
}
