using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    [SerializeField] private int level = 0;

    public string[] VisitorArmy;

    #region UnityMessages
    private void OnEnable()
    {
        EventController.GenerateLevel += OnGenerateLevel;
    }

    private void OnDisable()
    {
        EventController.GenerateLevel -= OnGenerateLevel;
    }
    #endregion

    public void OnGenerateLevel()
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
                };
                EventController.RaiseOnBuildVisitorArmy();
                break;

            case 2: // Level 2
                VisitorArmy = new string[]
                {
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
}
