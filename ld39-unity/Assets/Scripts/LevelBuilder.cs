using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour 
{
	public ToyManager toyManager;
	public EnemyToyManager enemyToyManager;

	public GameObject basicTile;

	public List<GameObject> fancyTiles;

	public GameObject dinoPrefab;

	public static int width = 8;
	public static int height = 8;

	private Tile[,] tiles = new Tile[8,8];
	private int[,] tileTypes = new int[8,8];

	private const int BASIC_TILE_TYPE = 1;
	private const int FANCY_TILE_TYPE = 2;

	// Use this for initialization
	void Awake () 
	{
		for ( int x = 0; x < width; x++ )
		{
			for ( int y = 0; y < height; y++ )
			{
				if ( Random.value < 0.3f )
				{
					tileTypes[x,y] = 2;
				}
				else
				{
					tileTypes[x,y] = 1;
				}
			}
		}

		for ( int x = 0; x < width; x++ )
		{
			for ( int y = 0; y < height; y++ )
			{
				GameObject tilePrefab = basicTile;

				if ( tileTypes[x,y] == FANCY_TILE_TYPE )
				{
					int randomIndex = Random.Range( 0, fancyTiles.Count );
					tilePrefab = fancyTiles[randomIndex];
				}

				GameObject instance = Instantiate( tilePrefab );
				instance.transform.position = new Vector3( x * 8, 0, y * 8 );
				tiles[x,y] = instance.GetComponent<Tile>();
			}
		}

		SpawnPlayers();
		SpawnEnemies();

		toyManager.Initialize();
	}

	void SpawnPlayers()
	{
		toyManager.toys = new List<Toy>();

		List<Vector2Int> playerPositions = new List<Vector2Int>();

		for ( int i = 0; i < 4; i++ )
		{
			Vector2Int randomPosition;

			do 
			{
				randomPosition = new Vector2Int( Random.Range( 0, 8 ), Random.Range( 0, 2 ) );
			}
			while ( playerPositions.Contains( randomPosition ) );

			playerPositions.Add( randomPosition );

		}

		foreach( Vector2Int playerPosition in playerPositions )
		{
			GameObject instance = Instantiate( dinoPrefab );
			Toy toy = instance.GetComponent<Toy>();

			toy.transform.position = tiles[playerPosition.x, playerPosition.y].playerSpawnPoints[0].position;

			toyManager.toys.Add( toy );

		}
	}

	void SpawnEnemies()
	{
		enemyToyManager.toys = new List<Toy>();

		List<Vector2Int> enemyPositions = new List<Vector2Int>();

		int numberOfEnemies = 3 + Round.currentRound;

		for ( int i = 0; i < numberOfEnemies; i++ )
		{
			Vector2Int randomPosition;

			do 
			{
				randomPosition = new Vector2Int( Random.Range( 0, 8 ), Random.Range( 2, 8 ) );
			}
			while ( enemyPositions.Contains( randomPosition ) );

			enemyPositions.Add( randomPosition );

		}

		foreach( Vector2Int enemyPosition in enemyPositions )
		{
			GameObject instance = Instantiate( dinoPrefab );
			instance.transform.position = tiles[enemyPosition.x, enemyPosition.y].playerSpawnPoints[0].position;
			Toy toy = instance.GetComponent<Toy>();
			toy.SetAsEnemy();

			enemyToyManager.toys.Add( toy );
		}

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
