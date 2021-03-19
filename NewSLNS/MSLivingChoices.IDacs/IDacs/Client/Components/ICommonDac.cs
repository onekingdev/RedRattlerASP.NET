using MSLivingChoices.Entities.Client.DisplayOptions;
using System;
using System.Collections.Generic;

namespace MSLivingChoices.IDacs.Client.Components
{
	public interface ICommonDac
	{
		Dictionary<int, List<CompetitiveItem>> GetCompetitiveItems(bool takeActiveOnly);
	}
}