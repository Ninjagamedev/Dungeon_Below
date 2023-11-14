using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color UnableColor;
    public Vector3 positionOffset;
    public Quaternion RotationOffset;
    [Header("Optional")]
    public GameObject turret;

    public TurretBlueprint turretBlueprint;

    public bool isUpgraded = false;

    public GameObject Card;
    public BuffCardBlueprint cardBlueprint;
    private Renderer rend;
    private Color startcolor;
    public Material[] materials;
    BuildManager buildManager;

    void Start() {
      rend = GetComponent<Renderer>(); 

      startcolor = rend.materials[1].color;
      buildManager = BuildManager.instance;
    }

    	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}


    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
//            if (Card != null)
  //          {
    //            Debug.Log("É AQUI QUE HA TORRE");
      //          buildManager.SelectNode(this);
        //        return;
          //  }
            Debug.Log("TORRE SELECT");
            buildManager.SelectNode(this);
            return;
        }


        if (!buildManager.CanBuild)
      {
            
            buildManager.SelectNode(this);

            return;
        }





    //Build a turret
    if (turretBlueprint != null)
        {
            BuildTurret(buildManager.GetTurretToBuild());
        }


     


    }
    void BuildTurret(TurretBlueprint blueprint)
    {
        Debug.Log(blueprint);
        if(blueprint.prefab != null)
        { 
      if(PlayerStats.Money < blueprint.cost)
        {
            Debug.Log ("Not enough money to build the tower");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
       GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position , RotationOffset);
        turret = _turret;

        turretBlueprint = blueprint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);
        //Após construir a torre deselecionar
        buildManager.deselectTower();
        }

    }

   public void GiveCard(BuffCardBlueprint card)
    {
        Debug.Log(card);
        if(card != null && !card.Equals(null))
        {
        Debug.Log("OLHA A CARTA " + card.card);
            if(PlayerStats.Money < card.cost)
        {
            Debug.Log ("Not enough money to give the buff");
            return;
        }
      PlayerStats.Money -= card.cost;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);
        }
    }


    public void UpgradeTurret()
    {
      if (PlayerStats.Money < turretBlueprint.upgradeCost)
      {
        Debug.Log("Not Enough Money to Upgrade");
        return;
      }
      PlayerStats.Money -= turretBlueprint.upgradeCost;
      //Destroy old turret
      Destroy(turret);
      //Create upgraded one 
       GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position , RotationOffset);
        turret = _turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);

        isUpgraded = true; 
      Debug.Log("Turret Upgraded");

    }

     void OnMouseEnter()
    {
      if(!buildManager.CanBuild)
      {
        return;
      }

        if (buildManager.HasMoney && buildManager.turretToBuild != null)
      {
            if(buildManager.turretToBuild.prefab != null)
            {
                Debug.Log(buildManager.turretToBuild.prefab);
                rend.materials[1].color = hoverColor;
            }

      }else{
        rend.materials[1].color = UnableColor;
      }
     
    }
    void OnMouseExit() {
        rend.materials[1].color = startcolor;
    }
}
