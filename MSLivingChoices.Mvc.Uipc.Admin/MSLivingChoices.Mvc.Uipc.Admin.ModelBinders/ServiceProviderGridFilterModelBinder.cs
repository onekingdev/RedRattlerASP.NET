using MSLivingChoices.Bcs.Admin.Components;
using MSLivingChoices.Entities.Admin;
using MSLivingChoices.Entities.Admin.Enums;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MSLivingChoices.Mvc.Uipc.Admin.ModelBinders
{
	public class ServiceProviderGridFilterModelBinder : IModelBinder
	{
		public ServiceProviderGridFilterModelBinder()
		{
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			ServiceProviderGridFilter filter = new ServiceProviderGridFilter()
			{
				ServiceProvider = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "serviceProvider"),
				Feature = UtilsForBinding.GetBooleanValue(bindingContext.ValueProvider, "feature"),
				FeatureStart = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "featureStart"),
				FeatureEnd = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "featureEnd"),
				Publish = UtilsForBinding.GetBooleanValue(bindingContext.ValueProvider, "publish"),
				PublishStart = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "publishStart"),
				PublishEnd = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "publishEnd")
			};
			string packages = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "packages");
			List<long> checkedPackages = (packages != null ? (new List<string>(packages.Split(new char[] { ',' }))).ConvertAll<long>(new Converter<string, long>(long.Parse)) : new List<long>());
			filter.Packages = new List<KeyValuePair<int, string>>();
			foreach (KeyValuePair<int, string> package in ItemTypeBc.Instance.GetAdditionalInfo(AdditionalInfoClass.Package))
			{
				if (!checkedPackages.Contains((long)package.Key))
				{
					continue;
				}
				filter.Packages.Add(package);
			}
			string categories = UtilsForBinding.GetStringValue(bindingContext.ValueProvider, "categories");
			List<long> checkedCategories = (categories != null ? (new List<string>(categories.Split(new char[] { ',' }))).ConvertAll<long>(new Converter<string, long>(long.Parse)) : new List<long>());
			filter.Categories = new List<KeyValuePair<int, string>>();
			foreach (KeyValuePair<int, string> category in ItemTypeBc.Instance.GetSHCCategoriesForServiceProvider())
			{
				if (!checkedCategories.Contains((long)category.Key))
				{
					continue;
				}
				filter.Categories.Add(category);
			}
			return filter;
		}
	}
}