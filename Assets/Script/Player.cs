//using Cinemachine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class Player : MonoBehaviour
//{
//    #region Singleton
//    private static Player _instance;
//    public static Player Instance => _instance;

//    private void Awake()
//    {
//        if (_instance != null)
//        {
//            Destroy(this);
//        }
//        else
//            _instance = this;
//      if(container!=null)  
//            {enemy3s = new GameObject[container.transform.childCount];
//            for (int i = 0; i < container.transform.childCount; i++)
//            {
//                enemy3s[i] = container.transform.GetChild(i).gameObject;
//            }
//        }
//    }

//    #endregion Singleton
//    [SerializeField]
//    private PhysicsMaterial2D noFriction;
//    [SerializeField]
//    private PhysicsMaterial2D fullFriction;

//    [SerializeField]
//    private float movementSpeed;
//    [SerializeField]
//    private float jumpForce;
//    [SerializeField]
//    private float GroundCheckRadius;
//    [SerializeField]
//    private float wallCheckDistance;
//    [SerializeField]
//    private float wallSlidingSpeed;
//    [SerializeField]
//    private float timeDoubleClick;

//    [SerializeField]
//    private Transform groundCheck;
//    [SerializeField]
//    private Transform wallCheck;
//    [SerializeField]
//    private Transform ledgeCheck;
//    [SerializeField]
//    private Transform jumpCheck;

//    [SerializeField]
//    private LayerMask WhatIsGround;
//    [SerializeField] GameObject container;
//    private GameObject[] enemy3s=null;

//    private float leftCount, rightCount;
//    private float xInput;
//    private float jumpCount;
//    private float stunCount;
//    private float stunHeadCount;

//    private bool isWallSliding;
//    private bool isGrounded;
//    private bool isTouchingWall;
//    private bool isLedge;
//    private bool isJump;
//    private bool isStun;
//    private bool isWallJump;
//    private bool isReadyJump;
//    private bool isAbleControl;
//    private string anim;

//    private int facingDirection = 1;

//    private Vector2 newVelocity;

//    private Rigidbody2D rb;

//    [SerializeField]private CinemachineVirtualCamera mCam;
//    private float mShakeTime = 0.2f;
//    private float ShakeIntensity = 3f;
//    private float timer;

//    private Animator animator;
//    [SerializeField] private Animator stunAnimator;

//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//        animator.SetTrigger("idle");
//        anim = "idle";

//        StopShake();
//        isAbleControl = true;
//        isWallJump = false;
//        isReadyJump = false;
//        isJump = false;
//        rb = GetComponent<Rigidbody2D>();
//        isStun = false;
//        stunCount = stunHeadCount=0;
//        if (rb != null)
//        {
//            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
//        }
//    }

//    private void Update()
//    {

//        CheckIfWallSliding();
//       if(leftCount>=0) leftCount -= Time.deltaTime;
//        if (rightCount >= 0) rightCount -= Time.deltaTime;
//        if (jumpCount >= 0) jumpCount -= Time.deltaTime;
//        if (stunCount >= 0) stunCount -= Time.deltaTime;
//        if (stunHeadCount >= 0) stunHeadCount -= Time.deltaTime;
//        else { stunAnimator.SetTrigger("not"); }
//        if (timer > 0)
//        {
//            timer -= Time.deltaTime;
//            if (timer <= 0) StopShake();
//        }
//    }

//    private void FixedUpdate()
//    {


//            ApplyMovement();
//        CheckSurroundings();
//    }
//    public void Jump()
//    {
//        animator.SetTrigger("jump");
//        anim = "jump";
//        AudioManager.Instance.PlaySFX("jump");
//        newVelocity.Set(movementSpeed*facingDirection*2,jumpForce);
//        rb.velocity = newVelocity;
//        isJump = true;
//        jumpCount = 0.3f;
//    }
//    private void ApplyMovement() {

