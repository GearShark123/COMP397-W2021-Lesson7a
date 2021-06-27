using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BobState
{
    IDLE, 
    RUN,
    JUMP,
    STUMBLE
}

public class BobBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS;
    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }

        if (HasLOS && Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            animator.SetInteger("AnimState", (int)BobState.STUMBLE);
            //transform.LookAt(transform.position - player.transform.forward);    
        }
        else if (HasLOS && agent.isOnOffMeshLink == false)
        {           
            animator.SetInteger("AnimState", (int)BobState.RUN);           
        }
        else if (HasLOS && agent.isOnOffMeshLink)
        {                        
            animator.SetInteger("AnimState", (int)BobState.JUMP);        
        }
        else
        {
            animator.SetInteger("AnimState", (int)BobState.IDLE);
        }
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    animator.SetInteger("AnimState", (int)BobState.IDLE);
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    animator.SetInteger("AnimState", (int)BobState.RUN);
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    animator.SetInteger("AnimState", (int)BobState.JUMP);
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
        }
    }    
}
