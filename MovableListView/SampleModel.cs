using System;
using PropertyChanged;
namespace MovableListView
{
	public enum DirectionSorting
	{
		Asc,
		Desc
	}

	[ImplementPropertyChanged]
	public class SampleModel
	{
		public string Name { get; set; }
		public DirectionSorting Direction { get; set; }
	}
}
