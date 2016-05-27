using Android.App;
using Android.OS;
using Droid.CustomPresenter;
using Droid.Views.Frags;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;

namespace Droid.Views.Activities
{
    [Activity(Label = "Presenter Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Container);

            var presenter = (DroidPresenter)Mvx.Resolve<IMvxAndroidViewPresenter>();
            var initialFragment = new InitialFragment {ViewModel = ViewModel };

            presenter.RegisterFragmentManager(FragmentManager,initialFragment);
        }
    }
}