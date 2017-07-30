using UnityEngine;

public struct Vector2Int  
{
	public int x;
	public int y;
	
	public Vector2Int( int x, int y )
	{
		this.x = x;
		this.y = y;
	}
	
	public override bool Equals( object theObject )
	{
		if ( theObject is Vector2Int )
		{
			return this.Equals( ( Vector2Int )theObject );
		}
		return false;
	}
	
	public bool Equals( Vector2Int theVector )
	{
		return (x == theVector.x ) && ( y == theVector.y );
	}
	
	public static bool operator == ( Vector2Int theLeft, Vector2Int theRight )
	{
		return theLeft.Equals( theRight );
	}
	
	public static bool operator != ( Vector2Int theLeft, Vector2Int theRight )
	{
		return !( theLeft.Equals( theRight ) );
	}
	
	
	public override int GetHashCode()
	{
		return x ^ y;
	}
	
	public static Vector2Int zero
	{
		get
		{
			return new Vector2Int( 0, 0 );
		}
	}
	
	public static Vector2Int one
	{
		get
		{
			return new Vector2Int( 1, 1 );
		}
	}

	public Vector3 ToVector3()
	{
		return ToVector3( 0 );
	}

	public Vector3 ToVector3( float theZPosition )
	{
		return new Vector3( this.x, this.y, theZPosition );
	}

	public Vector3 ToVector3WithOffset( Vector3 theOffset )
	{
		return new Vector3( this.x + theOffset.x, this.y + theOffset.y, theOffset.z );
	}
}
