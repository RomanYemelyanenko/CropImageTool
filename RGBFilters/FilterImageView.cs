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
using System.Drawing;

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
			if (_image != null && _filter != null) {
				var cFilter = new ColorMatrixColorFilter (_filter);
				Paint paint = new Paint ();
				paint.SetColorFilter (cFilter);

				Matrix matrix = new Matrix ();
				canvas.Save ();
				float horizotalScale = (Right - Left) / (float)_image.Width;
				float verticalScale = (Bottom - Top) / (float)_image.Height;
				float scale = horizotalScale < verticalScale ? horizotalScale : verticalScale;

				matrix.SetScale (scale,scale);

				canvas.DrawBitmap (_image, matrix, paint);
				canvas.Restore ();
			}

		}

	}
}

