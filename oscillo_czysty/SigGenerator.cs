using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oscillo_czysty
{
    class SigGenerator
    {
	public static double sawtooth(int t, int T, int offset)
	{
		if(t%T < T/2)
		{
			return (200.0/T) * t - (int)200*(t/T) + offset;
		}
		else
		{
			return -(200.0/T)*t + 200*((int)t/T + 1)+offset;
		}
	}
    }
}
