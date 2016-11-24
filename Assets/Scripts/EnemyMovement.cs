using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    // The enemy will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 0f;
    // A reference to the player
    private GameObject m_Player;
    // A reference to the nav mesh 
    private NavMeshAgent m_NavAgent;
    // A reference to the rigidbody component
    private Rigidbody m_Rigidbody;
    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        // when the enemy is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
        
    }
    private void OnDisable()
    {
        // when the enemy is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Player == null)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }
        
        float distance = (m_Player.transform.position - transform.position).magnitude;
        
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.Resume();
        }
        else
        {
            m_NavAgent.Stop();
        }
    }
}
