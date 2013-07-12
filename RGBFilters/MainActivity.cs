using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Util;
using System.IO;
using Android.Provider;

namespace RGBFilters
{
	[Activity (Label = "RGBFilters", MainLauncher = true)]
	public class RGBfilters : Activity
	{
		private const int PickImageRequestCode = 0x0100;
		private const int EditActivityStartRequestCode = 0x0100;

		private MainLayout _mainLayout;
		private Bitmap _bitmap;

		protected override void OnCreate (Bundle bundle)
		{
			_bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.image_for_cropping);
			base.OnCreate (bundle);

			_mainLayout = new MainLayout (this);
			_mainLayout.ImageView.SetBitmap (_bitmap);

			var filter = new ColorMatrix ();
			filter.SetScale (1, 1, 1, 1);
			_mainLayout.ImageView.SetFilter (filter);

			SetContentView (_mainLayout);

			_mainLayout.RedSeek.Progress = _mainLayout.GreenSeek.Progress  = _mainLayout.BlueSeek.Progress = _mainLayout.AlphaSeek.Progress = 50;

			_mainLayout.RedSeek.StopTrackingTouch += OnChangeSeek;
			_mainLayout.GreenSeek.StopTrackingTouch += OnChangeSeek;
			_mainLayout.BlueSeek.StopTrackingTouch += OnChangeSeek;
			_mainLayout.AlphaSeek.StopTrackingTouch += OnChangeSeek;
			_mainLayout.LoadImage.Click += LoadImage;
		}

		private void LoadImage(object sender, EventArgs e)
		{
			Intent intent = new Intent (Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
			StartActivityForResult (intent, PickImageRequestCode);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (requestCode == PickImageRequestCode && resultCode == Result.Ok && data != null) {
				var imageUri = data.DataString;
				Bitmap bitmap = BitmapFactory.DecodeStream(BaseContext.OpenFileInput(imageUri));
				_mainLayout.ImageView.SetBitmap (bitmap);
			}
			else if(requestCode == PickImageRequestCode && resultCode == Result.Canceled && data != null){
				Toast.MakeText(this, data.Action, ToastLength.Long).Show();
			}
		}

		private void OnChangeSeek(object sender, Android.Widget.SeekBar.StopTrackingTouchEventArgs e)
		{
			float scailR = 2f * _mainLayout.RedSeek.Progress / 100F;
			float scailG = 2f * _mainLayout.GreenSeek.Progress / 100F;
			float scailB = 2f * _mainLayout.BlueSeek.Progress / 100F;
			float scailA = 2f * _mainLayout.AlphaSeek.Progress / 100F;

			var filter = new ColorMatrix ();
			filter.SetScale (scailR, scailG, scailB, scailA);

			_mainLayout.ImageView.SetFilter (filter);

			_mainLayout.TextView.Text = "R: " + scailR + " | G: " + scailG + " | B : " + scailB + " | A: " + scailA;
			_mainLayout.ImageView.Invalidate ();

		}

	}
}


