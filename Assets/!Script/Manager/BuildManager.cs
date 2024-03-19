using UnityEngine;

public class BuildManager : MonoBehaviour
{

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        nodeUI = FindObjectOfType<NodeUI>();
    }



    public TurretBlueprint basicTurretLv1Blueprint;
    public TurretBlueprint basicTurretLv2Blueprint;
    public TurretBlueprint basicTurretLv3Blueprint;
    public TurretBlueprint luncherTurretLv1Blueprint;
    public TurretBlueprint luncherTurretLv2Blueprint;
    public TurretBlueprint luncherTurretLv3Blueprint;
    public TurretBlueprint lazerTurretLv1Blueprint;
    public TurretBlueprint lazerTurretLv2Blueprint;
    public TurretBlueprint lazerTurretLv3Blueprint;

    public static BuildManager instance;
    public GameObject sellButton;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    public GameObject sellEffect;

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
    public void OnBasicTurretBtnClick(int level)
    {

        if (selectedNode != null && !selectedNode.currentTurret)
        {
            if (MainGameController.instance.gold < turretToBuild.price)
            {
                Debug.Log("Need more gold to build the turret");
                return;
            }
        }

        if (level == 1)
        {
            turretToBuild = basicTurretLv1Blueprint;
            selectedNode = null;
        }
        else if (level == 2)
        {
            turretToBuild = basicTurretLv2Blueprint;
            selectedNode = null;
        }
        else if (level == 3)
        {
            turretToBuild = basicTurretLv3Blueprint;
            selectedNode = null;
        }
    }

    public void OnLuncherTurretBtnClick(int level)
    {
        if (selectedNode != null && !selectedNode.currentTurret)
        {
            if (MainGameController.instance.gold < turretToBuild.price)
            {
                Debug.Log("Need more gold to build the turret");
                return;
            }
        }

        if (level == 1)
        {
            turretToBuild = luncherTurretLv1Blueprint;
            selectedNode = null;
        }
        else if (level == 2)
        {
            turretToBuild = luncherTurretLv2Blueprint;
            selectedNode = null;
        }
        else if (level == 3)
        {
            turretToBuild = luncherTurretLv3Blueprint;
            selectedNode = null;
        }
    }


    public void OnLazerTurretBtnClick(int level)
    {

        if (selectedNode != null && !selectedNode.currentTurret)
        {
            if (MainGameController.instance.gold < turretToBuild.price)
            {
                Debug.Log("Need more gold to build the turret");
                return;
            }
        }

        if (level == 1)
        {
            turretToBuild = lazerTurretLv1Blueprint;
            selectedNode = null;
        }
        else if (level == 2)
        {
            turretToBuild = lazerTurretLv2Blueprint;
            selectedNode = null;
        }
        else if (level == 3)
        {
            turretToBuild = lazerTurretLv3Blueprint;
            selectedNode = null;
        }
    }

    public void ClearTurretToBuild()
    {
        turretToBuild = null;
    }
}

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int price;
}
