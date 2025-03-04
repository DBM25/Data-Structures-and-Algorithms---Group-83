using System;

namespace WebServiceManagementSystem
{
    public class SubscriptionDetails
    {
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public int PaymentSchedule { get; set; }  // Changed from PaymentDate to PaymentSchedule
        public decimal PaymentFee { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        //public string AsString()
        public override string ToString()
        {
            string result = "Subscription Type: " + SubscriptionType + ", " +
                            "Subscription Period: " + SubscriptionPeriod + ", " +
                            "Fee: " + PaymentFee + ", " +
                            "Payment Schedule: " + PaymentSchedule;
            return result;
        }
    }

    public enum SubscriptionPeriod
    {
        Monthly,
        Yearly
    }

    public enum SubscriptionType
    {
        Basic,
        Standard,
        Premium
    }
}