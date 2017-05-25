using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace nexBart.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
		List<string> _menuItems;
		string _currentItem;


		public List<string> MenuItems
		{
			get
			{
				return _menuItems;
			}
			private set
			{
				if(_menuItems != value)
				{
					_menuItems = value;
					NotifyPropertyChanged();
				}
			}
		}
		public string CurrentItem
		{
			get
			{
				return _currentItem;
			}
			set
			{
				if(_currentItem != value)
				{
					_currentItem = value;
					ItemChanged();
				}
			}
		}

		public MenuViewModel()
		{
			MenuItems = new List<string>
			{
				"Real Time",
				"Schedule",
				"Planner"
			};
		}

		void ItemChanged()
		{
			Debug.WriteLine(CurrentItem);
		}
    }
}
