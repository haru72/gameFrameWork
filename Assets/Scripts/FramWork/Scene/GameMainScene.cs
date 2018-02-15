using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainScene : MonoBehaviour
{

	void Awake()
	{
		Application.targetFrameRate = 60;

		StartCoroutine( UpdateCoroutine() );
	}

	void Update()
	{
	}


	IEnumerator UpdateCoroutine()
	{
		var player = GameObject.CreatePrimitive( PrimitiveType.Cube );
		player.transform.position = new Vector3();

		var player2 = GameObject.CreatePrimitive( PrimitiveType.Cube );
		player2.transform.position = new Vector3();

		var chkObj = GameObject.CreatePrimitive( PrimitiveType.Capsule );
		chkObj.transform.position = new Vector3();

		float size = 5f;
		var targetObj = GameObject.CreatePrimitive( PrimitiveType.Sphere );
		var quadObj = GameObject.CreatePrimitive( PrimitiveType.Quad );
		quadObj.transform.SetParent( targetObj.transform , false );
		quadObj.transform.localScale = new Vector3( size , size , size );
		quadObj.transform.Rotate(new Vector3(90,0,0));

		List<GameObject> childObjList = new List<GameObject>();
		{
			var targetChileObj = GameObject.CreatePrimitive( PrimitiveType.Sphere );
			targetChileObj.transform.SetParent( targetObj.transform , false );
			targetChileObj.transform.localPosition = new Vector3(
				-size / 2 , 0 , -size / 2
			);
			childObjList.Add( targetChileObj );
		}
		{
			var targetChileObj = GameObject.CreatePrimitive( PrimitiveType.Sphere );
			targetChileObj.transform.SetParent( targetObj.transform , false );
			targetChileObj.transform.localPosition = new Vector3(
				size - size / 2 , 0 , -size / 2
			);
			childObjList.Add( targetChileObj );
		}
		{
			var targetChileObj = GameObject.CreatePrimitive( PrimitiveType.Sphere );
			targetChileObj.transform.SetParent( targetObj.transform , false );
			targetChileObj.transform.localPosition = new Vector3(
				size - size / 2 , 0 , size - size / 2
			);
			childObjList.Add( targetChileObj );
		}
		{
			var targetChileObj = GameObject.CreatePrimitive( PrimitiveType.Sphere );
			targetChileObj.transform.SetParent( targetObj.transform , false );
			targetChileObj.transform.localPosition = new Vector3(
				-size / 2 , 0 , size - size / 2
			);
			childObjList.Add( targetChileObj );
		}

		while( true )
		{
			if( Input.GetKey( KeyCode.Z ) )
			{
				if( Input.GetKey( KeyCode.J ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x - 0.1f ,
						player.transform.position.y ,
						player.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.L ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x + 0.1f ,
						player.transform.position.y ,
						player.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.I ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x ,
						player.transform.position.y ,
						player.transform.position.z + 0.1f
					);
				} else if( Input.GetKey( KeyCode.K ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x ,
						player.transform.position.y ,
						player.transform.position.z - 0.1f
					);
				} else if( Input.GetKey( KeyCode.U ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x ,
						player.transform.position.y + 0.1f ,
						player.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.O ) )
				{
					player.transform.position = new Vector3(
						player.transform.position.x ,
						player.transform.position.y - 0.1f ,
						player.transform.position.z
					);
				}

			} else
			{
				if( Input.GetKey( KeyCode.J ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x - 0.1f ,
						player2.transform.position.y ,
						player2.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.L ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x + 0.1f ,
						player2.transform.position.y ,
						player2.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.I ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x ,
						player2.transform.position.y ,
						player2.transform.position.z + 0.1f
					);
				} else if( Input.GetKey( KeyCode.K ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x ,
						player2.transform.position.y ,
						player2.transform.position.z - 0.1f
					);
				} else if( Input.GetKey( KeyCode.U ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x ,
						player2.transform.position.y + 0.1f ,
						player2.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.O ) )
				{
					player2.transform.position = new Vector3(
						player2.transform.position.x ,
						player2.transform.position.y - 0.1f ,
						player2.transform.position.z
					);
				}

			}

			if( Input.GetKey( KeyCode.Z ) )
			{
				if( Input.GetKey( KeyCode.A ) )
				{
					targetObj.transform.Rotate(new Vector3(-1,0,0));
				} else if( Input.GetKey( KeyCode.D ) )
				{
					targetObj.transform.Rotate(new Vector3(-1,0,0));
				} else if( Input.GetKey( KeyCode.W ) )
				{
					targetObj.transform.Rotate(new Vector3( 0, - 1,0));
				} else if( Input.GetKey( KeyCode.S ) )
				{
					targetObj.transform.Rotate(new Vector3(0,1,0));
				} else if( Input.GetKey( KeyCode.Q ) )
				{
					targetObj.transform.Rotate(new Vector3(0,0,-1));
				} else if( Input.GetKey( KeyCode.E ) )
				{
					targetObj.transform.Rotate(new Vector3(0,0,1));
				}
			} else
			{
				if( Input.GetKey( KeyCode.A ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x - 0.1f ,
						targetObj.transform.position.y ,
						targetObj.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.D ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x + 0.1f ,
						targetObj.transform.position.y ,
						targetObj.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.W ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x ,
						targetObj.transform.position.y ,
						targetObj.transform.position.z + 0.1f
					);
				} else if( Input.GetKey( KeyCode.S ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x ,
						targetObj.transform.position.y ,
						targetObj.transform.position.z - 0.1f
					);
				} else if( Input.GetKey( KeyCode.Q ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x ,
						targetObj.transform.position.y + 0.1f ,
						targetObj.transform.position.z
					);
				} else if( Input.GetKey( KeyCode.E ) )
				{
					targetObj.transform.position = new Vector3(
						targetObj.transform.position.x ,
						targetObj.transform.position.y - 0.1f ,
						targetObj.transform.position.z
					);
				}
			}




			var posList = new List<Vector3>();
			foreach( var childObj in childObjList )
			{
				posList.Add( childObj.transform.position );
			}

			var chkPos = new Vector3();
			var chk = CollisionUtility.CalcCrossLineToPlane(
				out chkPos ,
				player.transform.position,
				player2.transform.position,
				posList
			);
			if( chk )
			{
				chkObj.transform.position = chkPos;
				chkObj.SetActive( true );
			} else
			{
				chkObj.SetActive( false );
			}


			yield return null;
		}

	}

}
