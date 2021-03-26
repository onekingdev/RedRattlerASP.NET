using MSLivingChoices.Mvc.Uipc.Client.CompetitiveFormatters.OptionsResolver;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MSLivingChoices.Mvc.Uipc.Client.ViewModels
{
	public class HomeQuickViewVm : ICommunityUnit
	{
		public AddressVm Address
		{
			get;
			set;
		}

		public PropertyVm Area
		{
			get;
			set;
		}

		public PropertyVm Bathes
		{
			get;
			set;
		}

		public PropertyVm Beds
		{
			get;
			set;
		}

		public HomeDisplayProperties DisplayProperties
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public ImageVm Image
		{
			get;
			set;
		}

		public List<ImageVm> Images
		{
			get;
			set;
		}

		public LeadFormVm LeadForm
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Package
		{
			get;
			set;
		}

		public string Price
		{
			get;
			set;
		}

		public string SaleType
		{
			get;
			set;
		}

		public string YearBuilt
		{
			get;
			set;
		}

		public HomeQuickViewVm()
		{
		}
	}
}