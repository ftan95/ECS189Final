using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenryMovement : MonoBehaviour
{
    private float DistanceToGround = 1.285f;
    // Start is called before the first frame update

    [SerializeField] private float Speed = 10.0f;

    [SerializeField] private float AttackDuration = 0.5f;
    [SerializeField] private AnimationCurve Attack;

    [SerializeField] private float DecayDuration = 0.25f;
    [SerializeField] private AnimationCurve Decay;

    [SerializeField] private float SustainDuration = 5.0f;
    [SerializeField] private AnimationCurve Sustain;

    [SerializeField] private float ReleaseDuration = 0.25f;
    [SerializeField] private AnimationCurve Release;
    [SerializeField] private float RopeLengthChangeAmount = 50.0f;

    private float AttackTimer;
    private float DecayTimer;
    private float SustainTimer;
    private float ReleaseTimer;

    private float InputDirection = 0.0f;

    private enum Phase { Attack, Decay, Sustain, Release, None };

    private Phase CurrentPhase;
    private bool LeftAndRightPressed = false;
    private float PrevInputDirection = 0.0f;
    private GrapplingHook _GrapplingHook;


    float rotationAngle = 0;
    float smoothTime = 0.1f;
    Quaternion PrevRotation;
    public Animator animator;



    void Start()
    {

        // PrevRotation = this.gameObject.transform.rotation;
        this.CurrentPhase = Phase.None;
        this.DistanceToGround = this.GetComponent<BoxCollider2D>().bounds.extents.y;
        _GrapplingHook = this.GetComponent<GrapplingHook>();
    }

    // Function to detect if we've hit something, then stop jump animation
    private void OnCollisionEnter2D(Collision2D collision)
    {

        animator.SetBool("IsJumping", false);
    }

    // Update is called once per frame
    void Update()
    {

        // var body = this.gameObject.GetComponent<Rigidbody2D>();
        // body.bodyType = RigidbodyType2D.
        //  Quaternion desiredRotation = Quaternion.Euler(0, 0, rotationAngle);
        Quaternion desiredRotation = Quaternion.Euler(0, 0, rotationAngle);
        //if (this.gameObject.transform.rotation.eulerAngles.y > desiredRotation.eulerAngles.y)
        // {
        //     Debug.Log("Lerping back to desired rotation");
        // this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothTime);
        // }


        /* Quaternion desiredRotation = Quaternion.Euler(0, 0, rotationAngle);
         this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothTime);
        if (this.gameObject.transform.rotation > desiredRotation)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothTime);
        }*/
        // Quaternion desiredRotation = Quaternion.Euler(0, 0, rotationAngle);
        // this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothTime);
        // if this.gameObject.transform.rotation = Quaternion.Euler
        // this.gameObject.transform.rotation = Quaternion.identity;
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        this.ManageLeftAndRightMovement();


        // If we are grappling
        if (_GrapplingHook.GetConnectionIsActive())
        {

            animator.SetBool("IsJumping", false);
            animator.SetBool("IsHoldingRope", true);
            animator.SetFloat("Speed", 0.0f);
            animator.enabled = false;

        }
        else
        {
            animator.gameObject.transform.parent.transform.rotation = Quaternion.identity;
            animator.enabled = true;
            animator.SetBool("IsHoldingRope", false);
            animator.SetFloat("Speed", Math.Abs(Input.GetAxisRaw("Horizontal")));

        }


        if (this.CurrentPhase != Phase.None)
        {
            var position = this.gameObject.transform.position;
            position.x += this.InputDirection * this.Speed * this.ADSREnvelope() * Time.deltaTime;
            this.gameObject.transform.position = position;
        }

        if (Input.GetButton("W") && _GrapplingHook.GetConnectionIsActive())
        {
            animator.SetBool("IsJumping", false);
        }
        else if (Input.GetButtonDown("W"))
        {
            animator.SetBool("IsJumping", true);
        }



        if (Input.GetButtonDown("W") && IsGrounded() /*&& !_GrapplingHook.GetConnectionIsActive()*/)
        {
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 6.0f);
            }
        }

        // Retract rope / climb the rope.
        if (Input.GetButton("W") && _GrapplingHook.GetConnectionIsActive())
        {

            _GrapplingHook.ChangeJointDistance(-this.RopeLengthChangeAmount);
        }
        // Retract rope / climb the rope.
        if (Input.GetButton("S") && _GrapplingHook.GetConnectionIsActive())
        {
            if (_GrapplingHook.GetJointDistance() + this.RopeLengthChangeAmount <= _GrapplingHook.GetInitialRopeLength())
            {
                _GrapplingHook.ChangeJointDistance(this.RopeLengthChangeAmount);
            }
        }

    }

    void ManageLeftAndRightMovement()
    {
        if (Input.GetButtonDown("D"))
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = 1.0f;
            this.PrevInputDirection = 1.0f;
            this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetButtonDown("A"))
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = -1.0f;
            this.PrevInputDirection = -1.0f;
            this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetButton("D"))
        {
            this.InputDirection = 1.0f;
            this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = false;
            this.GetComponent<SpriteRenderer>().flipX = false;

        }

        if (Input.GetButtonUp("D") && !Input.GetButton("A"))
        {
            this.InputDirection = 1.0f;
            this.CurrentPhase = Phase.Release;

        }


        if (Input.GetButton("A"))
        {
            this.InputDirection = -1.0f;
            this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<SpriteRenderer>().flipX = true;

        }

        if (Input.GetButtonUp("A") && !Input.GetButton("D"))
        {
            this.InputDirection = -1.0f;
            this.CurrentPhase = Phase.Release;

        }

        if (Input.GetButton("A") && Input.GetButton("D"))
        {
            this.InputDirection = this.PrevInputDirection;
            if (this.InputDirection == 1.0f)
            {
                this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = false;
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (this.InputDirection == -1.0f)
            {
                this.transform.Find("Animator").GetComponent<SpriteRenderer>().flipX = true;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }
    float ADSREnvelope()
    {
        float velocity = 0.0f;

        if (Phase.Attack == this.CurrentPhase)
        {
            velocity = this.Attack.Evaluate(this.AttackTimer / this.AttackDuration);
            this.AttackTimer += Time.deltaTime;
            if (this.AttackTimer > this.AttackDuration)
            {
                this.CurrentPhase = Phase.Decay;
            }
        }
        else if (Phase.Decay == this.CurrentPhase)
        {
            velocity = this.Decay.Evaluate(this.DecayTimer / this.DecayDuration);
            this.DecayTimer += Time.deltaTime;
            if (this.DecayTimer > this.DecayDuration)
            {
                this.CurrentPhase = Phase.Sustain;
            }
        }
        else if (Phase.Sustain == this.CurrentPhase)
        {
            velocity = this.Sustain.Evaluate(this.SustainTimer / this.SustainDuration);
            this.SustainTimer += Time.deltaTime;
            if (this.SustainTimer > this.SustainDuration)
            {
                this.CurrentPhase = Phase.Release;
            }
        }
        else if (Phase.Release == this.CurrentPhase)
        {
            velocity = this.Release.Evaluate(this.ReleaseTimer / this.ReleaseDuration);
            this.ReleaseTimer += Time.deltaTime;
            if (this.ReleaseTimer > this.ReleaseDuration)
            {
                this.CurrentPhase = Phase.None;
            }
        }
        return velocity;
    }

    private void ResetTimers()
    {
        this.AttackTimer = 0.0f;
        this.DecayTimer = 0.0f;
        this.SustainTimer = 0.0f;
        this.ReleaseTimer = 0.0f;
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, -Vector2.up, this.DistanceToGround + 0.5f);
    }
}
