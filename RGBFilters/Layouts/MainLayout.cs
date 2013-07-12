using System;
using Android.Widget;
using Android.Content;
using Android.Views;
using Android.Graphics;

namespace RGBFilters
{
	public class MainLayout : LinearLayout
	{
		public TextView Info;

		public readonly SeekBar RedSeek;
		public readonly SeekBar GreenSeek;
		public readonly SeekBar BlueSeek;
		public readonly SeekBar AlphaSeek;
		public readonly TextView TextView;
		public readonly FilterImageView ImageView;
		public readonly Button LoadImage;

		public MainLayout (Context context) : base(context)
		{



			Orientation = Orientation.Vertical;
			LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.MatchParent);
			SetGravity (GravityFlags.CenterHorizontal); //GravityFlags.CenterHorizontal);

			LoadImage = new Button (context);

			LoadImage.Text = "Load Image";
			AddView (LoadImage);

			RedSeek = new SeekBar (context);
			RedSeek.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.WrapContent, 0);
			AddView (RedSeek);

			GreenSeek = new SeekBar (context);
			GreenSeek.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.WrapContent, 0);
			AddView (GreenSeek);

			BlueSeek = new SeekBar (context);
			BlueSeek.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.WrapContent, 0);
			AddView (BlueSeek);

			AlphaSeek = new SeekBar (context);
			AlphaSeek.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.WrapContent, 0);
			AddView (AlphaSeek);

			TextView = new TextView (context);
			TextView.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.MatchParent, LayoutHelper.WrapContent, 0);
			AddView (TextView);

			ImageView = new FilterImageView (context);
			ImageView.LayoutParameters = new LinearLayout.LayoutParams (LayoutHelper.WrapContent, 0, 1);
			AddView (ImageView);

		}

	}
}

