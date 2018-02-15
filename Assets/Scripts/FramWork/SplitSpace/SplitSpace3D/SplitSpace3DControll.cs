using System;
using System.Collections.Generic;
using UnityEngine;

public class SplitSpace3DControll : Singleton<SplitSpace3DControll>
{
	SplitSpace3D _splitSpaceMain;

	public void Init()
	{
		_splitSpaceMain = new SplitSpace3D();
		var splitSpace3DData = new SplitSpace3D.SplitSpace3DData();
		splitSpace3DData._posCenter = new Vector3(0,1.5f,0);
		splitSpace3DData._sizeDiameter = new Vector3( 9 , 10 , 9 );
		splitSpace3DData._splitCountList.Add(
			new SplitSpace3D.SplitSpace3DData.SplitCount( 3 , 1 , 3 )
		);
		_splitSpaceMain.Init( splitSpace3DData );

	}

	public SplitSpace3D GetSplitSpaceMain()
	{
		return _splitSpaceMain;
	}


}
