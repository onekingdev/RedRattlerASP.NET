using Main.Models;
using MSLivingChoices.Mvc.Uipc.Client.CompetitiveFormatters.Core;
using MSLivingChoices.Mvc.Uipc.Client.Enums;
using MSLivingChoices.Mvc.Uipc.Client.Helpers;
using MSLivingChoices.Mvc.Uipc.Client.ViewModels;
using MSLivingChoices.Mvc.Uipc.Client.ViewModelsProviders;
using MSLivingChoices.Mvc.Uipc.Enums;
using MSLivingChoices.Mvc.Uipc.Results;
using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using MSLivingChoices.Utilities;

namespace Main.Controllers
{
	[ValidateInput(false)]
	public class ClientController : BaseController
	{
		 public ClientController()
		{
		}

		[HttpGet]
		public JsonResult Autocomplete(LookupLocationVm lookupLocation)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetAutocompleteVmList(lookupLocation));
		}

		[CompetitiveFormating]
		public ActionResult CommunityDetails(long id, PageType pageType,CommunitiesSearchVm searchVm)
		{
			CommunityDetailsVm result = ClientViewModelsProvider.GetCommunityDetailsVm(id, pageType);
			if (result == null)
			{
				return base.NotFound();
			}
			if (!result.ShouldRedirect(result.Community.ListingTypes))
			{
				result.Community.Code = "";
				if (result.Community.ListingTypes.Contains(MSLivingChoices.Entities.Client.Enums.ListingType.ActiveAdultCommunities))
					result.Community.Code += "AAC-";
				if(result.Community.ListingTypes.Contains(MSLivingChoices.Entities.Client.Enums.ListingType.ActiveAdultHomes))
					result.Community.Code += "AAH-";
				if (result.Community.ListingTypes.Contains(MSLivingChoices.Entities.Client.Enums.ListingType.SeniorHousingAndCare))
					result.Community.Code += "SHC-";

				ViewBag.SC = ClientViewModelsProvider.GetNewSmililarCommunity(searchVm, id);
				
				return base.View("~/Views/Client/Details/Community.cshtml", result);
			}
			return base.Http301Redirect(result.Seo.CanonicalUrl);
		}
		[CompetitiveFormating]
		[HttpGet]
		public JsonResult CommunityQuickView(long communityId, SearchType searchType)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetCommunityQuickViewVm(communityId, searchType));
		}

		[HttpGet]
		public JsonResult CommunitySearchUrl(CommunitiesSearchVm searchVm)
		{
			JsonResult result = new AllowGetJsonResult(MslcUrlBuilder.PagingUrl(searchVm, 1));
			return new AllowGetJsonResult(MslcUrlBuilder.PagingUrl(searchVm, 1));
		}
		public ActionResult Ebook()
		{
			return base.View("~/Views/Client/Static/Ebook.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.Ebook));
		}
        [HttpPost]
        public ActionResult Ebook(MSLivingChoices.Mvc.Uipc.Client.MSLivingChoices.Mvc.Uipc.Client.ViewModels.EbookOrder ord)
        {
			//string from = ord.Email;

			//string to = "kameron.seniorliving@gmail.com";

			//string subject = ord.FirstName+" "+ord.LastName + " Contacted From ORDER PAGE.";

			//string message = (ord.chkPAS?"BOOK REQUIRED PRODUCT AND SERVICES \n":"")+(ord.chkHomeHealth? "BOOK REQUIRED HOME HEALTH \n" : "")+ (ord.chkCommunities ? "BOOK REQUIRED COMMUNITIES \n" : "") +ord.Phone==""?ord.Phone+"\n":""+ "\n \n"+ord.ExtraMessage+"\n \n \n"+(ord.rad=="Yes"?"USER WOULD LOVE IF WE CAN CONTACT HIM VIA"+ord.Phone+" OR VIA "+ord.Email+"\n":"")+ "\n\n\n"+ord.street+"\n"+ord.state+"\n"+ord.zip;

			//SmtpClient objSmtpClient = new SmtpClient();
			//objSmtpClient.UseDefaultCredentials = true;
			//objSmtpClient.Host = "smtp.gmail.com";
			//objSmtpClient.Port = 587;

			//objSmtpClient.EnableSsl = true;
			//try
			//{
			//	objSmtpClient.Send(from, to, subject, message);
			//}
			//catch (Exception ex)
			//{
			//	return new AllowGetJsonResult(new { success = false, Message = ex.Message });
			//}


				return new AllowGetJsonResult(new { success = ClientViewModelsProvider.EBook(ord) });
        }
        [CompetitiveFormating]
		[HttpGet]
		public JsonResult FloorPlansQuickView(long communityId, SearchType searchType)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetFloorPlanQuickViewVmList(communityId, searchType));
		}

		[CompetitiveFormating]
		[HttpGet]
		public JsonResult HomesQuickView(long communityId, SearchType searchType)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetHomeQuickViewVmList(communityId, searchType));
		}

		public ActionResult Index()
		{
			return base.View("~/Views/Client/Search/Index.cshtml", ClientViewModelsProvider.GetIndexSearchVm());
		}

		public ActionResult All()
		{
			// Model
			return base.View("~/Views/Client/Search/All.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.SeniorType));
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintCommunity(long id, PageType pageType)
		{
			CommunityDetailsVm result = ClientViewModelsProvider.GetCommunityDetailsVmForPrint(id, pageType);
			if (result == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/Community.cshtml", result);
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintCommunityCoupon(long id, PageType pageType)
		{
			CommunityDetailsVm result = ClientViewModelsProvider.GetCommunityDetailsVmForPrint(id, pageType);
			if (result == null || result.Coupon == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/CommunityCoupon.cshtml", result);
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintCommunityDirection(long id, PageType pageType, double? lat, double? lon)
		{
			if (!lat.HasValue || !lon.HasValue)
			{
				return base.NotFound();
			}
			CommunityPrintDirectionVm result = ClientViewModelsProvider.GetCommunityPrintDirectionVm(id, pageType, lat.Value, lon.Value);
			if (result == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/CommunityDirection.cshtml", result);
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintServiceProvider(long id)
		{
			ServiceProviderDetailsVm result = ClientViewModelsProvider.GetServiceProviderDetailsVmForPrint(id);
			if (result == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/ServiceProvider.cshtml", result);
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintServiceProviderCoupon(long id)
		{
			ServiceProviderDetailsVm result = ClientViewModelsProvider.GetServiceProviderDetailsVmForPrint(id);
			if (result == null || result.Coupon == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/ServiceProviderCoupon.cshtml", result);
		}

		[CompetitiveFormating]
		[HttpGet]
		public ActionResult PrintServiceProviderDirection(long id, double? lat, double? lon)
		{
			if (!lat.HasValue || !lon.HasValue)
			{
				return base.NotFound();
			}
			ServiceProviderPrintDirectionVm result = ClientViewModelsProvider.GetServiceProviderPrintDirectionVm(id, lat.Value, lon.Value);
			if (result == null)
			{
				return base.NotFound();
			}
			return base.View("~/Views/Client/Print/ServiceProviderDirection.cshtml", result);
		}
		public ActionResult PrivacyPolicy()
		{
			return base.View("~/Views/Client/Static/PrivacyPolicy.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.PrivacyPolicy));
		}
		[HttpPost]
		public JsonResult ProcessLead(LeadFormVm leadFormVm)
		{
			return new JsonNetResult(ClientViewModelsProvider.ProcessLead(leadFormVm));
		}
		[HttpGet]
		public JsonResult ProviderSearchUrl(ServiceProvidersSearchVm searchVm)
		{
			return new AllowGetJsonResult(MslcUrlBuilder.PagingUrl(searchVm, 1));
		}

		[CompetitiveFormating]
		public ActionResult SearchCommunities(CommunitiesSearchVm searchVm)
		{
			//{
			//Value = ActiveAdultHomes
			//Value = SeniorHousingAndCare
			// }
			//{
			//Value = ActiveAdultCommunities
			//
			//}
			searchVm.Criteria.Components["SearchType"] = "ActiveAdultHomes";
			searchVm.SearchBar.SearchType = SearchType.ActiveAdultHomes;
			CommunitiesSearchVm result = ClientViewModelsProvider.GetCommunitiesSearchVm(searchVm);
            searchVm.Criteria.Components["SearchType"] = "SeniorHousingAndCare";
            searchVm.SearchBar.SearchType = SearchType.SeniorHousingAndCare;
            result.Result.AddRange(ClientViewModelsProvider.GetCommunitiesSearchVm(searchVm).Result);
            searchVm.Criteria.Components["SearchType"] = "ActiveAdultCommunities";
            searchVm.SearchBar.SearchType = SearchType.ActiveAdultCommunities;
            result.Result.AddRange(ClientViewModelsProvider.GetCommunitiesSearchVm(searchVm).Result);

			var MainResult = result.Result.DistinctBy(x => x.Id).ToList();


			result.Result = MainResult;

			if (!result.ValidationResult.IsValid)
			{
				return base.NotFound();
			}
			if (!result.ShouldRedirect())
			{
				return base.View("~/Views/Client/Search/Communities.cshtml", result);
			}
			return base.Http301Redirect(result.Seo.CanonicalUrl);
		}
		[HttpGet]
		public JsonResult SearchFromBar(LookupLocationVm lookupLocation)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetLookupLocationValidationVm(lookupLocation));
		}

		[CompetitiveFormating]
		public ActionResult SearchServiceProviders(ServiceProvidersSearchVm searchVm)
		{
			ServiceProvidersSearchVm result = ClientViewModelsProvider.GetServiceProvidersSearchVm(searchVm);
			if (!result.ValidationResult.IsValid)
			{
				return base.NotFound();
			}
			if (!result.ShouldRedirect())
			{
				return base.View("~/Views/Client/Search/Providers.cshtml", result);
			}
			return base.Http301Redirect(result.Seo.CanonicalUrl);
		}

		[CompetitiveFormating]
		public ActionResult SearchTypeStub(SearchTypeStubSearchVm searchVm)
		{
			if(searchVm.PageType == PageType.ServiceProvidersByType)
            {
				ClientViewModelsProvider.GetIndexSearchVm();
				return base.View("~/Views/Client/Search/SearchTypeServiceProviders.cshtml", ClientViewModelsProvider.GetSearchTypeSearchVm(searchVm));
            }
			else if(searchVm.PageType == PageType.ShcByType)
			{
				ClientViewModelsProvider.GetIndexSearchVm();
				return base.View("~/Views/Client/Static/SeniorType.cshtml", ClientViewModelsProvider.GetSearchTypeSearchVm(searchVm));
            }
            else
            {
				return base.View("~/Views/Client/Search/SearchType.cshtml", ClientViewModelsProvider.GetSearchTypeSearchVm(searchVm));
            }
		}

		[CompetitiveFormating]
		public ActionResult ServiceProviderDetails(long id,ServiceProvidersSearchVm searchVm)
		{
			ServiceProviderDetailsVm result = ClientViewModelsProvider.GetServiceProviderDetailsVm(id);
			if (result == null)
			{
				return base.NotFound();
			}
			if (!result.ShouldRedirect())
			{
				result.ServiceProvider.Code = "SP-";
				ViewBag.SC = ClientViewModelsProvider.GetNewSmililarServiceProvider(searchVm, id); ;
				return base.View("~/Views/Client/Details/ServiceProvider.cshtml", result);
			}
			return base.Http301Redirect(result.Seo.CanonicalUrl);
		}

		[CompetitiveFormating]
		[HttpGet]
		public JsonResult ServiceProviderQuickView(long serviceProviderId)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetServiceProviderQuickViewVm(serviceProviderId));
		}
		[CompetitiveFormating]
		[HttpGet]
		public JsonResult SpecHomesQuickView(long communityId, SearchType searchType)
		{
			return new AllowGetJsonResult(ClientViewModelsProvider.GetSpecHomeQuickViewVmList(communityId, searchType));
		}

		public ActionResult TermsOfUse()
		{
			return base.View("~/Views/Client/Static/TermsOfUse.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.TermsOfUse));
		}
		public ActionResult ContactUs()
		{
			return base.View("~/Views/Client/Static/ContactUs.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.ContactUs));
		}

		[HttpPost]
		public ActionResult ContactUs(MSLivingChoices.Mvc.Uipc.Client.MSLivingChoices.Mvc.Uipc.Client.ViewModels.ContactUs contactUs)
		{
            return new AllowGetJsonResult(new { success = ClientViewModelsProvider.SaveContact(contactUs) } );
		}

		public ActionResult AboutUs()
		{
			return base.View("~/Views/Client/Static/AboutUs.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.AboutUs));
		}

		public ActionResult SeniorType()
		{
			return base.View("~/Views/Client/Static/SeniorType.cshtml", ClientViewModelsProvider.GetStaticContent(PageType.SeniorType));
		}
	}
}