//        if (isStun)
//        {
//            if(stunCount<=0) { isStun=false; }
//            else return; 
//        }
//        else
//        {
//            isAbleControl = true;
//        }
//        if(Mathf.Abs(xInput)==2 &&isTouchingWall&&!isLedge) {
//          if(jumpCount <= 0)
//            {
//                Stun();
//                headPound();
//                return; 
//            }
//          else
//            {
//                xInput = facingDirection;
//                newVelocity.Set(rb.velocity.x, movementSpeed * Mathf.Abs(xInput));
//                rb.velocity = newVelocity;
//                anim = "wall";
//                animator.SetTrigger("wall");
//                return;
//            }
//        }
//        if (isJump)
//        {

//            if (jumpCount <= 0 || isGrounded) 
//            { 
//                isJump = false;
//                isWallJump = false;
//                if(xInput==0)
//                {
//                    animator.SetTrigger("idle");
//                    anim = "idle";
//                }
//                else
//                {
//                    animator.SetTrigger("run");
//                    anim = "run";
//                }
//                return; 
//            }
//            newVelocity.Set(movementSpeed * facingDirection*1.5f, rb.velocity.y);
//            rb.velocity = newVelocity;
//        }
//       else if (isReadyJump && isGrounded && Mathf.Abs(xInput)==2){ Jump(); return; }
//        if(!isWallJump)
//        {
//            if (isWallSliding && isLedge&& xInput!=0)
//            {
//                animator.SetTrigger("walk");
//                anim = "walk";
//                transform.position = new Vector2(transform.position.x + 0.1f * facingDirection * transform.localScale.x, transform.position.y + 1f);
//                return;
//            }

//            if (xInput == 0.0f) rb.sharedMaterial = fullFriction;  else rb.sharedMaterial = noFriction;
//            newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
//            rb.velocity = newVelocity;

//            if (isTouchingWall)
//            {
//                newVelocity.Set(rb.velocity.x, movementSpeed*Mathf.Abs(xInput));
//                rb.velocity = newVelocity;
//                if (xInput==0 &&anim!="slide"&&!isGrounded)
//                {

//                    anim = "slide";
//                    animator.SetTrigger("slide");
//                }
//                if(Mathf.Abs(xInput)==1 && anim!="wall" && anim!="idle")
//                {
//                    anim = "wall";
//                    animator.SetTrigger("wall");
//                }
//            }
//            if ((anim=="slide" && isGrounded))
//            {
//                animator.SetTrigger("idle");
//                anim = "idle";
//                return;
//            }
//        }

//    }
//    IEnumerator GameOver()
//    {
//        // Yield for 0.5 seconds
//        yield return new WaitForSeconds(3f);

//        // Call the Check() function after the delay
//        SenceController.Instance.LoadSence("LevelSelectionMap");
//    }

//    public void death()
//    {
//        isAbleControl = false;
//        ShakeCamera();

//        stunHeadCount = 3f;
//        stunCount = 10f;
//        stunAnimator.SetTrigger("stun");
//        animator.SetTrigger("death");
//        newVelocity.Set(0, 20f);
//        rb.velocity = newVelocity;
//        AudioManager.Instance.PlaySFX("gameOver");
//        StartCoroutine(GameOver());
//    }

//    public void StunHead()
//    {
//        Stun();
//        stunAnimator.SetTrigger("stun");
//        if(stunHeadCount<=0) stunHeadCount = 3f;
//        else death();
//        LvManager.Instance.penalty();
//    }
//    public void Stun()
//    {
//        animator.SetTrigger("stun");
//        anim = "stun";
//        AudioManager.Instance.PlaySFX("hurt");
//        ShakeCamera();
//        xInput = 0;
//        leftCount = -1;
//        rightCount = -1;
//        isStun = true;
//        isAbleControl = false;
//        stunCount = 0.5f;
//        newVelocity.Set(-facingDirection*1f, 10f);
//        rb.velocity = newVelocity;
//    }
//    public void headPound()
//    {
//        if(enemy3s!=null)
//        {for(int i = 0;i<enemy3s.Length;i++)
//        {

