using System;
using System.Linq;
using UnityEngine;

public class StageController : Utility.MonoSingleton<StageController>
{
    [SerializeField] private int level = 0;
    public int Level { get { return level; } }

    [SerializeField] private int stage = 0;
    public int Stage { get { return stage; } }

    [SerializeField]
    private int noOfBossDefeated;
    public int NoOfBossDefeated { get { return noOfBossDefeated; } }

    [SerializeField]
    private bool isBossFight;
    public bool IsBossFight { get { return isBossFight; } }


    public string[] VisitorArmy;

    public PhaseType BossPhase;


    public enum PhaseType
    {
        EnterPhase,
        PhaseOne,
        PhaseTwo,
        PhaseThree,
        PhaseTransition
    }


    #region UnityMessages
    private void OnEnable()
    {
        EventController.GenerateStage += OnStageGenerate;
        EventController.RestartGameUI += RestartGame;
        EventController.BossDeath += BossDead;
        EventController.SetStage += SetStage;
    }

    private void OnDisable()
    {
        EventController.GenerateStage -= OnStageGenerate;
        EventController.RestartGameUI -= RestartGame;
        EventController.BossDeath -= BossDead;
        EventController.SetStage -= SetStage;
    }
    #endregion

    private void RestartGame()
    {
        level = 0;
        stage = 0;
        noOfBossDefeated = 0;
        isBossFight = false;
    }

    private void SetStage()
    {
        level++;
        stage++;
        if (level > 3)
        {
            isBossFight = true;
        }
        else
        {
            isBossFight = false;
        }
    }

    public void OnStageGenerate()
    {
        if (isBossFight)
        {
            BossStage();
        }
        else
        {
            isBossFight = false;
            int random = UnityEngine.Random.Range(1, 3);
            switch (random)
            {
                case 1:
                    VisitorArmy = new string[]
                    {
                        "#######",
                        "-#####-",
                        "--###--",
                        "---#---",
                        "-------"
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;

                case 2:
                    VisitorArmy = new string[]
                    {
                        "##-#-##",
                        "-##-##-",
                        "--###--",
                        "-#-#-#-",
                        "--###--"
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;

                case 3:
                    VisitorArmy = new string[]
                    {
                        "##--##-",
                        "--##-##",
                        "###----",
                        "----###",
                        "-#-#-#-"
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;
            }
        }
    }

    private void ShuffleVisitorArmy()
    {
        var rng = new System.Random();
        var keys = VisitorArmy.Select(e => rng.NextDouble()).ToArray();
        Array.Sort(keys, VisitorArmy);
    }

    private void BossStage()
    {
        level = 0;
        BossPhase = PhaseType.EnterPhase;
        EventController.RaiseOnBossSpawn();
    }

    private void BossDead()
    {
        noOfBossDefeated = stage / 4;
    }

}
