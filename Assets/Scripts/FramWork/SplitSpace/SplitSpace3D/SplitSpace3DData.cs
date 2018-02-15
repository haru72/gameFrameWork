using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SplitSpace3D : SplitSpace
{
	public class SplitSpace3DData
	{
		//中心
		public Vector3 _posCenter;
		//直径
		public Vector3 _sizeDiameter;

		public class SplitCount
		{
			public int _x = 0;
			public int _y = 0;
			public int _z = 0;
			public SplitCount( int x , int y , int z )
			{
				_x = x;
				_y = y;
				_z = z;
			}
		}
		/// <summary>
		/// 縦・横・高さそれぞれ何分割するか
		/// リストの0番目が自身の分割数
		/// 2番目以降は翌階層ごとの分割数
		/// </summary>
		public List<SplitCount> _splitCountList = new List<SplitCount>();

		Vector3 _posLeftDownForwaerd;
		Vector3 _posRightUpBack;

		public SplitCount GetSplitCountSelf()
		{
			if( _splitCountList.Count == 0 )
			{
				return null;
			}
			return _splitCountList[0];
		}

		public List<SplitCount> GetSplitCountListChild()
		{
			var retList = new List<SplitCount>();
			for( int i = 1 ; i < _splitCountList.Count ; i++ )
			{
				retList.Add( _splitCountList[i] );
			}
			return retList;
		}

		public Vector3 GetLeftDownForwaerd()
		{
			if( _posLeftDownForwaerd == Vector3.zero && _posRightUpBack == Vector3.zero )
			{
				_posLeftDownForwaerd = new Vector3(
					_posCenter.x - _sizeDiameter.x / 2 ,
					_posCenter.y - _sizeDiameter.y / 2 ,
					_posCenter.z - _sizeDiameter.z / 2
				);
				_posRightUpBack = new Vector3(
					_posCenter.x + _sizeDiameter.x / 2 ,
					_posCenter.y + _sizeDiameter.y / 2 ,
					_posCenter.z + _sizeDiameter.z / 2
				);
			}

			return _posLeftDownForwaerd;
		}

		public Vector3 GetRightUpBack()
		{
			if( _posLeftDownForwaerd == Vector3.zero && _posRightUpBack == Vector3.zero )
			{
				_posLeftDownForwaerd = new Vector3(
					_posCenter.x - _sizeDiameter.x / 2 ,
					_posCenter.y - _sizeDiameter.y / 2 ,
					_posCenter.z - _sizeDiameter.z / 2
				);
				_posRightUpBack = new Vector3(
					_posCenter.x + _sizeDiameter.x / 2 ,
					_posCenter.y + _sizeDiameter.y / 2 ,
					_posCenter.z + _sizeDiameter.z / 2
				);
			}

			return _posRightUpBack;
		}

	}
}