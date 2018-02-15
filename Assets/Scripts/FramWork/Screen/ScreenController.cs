using System.Collections.Generic;
using UnityEngine;

public class ScreenController : Singleton<ScreenController>
{


	public class Space
	{
		Vector2 _posLeftDown;

		public Space( Vector2 posLeftDown )
		{
			_posLeftDown = posLeftDown;
		}

		public Vector2 GetPosLeftDown()
		{
			return _posLeftDown;
		}

		public Vector2 GetPosCenter()
		{
			return _posLeftDown + GetSize() / 2;
		}

		public Vector2 GetSize()
		{
			return ScreenController.GetInstance()._sizeOfSpaceOne;
		}

		public bool IsIn( Vector2 pos )
		{
			var size = GetSize();
			if( _posLeftDown.x <= pos.x && _posLeftDown.x + size.x >= pos.x &&
				 _posLeftDown.y <= pos.y && _posLeftDown.y + size.y >= pos.y
			)
			{
				return true;
			}
			return false;
		}

	}

	int _numX = 0;
	int _numY = 0;
	float _nearRange = 0;
	Vector2 _sizeOfSpaceOne;
	List<List<Space>> _spaceListList = new List<List<Space>>();
	public void Init( int numX , int numY )
	{
		_nearRange = Camera.main.transform.position.z;

		_numX = numX;
		_numY = numY;

		_sizeOfSpaceOne = new Vector2(
			Screen.width / _numX ,
			Screen.height / _numY
		);

		for( int i = 0 ; i < _numY ; i++ )
		{
			var spaceList = new List<Space>();
			for( int j = 0 ; j < _numX ; j++ )
			{
				var space = new Space(
					new Vector2( _sizeOfSpaceOne.x * j , _sizeOfSpaceOne.y * i )
				);

				spaceList.Add( space );
			}
			_spaceListList.Add( spaceList );
		}
	}

	public Space GetDisplaySpaceByRandom()
	{
		var id_1 = UnityEngine.Random.Range( 0 , _spaceListList.Count );
		var id_2 = UnityEngine.Random.Range( 0 , _spaceListList[id_1].Count );

		return _spaceListList[id_1][id_2];
	}

	public float GetNearRange()
	{
		return _nearRange;
	}


}