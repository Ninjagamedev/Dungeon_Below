using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager instance;

    void Awake() {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManage in scene");
        }
       instance = this; 
    }

    public GameObject WarriorTowerPrefab;
    public GameObject ArcherTowerPrefab;
    public GameObject MageTowerPrefab;
    public GameObject SecretTowerPrefab;
    public GameObject AttackSpeedCardPrefab;
    public Turret LastSelectedTurret;

    public GameObject buildEffect; 
    public TurretBlueprint turretToBuild;
    public BuffCardBlueprint cardToBuild;


    public Node selectedNode;

    public NodeUI nodeUI;
    public bool CanBuild{get {return turretToBuild != null ;}}
    public bool HasMoney{get {return PlayerStats.Money >= turretToBuild.cost;}}
    public bool canCard{ get { return cardToBuild != null; } }


    public void deselectTower()
    {
        turretToBuild = null;
    }
 
    public void SelectNode(Node node)
    {
        if(LastSelectedTurret != null)
        {
            LastSelectedTurret.hideRange();
        }

        if(selectedNode == node)
        {

           DeselectNode();
            return;
        }
        //Para buscar a torre no node certo é so usar node.turret
        selectedNode = node;

        if (cardToBuild != null && !cardToBuild.Equals(null) && selectedNode.turret != null)
        {
            if(cardToBuild.card != null)
            {
                if (PlayerStats.Money >= cardToBuild.cost)
                {

                    node.GiveCard(cardToBuild);
                    GameObject turretToBless = selectedNode.turret;
                    Debug.Log(turretToBless);
                    Turret Tower = turretToBless.GetComponent<Turret>();
                    Tower.ReceiveBlessing(cardToBuild, cardToBuild.card.Attribute, cardToBuild.card.Increase);
                    cardToBuild = null;
                }
                else
                {
                    cardToBuild = null;
                }

            }

        }

        if(selectedNode.turret != null)
        {
            turretToBuild = null;
            GameObject turret = selectedNode.turret;
            LastSelectedTurret = turret.GetComponent<Turret>();
            LastSelectedTurret.showRange();
            nodeUI.SetTarget(node);
        }

    }
    public void DeselectNode(){
        if(selectedNode != null)
        {
            if(LastSelectedTurret != null)
            {
                LastSelectedTurret.hideRange();
                LastSelectedTurret = null;
            }
            selectedNode = null;
            nodeUI.Hide();
        }

    }
    
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {

        turretToBuild = turret;
        cardToBuild = null;
        DeselectNode(); 
    }

        public void SelectCardToBuild(BuffCardBlueprint Card)
    {
        cardToBuild = Card;
        turretToBuild = null;
        DeselectNode(); 
    }

    public BuffCardBlueprint GetCardToBuild()
    { //Vai buscar a carta que vai ser usada para dar o buff
        if (cardToBuild != null)
        {
            return cardToBuild;
        }
        cardToBuild = null;
        return null;
    }
    public TurretBlueprint GetTurretToBuild()
    {
        removeCardToBuild();
        if(turretToBuild != null)
        {
            return turretToBuild;
        }
        return null;
    }



    public void removeCardToBuild()
    {
        if(cardToBuild != null)
        {
            cardToBuild = null;
        }
    }
}
