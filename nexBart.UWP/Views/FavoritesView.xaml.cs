using nexBart.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace nexBart.UWP.Views
{
	public sealed partial class FavoritesView : UserControl
	{
		public FavoritesModel ViewModel;
		
		public FavoritesView()
		{
			this.InitializeComponent();

			ViewModel = new FavoritesModel();
			DataContext = ViewModel;
		}
	}
}
