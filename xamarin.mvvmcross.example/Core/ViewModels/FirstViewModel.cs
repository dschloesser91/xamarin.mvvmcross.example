using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public IMvxCommand NavigateCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SecondViewModel>());}
        }
    }
}
