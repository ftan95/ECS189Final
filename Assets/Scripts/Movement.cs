using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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

    private float AttackTimer;
    private float DecayTimer;
    private float SustainTimer;
    private float ReleaseTimer;

    private float InputDirection = 0.0f;

    private enum Phase { Attack, Decay, Sustain, Release, None};

    private Phase CurrentPhase;
    private bool LeftAndRightPressed = false;
    private float PrevInputDirection = 0.0f; 


    void Start()
    {
        this.CurrentPhase = Phase.None;
        this.DistanceToGround = this.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        this.ManageLeftAndRightMovement();

        if (this.CurrentPhase != Phase.None)
        {
            var position = this.gameObject.transform.position;
            position.x += this.InputDirection * this.Speed * this.ADSREnvelope() * Time.deltaTime;
            this.gameObject.transform.position = position;
        } 

        

        if (Input.GetButtonDown("W") && IsGrounded())
        {
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3.0f);
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
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

         if (Input.GetButtonDown("A"))
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = -1.0f;
            this.PrevInputDirection = -1.0f;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetButton("D"))  
        {
            this.InputDirection = 1.0f;
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
            this.GetComponent<SpriteRenderer>().flipX = true;
            
        }

        if (Input.GetButtonUp("A") && !Input.GetButton("D"))
        {
            this.InputDirection = -1.0f;
            this.CurrentPhase = Phase.Release;
            
        }

        if(Input.GetButton("A") && Input.GetButton("D"))
        {
            this.InputDirection = this.PrevInputDirection;
            if (this.InputDirection == 1.0f)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (this.InputDirection == -1.0f)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }
    float ADSREnvelope()
    {
        float velocity = 0.0f;

        if(Phase.Attack == this.CurrentPhase)
        {
            velocity = this.Attack.Evaluate(this.AttackTimer / this.AttackDuration);
            this.AttackTimer += Time.deltaTime;
            if(this.AttackTimer > this.AttackDuration)
            {
                this.CurrentPhase = Phase.Decay;
            }
        } 
        else if(Phase.Decay == this.CurrentPhase)
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
        return  Physics2D.Raycast(this.transform.position, -Vector2.up, this.DistanceToGround + 0.1f);
    }
}
