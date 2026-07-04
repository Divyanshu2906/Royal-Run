using UnityEngine;

public class Apple : Pickup
{

    [SerializeField] float AdjustMoveSpeed = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }
    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(AdjustMoveSpeed);
    }
}
