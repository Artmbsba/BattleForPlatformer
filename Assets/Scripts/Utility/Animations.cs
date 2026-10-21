using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animations : MonoBehaviour
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int IsJumpingHas = Animator.StringToHash("IsJumping");
    private readonly int HitHash = Animator.StringToHash("Hit");
    private readonly int KickHash = Animator.StringToHash("Kick");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(SpeedHash, speed);
    }

    public void SetJumping(bool isJumping)
    {
        _animator.SetBool(IsJumpingHas,isJumping);
    }

    public void TriggerHit()
    {
        _animator.SetTrigger(HitHash);
    }

    public void TriggerKick()
    {
        _animator.SetTrigger(KickHash);
    }
}
