using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwo : IState
{
    private BossAISM2 boss;
    public PhaseTwo(BossAISM2 _boss)
    {
        boss = _boss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
