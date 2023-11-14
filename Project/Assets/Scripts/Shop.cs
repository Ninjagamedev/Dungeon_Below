using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Shop : MonoBehaviour
{
        BuildManager buildManager;
        public TurretBlueprint WarriorTower;
        public TurretBlueprint ArcherTower;
        public TurretBlueprint MageTower;
        public TurretBlueprint SecretTower;

        public BuffCardBlueprint AttackSpeed;
        
     void Start() {
        {
            buildManager = BuildManager.instance;
        }
    }
    public void SelectWarriorTower()
    {
          if (EventSystem.current.IsPointerOverGameObject())
             {
            Debug.Log("Standard Turret Purchased");
            buildManager.SelectTurretToBuild(WarriorTower);               
             }

    }
    public void SelectArcherTower()
    {
        if (EventSystem.current.IsPointerOverGameObject())
             {
            if (buildManager.turretToBuild != ArcherTower)
            {
                Debug.Log("Archer Purchased");
                buildManager.SelectTurretToBuild(ArcherTower);
            }
            else
            {
                buildManager.deselectTower();
            }
        }

    }

    public void SelectSecretTower()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (buildManager.turretToBuild != SecretTower)
            {
                Debug.Log("Secret Purchased");
                buildManager.SelectTurretToBuild(SecretTower);
            }
            else
            {
                buildManager.deselectTower();
            }
        }

    }


    public void SelectMageTower()
    {

        if (EventSystem.current.IsPointerOverGameObject())
             {
            if(buildManager.turretToBuild != MageTower)
            {
                Debug.Log("Mage Purchased");
                buildManager.SelectTurretToBuild(MageTower);

            }
            else
            {
                buildManager.deselectTower();
            }
          
             }


    }
    public void SelectAttackSpeedBuff()
    {
             if (EventSystem.current.IsPointerOverGameObject())
             {
                Debug.Log("Attack Speed Purchased");
                buildManager.SelectCardToBuild(AttackSpeed);
                      
             }
    }



}
