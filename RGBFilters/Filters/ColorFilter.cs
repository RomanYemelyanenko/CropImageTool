using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using System.Drawing;
using Android.App;

namespace RGBFilters
{
	public class ColorFilter : IColorFilter
	{
		View Parent { get; set; }

		public IRecognizer Recognizer {get; set;}

		ColorMatrixColorFilter CFilter{ get; set;}

		public ColorFilter (View parent, ColorMatrix cm)
		{
			CFilter = new ColorMatrixColorFilter(cm);

			Parent = parent;
		}

		public bool IsConflictWith(IFilter filter){
			return true; //filter is IColorFilter;
		}

		public void InitWithImg(SmartBitmap img)
		{
			var dummyAdapter = new DummyAdapter();
			dummyAdapter.Init();

			Recognizer = dummyAdapter;
		}

		public void TransformImg (SmartBitmap img)
		{
			img.Paint.SetColorFilter(CFilter);
		}

		public void DrawOn (Android.Graphics.Canvas canvas)
		{}

		public Android.Graphics.Bitmap ApplyTo(SmartBitmap img)
		{
			return null;
		}
	}
}

