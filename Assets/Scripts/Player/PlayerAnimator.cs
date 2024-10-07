using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance;
    
    [SerializeField] private PlayerController _playerController;
    
    // Spine 2D animation reference
    [SerializeField] protected SkeletonAnimation skeletonAnimation;
    
    // Define animation references for movement and combat states
    [Header("Character Animation (Movements)")]
    [SerializeField] protected AnimationReferenceAsset walkAnimation;
    [SerializeField] protected AnimationReferenceAsset idleAnimation;
    [SerializeField] protected AnimationReferenceAsset jumpStartAnimation;
    [SerializeField] protected AnimationReferenceAsset jumpFallAnimation;
    [SerializeField] protected AnimationReferenceAsset jumpEndAnimation;
    [SerializeField] protected AnimationReferenceAsset deathAnimation;

    public bool isDead => currentAnimation == deathAnimation.name;
    public float deathDuration => deathAnimation.Animation.Duration + 2f;
    
    protected string currentAnimation;

    void OnEnable()
    {
        _playerController.OnJump += () => SetAnimation(jumpStartAnimation, false);
    }

    void OnDisable()
    {
        _playerController.OnJump = null;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentAnimation == deathAnimation.name)
        {
            return;
        }
        
        if (!_playerController.IsOnGround && _playerController.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            SetAnimation(jumpFallAnimation, false);
            return;
        }
        
        if (currentAnimation == jumpStartAnimation.name)
        {
            if (_playerController.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                SetAnimation(jumpFallAnimation, false);
            }
            return;
        }

        if (currentAnimation == jumpFallAnimation.name)
        {
            if (_playerController.IsOnGround)
            {
                SetAnimation(jumpEndAnimation, false);
            }
            return;
        }
        

        if (_playerController.PlayerMoveInput != 0 && _playerController.IsOnGround)
        {
            HandleMovementAnimation();
        }
        else
        {
            SetAnimation(idleAnimation, true);  // Loop idle animation
        }
    }
    
    /// <summary>
    /// Plays movement animations like Walk or Idle
    /// </summary>
    protected void HandleMovementAnimation()
    {
        bool isWalking = false;
        
        // Example movement logic (replace with actual movement conditions)
        if (_playerController.IsOnGround)
        { 
            isWalking =  _playerController.PlayerMoveInput == 0 ? false : true; 
        }
        
        if (isWalking)
        {
            SetAnimation(walkAnimation, true);  // Loop walk animation
        }
        else
        {
            SetAnimation(idleAnimation, true);  // Loop idle animation
        }
    }
    
    public void TriggerPlayerIdle()
    {
        SetAnimation(idleAnimation, false);
    }

    public void TriggerPlayerDeath()
    {
        SetAnimation(deathAnimation, false);
    }

    /// <summary>
    /// Sets the animation on the Spine SkeletonAnimation
    /// </summary>
    /// <param name="animation">The animation to play</param>
    /// <param name="loop">Whether to loop the animation</param>
    protected void SetAnimation(AnimationReferenceAsset animation, bool loop)
    {
        if (currentAnimation == animation.name) return;  // Prevent reapplying same animation

        skeletonAnimation.state.SetAnimation(0, animation, loop);
        currentAnimation = animation.name;
    }

}
