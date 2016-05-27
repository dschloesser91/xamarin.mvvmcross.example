using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.IoC;
using MvvmCross.Droid.FullFragging.Fragments;

namespace Droid.CustomPresenter.FragmentTypeLookup
{
    public class FragmentTypeLookup : IFragmentTypeLookup
    {
        private readonly IDictionary<string, Type> _fragmentLookup = new Dictionary<string, Type>();

        public FragmentTypeLookup()
        {
            _fragmentLookup = (from type in GetType().Assembly.ExceptionSafeGetTypes()
                               where !type.IsAbstract
                                  && !type.IsInterface
                                  && typeof(MvxFragment).IsAssignableFrom(type)
                                  && type.Name.EndsWith("View")
                               select type).ToDictionary(getStrippedName);
        }

        private string getStrippedName(Type type)
        {
            return type.Name.TrimEnd("View".ToCharArray())
                            .TrimEnd("ViewModel".ToCharArray());
        }

        public bool TryGetFragmentType(Type viewModelType, out Type fragmentType)
        {
            var strippedName = getStrippedName(viewModelType);

            if (!_fragmentLookup.ContainsKey(strippedName))
            {
                fragmentType = null;

                return false;
            }

            fragmentType = _fragmentLookup[strippedName];

            return true;
        }
    }
}