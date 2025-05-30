using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject placedTower;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (placedTower == null)
            {
                placedTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            }
        }
        else if (Input.GetMouseButtonDown(1)) // Right-click
        {
            if (placedTower != null)
            {
                Tower towerScript = placedTower.GetComponent<Tower>();
                if (towerScript != null)
                {
                    towerScript.Upgrade();
                }
            }
        }
    }
}
