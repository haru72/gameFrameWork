using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SplitSpace
{
	public interface ISplitSpaceObj
	{
		Vector3 GetPos();
		void Delete();
		bool IsNotDelete();
		void SetActive( bool active );
	}

	/// <summary>
	/// 保持しているオブジェクトのリスト
	/// </summary>
	protected List<ISplitSpaceObj> _innerObjectList = new List<ISplitSpaceObj>();
	/// <summary>
	/// 保持している空間のリスト
	/// </summary>
	protected List<SplitSpace> _splitSpaceList = new List<SplitSpace>();

	protected SplitSpace _splitSpaceParent;

	bool _active = true;

	public SplitSpace GetParent()
	{
		return _splitSpaceParent;
	}

	public void SetParent( SplitSpace splitSpaceParent )
	{
		_splitSpaceParent = splitSpaceParent;
	}

	public bool Add( ISplitSpaceObj obj )
	{
		if( ! IsInSpace( obj.GetPos() ) )
		{
			return false;
		}

		foreach( var splitSpace in _splitSpaceList )
		{
			if( splitSpace.Add( obj ) )
			{
				return true;
			}
		}
		_innerObjectList.Add( obj );
		return true;
	}


	public int ObjectCount()
	{
		return _innerObjectList.Count;
	}

	public int ObjectCountInAllSpace()
	{
		int count = 0;
		foreach( var splitSpace in _splitSpaceList )
		{
			count += splitSpace.ObjectCountInAllSpace();
		}

		return count;
	}

	public void SetActive( bool active )
	{
		if( !active )
		{
			var removeObjList = new List<ISplitSpaceObj>();
			foreach( var innerObject in _innerObjectList )
			{
				if( !innerObject.IsNotDelete() )
				{
					innerObject.Delete();
					removeObjList.Add( innerObject );
				} else
				{
					innerObject.SetActive( false );
				}
			}
			foreach( var removeObj in removeObjList )
			{
				_innerObjectList.Remove( removeObj );
			}
		}
		else
		{
			foreach( var innerObject in _innerObjectList )
			{
				innerObject.SetActive( true );
			}
		}
	}

	public void SetActiveInAllSpace( bool active )
	{
		foreach( var splitSpace in _splitSpaceList )
		{
			splitSpace.SetActiveInAllSpace( active );
		}

		SetActive( active );
	}


	abstract protected void Split();
	abstract public bool IsInSpace( Vector3 pos );


}
