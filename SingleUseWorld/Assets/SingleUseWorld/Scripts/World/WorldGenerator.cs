using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    public class WorldGenerator : MonoBehaviour
    {
        #region Nested Classes
        #endregion

        #region Fields
        private const int PPU = 16;

        [SerializeField] Sprite _groundSprite;
        [Min(1)]
        [SerializeField] int _columns;
        [Min(1)]
        [SerializeField] int _rows;

        [Range(0, 100)]
        [SerializeField] int _grassAmount;
        [Range(0, 100)]
        [SerializeField] int _propsAmount;
        [Range(0, 100)]
        [SerializeField] int _surroundingsAmount;

        [SerializeField] float _levelBoundarySafeZone;
        [SerializeField] float _levelBoundaryThickness;
        [SerializeField] float _worldBoundaryOffset;


        [SerializeField] Sprite _borderSprite_N;
        [SerializeField] Sprite _borderSprite_S;
        [SerializeField] Sprite _borderSprite_W;
        [SerializeField] Sprite _borderSprite_E;

        [SerializeField] Sprite _borderSprite_NW;
        [SerializeField] Sprite _borderSprite_NE;
        [SerializeField] Sprite _borderSprite_SW;
        [SerializeField] Sprite _borderSprite_SE;

        [SerializeField] List<Sprite> _grassSprites;
        [SerializeField] List<Sprite> _propsSprites;
        [SerializeField] List<Sprite> _surroundingsSprites;

        private Transform _levelBoundaryContainer;
        private Transform _worldBoundaryContainer;
        private Transform _terrainContainer;

        private Transform _surroundingsSubcontainer;
        private Transform _groundSubcontainer;
        private Transform _bordersSubcontainer;
        private Transform _grassSubcontainer;
        private Transform _propsSubcontainer;

        private LevelBoundary _levelBoundary;
        #endregion

        #region Properties
        private Vector2 Size
        {
            get => new Vector2(_columns, _rows);
        }
        #endregion

        #region Delegates & Events
        #endregion

        #region Constructors
        #endregion

        #region LifeCycle Methods
        #endregion

        #region Public Methods
        public void Generate()
        {
            Clear();
            GenerateContainers();

            GenerateLevelBoundary();
            GenerateWorldBoundary();

            GenerateSurroundings();
            GenerateGround();
            GenerateGroundBorders();
            GenerateGrass();
            GenerateProps();
        }
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void GenerateContainers()
        {
            _levelBoundaryContainer = new GameObject("LevelBoundary").transform;
            _worldBoundaryContainer = new GameObject("WorldBoundary").transform;
            _terrainContainer = new GameObject("Terrain").transform;

            _surroundingsSubcontainer = new GameObject("Surrounding").transform;
            _groundSubcontainer = new GameObject("Ground").transform;
            _bordersSubcontainer = new GameObject("Border").transform;
            _grassSubcontainer = new GameObject("Grass").transform;
            _propsSubcontainer = new GameObject("Props").transform;

            _levelBoundaryContainer.SetParent(transform);
            _worldBoundaryContainer.SetParent(transform);
            _terrainContainer.SetParent(transform);

            _surroundingsSubcontainer.SetParent(_terrainContainer);
            _groundSubcontainer.SetParent(_terrainContainer);
            _bordersSubcontainer.SetParent(_terrainContainer);
            _grassSubcontainer.SetParent(_terrainContainer);
            _propsSubcontainer.SetParent(_terrainContainer);
        }

        private Vector3 GetGroundStartPosition()
        {
            var spriteSize = _groundSprite.bounds.size;
            spriteSize.z = 0f;

            var centerOffset = _groundSprite.bounds.max - _groundSprite.bounds.center;
            centerOffset.y *= -1f;
            centerOffset.z = 0f;

            var rowColmnOffset = new Vector3(-((float)_columns) / 2f, ((float)_rows) / 2f);
            var startPosition = Vector3.Scale(spriteSize, rowColmnOffset) + centerOffset;

            return startPosition;
        }

        private Vector3 GetGroundIncrement()
        {
            var spriteSize = _groundSprite.bounds.size;
            spriteSize.z = 0f;

            return spriteSize;
        }

        private void GenerateGround()
        {
            var startPosition = GetGroundStartPosition();
            var increment = GetGroundIncrement();

            for (int column = 0; column < _columns; column++)
            {
                for (int row = 0; row < _rows; row++)
                {
                    var groundObject = CreateObject(_groundSprite);
                    groundObject.transform.SetParent(_groundSubcontainer);

                    var multiplier = new Vector3(column, -row, 0);
                    groundObject.transform.position = startPosition + Vector3.Scale(increment, multiplier);
                }
            }
        }

        private void GenerateGroundBorders()
        {
            var spriteSize = _borderSprite_NW.bounds.size;
            spriteSize.z = 0f;

            var centerOffset = _borderSprite_NW.bounds.max - _borderSprite_NW.bounds.center;
            centerOffset.y *= -1f;
            centerOffset.z = 0f;

            var startPosition = GetGroundStartPosition() - centerOffset;
            var increment = GetGroundIncrement();

            var groundBorderObject_NW = CreateObject(_borderSprite_NW, 3);
            var groundBorderObject_NE = CreateObject(_borderSprite_NE, 3);
            var groundBorderObject_SW = CreateObject(_borderSprite_SW, 3);
            var groundBorderObject_SE = CreateObject(_borderSprite_SE, 3);

            groundBorderObject_NW.transform.SetParent(_bordersSubcontainer);
            groundBorderObject_NE.transform.SetParent(_bordersSubcontainer);
            groundBorderObject_SW.transform.SetParent(_bordersSubcontainer);
            groundBorderObject_SE.transform.SetParent(_bordersSubcontainer);

            groundBorderObject_NW.transform.position = startPosition;
            groundBorderObject_NE.transform.position = startPosition + Vector3.Scale(Vector3.right * _columns, increment);
            groundBorderObject_SW.transform.position = startPosition + Vector3.Scale(Vector3.up * -_rows, increment);
            groundBorderObject_SE.transform.position = startPosition + Vector3.Scale(new Vector3(_columns, -_rows), increment);

            for (int column = 1; column < _columns; column++)
            {
                var multiplier_N = new Vector3(column, 0f, 0f);
                var multiplier_S = new Vector3(column, -_rows, 0f);

                var groundBorderObject_N = CreateObject(_borderSprite_N, 3);
                var groundBorderObject_S = CreateObject(_borderSprite_S, 3);

                groundBorderObject_N.transform.SetParent(_bordersSubcontainer);
                groundBorderObject_S.transform.SetParent(_bordersSubcontainer);

                groundBorderObject_N.transform.position = startPosition + Vector3.Scale(multiplier_N, increment);
                groundBorderObject_S.transform.position = startPosition + Vector3.Scale(multiplier_S, increment);
            }

            for (int row = 1; row < _rows; row++)
            {
                var multiplier_W = new Vector3(0f, -row, 0f);
                var multiplier_E = new Vector3(_columns, -row, 0f);

                var groundBorderObject_W = CreateObject(_borderSprite_W, 3);
                var groundBorderObject_E = CreateObject(_borderSprite_E, 3);

                groundBorderObject_W.transform.SetParent(_bordersSubcontainer);
                groundBorderObject_E.transform.SetParent(_bordersSubcontainer);

                groundBorderObject_W.transform.position = startPosition + Vector3.Scale(multiplier_W, increment);
                groundBorderObject_E.transform.position = startPosition + Vector3.Scale(multiplier_E, increment);
            }
        }

        private void GenerateGrass()
        {
            for (int grassIndex = 0; grassIndex < _grassAmount; grassIndex++)
            {
                var grassSpriteIndex = Random.Range(0, _grassSprites.Count-1);
                var grassSprite = _grassSprites[grassSpriteIndex];
                var grassObject = CreateObject(grassSprite, 1);
                var grassPosition = _levelBoundary.GetRandomPositionInside();
                var grassRotation = RandomRotation();
                grassPosition = MakePixelPerfectPosition(grassPosition);

                grassObject.transform.SetParent(_grassSubcontainer);
                grassObject.transform.position = grassPosition;
                grassObject.transform.eulerAngles = grassRotation;
            }
        }

        private void GenerateProps()
        {
            for (int propIndex = 0; propIndex < _grassAmount; propIndex++)
            {
                var propSpriteIndex = Random.Range(0, _propsSprites.Count - 1);
                var propSprite = _propsSprites[propSpriteIndex];
                var propObject = CreateObject(propSprite, 2);
                var propPosition = _levelBoundary.GetRandomPositionInside();
                propPosition = MakePixelPerfectPosition(propPosition);

                propObject.transform.SetParent(_propsSubcontainer);
                propObject.transform.position = propPosition;
            }
        }

        private void GenerateSurroundings()
        {
            var bounds = _levelBoundaryContainer.gameObject.GetComponent<BoxCollider2D>().bounds;

        }

        private void GenerateLevelBoundary()
        {
            var layer = LayerMask.NameToLayer(PhysicsLayer.LevelBoundary.ToString());
            _levelBoundaryContainer.gameObject.layer = layer;

            var boxCollider2D = _levelBoundaryContainer.gameObject.AddComponent<BoxCollider2D>();
            var levelSize = Vector2.Scale(Size, GetGroundIncrement());

            boxCollider2D.size = levelSize - new Vector2(_levelBoundarySafeZone, _levelBoundarySafeZone) * 2;
            boxCollider2D.isTrigger = true;

            _levelBoundary = _levelBoundaryContainer.gameObject.AddComponent<LevelBoundary>();
            _levelBoundary._boxCollider = boxCollider2D;

            var levelBoundary_N = new GameObject("Level Boundary N");
            var levelBoundary_S = new GameObject("Level Boundary S");
            var levelBoundary_W = new GameObject("Level Boundary W");
            var levelBoundary_E = new GameObject("Level Boundary E");

            levelBoundary_N.layer = layer;
            levelBoundary_S.layer = layer;
            levelBoundary_W.layer = layer;
            levelBoundary_E.layer = layer;

            levelBoundary_N.transform.SetParent(_levelBoundaryContainer);
            levelBoundary_S.transform.SetParent(_levelBoundaryContainer);
            levelBoundary_W.transform.SetParent(_levelBoundaryContainer);
            levelBoundary_E.transform.SetParent(_levelBoundaryContainer);

            var boxCollider2D_N = levelBoundary_N.AddComponent<BoxCollider2D>();
            var boxCollider2D_S = levelBoundary_S.AddComponent<BoxCollider2D>();
            var boxCollider2D_W = levelBoundary_W.AddComponent<BoxCollider2D>();
            var boxCollider2D_E = levelBoundary_E.AddComponent<BoxCollider2D>();

            boxCollider2D_N.size = new Vector2(levelSize.x + _levelBoundaryThickness * 2, _levelBoundaryThickness);
            boxCollider2D_S.size = new Vector2(levelSize.x + _levelBoundaryThickness * 2, _levelBoundaryThickness);
            boxCollider2D_W.size = new Vector2(_levelBoundaryThickness, levelSize.y + _levelBoundaryThickness * 2);
            boxCollider2D_E.size = new Vector2(_levelBoundaryThickness, levelSize.y + _levelBoundaryThickness * 2);

            boxCollider2D_N.offset = new Vector2(0f, levelSize.y / 2 + _levelBoundaryThickness / 2);
            boxCollider2D_S.offset = new Vector2(0f, -levelSize.y / 2 - _levelBoundaryThickness / 2);
            boxCollider2D_W.offset = new Vector2(-levelSize.x / 2 - _levelBoundaryThickness / 2, 0f);
            boxCollider2D_E.offset = new Vector2(levelSize.x / 2 + _levelBoundaryThickness / 2, 0f);
        }

        private void GenerateWorldBoundary()
        {
            _worldBoundaryContainer.gameObject.layer = LayerMask.NameToLayer(PhysicsLayer.WorldBoundary.ToString());
            _worldBoundaryContainer.gameObject.AddComponent<WorldBoundary>();

            var boxCollider2D = _worldBoundaryContainer.gameObject.AddComponent<BoxCollider2D>();
            var offset = Vector2.Scale(new Vector2(_worldBoundaryOffset, _worldBoundaryOffset), GetGroundIncrement());
            
            boxCollider2D.size = Vector2.Scale(Size, GetGroundIncrement()) + offset*2;
            boxCollider2D.isTrigger = true;
        }

        private GameObject CreateObject(Sprite sprite, int orderInLayer = 0)
        {
            var newGameObject = new GameObject(sprite.name);
            var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = "Terrain";
            spriteRenderer.sortingOrder = orderInLayer;

            return newGameObject;
        }

        private void Clear()
        {
            var children = new List<GameObject>();
            foreach (Transform child in transform)
                children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
        }

        private Vector3 RandomRotation()
        {
            float rotationAngle = 90f * Random.Range(0, 3);
            return new Vector3(0f, 0f, rotationAngle); 
        }

        private Vector3 MakePixelPerfectPosition(Vector3 vector)
        {
            var pixelSize = 1f / PPU;
            var x = (int)(vector.x / pixelSize) * pixelSize;
            var y = (int)(vector.y / pixelSize) * pixelSize;

            return new Vector3(x, y, 0f);
        }
        #endregion
    }
}