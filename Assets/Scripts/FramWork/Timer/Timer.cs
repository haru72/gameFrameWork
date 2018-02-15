using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Timer
{
	int _frame = 0;
	int _frameMax = 100;

	bool _isActive = true;

	public void Init( int frameMax )
	{
		_frame = 0;
		_frameMax = frameMax;
	}

	public void Update()
	{
		if( ! _isActive )
		{
			return;
		}

		if( IsEnd() )
		{
			return;
		}

		_frame++;
	}

	public void Reset()
	{
		_frame = 0;
	}

	public bool IsEnd()
	{
		return _frame >= _frameMax;
	}

	public float GetPercent()
	{
		return (float)_frame / _frameMax;
	}

	public void SetActive( bool isActive )
	{
		_isActive = isActive;
	}

}
