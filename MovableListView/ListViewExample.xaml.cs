using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace MovableListView
{
	public partial class ListViewExample : ContentPage
	{
		public ObservableCollection<SampleModel> BindingList { get; set; }

		public ListViewExample()
		{
			BindingList = new ObservableCollection<SampleModel>();
			Init(this.BindingList);
			InitializeComponent();
		}

		private void Init(ObservableCollection<SampleModel> initArray)
		{
			for (int i = 0; i < 10; i++)
			{
				initArray.Add(new SampleModel()
				{
					Name = $"Test - {i}",
					Direction = DirectionSorting.Asc
				});
			}
		}


		#region Working with changing sorting direction and move items
		void DirectionChangeClicked(object sender, System.EventArgs e)
		{
			var binding = GetBindingFromView<SampleModel>(sender);
			binding.Direction = binding.Direction == DirectionSorting.Asc ? DirectionSorting.Desc : DirectionSorting.Asc;
		}

		void ToUpClicked(object sender, System.EventArgs e)
		{
			var binding = GetBindingFromView<SampleModel>(sender);
			var currentIndexInListView = this.BindingList.IndexOf(binding);
			if (currentIndexInListView == 0)
				return;
			this.BindingList.Move(currentIndexInListView, currentIndexInListView - 1);
		}

		void ToDownClicked(object sender, System.EventArgs e)
		{
			var binding = GetBindingFromView<SampleModel>(sender);
			var currentIndexInListView = this.BindingList.IndexOf(binding);
			if ((currentIndexInListView + 1) == this.BindingList.Count)
				return;
			this.BindingList.Move(currentIndexInListView, currentIndexInListView + 1);
		}
		#endregion


		private T GetBindingFromView<T>(object view)
		{
			var convertedObject = view as Xamarin.Forms.View;
			var binding = (T)convertedObject.BindingContext;
			return binding;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			this.BindingContext = this;
		}
	}

	public class OrderConverter : IValueConverter, IMarkupExtension
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var convertedValue = (DirectionSorting)value;
			switch (convertedValue)
			{
				case DirectionSorting.Asc:
					return "▴";
				case DirectionSorting.Desc:
					return "▾";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}


}
