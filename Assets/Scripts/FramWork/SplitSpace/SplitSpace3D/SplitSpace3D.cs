using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SplitSpace3D : SplitSpace
{
	SplitSpace3DData _splitSpace3DData;

	public void Init( SplitSpace3DData splitSpace3DData )
	{
		_splitSpace3DData = splitSpace3DData;
		Split();
	}

	override public bool IsInSpace( Vector3 pos )
	{
		var posLeftDownForwaerd = _splitSpace3DData.GetLeftDownForwaerd();
		var posRightUpBack = _splitSpace3DData.GetRightUpBack();
		if( posLeftDownForwaerd.x <= pos.x && pos.x < posRightUpBack.x &&
			posLeftDownForwaerd.y <= pos.y && pos.y < posRightUpBack.y &&
			posLeftDownForwaerd.z <= pos.z && pos.z < posRightUpBack.z
		) {
			return true;
		}
		return false;
	}


	//TODO:範囲内にこの空間が収まっているかチェック
	//TODO:範囲内に収まっている最小の空間を返す
	//TODO:範囲内にこの空間と重なる部分があるかチェック
	//TODO:範囲内に重なる部分がある最小の空間を返す

	/// <summary>
	/// 最小の空間を返す
	/// 自身より下がなかったら自身を返す
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public SplitSpace3D GetSplitSpaceMinimum( Vector3 pos )
	{
		if( ! IsInSpace( pos ) )
		{
			return null;
		}

		foreach( var splitSpace in _splitSpaceList )
		{
			var minimum = ((SplitSpace3D)splitSpace).GetSplitSpaceMinimum( pos );
			if( minimum != null )
			{
				return minimum;
			}
		}

		//自身より下がない
		return this;
	}

	/// <summary>
	/// 
	/// </summary>
	protected override void Split()
	{
		if( _splitSpaceList.Count > 0 )
		{
			return;
		}

		var splitCount = _splitSpace3DData.GetSplitCountSelf();
		if( splitCount == null )
		{
			return;
		}
		var splitCountListChild = _splitSpace3DData.GetSplitCountListChild();

		var sizeDiameter = new Vector3(
			_splitSpace3DData._sizeDiameter.x / splitCount._x,
			_splitSpace3DData._sizeDiameter.y / splitCount._y,
			_splitSpace3DData._sizeDiameter.z / splitCount._z
			);
		var posLeftDownForwaerd = _splitSpace3DData.GetLeftDownForwaerd();

		for( int z = 0 ; z < splitCount._z ; z++ )
		{
			for( int y = 0 ; y < splitCount._y ; y++ )
			{
				for( int x = 0 ; x < splitCount._x ; x++ )
				{
					var splitSpace = new SplitSpace3D();
					var splitSpaceData = new SplitSpace3DData();
					splitSpaceData._posCenter = new Vector3(
						posLeftDownForwaerd.x + sizeDiameter.x / 2 + (sizeDiameter.x * x) ,
						posLeftDownForwaerd.y + sizeDiameter.y / 2 + (sizeDiameter.y * y) ,
						posLeftDownForwaerd.z + sizeDiameter.z / 2 + (sizeDiameter.z * z)
					);
					splitSpaceData._sizeDiameter = sizeDiameter;
					splitSpaceData._splitCountList = splitCountListChild;
					splitSpace.Init( splitSpaceData );
					splitSpace.SetParent( this );
					_splitSpaceList.Add( splitSpace );
				}
			}
		}


	}


}
