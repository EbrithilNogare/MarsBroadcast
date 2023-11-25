using UnityEngine;
using UnityEngine.Tilemaps;

public class SurfaceController : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap adormentTilemap;
    public Tile groundTile;
    public Tile[] adormentTiles;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RenderTiles();
    }

    void RenderTiles()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraSize = new Vector3(mainCamera.orthographicSize * 2 * mainCamera.aspect, mainCamera.orthographicSize * 2, 0);

        Vector3Int minCell = groundTilemap.WorldToCell(cameraPosition - cameraSize / 2);
        Vector3Int maxCell = groundTilemap.WorldToCell(cameraPosition + cameraSize / 2);

        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (groundTilemap.GetTile(cellPosition) == null)
                {
                    groundTilemap.SetTile(cellPosition, groundTile);
                    if (Random.value < 0.2f)
                    {
                        int randomIndex = Random.Range(0, adormentTiles.Length);
                        adormentTilemap.SetTile(cellPosition, adormentTiles[randomIndex]);
                    }
                }
            }
        }
    }
}
