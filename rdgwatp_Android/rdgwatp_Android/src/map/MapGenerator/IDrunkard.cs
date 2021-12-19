using System;
using System.Collections.Generic;
using System.Text;

namespace rdgwatp_Android.src.map.MapGenerator
{
	//class Level;
	struct K_Rect
	{
		int x, y, w, h, rng;
	}
	interface IDrunkard
    {
		int Get_Edge_Direction();
		bool Sober_Up();
		void Think();
		void Turn();
	}
}
