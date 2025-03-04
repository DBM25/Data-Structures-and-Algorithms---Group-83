using System;

namespace WebServiceManagementSystem
{
    public class Service
    {
        public string SiteName { get; set; }
        public string Link { get; set; }
        public WebServiceType WebServiceType { get; set; }
        public PricingModel PricingModel { get; set; }
        public LinkedList<UserProfile> UserProfiles { get; set; }

        public Service()
        {
            UserProfiles = new LinkedList<UserProfile>();
        }

        //public string AsString()
        public override string ToString()
        {
            string result = "Site: " + SiteName + ", " +
                            "Link: " + Link + ", " +
                            "Type: " + WebServiceType + ", " +
                            "Pricing: " + PricingModel;
            return result;
        }
    }

    public enum WebServiceType
    {
        Enterprise,
        Education,
        News,
        SocialMedia,
        Entertainment
    }

    public enum PricingModel
    {
        FreeToUse,
        LifetimeLicensed,
        Subscription,
        InHousePersonalService
    }
}