using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float DistanceToGround = 1.285f;
    //bool CanJump = true;
    //bool CanWalk = true;
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

    void Start()
    {
        this.CurrentPhase = Phase.None;
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

    // Update is called once per frame
    void Update()
    {

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetButtonDown("D")) 
        {
            Debug.Log("Pressing Right");
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = 1.0f;
        }

        if (Input.GetButton("D"))  
        {
            this.InputDirection = 1.0f;
        }

        if (Input.GetButtonUp("D"))
        {
            this.InputDirection = 1.0f;
            this.CurrentPhase = Phase.Release;
        }

        if (Input.GetButtonDown("A"))
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = -1.0f;
        }

        if (Input.GetButton("A"))
        {
            this.InputDirection = -1.0f;
        }

        if (Input.GetButtonUp("A"))
        {
            this.InputDirection = -1.0f;
            this.CurrentPhase = Phase.Release;
        }

        if (this.CurrentPhase != Phase.None)
        {
            var position = this.gameObject.transform.position;
            position.x += this.InputDirection * this.Speed * this.ADSREnvelope() * Time.deltaTime;
            this.gameObject.transform.position = position;
        }

        

        if (Input.GetAxis("Vertical") > 0.01 /*&& IsGrounded()*/)
        {
            if (rigidBody != null)
            {
                Debug.Log("Jumping");
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3.0f);
            }
        }

    }

    bool IsGrounded()
    {
        var grounded = Physics.Raycast(this.transform.position, -Vector3.up, this.DistanceToGround + 0.1f);
        var debugRay = new Vector3(0f, this.transform.position.y - this.DistanceToGround - 0.1f, 0f);
        Debug.DrawRay(this.transform.position, debugRay, Color.green, 1.0f);
        Debug.Log(grounded);
        return grounded;
    }
}
