using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Animator anim;

    Rigidbody rb;
    [SerializeField] Transform target;
    [SerializeField] Transform poison;
    //[SerializeField] float forceMultiplier = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        int dice = Random.Range(0, 2);
        if (this.transform.localPosition.y < -1)
        {
            this.rb.angularVelocity = Vector3.zero;
            this.rb.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
        if(dice == 0)
        {
            poison.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
        }
        else
        {
            poison.localPosition = new Vector3(0f, 30f, 0f);
        }
        target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(poison.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        SetReward(-0.001f);

        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        //rb.AddForce(controlSignal * forceMultiplier);
        this.transform.position += controlSignal.normalized * 5f * Time.deltaTime;

        if (controlSignal.magnitude > 0.01f)
        {
            this.anim.SetBool("isWalking", true);
            Quaternion q = Quaternion.LookRotation(controlSignal, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, 0.1f);
        }
        else
        {
            this.anim.SetBool("isWalking", false);
        }

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        float distanceToPoison = Vector3.Distance(this.transform.localPosition, poison.localPosition);

        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        } else if (distanceToPoison < 1.42f)
        {
            SetReward(-2f);
            EndEpisode();
        } else if (this.transform.localPosition.y < -1)
        {
            SetReward(-0.5f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continousActionsOut = actionsOut.ContinuousActions;
        continousActionsOut[0] = Input.GetAxis("Horizontal");
        continousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
