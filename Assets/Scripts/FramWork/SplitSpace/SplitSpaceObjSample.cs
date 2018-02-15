using System;
using System.Collections.Generic;
using UnityEngine;

public class SplitSpaceObjSample : SplitSpace.ISplitSpaceObj
{
	GameObject _obj;

	public void Init( GameObject obj )
	{
		_obj = obj;
	}

	public void Delete()
	{
		GameObject.Destroy( _obj );
	}
	public Vector3 GetPos()
	{
		return _obj.transform.position;
	}

	public void SetActive( bool active )
	{
		_obj.SetActive( active );
	}

	public bool IsNotDelete()
	{
		return true;
	}

}
