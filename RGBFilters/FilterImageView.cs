using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace RGBFilters
{
	public class FilterImageView : View
	{
		private Bitmap _image;
		private Paint _paint;
		private ColorMatrix _filter;

		public FilterImageView (Context context) :
			base (context)
		{
			Initialize ();
		}

		public void SetBitmap(Bitmap image)
		{
			_image = image;
			_paint = new Paint ();
		}

		public void SetFilter(ColorMatrix filter)//float red, float green, float blue, float alpha)
		{
			_filter = filter;
		}

		protected override void OnDraw (Canvas canvas)
		{
			var savedTime = DateTime.UtcNow;
			if(_image != null){
				canvas.DrawBitmap(_image, _filter); 
			}
		}

		public FilterImageView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public FilterImageView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
	}

	public static class CanvasExtension
	{
		public static void DrawBitmap (this Canvas canvas, Bitmap img, ColorMatrix filters, Rect rect, Paint paint)
		{
			img.DrawOn (canvas, filters, rect, pai);
		}
	}

	public	static class BitmapExtension
	{
		public static void DrawOn (this Bitmap map, Canvas canvas, ColorMatrix filter, Rect rect, Paint paint)
		{

			filter.TransformImg (map);

			canvas.Save ();

			//canvas.ClipRect (BorderRect.Left, BorderRect.Top, BorderRect.Right, BorderRect.Bottom);

			Matrix.SetScale (rect.Width() / (float)map.Width, rect.Height() / (float)map.Height);
			Matrix.PostTranslate (rect.Left, rect.Top);
			//Matrix.PostRotate (Rect.RotationDg, Rect.Left, Rect.Top);
			canvas.DrawBitmap (map, filter, paint);
			canvas.Restore ();

		}
	}

	public	static class ColorMatrixExtension
	{
		public static void TransformImg(this ColorMatrix matrix, Bitmap map)
		{

		}
	}
}

