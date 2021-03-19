using System;
using System.Web.Mvc;

namespace MSLivingChoices.Mvc.Uipc.Admin.ModelBinders
{
	internal static class UtilsForBinding
	{
		internal static bool? GetBooleanValue(IValueProvider valueProvider, string propertyName)
		{
			bool result;
			bool? nullable;
			ValueProviderResult value = valueProvider.GetValue(propertyName);
			if (value == null || string.IsNullOrWhiteSpace(value.get_AttemptedValue()))
			{
				nullable = null;
				return nullable;
			}
			if (bool.TryParse(value.get_AttemptedValue(), out result))
			{
				return new bool?(result);
			}
			nullable = null;
			return nullable;
		}

		internal static string GetStringValue(IValueProvider valueProvider, string propertyName)
		{
			ValueProviderResult value = valueProvider.GetValue(propertyName);
			if (value == null || string.IsNullOrWhiteSpace(value.get_AttemptedValue()))
			{
				return null;
			}
			return value.get_AttemptedValue();
		}
	}
}