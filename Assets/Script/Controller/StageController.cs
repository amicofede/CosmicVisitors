using System;
using System.Linq;
using UnityEngine;

public class StageController : Utility.MonoSingleton<StageController>
{
    [SerializeField] private int level = 0;
    public int Level { get { return level; } }

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
    }

    private void OnDisable()
    {
        EventController.GenerateStage -= OnStageGenerate;
        EventController.RestartGameUI -= RestartGame;
    }
    #endregion

    private void RestartGame()
    {
        level = 0;
    }

    public void OnStageGenerate()
    {
        level++;
        if (level > 3)
        {
            BossStage();
        }
        else
        {
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

    public void BossStage()
    {
        level = 0;
        Debug.Log("remove comment");
        //BossPhase = PhaseType.EnterPhase;
        EventController.RaiseOnBossSpawn();
    }

}
