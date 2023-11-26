using UnityEngine;
using UnityEngine.Tilemaps;

public class SurfaceController : MonoBehaviour
{
    [SerializeField] int tilesBetweenFootStep;
    [SerializeField] int firstFootStepOffset;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap adormentTilemap;
    [SerializeField] Tilemap footstepsTilemap;
    [SerializeField] Tile groundTile;
    [SerializeField] Tile[] adormentTiles;
    [SerializeField] Tile footStepTile;

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

                    if (y > firstFootStepOffset - 2 && (y + firstFootStepOffset + (Mathf.Abs(x / 2f) % 2)) % tilesBetweenFootStep == 0)
                    {
                        footstepsTilemap.SetTile(cellPosition, footStepTile);
                    }
                    else if (Random.value < 0.2f)
                    {
                        int randomIndex = Random.Range(0, adormentTiles.Length);
                        adormentTilemap.SetTile(cellPosition, adormentTiles[randomIndex]);
                    }
                }
            }
        }
    }

    public bool IsThisTileFootStep(int x, int y)
    {
        Vector3Int cellPosition = new Vector3Int(x, y, 0);
        TileBase tile = footstepsTilemap.GetTile(cellPosition);
        return (tile == footStepTile);
    }

    public void CleanTile(int x, int y)
    {
        if (!IsThisTileFootStep(x, y)) return;

        Vector3Int cellPosition = new Vector3Int(x, y, 0);
        TileBase tile = footstepsTilemap.GetTile(cellPosition);
        groundTilemap.SetTile(cellPosition, null);
    }
}
