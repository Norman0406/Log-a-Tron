using System;
using System.Collections.Generic;

namespace Logatron.ViewModels.RadioViewModels
{
    public static class RadioViewModelFactory
    {
        public static IList<string> ListRadioViewModels()
        {
            return new List<string>()
            {
                OmniRig1ViewModel.Name,
                OmniRig2ViewModel.Name
            };
        }

        public static RadioViewModel CreateRadioViewModel(string name)
        {
            return name switch
            {
                OmniRig1ViewModel.Name => new OmniRig1ViewModel(),
                OmniRig2ViewModel.Name => new OmniRig2ViewModel(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
