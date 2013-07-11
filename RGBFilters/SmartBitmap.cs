using System;
using Android.Graphics;
using System.Collections.Generic;
using Android.Views;
using Android.Graphics.Drawables;
using System.Drawing;

namespace RGBFilters
{
	public class SmartBitmap : IDisposable
	{
		public Paint Paint { get; private set; }

		public RectangleF BorderRect { get; set; }

		public RotatableRectF Rect { get; set; }

		private Bitmap OriginalBitmap{ get; set; }

		private Bitmap Bitmap { 
			get { return OriginalBitmap;} 
		}

		public int Width {
			get { return OriginalBitmap.Width;}
		}

		public int Height {
			get { return OriginalBitmap.Height;}
		}

		private Matrix Matrix { get; set;}

		public SmartBitmap (Bitmap bmp)
		{
			OriginalBitmap = bmp;
			Rect = RotatableRectF.FromRect (new RectangleF (0, 0, Bitmap.Width, Bitmap.Height));
			Matrix = new Matrix ();

			Paint = new Paint ();
		}

		public void DrawOn (Canvas canvas, List<IFilter> filters)
		{
			foreach (var filter in filters) {
				filter.TransformImg (this);
			}
			canvas.Save ();

			canvas.ClipRect (BorderRect.Left, BorderRect.Top, BorderRect.Right, BorderRect.Bottom);

			Matrix.SetScale (Rect.Width / (float)Bitmap.Width, Rect.Height / (float)Bitmap.Height);
			Matrix.PostTranslate (Rect.Left, Rect.Top);
			Matrix.PostRotate (Rect.RotationDg, Rect.Left, Rect.Top);
			canvas.DrawBitmap (Bitmap, Matrix, Paint);
			canvas.Restore ();

			foreach (var filter in filters) {
				filter.DrawOn (canvas);
			}
		}

		public Bitmap Apply(List<IFilter> filters)
		{
			return null;
		}

		public void Dispose ()
		{
			Paint.Dispose();
		}
	}
}