//            if(enemy3s[i]!=null) if(Vector3.Distance(this.transform.position, enemy3s[i].transform.position)<=22f) enemy3s[i].GetComponent<enemy3>().PlayStun();
//        }
//           if(boss.Instance!=null) boss.Instance.PlayDeath();
//        }
//    }
//    private void Flip(float angle)
//    {

//        transform.Rotate(0.0f, angle, 0.0f);
//    }
//    #region Clicking
//    public void leftClick()
//    {
//        if(isAbleControl)
//        {
//            if (isWallSliding && !isGrounded && facingDirection == 1)
//            {
//                xInput = -1;
//                Flip(180);
//                facingDirection = -1;
//              Jump(); isWallJump = true; 
//            }
//            if (!isWallSliding || isGrounded || xInput != 0 || facingDirection == 1)
//            {
//                xInput = -1;
//                animator.SetTrigger("walk");
//                anim = "walk";
//                if (facingDirection == 1)
//                {
//                    if (xInput == 2) xInput = 0;
//                    else
//                    {
//                        Flip(180);
//                        facingDirection *= -1;
//                        animator.SetTrigger("walk");
//                        anim = "walk";
//                        xInput = -1;
//                    }

//                }
//                if (leftCount > 0 && isGrounded)
//                {
//                    AudioManager.Instance.PlaySFX("run");
//                    xInput = -2;
//                    animator.SetTrigger("run");
//                    anim = "run";
//                }

//            }
//        }
//    }
//    public void rightClick()
//    {
//        if (isAbleControl)
//        {
//            if (isWallSliding && !isGrounded && facingDirection == -1)
//            {
//                xInput = 1;
//                Flip(180);
//                facingDirection *= -1;
//                Jump(); isWallJump = true;
//            }
//            if (!isWallSliding || isGrounded || xInput != 0 || facingDirection == -1)
//            {
//                xInput = 1;
//                animator.SetTrigger("walk");
//                anim = "walk";
//                if (facingDirection == -1)
//                {
//                    if (xInput == -2) xInput = 0;
//                    else
//                    {
//                        Flip(180);
//                        facingDirection *= -1;
//                        animator.SetTrigger("walk");
//                        anim = "walk";
//                        xInput = 1;
//                    }
//                }

//                if (rightCount > 0 && isGrounded)
//                {
//                    animator.SetTrigger("run");
//                    anim = "run";
//                    AudioManager.Instance.PlaySFX("run");
//                    xInput = 2;
//                }

//            }
//        }

//    }
//    public void leftUp()
//    {
//        if (anim != "slide")  
//        { 
//            animator.SetTrigger("idle");
//            anim = "idle";
//        }
//        xInput = 0;
//        leftCount = timeDoubleClick;
//    }
//    public void rightUp()
//    {

//        if (anim != "slide")
//        {
//            animator.SetTrigger("idle");
//            anim = "idle";
//        }
//        xInput = 0;
//        rightCount=timeDoubleClick;
//    }

//    #endregion
//    #region Checking
//    private void CheckIfWallSliding()
//    {
//        if(isTouchingWall && !isGrounded )
//        {
//            isWallSliding = true;
//        }
//        else
//        {
//            isWallSliding = false;
//        }
//    }
//    private void CheckSurroundings()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, WhatIsGround);

//        isReadyJump = !Physics2D.Raycast(jumpCheck.position, -transform.up, wallCheckDistance+0.5f,WhatIsGround);
//        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
//        isLedge = !Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, WhatIsGround);


//    }
//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(groundCheck.position, GroundCheckRadius);


//        Gizmos.DrawLine(new Vector3(jumpCheck.position.x, jumpCheck.position.y - wallCheckDistance, jumpCheck.position.z),jumpCheck.position);
//        Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x+wallCheckDistance,wallCheck.position.y,wallCheck.position.z));
//        Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));

