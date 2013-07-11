using System;
using Android.Graphics;
using Android.Views;
using Android.Graphics.Drawables;

namespace RGBFilters
{
	public interface IFilter
	{
		IRecognizer Recognizer {get;}
		bool IsConflictWith (IFilter filter);

		void InitWithImg (SmartBitmap img);
		void TransformImg(SmartBitmap img);
		void DrawOn(Canvas canvas);


		Bitmap ApplyTo(SmartBitmap bitmap);
	}

	public interface ICropFilter : IFilter
	{
	}

	public interface IRotationFilter : IFilter
	{
	}

	public interface IColorFilter : IFilter
	{
	}
}

