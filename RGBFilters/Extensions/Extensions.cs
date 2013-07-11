using System;
using System.Drawing;
using Android.Graphics;
using System.Collections.Generic;

namespace RGBFilters
{
	static class RectangleFExtension
	{
		public static float GetCenterX (this RectangleF rect) // It's incorrect but exactly what I need
		{ 
			return rect.Left + rect.Width / 2f;
		}

		public static float GetCenterY (this RectangleF rect) // It's incorrect but exactly what I need
		{ 
			return rect.Top + rect.Height / 2f; 
		}

		public static RectangleF Move (this RectangleF rect, float dx, float dy)
		{
			return new RectangleF(rect.Left + dx, rect.Top + dy, rect.Width, rect.Height);
		}

		public static RectangleF Scale (this RectangleF rect, float scaleFactor, float focusX, float focusY)
		{
			var newLeft = (focusX - (focusX - rect.Left) * scaleFactor).Round ();
			var newTop = (focusY - (focusY - rect.Top) * scaleFactor).Round ();

			var newWidth = (rect.Width * scaleFactor).Round ();
			var newHeight = (rect.Height * scaleFactor).Round ();

			return new RectangleF(newLeft, newTop, newWidth, newHeight);
		}

		public static RectangleF GetOverRect(this RectangleF selfRect, RectangleF borderRect) // Later, vault hunter
		{
			var overLeft = Math.Max(0, borderRect.Left - selfRect.Left);
			var overTop = Math.Max(0, borderRect.Top - selfRect.Top);
			var overRight = Math.Max(0, selfRect.Right - borderRect.Right);
			var overBottom = Math.Max(0, selfRect.Bottom - borderRect.Bottom);

			return RectangleF.FromLTRB(overLeft, overTop, overRight, overBottom);
		}


	}

	static class FilterExtension
	{
		public static Bitmap Apply(this SmartBitmap image, IFilter filter){
			return filter.ApplyTo(image);
		}
	}
	
	static class MathExtensions
	{
		public static int Round (this float value)
		{
			return (int)Math.Round (value);
		}
		
		public static int Round (this double value)
		{
			return (int)Math.Round (value);
		}
	}

	static class MatrixExtensions
	{
		private static float[] matrixValues = new float[9];

		public static float GetScale (this Matrix matrix)
		{
			matrix.GetValues (matrixValues);
			return matrixValues [Matrix.MscaleX];
		}

		public static float GetTranslationX (this Matrix matrix)
		{
			matrix.GetValues (matrixValues);
			return matrixValues [Matrix.MtransX];
		}

		public static float GetTranslationY (this Matrix matrix)
		{
			matrix.GetValues (matrixValues);
			return matrixValues [Matrix.MtransY];
		}
	}

	static class CanvasExtension
	{
		public static void DrawSmartBitmap (this Canvas canvas, SmartBitmap img, List<IFilter> filters)
		{
			img.DrawOn (canvas, filters);
		}

		public static void DrawRotatableRectF (this Canvas canvas, RotatableRectF rect, Paint paint){
			foreach(var side in rect.GetSides()){
				canvas.DrawLine (side.StartPoint.X, side.StartPoint.Y, side.EndPoint.X, side.EndPoint.Y, paint);
			}
		}
	}

	static class PointExctension
	{
		public static float GetDistanceTo(this System.Drawing.PointF pointA, System.Drawing.PointF pointB)
		{
			return (float)Math.Sqrt ((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
		}
	}

	static class LineExctension
	{
		public static System.Drawing.PointF GetIntersectionWith(this Line lineA, Line lineB)
		{
			var x = (lineB.B - lineA.B) / (lineA.K - lineB.K);
			var y = lineA.K * x + lineA.B;
			return new System.Drawing.PointF(x, y);
		}
	}

	static class RotatableRectFExtension
	{
		public static Line[] GetSides(this RotatableRectF rect)
		{
			var verticies = rect.GetVertices ();
			if (verticies.Length != 4) 
				throw new ArgumentException ("Something is terribly wrong with input rect");

			return new Line[]{
				new Line(verticies[3], verticies[0]), // left
				new Line(verticies[0], verticies[1]), // top
				new Line(verticies[1], verticies[2]), // right
				new Line(verticies[2], verticies[3]) // bottom
			};	
		}

		public static System.Drawing.PointF[] GetVertices(this RotatableRectF rect){
			return new System.Drawing.PointF[]{
				new System.Drawing.PointF(rect.Left, 
				                          rect.Top), // left | top

				new System.Drawing.PointF(rect.Left + rect.Width * rect.CosAlpha, 
				                          rect.Top + rect.Width * rect.SinAlpha), // right | top

				new System.Drawing.PointF(rect.Left + rect.Width * rect.CosAlpha - rect.Height * rect.SinAlpha, 
				                          rect.Top + rect.Height * rect.CosAlpha + rect.Width * rect.SinAlpha), // right | bottom

				new System.Drawing.PointF(rect.Left - rect.Height * rect.SinAlpha, 
				                          rect.Top + rect.Height * rect.CosAlpha), // left | bottom
			};
		}

		public static float GetCenterX (this RotatableRectF rect) // It's incorrect but exactly what I need
		{ 
			return rect.Left + (rect.Width / 2f) * rect.CosAlpha - (rect.Height / 2f) * rect.SinAlpha;
		}

		public static float GetCenterY (this RotatableRectF rect) // It's incorrect but exactly what I need
		{ 
			return rect.Top + (rect.Height / 2f) * rect.CosAlpha + (rect.Width / 2f) * rect.SinAlpha;
		}
	}
}

