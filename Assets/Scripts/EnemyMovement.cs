using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    // The enemy will stop moving towards the player once it reaches this distance
    public float m_CloseDistance = 0f;
    // A reference to the player - this will be set when the enemy is loaded
    private GameObject m_Player;
    // A reference to the nav mesh agent component
    private NavMeshAgent m_NavAgent;
    // A reference to the rigidbody component
    private Rigidbody m_Rigidbody;
    // Will be set to true when this tank should follow the player
    private bool m_Follow;


    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;

    }
    private void OnEnable()
    {
        // when the enemy is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;
        m_Follow = true;
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

    // Update is called once per frame
    void Update()
    {
        
        if (m_Follow == false)
            return;

        if (m_Player == null)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }
        // get distance from player to enemy
        float distance = (m_Player.transform.position - transform.position).magnitude;
        // if distance < closedistance, stop moving
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
