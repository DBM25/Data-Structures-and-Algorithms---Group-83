using System;

namespace WebServiceManagementSystem
{
    public class UserProfile
    {
        public DateTime DateAdded { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityAnswer1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string[] RecoveryCodes { get; set; }
        public string EmailUsed { get; set; }
        public string PhoneNumberUsed { get; set; }
        public SubscriptionDetails SubscriptionDetails { get; set; }

        //public string AsString()
        public override string ToString()
        {
            return "Name: " + Name + ", " +
                   "Username: " + Username + ", " +
                   "Email: " + EmailUsed + ", " +
                   "Phone: " + PhoneNumberUsed + ", " +
                   "Subscription: " + SubscriptionDetails.ToString();
        }
    }
}