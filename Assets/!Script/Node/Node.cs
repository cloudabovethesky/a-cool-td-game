using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Vector3 positionOffset;
    public GameObject currentTurret;
    private Renderer rend;
    [ColorUsage(true, true)]
    private Color startColor;
    [ColorUsage(true, true)]
    public Color hoverColor;
    public TurretBlueprint turretBlueprint;
    public bool isSelected = false;
    public Vector3 uiOffset;


    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>(false);
        startColor = rend.material.GetColor("_EmissiveColor");
        buildManager = BuildManager.instance;
        uiOffset = new Vector3(0f, 1f, 0f);
    }

    private void OnMouseEnter()
    {
        rend.material.SetColor("_EmissiveColor", hoverColor);
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_EmissiveColor", startColor);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        TurretBlueprint turretToBuild = BuildManager.instance.GetTurretToBuild();
        if (turretToBuild == null)
        {
            if (currentTurret != null)
            {
                MainGameController.instance.nodeUI.ShowNodeUI(this);
                Debug.Log("Can't build on this node!");
            }
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        MainGameController.instance.nodeUI.HideNodeUI();

        if (currentTurret != null)
        {
            Debug.Log("Already a turret built on this node!");
            return;
        }

        if (MainGameController.instance.gold < turretToBuild.price)
        {
            Debug.Log("Need more gold to build the turret");
            return;
        }

        currentTurret = Instantiate(turretToBuild.prefab, transform.position, Quaternion.identity);
        turretBlueprint = turretToBuild;
        MainGameController.instance.gold -= turretToBuild.price;

        BuildManager.instance.ClearTurretToBuild();
    }
}
