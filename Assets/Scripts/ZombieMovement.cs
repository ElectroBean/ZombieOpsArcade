using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {
    // The enemy will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 0f;
    // A reference to the player
    private GameObject m_Player;
    // A reference to the nav mesh 
    private NavMeshAgent m_NavAgent;
    // A reference to the rigidbody component
    private Rigidbody m_Rigidbody;
    public bool attacking;
    public bool dead;
    public Animator anim;
    private CapsuleCollider col;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

    }
   
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        dead = GetComponentInParent<ZombieHealth>().m_Dead;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Player == null)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }

        float distance = (m_Player.transform.position - transform.position).magnitude;

        if (dead != true)
        {
            if (distance > m_CloseDistance)
            {
                attacking = false;
                m_NavAgent.SetDestination(m_Player.transform.position);
                m_NavAgent.Resume();
                anim.Play("walk");
            }
            else
            {
                //if distance < m_CloseDistance, stops the navagent
                m_NavAgent.Stop();
                anim.Play("attack");
                attacking = true;
            }
        }
      if(dead == true)
        {
            m_NavAgent.Stop();
            m_Rigidbody.freezeRotation = true;
            col.enabled = false;
        }
    }
}
