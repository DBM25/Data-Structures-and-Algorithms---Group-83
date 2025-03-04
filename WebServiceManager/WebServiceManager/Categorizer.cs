using System;
using System.Collections.Generic;

namespace WebServiceManagementSystem
{
    public class Categorizer
    {
        private ServiceManager serviceManager;

        public Categorizer(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public void ShowUserProfilesByCategoryOrPricingModel()
        {
            Console.Clear();
            Console.WriteLine("Select category or pricing model:");
            Console.WriteLine("1. All");
            Console.WriteLine("2. Enterprise");
            Console.WriteLine("3. Education");
            Console.WriteLine("4. News");
            Console.WriteLine("5. Social Media");
            Console.WriteLine("6. Entertainment");
            Console.WriteLine("7. Free to Use");
            Console.WriteLine("8. Lifetime Licensed");
            Console.WriteLine("9. Subscription");
            Console.WriteLine("10. In-House Personal Service");
            Console.Write("Enter your choice: ");
            string categoryChoice = Console.ReadLine();
            List<UserProfile> userProfiles = new List<UserProfile>();

            switch (categoryChoice)
            {
                case "1":
                    userProfiles = serviceManager.GetAllUserProfiles();
                    break;
                case "2":
                    userProfiles = serviceManager.GetUserProfilesByCategory(WebServiceType.Enterprise);
                    break;
                case "3":
                    userProfiles = serviceManager.GetUserProfilesByCategory(WebServiceType.Education);
                    break;
                case "4":
                    userProfiles = serviceManager.GetUserProfilesByCategory(WebServiceType.News);
                    break;
                case "5":
                    userProfiles = serviceManager.GetUserProfilesByCategory(WebServiceType.SocialMedia);
                    break;
                case "6":
                    userProfiles = serviceManager.GetUserProfilesByCategory(WebServiceType.Entertainment);
                    break;
                case "7":
                    userProfiles = serviceManager.GetUserProfilesByPricingModel(PricingModel.FreeToUse);
                    break;
                case "8":
                    userProfiles = serviceManager.GetUserProfilesByPricingModel(PricingModel.LifetimeLicensed);
                    break;
                case "9":
                    userProfiles = serviceManager.GetUserProfilesByPricingModel(PricingModel.Subscription);
                    break;
                case "10":
                    userProfiles = serviceManager.GetUserProfilesByPricingModel(PricingModel.InHousePersonalService);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    return;
            }

            Console.WriteLine("Sort by:");
            Console.WriteLine("1. Name (Ascending)");
            Console.WriteLine("2. Name (Descending)");
            Console.WriteLine("3. Newest Date");
            Console.WriteLine("4. Oldest Date");
            Console.Write("Enter your choice: ");
            string sortChoice = Console.ReadLine();

            switch (sortChoice)
            {
                case "1":
                    MergeSort(userProfiles, CompareByNameAscending);
                    break;
                case "2":
                    MergeSort(userProfiles, CompareByNameDescending);
                    break;
                case "3":
                    MergeSort(userProfiles, CompareByNewestDate);
                    break;
                case "4":
                    MergeSort(userProfiles, CompareByOldestDate);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    return;
            }

            foreach (UserProfile profile in userProfiles)
            {
                Console.WriteLine(profile);

                Console.WriteLine("Options:");
                Console.WriteLine("1. Delete Profile");
                Console.WriteLine("2. Edit Profile");
                Console.WriteLine("3. Skip to Next");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DeleteProfile(profile);
                        break;
                    case "2":
                        EditProfile(profile);
                        break;
                    case "3":
                        continue;
                    default:
                        Console.WriteLine("Invalid choice. Skipping to next profile.");
                        break;
                }
            }
        }

        private int CompareByNameAscending(UserProfile x, UserProfile y)
        {
            return x.Name.CompareTo(y.Name);
        }

        private int CompareByNameDescending(UserProfile x, UserProfile y)
        {
            return y.Name.CompareTo(x.Name);
        }

