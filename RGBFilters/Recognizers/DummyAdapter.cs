using System;
using Android.Views;

namespace RGBFilters
{
	public class DummyAdapter : IRecognizer
	{


		public event Action Begin = () => {};
		public event Action End = () => {};

		public bool IsProcess {
			get {
				return false;
			} 
		}

		public DummyAdapter ()
		{}

		public void Init()
		{}

		public void Process(MotionEvent e, MotionEvent prevE){
		}

		public bool CanProcess(MotionEvent e, MotionEvent prevE){
			return false;
		}


	}
}

