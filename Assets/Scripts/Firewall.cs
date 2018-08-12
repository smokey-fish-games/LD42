using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : Enemy
{

    private float internalCooldown;
    private float internalDetectionRad;
    public override float cooldown
    {
        get { return internalCooldown; }
        set { internalCooldown = value; }
    }

    public override float detectionRadius
    {
        get { return internalDetectionRad; }
        set { internalDetectionRad = value; }
    }
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }
}