//    }
//    #endregion
//    public void ShakeCamera()
//    {
//        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//        mMultiChannelPerlin.m_AmplitudeGain = ShakeIntensity;
//        timer = mShakeTime;
//    }
//    public void StopShake()
//    {
//        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//        mMultiChannelPerlin.m_AmplitudeGain = 0f;
//        timer = 0f;
//    }
//    public void DisControlable()
//    {
//        isAbleControl = false;
//    }
//    public void ControlAble()
//    {
//        isAbleControl = true;
//    }
//}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region Singleton
    private static Player _instance;
    public static Player Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
            _instance = this;
        if (container != null)
        {
            enemy3s = new GameObject[container.transform.childCount];
            for (int i = 0; i < container.transform.childCount; i++)
            {
                enemy3s[i] = container.transform.GetChild(i).gameObject;
            }
        }
    }

    #endregion Singleton
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float GroundCheckRadius;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private float wallSlidingSpeed;
    [SerializeField]
    private float timeDoubleClick;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform jumpCheck;

    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField] GameObject container;
    private GameObject[] enemy3s = null;

    private float leftCooldown, rightCooldown;
    private float xInput;
    private float jumpCooldown;
    private float stunHeadCooldown;

    private bool isWallSliding;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isLedge;
    // private bool isJump;
    // private bool isWallJump;
    private bool isReadyJump;
    public bool isAbleControl;
    private string state;

    private int facingDirection = 1;

    private Vector2 newVelocity;

    private Rigidbody2D rb;

    [SerializeField]
    private CinemachineVirtualCamera mCam;
    private float mShakeTime = 0.2f;
    private float ShakeIntensity = 3f;

    private Animator animator;
    [SerializeField]
    private Animator stunAnimator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("idle");
        state = "idle";
        xInput = 0;
        isAbleControl = true;
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

    private void Update()
    {
        CheckIfWallSliding();
        UpdateTimers();
    }
    private void UpdateTimers()
    {
        if (leftCooldown >= 0) leftCooldown -= Time.deltaTime;
        if (rightCooldown >= 0) rightCooldown -= Time.deltaTime;
        if (jumpCooldown >= 0) jumpCooldown -= Time.deltaTime;
        if (stunHeadCooldown >= 0) stunHeadCooldown -= Time.deltaTime;
        else { stunAnimator.SetTrigger("not"); }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    public void Jump()
    {
        Debug.Log("Jump");
        animator.SetTrigger("jump");
        AudioManager.Instance.PlaySFX("jump");
        newVelocity.Set(movementSpeed * facingDirection * 2, jumpForce);
        rb.velocity = newVelocity;
        jumpCooldown = 0.3f;
    }
    private void ApplyMovement()
    {
        if (xInput == 0.0f) rb.sharedMaterial = fullFriction; else rb.sharedMaterial = noFriction;

        if (state == "walk")
        {
            
            newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
            rb.velocity = newVelocity;
            if (isTouchingWall)
            {
                xInput = facingDirection;
                state = "wall";
                animator.SetTrigger("wall");
            }
            return;
        }
        if (state == "run")
        {
            // nhảy
            if (isReadyJump && isGrounded)
            {
                Jump();
                state = "jump";
                return;
            }
            //đụng tường
            if (isTouchingWall && !isLedge)
            {
                if (jumpCooldown <= 0)
                {
                    StartCoroutine(Stun());
                    headPound();
                    return;
                }
                else
                {
                    xInput = facingDirection;
                    newVelocity.Set(rb.velocity.x, movementSpeed * Mathf.Abs(xInput));
                    rb.velocity = newVelocity;
                    state = "wall";
                    animator.SetTrigger("wall");

                    return;
                }
            }
            newVelocity.Set(movementSpeed * xInput, rb.velocity.y);
            rb.velocity = newVelocity;
            return;
        }
        if (state == "walljump")
        {
            newVelocity.Set(movementSpeed * facingDirection * 1.5f, rb.velocity.y);
            rb.velocity = newVelocity;
            if (isTouchingWall)
            {
                state = "wall";
                animator.SetTrigger("wall");
            }
            xInput *= 1.5f;
            return;
        }
        if (state == "wall")
        {
            if (xInput == 0 && !isGrounded)
            {

                state = "slide";
                animator.SetTrigger("slide");
                return;
            }
            newVelocity.Set(rb.velocity.x, movementSpeed * Mathf.Abs(xInput));
            rb.velocity = newVelocity;
            //nhảy lên để đi thẳng
            if (isLedge && xInput != 0)
            {
                animator.SetTrigger("walk");
                state = "walk";
                transform.position = new Vector2(transform.position.x + 0.1f * facingDirection * transform.localScale.x, transform.position.y + 1f);
                return;
            }
            return;
        }
        if (state == "jump")
        {
            if (jumpCooldown <= 0 || isGrounded)
            {
                if (xInput == 0)
                {
                    animator.SetTrigger("idle");
                    state = "idle";
                }
                else
                {
                    animator.SetTrigger("run");
                    state = "run";
                }
                return;
            }
            newVelocity.Set(movementSpeed * facingDirection * 1.5f, rb.velocity.y);
            rb.velocity = newVelocity;
            return;
        }
        if ((state == "slide" ))
        {
            if(isGrounded)
            {
                animator.SetTrigger("idle");
                state = "idle";
                return;
            }
            if(isTouchingWall)
            {
                newVelocity.Set(rb.velocity.x, 0);
                rb.velocity = newVelocity;
                return;
            }
            animator.SetTrigger("idle");
            state = "idle";
            return;
        }
    }
    IEnumerator GameOver()
    {
        // Yield for 0.5 seconds
        yield return new WaitForSeconds(3f);

        // Call the Check() function after the delay
        SenceController.Instance.LoadSence("LevelSelectionMap");
    }

    public void death()
    {
        isAbleControl = false;
        ShakeCamera();
        stunAnimator.SetTrigger("stun");
        animator.SetTrigger("death");
        newVelocity.Set(0, 20f);
        rb.velocity = newVelocity;
        AudioManager.Instance.PlaySFX("gameOver");
        StartCoroutine(GameOver());
    }

    public void StunHead()
    {
        StartCoroutine(Stun());
        stunAnimator.SetTrigger("stun");
        if (stunHeadCooldown <= 0) stunHeadCooldown = 3f;
        else death();
        LvManager.Instance.penalty();
    }
    IEnumerator Stun()
    {
        animator.SetTrigger("stun");
        state = "stun";
        AudioManager.Instance.PlaySFX("hurt");
        StartCoroutine(ShakeCamera());
        xInput = 0;
        leftCooldown = -1;
        rightCooldown = -1;
        isAbleControl = false;
        newVelocity.Set(-facingDirection * 1f, 10f);
        rb.velocity = newVelocity;
        yield return new WaitForSeconds(1f);
        isAbleControl = true;
    }
    public void headPound()
    {
        if (enemy3s != null)
        {
            for (int i = 0; i < enemy3s.Length; i++)
            {

                if (enemy3s[i] != null) if (Vector3.Distance(this.transform.position, enemy3s[i].transform.position) <= 22f) enemy3s[i].GetComponent<enemy3>().PlayStun();
            }
            if (boss.Instance != null) boss.Instance.PlayDeath();
        }
    }
    private void Flip(float angle)
    {

        transform.Rotate(0.0f, angle, 0.0f);
    }
    #region Clicking
    public void leftClick()
    {
        if (isAbleControl)
        {
            //if (isWallSliding && facingDirection == 1)
            //{
            //    xInput = -1;
            //    Flip(180);
            //    facingDirection = -1;
            //    Jump();
            //    state = "walljump";
            //}
            //if (xInput == 0 && isTouchingWall && facingDirection == 1)
            //{
            //    Flip(180);
            //    xInput = -1;
            //    facingDirection = -1;
            //    animator.SetTrigger("walk");
            //    state = "walk";
            //}

            //if (!isWallSliding || isGrounded || xInput != 0 || facingDirection == 1)
            //{
            //    xInput = -1;
            //    animator.SetTrigger("walk");
            //    state = "walk";
            //    if (facingDirection == 1)
            //    {
            //        if (xInput == 2) xInput = 0;
            //        else
            //        {
            //            Flip(180);
            //            facingDirection *= -1;
            //            animator.SetTrigger("walk");
            //            state = "walk";
            //            xInput = -1;
            //        }

            //    }
            //    if (leftCooldown > 0 && isGrounded)
            //    {
            //        AudioManager.Instance.PlaySFX("run");
            //        xInput = -2;
            //        animator.SetTrigger("run");
            //        state = "run";
            //    }

            //}
            if (isWallSliding && !isGrounded && facingDirection == 1)
            {
                xInput = -1;
                Flip(180);
                facingDirection = -1;
                Jump();
                state = "walljump";
                return;
            }
            if (!isWallSliding || isGrounded || xInput != 0 || facingDirection == 1)
            {
                xInput = -1;
                animator.SetTrigger("walk");
                state = "walk";
                if (facingDirection == 1)
                {
                    if (xInput == 2) xInput = 0;
                    else
                    {
                        Flip(180);
                        facingDirection *= -1;
                        animator.SetTrigger("walk");
                        state = "walk";
                        xInput = -1;
                    }

                }
                if (leftCooldown > 0 && isGrounded)
                {
                    AudioManager.Instance.PlaySFX("run");
                    xInput = -2;
                    animator.SetTrigger("run");
                    state = "run";
                }
            }
        }
    }
    public void rightClick()
    {
        if (isAbleControl)
        {
            if (isWallSliding && facingDirection == -1)
            {
                xInput = 1;
                Flip(180);
                facingDirection *= -1;
                Jump();
                state = "walljump";
                return ;
            }
            if (!isWallSliding || isGrounded || xInput != 0 || facingDirection == -1)
            {
                xInput = 1;
                animator.SetTrigger("walk");
                state = "walk";
                if (facingDirection == -1)
                {
                    if (xInput == -2) xInput = 0;
                    else
                    {
                        Flip(180);
                        facingDirection *= -1;
                        animator.SetTrigger("walk");
                        state = "walk";
                        xInput = 1;
                    }
                }

                if (rightCooldown > 0 && isGrounded)
                {
                    animator.SetTrigger("run");
                    state = "run";
                    AudioManager.Instance.PlaySFX("run");
                    xInput = 2;
                }

            }
        }
    }
    public void leftUp()
    {
        if (state != "wall")
        {
            animator.SetTrigger("idle");
            state = "idle";
        }
        xInput = 0;
        leftCooldown = timeDoubleClick;
    }
    public void rightUp()
    {

        if (state != "wall")
        {
            animator.SetTrigger("idle");
            state = "idle";
        }
        xInput = 0;
        rightCooldown = timeDoubleClick;
    }

    #endregion
    #region Checking
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, WhatIsGround);

        isReadyJump = !Physics2D.Raycast(jumpCheck.position, -transform.up, wallCheckDistance + 0.5f, WhatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
        isLedge = !Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, WhatIsGround);


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, GroundCheckRadius);


        Gizmos.DrawLine(new Vector3(jumpCheck.position.x, jumpCheck.position.y - wallCheckDistance, jumpCheck.position.z), jumpCheck.position);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckDistance, ledgeCheck.position.y, ledgeCheck.position.z));

    }
    #endregion
    IEnumerator ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin mMultiChannelPerlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mMultiChannelPerlin.m_AmplitudeGain = ShakeIntensity;
        yield return new WaitForSeconds(mShakeTime);
        mMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

    public void DisControlable()
    {
        isAbleControl = false;
    }
    public void ControlAble()
    {
        isAbleControl = true;
    }
}

