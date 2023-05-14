using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    [SerializeField] private int level = 0;
    public int Level { get { return level; } }

    public string[] VisitorArmy;

    #region UnityMessages
    private void OnEnable()
    {
        EventController.GenerateStage += OnStageGenerate;
        EventController.SpaceshipAnimationStarted += DestroyAll;
        EventController.RestartGameUI += RestartGame;
    }

    private void OnDisable()
    {
        EventController.GenerateStage -= OnStageGenerate;
        EventController.SpaceshipAnimationStarted -= DestroyAll;
        EventController.RestartGameUI -= RestartGame;
    }
    #endregion

    private void RestartGame()
    {
        level = 0;
        DestroyAllActor();
    }

    public void OnStageGenerate()
    {
        level++;
        switch (level)
        {
            case 1: // Level 1
                VisitorArmy = new string[]
                {
                    "#######",
                    "-#####-",
                    "--###--",
                    "---#---",
                    "-------",
                    "-------",
                    "-------",
                    //"---#---",
                    //"-------",
                    //"-------",
                    //"-------",
                    //"-------",
                    //"-------",
                    //"-------",
                };
                EventController.RaiseOnBuildVisitorArmy();
                break;

            case 2: // Level 2
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
                    "---#---",
                    "-------",
                };
                EventController.RaiseOnBuildVisitorArmy();
                break;

            case 3: // Level 3
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
                    "--#-#--",
                    "-##-##-",
                    "##-#-##",
                    "-##-##-",
                    "--###--",
                };
                EventController.RaiseOnBuildVisitorArmy();
                break;

            case 4: // Boss
                break;
        }
    }

    public void DestroyAll()
    {
        GameObject[] objectToDelete = GameObject.FindGameObjectsWithTag("Laser");
        for (int i = 0; i < objectToDelete.Length; i++)
        {
            Destroy(objectToDelete[i]);
        }
    }

    private void DestroyAllActor()
    {
        GameObject[] LaserToDelete = GameObject.FindGameObjectsWithTag("Laser");
        GameObject[] VisitorToDelete = GameObject.FindGameObjectsWithTag("Visitor");
        GameObject[] SpaceshipToDelete = GameObject.FindGameObjectsWithTag("Spaceship");
        for (int i = 0; i < LaserToDelete.Length; i++)
        {
            Destroy(LaserToDelete[i]);
        }
        for (int i = 0; i < VisitorToDelete.Length; i++)
        {
            Destroy(VisitorToDelete[i]);
        }
        for (int i = 0; i < SpaceshipToDelete.Length; i++)
        {
            Destroy(SpaceshipToDelete[i]);
        }
    }
}
