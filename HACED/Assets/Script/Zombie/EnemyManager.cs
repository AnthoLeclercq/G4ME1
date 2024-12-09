using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    private GameObject _target;
    public GameObject _targetLook;
    public int damage = 20;
    [HideInInspector] public float gravity = 9.81f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _target.transform.position);
        _agent.destination = _target.transform.position;

        if (_agent.velocity.magnitude <= 0.1f && distanceToPlayer <= _agent.stoppingDistance)
            _animator.SetBool("Walk", false);
        else
            _animator.SetBool("Walk", true);

        _targetLook.transform.LookAt(_target.transform);
        transform.rotation = Quaternion.Euler(0f, _targetLook.transform.eulerAngles.y, 0f);
    }

    public void DamagePlayer() => _target.GetComponent<PlayerHealth>().TakeDamage(damage);
}
