using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseThree : IState
{
    private BossAISM2 boss;
    public PhaseThree(BossAISM2 _boss)
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
