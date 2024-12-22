using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class BaseEntities : MonoBehaviour
{
    private GameManager game_manager;

    private int entity_team;
    private Vector3 entity_position;

    BaseEntities target = null;
    NavMeshAgent agent;

    [SerializeField] float attack_speed = 1f;
    [SerializeField] float attack_range = 2f;
    [SerializeField] float move_speed = 1f;
    [SerializeField] float health = 1f;

    private float attack_timer;

    private bool reached_search_location = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target == null)
        {
            SelectTarget();
        }
        else
        {
            if (!reached_search_location)
            {
                IsAtDestination();
            }
            else
            {
                Attack();
            }
        }
    }

    public void Setup(int team, Vector3 position, GameManager manager)
    {
        entity_team = team;
        entity_position = position;
        game_manager = manager;
    }

    protected void SelectTarget()
    {
        var enemies = game_manager.GetOpposingTeam(entity_team);
        float closet_entity_distance = Mathf.Infinity;
        float distance_between = 0;
        BaseEntities possible_target = null;


        foreach(BaseEntities entity in enemies)
        {
            distance_between = Vector3.Distance(entity.transform.position, entity_position);
            if (distance_between < closet_entity_distance)
            {
                closet_entity_distance = distance_between;
                possible_target = entity;
            }
        }
        target = possible_target;

        if(target != null)
        {
            Debug.Log("No Target");
            agent.SetDestination(target.transform.position);
        }
        Debug.Log(target);
    }

    protected void IsAtDestination()
    {
        if (!agent.pathPending && agent.remainingDistance <= attack_range)
        {
            reached_search_location = true;
            agent.ResetPath();
            Debug.Log("Reset : " + agent.remainingDistance);
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }   
    }

    protected virtual void Attack()
    {
        attack_timer += Time.deltaTime;

        if(attack_timer >= attack_speed)
        {
            target.TakeDamage(1);
            attack_timer = 0;
        }
    }

    protected void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            game_manager.UnitDeath(this, entity_team);
        }
    }
}
