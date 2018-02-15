using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GaugeDivision : Gauge
{
	public enum Dir
	{
		TopToDown,
		DownToTop,
		LeftToRight,
		RightToLeft,
	}

	Dir _dir;
	Image _imageBase;

	GameObject _obj;

	int _valueChangeTimer = 0;
	int _valueChangeTimerEnd = 60;
	int _valueChange = 0;

	List<GaugeDivisionOne> _gaugeDivisionOneList = new List<GaugeDivisionOne>();

	public GaugeDivision( Dir dir , Image imageBase )
	{
		_dir = dir;
		_imageBase = imageBase;
	}

	protected override void InitSub()
	{
		_obj = new GameObject( "GaugeDivision" );

		float x = 0;
		float y = 0;
		switch( _dir )
		{
			case Dir.TopToDown:
				y = 1;
				break;
			case Dir.DownToTop:
				y = -1;
				break;
			case Dir.LeftToRight:
				x = 1;
				break;
			case Dir.RightToLeft:
				x = -1;
				break;
		}

		for( int i = 0 ; i < _valueMax ; i++ )
		{
			var image = GameObject.Instantiate<Image>( _imageBase );
			image.rectTransform.position = new Vector3(
				_imageBase.rectTransform.sizeDelta.x * x * i ,
				_imageBase.rectTransform.sizeDelta.y * y * i ,
				0
			);
			var divisionOne = new GaugeDivisionOne();
			divisionOne.Init( image );
			_gaugeDivisionOneList.Add( divisionOne );
		}
	}

	public void Setup( GameObject parent , Vector3 pos )
	{
		_obj.transform.SetParent( parent.transform );
		_obj.GetComponent<RectTransform>().position = pos;
	}

	public void Update()
	{
		if( _valueChange != _value )
		{
			_valueChangeTimer++;
			if( _valueChangeTimer >= _valueChangeTimerEnd )
			{
				_valueChangeTimer = 0;
				if( _value > 0 )
				{
					_gaugeDivisionOneList[_valueChange].StartAnim(true);
					_valueChange++;
				} else
				{
					_gaugeDivisionOneList[_valueChange].StartAnim(false);
					_valueChange--;
				}
			}
		}

		for( int i = 0 ; i < _gaugeDivisionOneList.Count ; i++ )
		{
			_gaugeDivisionOneList[i].Update();
		}
	}

}


public class GaugeDivisionOne
{
	Image _image;
	bool _isAnim = false;
	bool _isPlus = false;

	public void Init( Image image )
	{
		_image = image;
	}

	public void Update()
	{
		if( _isAnim )
		{
			Animation();
		}
	}


	public void StartAnim( bool isPlus )
	{
		_isAnim = true;
		_isPlus = isPlus;
	}

	void Animation()
	{
	}

}
