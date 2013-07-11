using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Widget;
using Android.Graphics;

namespace RGBFilters
{
	[Activity (Label = "RGBFilters", MainLauncher = true)]
	public class RGBfilters : Activity
	{

		private TextView _info;
		private SeekBar _rebSeek;
		private SeekBar _greenSeek;
		private SeekBar _blueSeek;
		private SeekBar _alphaSeek;
		private SurfaceView _image;

		private Bitmap _bitmap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			_bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.image_for_cropping);

			SetContentView (Resource.Layout.Main);

			SeekBar _rebSeek = FindViewById<SeekBar> (Resource.Id.redSeek);
			SeekBar _greenSeek = FindViewById<SeekBar> (Resource.Id.greenSeek);
			SeekBar _blueSeek = FindViewById<SeekBar> (Resource.Id.blueSeek);
			SeekBar _alphaSeek = FindViewById<SeekBar> (Resource.Id.alphaSeek);

			_rebSeek.Max = _greenSeek.Max = _blueSeek.Max = 100;

			_rebSeek.StopTrackingTouch += OnChangeSeek;
			_greenSeek.StopTrackingTouch += OnChangeSeek;
			_blueSeek.StopTrackingTouch += OnChangeSeek;

			_info = FindViewById<TextView> (Resource.Id.info);

			_info.Text = _rebSeek.Max.ToString();
			}

		private void OnChangeSeek(object sender, Android.Widget.SeekBar.StopTrackingTouchEventArgs e)
		{
			float scailR = 2f * _rebSeek.Max / 100F;
			float scailG = 2f * _greenSeek.Max / 100F;
			float scailB = 2f * _blueSeek.Max / 100F;
			float scailA = 2f * _blueSeek.Max / 100F;

			var filter = new ColorMatrix ();
			filter.SetScale (scailR, scailG, scailB,scailA);
			FilterImageView view = new FilterImageView (this);
			view.SetFilter (filter);
			view.SetBitmap (_bitmap);

			_info.Text = e.SeekBar.Progress.ToString();
		}
	}
}


