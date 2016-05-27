using System;
using Android.App;
using MvvmCross.Droid.Views;
using Droid.CustomPresenter.FragmentTypeLookup;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Core.ViewModels;

namespace Droid.CustomPresenter
{
    public class DroidPresenter : MvxAndroidViewPresenter
    {
        private readonly IMvxAndroidViewModelLoader _viewModelLoader;
        private readonly IFragmentTypeLookup _fragmentTypeLookup;
        private FragmentManager _fragmentManager;

        public DroidPresenter(IMvxAndroidViewModelLoader viewModelLoader, IFragmentTypeLookup fragmentTypeLookup)
        {
            _fragmentTypeLookup = fragmentTypeLookup;
            _viewModelLoader = viewModelLoader;
        }

        public void RegisterFragmentManager(FragmentManager fragmentManager, MvxFragment initialFragment)
        {
            _fragmentManager = fragmentManager;
            showFragment(initialFragment, false);
        }

        public override void Show(MvxViewModelRequest request)
        {
            Type fragmentType;

            if (_fragmentManager == null || !_fragmentTypeLookup.TryGetFragmentType(request.ViewModelType, out fragmentType))
            {
                base.Show(request);
                return;
            }

            var fragment = (MvxFragment)Activator.CreateInstance(fragmentType);
            fragment.ViewModel = ((MvxViewModelLoader)_viewModelLoader).LoadViewModel(request,null);
            showFragment(fragment,true);

        }

        private void showFragment(MvxFragment fragment, bool addToBackStack)
        {
            var transaction = _fragmentManager.BeginTransaction();

            if (addToBackStack)
            {
                transaction.AddToBackStack(fragment.GetType().Name);

                transaction.Replace(Resource.Id.contentFrame, fragment)
                           .Commit();
            }
        }

        public override void Close(IMvxViewModel viewModel)
        {
            var currentFragment = _fragmentManager.FindFragmentById(Resource.Id.contentFrame) as MvxFragment;
            if (currentFragment != null && currentFragment.ViewModel == viewModel)
            {
                _fragmentManager.PopBackStackImmediate();

                return;
            }
                base.Close(viewModel);
        }
    }
}