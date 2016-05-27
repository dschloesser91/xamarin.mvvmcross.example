using Android.OS;
using Android.Views;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Binding.Droid.BindingContext;

namespace Droid.Views.Frags
{
    public class InitialFragment: MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.FirstView,null);
        }
    }
}