using UnityEngine;

public class Surface : MonoBehaviour {
    public Transform placeholder;
    public GameObject tilePrefab;

    public Vector2 placeholderSize = new Vector2(100, 100);
    Vector2 _placeholderSize;

    [Range(1, 20)]
    public int tileSize = 10;
    int _tileSize;

    [Range(1, 10)]
    public int maxTileHeight = 3;
    int _maxTileHeight;

    public enum DrawMode { Steps, Gradient }
    public DrawMode drawMode = DrawMode.Steps;
    DrawMode _drawMode;

    [Range(1, 10)]
    public float smoothness = 3;
    float _smoothness;

    [Range(1, 10)]
    public int maxSteps = 3;
    int _maxSteps;

    float placeholderBorder = 5;
    Transform tileParent;
    Tile[,] tiles;

    void Awake() {
        placeholder.gameObject.SetActive(true);
        UpdatePlaceholder();

        _placeholderSize = placeholderSize;
        _tileSize = tileSize;
        _maxTileHeight = maxTileHeight;
        _maxSteps = maxSteps;
        _smoothness = smoothness;
        _drawMode = drawMode;
    }

    void Update() {
        if (placeholderSize != _placeholderSize) {
            UpdatePlaceholder();
            _placeholderSize = placeholderSize;
        }

        if (tileSize != _tileSize) {
            GenerateTiles();
            _tileSize = tileSize;
        }

        if (maxTileHeight != _maxTileHeight) {
            ResizeTiles();
            _maxTileHeight = maxTileHeight;
        }

        if (drawMode != _drawMode) {
            ResizeTiles();
            _drawMode = drawMode;
        }

        if (smoothness != _smoothness) {
            ResizeTiles();
            _smoothness = smoothness;
        }

        if (maxSteps != _maxSteps) {
            ResizeTiles();
            _maxSteps = maxSteps;
        }
    }

    void UpdatePlaceholder() {
        placeholder.localScale = new Vector3(
            placeholderSize.x + placeholderBorder,
            placeholder.localScale.y,
            placeholderSize.y + placeholderBorder);

        GenerateTiles();
    }

    void GenerateTiles() {
        if (tileParent != null) {
            Destroy(tileParent.gameObject);
        }

        tileParent = (new GameObject("Tiles")).transform;
        tileParent.parent = transform;

        int rows = Mathf.FloorToInt(placeholderSize.x / tileSize);
        int columns = Mathf.FloorToInt(placeholderSize.y / tileSize);
        tiles = new Tile[rows, columns];

        float xPadding = (placeholderSize.x % tileSize) / 2.0f;
        float zPadding = (placeholderSize.y % tileSize) / 2.0f;

        for (int i = 0; i < rows; i++) {
            float xPos = i * tileSize + xPadding + tileSize / 2.0f - placeholderSize.x / 2.0f;

            for (int j = 0; j < columns; j++) {
                GameObject tile = Instantiate(tilePrefab);
                float zPos = j * tileSize + zPadding + tileSize / 2.0f - placeholderSize.y / 2.0f;
                tile.transform.position = new Vector3(xPos, 0, zPos);
                tile.transform.localScale = Vector3.one * tileSize;
                tile.transform.parent = tileParent;
                tiles[i, j] = tile.GetComponent<Tile>();
            }
        }

        ResizeTiles();
    }

    public void ResizeTiles() {
        float stepSize = maxTileHeight / (float)maxSteps;

        float startingI = Random.Range(0, 1000);
        float startingJ = Random.Range(0, 1000);

        for (int i = 0; i < tiles.GetLength(0); i++) {
            for (int j = 0; j < tiles.GetLength(1); j++) {
                float random = Mathf.PerlinNoise(
                    (startingI + i * smoothness * 100) / 1000,
                    (startingJ + j * smoothness * 100) / 1000);

                if (drawMode == DrawMode.Steps) {
                    float tileHeight = Mathf.FloorToInt(random * (maxSteps + 1)) * stepSize;
                    tiles[i, j].UpdateHeight(tileHeight);
                } else if (drawMode == DrawMode.Gradient) {
                    tiles[i, j].UpdateHeight(random * maxTileHeight);
                }
            }
        }
    }
}
