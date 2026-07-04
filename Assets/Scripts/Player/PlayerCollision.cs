using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float CollisionCooldown = 1f; 
    [SerializeField] float AdjustMoveSpeed = -2f;
    const string hitstring = "Hit";
    float CooldownTimer = 0f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    void Update()
    {
        CooldownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {

        if(CooldownTimer < CollisionCooldown) return;
        levelGenerator.ChangeChunkMoveSpeed(AdjustMoveSpeed);
        animator.SetTrigger(hitstring);
        CooldownTimer = 0f;
    }
}