        private int CompareByNewestDate(UserProfile x, UserProfile y)
        {
            return y.DateAdded.CompareTo(x.DateAdded);
        }

        private int CompareByOldestDate(UserProfile x, UserProfile y)
        {
            return x.DateAdded.CompareTo(y.DateAdded);
        }

        public void MergeSort(List<UserProfile> list, Comparison<UserProfile> comparison)
        {
            if (list.Count <= 1)
                return;

            int mid = list.Count / 2;
            List<UserProfile> left = new List<UserProfile>(list.GetRange(0, mid));
            List<UserProfile> right = new List<UserProfile>(list.GetRange(mid, list.Count - mid));

            MergeSort(left, comparison);
            MergeSort(right, comparison);
            Merge(list, left, right, comparison);
        }

        private void Merge(List<UserProfile> result, List<UserProfile> left, List<UserProfile> right, Comparison<UserProfile> comparison)
        {
            int leftIndex = 0, rightIndex = 0, targetIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result[targetIndex++] = left[leftIndex++];
                }
                else
                {
                    result[targetIndex++] = right[rightIndex++];
                }
            }

            while (leftIndex < left.Count)
            {
                result[targetIndex++] = left[leftIndex++];
            }

            while (rightIndex < right.Count)
            {
                result[targetIndex++] = right[rightIndex++];
            }
        }

        private void DeleteProfile(UserProfile userProfile)
        {
            Console.Write("Are you sure you want to delete this profile? (yes/no): ");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation == "yes" || confirmation == "y")
            {
                serviceManager.RemoveUserProfile(userProfile);
                Console.WriteLine("User profile deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }

        private void EditProfile(UserProfile userProfile)
        {
            Console.Clear();
            Console.WriteLine("Editing User Profile");
            userProfile.Name = GetStringInput($"Enter new name (current: {userProfile.Name}):", userProfile.Name);
            userProfile.Password = GetStringInput($"Enter new password (current: {userProfile.Password}):", userProfile.Password);
            userProfile.EmailUsed = GetStringInput($"Enter new email (current: {userProfile.EmailUsed}):", userProfile.EmailUsed);
            userProfile.PhoneNumberUsed = GetStringInput($"Enter new phone number (current: {userProfile.PhoneNumberUsed}):", userProfile.PhoneNumberUsed);
            userProfile.SecurityQuestion1 = GetStringInput($"Enter new security question 1 (current: {userProfile.SecurityQuestion1}):", userProfile.SecurityQuestion1);
            userProfile.SecurityAnswer1 = GetStringInput($"Enter new security answer 1 (current: {userProfile.SecurityAnswer1}):", userProfile.SecurityAnswer1);
            userProfile.SecurityQuestion2 = GetStringInput($"Enter new security question 2 (current: {userProfile.SecurityQuestion2}):", userProfile.SecurityQuestion2);
            userProfile.SecurityAnswer2 = GetStringInput($"Enter new security answer 2 (current: {userProfile.SecurityAnswer2}):", userProfile.SecurityAnswer2);
            int numRecoveryCodes = GetIntInput($"Enter new number of recovery codes (current: {userProfile.RecoveryCodes.Length}):", userProfile.RecoveryCodes.Length);
            string[] recoveryCodes = new string[numRecoveryCodes];
            for (int i = 0; i < numRecoveryCodes; i++)
            {
                recoveryCodes[i] = GetStringInput($"Enter recovery code {i + 1} (current: {userProfile.RecoveryCodes[i]}):", userProfile.RecoveryCodes[i]);
            }
            userProfile.RecoveryCodes = recoveryCodes;

            Console.WriteLine("User profile updated successfully.");
        }

        private string GetStringInput(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} (default: {defaultValue}): ");
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        private int GetIntInput(string prompt, int defaultValue)
        {
            while (true)
            {
                Console.Write($"{prompt} (default: {defaultValue}): ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return defaultValue;
                }
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}