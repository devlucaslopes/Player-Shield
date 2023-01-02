using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform ShotPoint;

    private float _timeToNextAttack;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > _timeToNextAttack)
        {
            _timeToNextAttack = Time.time + TimeBetweenAttack;
            Instantiate(BulletPrefab, ShotPoint.position, ShotPoint.rotation);
        }
    }
}
