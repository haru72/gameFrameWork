using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileUtility : Singleton<FileUtility>
{
	public string ReadFile( string filePath )
	{
		var str = Resources.Load<TextAsset>( filePath ).text;
		return str;
	}

}