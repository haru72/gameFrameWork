using UnityEngine;

public class Gauge
{
	protected int _value;
	protected int _valueMax;

	public void Init( int valueMax , int value )
	{
		_valueMax = valueMax;
		_value = value;

		InitSub();
	}

	protected virtual void InitSub()
	{
	}

	public int GetValue()
	{
		return _value;
	}

	public int GetValueMax()
	{
		return _valueMax;
	}

	public float GetPerent()
	{
		return (float)_value / _valueMax;
	}

	public void Add( int add )
	{
		if( _value + add > _valueMax )
		{
			add = _valueMax - _value;
		}
		else if( _value + add < 0 )
		{
			add = _value;
		}

		AddSub( add );
		_value += add;
	}

	protected virtual void AddSub( int add )
	{
	}

}