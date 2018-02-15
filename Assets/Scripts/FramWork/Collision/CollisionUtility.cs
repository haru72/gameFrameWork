using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollisionUtility
{

	static public bool IsGoOverAtoB( float posA , float movedA , float posB )
	{
		if(
			( ( movedA > 0 ) && ( posA >= posB ) && ( ( posA - movedA ) < posB) ) ||
			( ( movedA < 0 ) && ( posA <= posB ) && ( ( posA - movedA ) > posB) )
		){
			return true;
		}
		return false;
	}

	static public bool IsHitPointToBox( Vector3 point , Vector3 boxCenter , Vector3 boxSize )
	{
		if( (point.x >= boxCenter.x - boxSize.x) && (point.x <= boxCenter.x + boxSize.x) &&
			(point.y >= boxCenter.y - boxSize.y) && (point.y <= boxCenter.y + boxSize.y) &&
			(point.z >= boxCenter.z - boxSize.z) && (point.z <= boxCenter.z + boxSize.z)
		){
			return true;
		}
		return false;
	}


	static public bool IsHitBoxAToB( Vector3 boxCenterA , Vector3 boxSizeA , Vector3 boxCenterB , Vector3 boxSizeB )
	{
		if( (boxCenterA.x + boxSizeA.x >= boxCenterB.x - boxSizeB.x) && (boxCenterA.x - boxSizeA.x <= boxCenterB.x + boxSizeB.x) &&
			(boxCenterA.y + boxSizeA.y >= boxCenterB.y - boxSizeB.y) && (boxCenterA.y - boxSizeA.y <= boxCenterB.y + boxSizeB.y) &&
			(boxCenterA.z + boxSizeA.z >= boxCenterB.z - boxSizeB.z) && (boxCenterA.z - boxSizeA.z <= boxCenterB.z + boxSizeB.z)
		)
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// 線分と平面の交点取得
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="vertexList"></param>
	/// <returns></returns>
	static public bool CalcCrossLineToPlane(
		out Vector3 ans ,
		Vector3 lineStartPos , Vector3 lineEndPos ,
		List<Vector3> vertexList
	){
		var normVec = CalcNormVec( vertexList[2] , vertexList[0] , vertexList[1] );
		var chk = CalcCrossLineToPlane( out ans , lineStartPos , lineEndPos , vertexList[0] , normVec );
		if( ! chk )
		{
			return false;
		}
		chk = IsExistPointOnPlane( ans , vertexList );
		return chk;
	}

	/// <summary>
	/// 線分と平面の交点取得
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="onPlanePos">平面上の点</param>
	/// <param name="planeNormVec">平面の法線</param>
	/// <returns></returns>
	static public bool CalcCrossLineToPlane(
		out Vector3 ans,
		Vector3 lineStartPos , Vector3 lineEndPos ,
		Vector3 onPlanePos , Vector3 vecPlaneNorm
	){
		ans = new Vector3();
		if( ! IsCrossLineToPlane( lineStartPos , lineEndPos , onPlanePos , vecPlaneNorm ) )
		{
			return false;
		}

		var range1 = CalcRangeByPointToPlane( lineStartPos , onPlanePos , vecPlaneNorm );
		var range2 = CalcRangeByPointToPlane( lineEndPos , onPlanePos , vecPlaneNorm );

		var d1 = Vector3.Distance( new Vector3() , range1 );
		var d2 = Vector3.Distance( new Vector3() , range2 );

		var a = 0f;
		if( (d1 + d2) != 0 )
		{
			a = d1 / (d1 + d2);
		}


		//onPlanePosを原点とする
		var vecPlaneToStart = lineStartPos - onPlanePos;
		var vecPlaneToEnd = lineEndPos - onPlanePos;

		var tempVec1 = vecPlaneToStart * (1f-a);
		var tempVec2 = vecPlaneToEnd * a;

		var toCrossPoint = tempVec1 + tempVec2;
		ans = onPlanePos + toCrossPoint;

		return true;
	}


	/// <summary>
	/// 法線ベクトル算出
	/// </summary>
	/// <param name="pos1"></param>
	/// <param name="pos2"></param>
	/// <param name="pos3"></param>
	/// <returns></returns>
	static public Vector3 CalcNormVec( Vector3 pos1 , Vector3 pos2 , Vector3 pos3 )
	{
		var vec1 = pos2 - pos1;
		var vec2 = pos3 - pos1;
		return Vector3.Cross( vec1 , vec2 );
	}

	/// <summary>
	/// 無限に広がる平面と線分とのあたり判定
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="onPlanePos">平面上の点</param>
	/// <param name="planeNormVec">平面の法線</param>
	/// <returns></returns>
	static public bool IsCrossLineToPlane(
		Vector3 lineStartPos , Vector3 lineEndPos , Vector3 onPlanePos ,Vector3 vecPlaneNorm )
	{
		//onPlanePosを原点とする
		var vecPlaneToStart = lineStartPos - onPlanePos;
		var vecPlaneToEnd = lineEndPos - onPlanePos;

		//同じ方向にないことをチェック
		var dot1 = Vector3.Dot( vecPlaneToStart , vecPlaneNorm );
		var dot2 = Vector3.Dot( vecPlaneToEnd , vecPlaneNorm );

		if( dot1 == dot2 )
		{
			return false;
		}

		bool chk = dot1 * dot2 <= 0;

		return chk;
	}

	/// <summary>
	/// 点と平面の距離算出
	/// </summary>
	/// <param name="pointPos"></param>
	/// <param name="onPlanePos"></param>
	/// <param name="vecPlaneNorm"></param>
	/// <returns></returns>
	static public Vector3 CalcRangeByPointToPlane( Vector3 pointPos , Vector3 onPlanePos , Vector3 vecPlaneNorm )
	{
		//onPlanePosを原点とする
		var vecPlaneToPoint = pointPos - onPlanePos;

		var dot = Vector3.Dot( vecPlaneNorm , vecPlaneToPoint );

		var dotAbs = Mathf.Abs( dot );
		var retX = 0f;
		var retY = 0f;
		var retZ = 0f;
		if( vecPlaneNorm.x != 0 )
		{
			retX = dotAbs / Mathf.Abs( vecPlaneNorm.x );
		}
		if( vecPlaneNorm.y != 0 )
		{
			retY = dotAbs / Mathf.Abs( vecPlaneNorm.y );
		}
		if( vecPlaneNorm.z != 0 )
		{
			retZ = dotAbs / Mathf.Abs( vecPlaneNorm.z );
		}

		var ret = new Vector3( retX , retY , retZ );

		return ret;
	}

	/// <summary>
	/// 点が平面上に存在するかチェック
	/// </summary>
	/// <param name="pointPos"></param>
	/// <param name="vertexList">平面の頂点。右回りか左回りか統一する</param>
	/// <returns></returns>
	static public bool IsExistPointOnPlane( Vector3 pointPos , List<Vector3> vertexList )
	{
		var vertex1 = vertexList[0];
		var vertex2 = vertexList[1];

		var normResult = CalcNormVec( vertex2 , vertex1 , pointPos );

		for(int i = 1 ; i < vertexList.Count ; i++ )
		{
			vertex1 = vertexList[i];
			if( i + 1 >= vertexList.Count )
			{
				vertex2 = vertexList[0];
			}
			else
			{
				vertex2 = vertexList[i+1];
			}

			var tmpNormResult = CalcNormVec( vertex2 , vertex1 , pointPos );
			//法線の向きが違うとき、点は平面の外にある
			float resX = normResult.x * tmpNormResult.x;
			float resY = normResult.y * tmpNormResult.y;
			float resZ = normResult.z * tmpNormResult.z;

			float resXAbs = Mathf.Abs( resX );
			float resYAbs = Mathf.Abs( resY );
			float resZAbs = Mathf.Abs( resZ );
			float chk = 0f;
			if( resXAbs > resYAbs && resXAbs > resZAbs )
			{
				chk = resX;
			} else if( resYAbs > resZAbs )
			{
				chk = resY;
			} else
			{
				chk = resZ;
			}

			if( chk < 0 )
			{
				return false;
			}

		}

		return true;
	}
}
