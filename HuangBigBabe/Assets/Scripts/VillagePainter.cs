using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class VillagePainter : MonoBehaviour
{
    [Header("Tilemap References")]
    public Tilemap bgTilemap;
    public Tilemap decorTilemap;
    
    [Header("Tiles")]
    public TileBase grassTile;
    public TileBase[] houseTiles;
    public TileBase[] treeTiles;
    public TileBase[] pathTiles;
    
    void Start()
    {
        PaintVillage();
    }
    
    public void PaintVillage()
    {
        if (bgTilemap == null || decorTilemap == null)
        {
            Debug.LogError("Please assign tilemaps!");
            return;
        }
        
        // Load tiles from assets
        var grass = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/GressGround1.asset");
        var tile0 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_0.asset");
        var tile1 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_1.asset");
        var tile2 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_2.asset");
        var tile3 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_3.asset");
        var tile4 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_4.asset");
        var tile5 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_5.asset");
        var tile6 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_6.asset");
        var tile7 = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tile/tiles/Overworld-1.png_7.asset");
        
        // Clear existing tiles
        bgTilemap.ClearAllTiles();
        decorTilemap.ClearAllTiles();
        
        // 1. Fill grass on bg layer (camera shows about 20x10 units, centered at origin)
        for (int x = -15; x <= 15; x++)
        {
            for (int y = -10; y <= 10; y++)
            {
                bgTilemap.SetTile(new Vector3Int(x, y, 0), grass);
            }
        }
        
        // 2. Draw village on decor layer
        
        // House 1 (left side)
        DrawHouse(decorTilemap, -10, -3, tile1, tile2, tile3, tile4, tile5, tile6, tile7);
        
        // House 2 (right side)
        DrawHouse(decorTilemap, 5, -3, tile1, tile2, tile3, tile4, tile5, tile6, tile7);
        
        // House 3 (center back)
        DrawHouse(decorTilemap, -2, 4, tile1, tile2, tile3, tile4, tile5, tile6, tile7);
        
        // Trees scattered around
        DrawTree(decorTilemap, -13, 2, tile0);
        DrawTree(decorTilemap, -14, -1, tile0);
        DrawTree(decorTilemap, 12, 3, tile0);
        DrawTree(decorTilemap, 13, -2, tile0);
        DrawTree(decorTilemap, 0, 7, tile0);
        DrawTree(decorTilemap, -5, 7, tile0);
        DrawTree(decorTilemap, 5, 7, tile0);
        
        // Path/road through village
        for (int x = -12; x <= 12; x++)
        {
            decorTilemap.SetTile(new Vector3Int(x, -1, 0), tile7);
        }
        
        // Small details - fences
        DrawFence(decorTilemap, -6, -6, 4);
        DrawFence(decorTilemap, 2, -6, 4);
        
        Debug.Log("Village painted successfully!");
    }
    
    void DrawHouse(Tilemap tm, int baseX, int baseY, 
                   TileBase wall, TileBase wallLeft, TileBase wallRight, 
                   TileBase roofLeft, TileBase roof, TileBase roofRight, TileBase door)
    {
        // Base/walls (2x2)
        tm.SetTile(new Vector3Int(baseX, baseY, 0), wallLeft);
        tm.SetTile(new Vector3Int(baseX + 1, baseY, 0), wall);
        tm.SetTile(new Vector3Int(baseX + 2, baseY, 0), wall);
        tm.SetTile(new Vector3Int(baseX + 3, baseY, 0), wallRight);
        
        // Roof (3x1 above walls)
        tm.SetTile(new Vector3Int(baseX, baseY + 1, 0), roofLeft);
        tm.SetTile(new Vector3Int(baseX + 1, baseY + 1, 0), roof);
        tm.SetTile(new Vector3Int(baseX + 2, baseY + 1, 0), roof);
        tm.SetTile(new Vector3Int(baseX + 3, baseY + 1, 0), roofRight);
    }
    
    void DrawTree(Tilemap tm, int x, int y, TileBase treeTile)
    {
        tm.SetTile(new Vector3Int(x, y, 0), treeTile);
    }
    
    void DrawFence(Tilemap tm, int startX, int y, int length)
    {
        for (int i = 0; i < length; i++)
        {
            tm.SetTile(new Vector3Int(startX + i, y, 0), houseTiles != null && houseTiles.Length > 0 ? houseTiles[0] : null);
        }
    }
}
