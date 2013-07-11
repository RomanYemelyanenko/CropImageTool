using System;
using System.Linq;
using System.Collections.Generic;
using Android.Views;

namespace RGBFilters
{
	public interface IRecognizer
	{
		event Action End; // ?
		event Action Begin;
		bool IsProcess {get;}
		bool CanProcess(MotionEvent e, MotionEvent prevE);
		void Process(MotionEvent e, MotionEvent prevE);
	}
}

