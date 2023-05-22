using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class StageController : Utility.MonoSingleton<StageController>
{
    [SerializeField] private int level = 0;
    public int Level { get { return level; } }

    public string[] VisitorArmy;

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
            level = 0;
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
                        //"---#---",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        "#######",
                        "-#####-",
                        "--###--",
                        "---#---",
                        "-------",
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;

                case 2:
                    VisitorArmy = new string[]
                    {
                        //"---#---",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        "##-#-##",
                        "-##-##-",
                        "--###--",
                        "-#-#-#-",
                        "--###--",
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;

                case 3:
                    VisitorArmy = new string[]
                    {
                        //"---#---",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        //"-------",
                        "-##-##-",
                        "##-#-##",
                        "-##-##-",
                        "##-#-##",
                        "-##-##-",
                    };
                    ShuffleVisitorArmy();
                    EventController.RaiseOnBuildVisitorArmy();
                    break;

                case 4: // Boss
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
        EventController.RaiseOnBossSpawn();
    }

}
