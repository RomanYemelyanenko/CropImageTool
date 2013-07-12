using System;
using Android.Views;
using Android.Widget;
using Android.Content;

namespace RGBFilters
{
	public static class LayoutHelper
	{
		public const int WrapContent = ViewGroup.LayoutParams.WrapContent;
		public const int MatchParent = ViewGroup.LayoutParams.MatchParent;

		public static View CreateSpacer(Context context, float weight)
		{
			var spacer = new FrameLayout(context);
			spacer.LayoutParameters = new LinearLayout.LayoutParams(0, 0, weight);
			return spacer;
		}
	}
}

