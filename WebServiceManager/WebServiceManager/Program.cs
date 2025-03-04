
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebServiceManagementSystem;

namespace WebServiceManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!AuthenticateUser())
            {
                Console.WriteLine("Authentication failed. Exiting...");
                return;
            }

            ServiceManager serviceManager = new ServiceManager();
            Categorizer categorizer = new Categorizer(serviceManager);
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Web Service Management System");
                Console.WriteLine("1. Add Details");
                Console.WriteLine("2. Show User Profiles by Category");
                Console.WriteLine("3. Show User Profiles by Service");
                Console.WriteLine("4. Generate Random Password");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDetails(serviceManager);
                        break;
                    case "2":
                        categorizer.ShowUserProfilesByCategoryOrPricingModel();
                        break;
                    case "3":
                        ShowUserProfilesByService(serviceManager, categorizer);
                        break;
                    case "4":
                        GeneratePassword();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static bool AuthenticateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            UserAuthenticator authenticator = new UserAuthenticator();

            if (authenticator.Authenticate(username, password))
            {
                Console.WriteLine("Authentication successful.");
                return true;
            }

            Console.WriteLine("Invalid username or password.");
            return false;
        }

        static void AddDetails(ServiceManager serviceManager)
        {
            Console.Clear();
            Console.WriteLine("Add Details");
            Console.WriteLine("1. Add Service");
            Console.WriteLine("2. Add User Profile");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddService(serviceManager);
                    break;
                case "2":
                    AddUserProfile(serviceManager);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void AddService(ServiceManager serviceManager)
        {
            Console.Clear();
            string siteName = GetStringInput("Enter site name:");
            string link = GetStringInput("Enter link:");

            WebServiceType webServiceType = GetEnumInput<WebServiceType>("Select Web Service Type:");
            PricingModel pricingModel = GetEnumInput<PricingModel>("Select Pricing Model:");

            Service service = new Service
            {
                SiteName = siteName,
                Link = link,
                WebServiceType = webServiceType,
                PricingModel = pricingModel
            };

            serviceManager.AddService(service);
            Console.WriteLine("Service added successfully.");
        }

        static void AddUserProfile(ServiceManager serviceManager)
        {
            Console.Clear();
            string serviceName = GetStringInput("Enter service name:");

            string name = GetStringInput("Enter user's name:");
            string username = GetStringInput("Enter username:");
            string password = GetStringInput("Enter password:");
            string emailUsed = GetStringInput("Enter email used:");
            string phoneNumberUsed = GetStringInput("Enter phone number used:");
            string securityQuestion1 = GetStringInput("Enter security question 1:");
            string securityAnswer1 = GetStringInput("Enter security answer 1:");
            string securityQuestion2 = GetStringInput("Enter security question 2:");
            string securityAnswer2 = GetStringInput("Enter security answer 2:");

            int numRecoveryCodes = GetIntInput("Enter number of recovery codes:");
            string[] recoveryCodes = new string[numRecoveryCodes];
            for (int i = 0; i < numRecoveryCodes; i++)
            {
                recoveryCodes[i] = GetStringInput($"Enter recovery code {i + 1}:");
            }

            SubscriptionPeriod subscriptionPeriod = GetEnumInput<SubscriptionPeriod>("Select Subscription Period:");
            int paymentSchedule;
            if (subscriptionPeriod == SubscriptionPeriod.Monthly)
            {
                paymentSchedule = GetIntInput("Enter payment date (day of month):");
            }
            else
            {
                paymentSchedule = GetIntInput("Enter payment month (1-12):");
            }
            decimal paymentFee = GetDecimalInput("Enter payment fee:");
            SubscriptionType subscriptionType = GetEnumInput<SubscriptionType>("Select Subscription Type:");

            SubscriptionDetails subscriptionDetails = new SubscriptionDetails
            {
                SubscriptionPeriod = subscriptionPeriod,
                PaymentSchedule = paymentSchedule,
                PaymentFee = paymentFee,
                SubscriptionType = subscriptionType
            };

            UserProfile userProfile = new UserProfile
            {
                DateAdded = DateTime.Now,
                Name = name,
                Username = username,
                Password = password,
                SecurityQuestion1 = securityQuestion1,
                SecurityAnswer1 = securityAnswer1,
                SecurityQuestion2 = securityQuestion2,
                SecurityAnswer2 = securityAnswer2,
                RecoveryCodes = recoveryCodes,
                EmailUsed = emailUsed,
                PhoneNumberUsed = phoneNumberUsed,
                SubscriptionDetails = subscriptionDetails
            };

            serviceManager.AddUserProfileToService(serviceName, userProfile);
            Console.WriteLine("User profile added successfully.");
        }

        static void ShowUserProfilesByService(ServiceManager serviceManager, Categorizer categorizer)
        {
            Console.Clear();
            string serviceName = GetStringInput("Enter service name:");
            var serviceNode = serviceManager.GetServiceNode(serviceName);

            if (serviceNode != null)
            {
                List<UserProfile> userProfiles = new List<UserProfile>();
                var current = serviceNode.Data.UserProfiles.Head;

                while (current != null)
                {
                    userProfiles.Add(current.Data);
                    current = current.Next;
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
                        categorizer.MergeSort(userProfiles, delegate (UserProfile x, UserProfile y) { return x.Name.CompareTo(y.Name); });
                        break;
                    case "2":
                        categorizer.MergeSort(userProfiles, delegate (UserProfile x, UserProfile y) { return y.Name.CompareTo(x.Name); });
                        break;
                    case "3":
                        categorizer.MergeSort(userProfiles, delegate (UserProfile x, UserProfile y) { return y.DateAdded.CompareTo(x.DateAdded); });
                        break;
                    case "4":
                        categorizer.MergeSort(userProfiles, delegate (UserProfile x, UserProfile y) { return x.DateAdded.CompareTo(y.DateAdded); });
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        return;
                }

                foreach (var profile in userProfiles)
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
                            DeleteProfile(serviceManager, serviceNode, profile);
                            break;
                        case "2":
                            EditProfile(serviceManager, serviceNode, profile);
                            break;
                        case "3":
                            continue;
                        default:
                            Console.WriteLine("Invalid choice. Skipping to next profile.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Service not found.");
            }
        }

        static void DeleteProfile(ServiceManager serviceManager, Node<Service> serviceNode, UserProfile userProfile)
        {
            Console.Write("Are you sure you want to delete this profile? (yes/no): ");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation == "yes" || confirmation == "y")
            {
                serviceNode.Data.UserProfiles.Delete(userProfile);
                Console.WriteLine("User profile deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }

        static void EditProfile(ServiceManager serviceManager, Node<Service> serviceNode, UserProfile userProfile)
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

        static void GeneratePassword()
        {
            Console.Clear();
            int length = GetIntInput("Enter the number of characters for the password:");
            bool includeSpecialChars = GetYesNoInput("Include special characters? (yes/no):");

            string password = PasswordGenerator.GeneratePassword(length, includeSpecialChars);
            Console.WriteLine($"Generated Password: {password}");
        }

        static TEnum GetEnumInput<TEnum>(string prompt) where TEnum : struct
        {
            while (true)
            {
                Console.WriteLine(prompt);
                foreach (var value in Enum.GetValues(typeof(TEnum)))
                {
                    Console.WriteLine($"{(int)value}. {value}");
                }
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                if (Enum.TryParse(input, out TEnum result))
                {
                    return result;
                }
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        static int GetIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static int GetIntInput(string prompt, int defaultValue)
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


        static decimal GetDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
        }


        static string GetStringInput(string prompt)
        {
            Console.Write(prompt + " ");
            return Console.ReadLine();
        }

        static string GetStringInput(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} (default: {defaultValue}): ");
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        static bool GetYesNoInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " ");
                string input = Console.ReadLine().ToLower();
                if (input == "yes" || input == "y")
                {
                    return true;
                }
                if (input == "no" || input == "n")
                {
                    return false;
                }
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }

    }
}



/*

// Alternative main program for checking the efficiency of the sorting methods


using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebServiceManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceManager serviceManager = new ServiceManager();

            // Adding dummy data for testing
            for (int i = 0; i < 100; i++)
            {
                Service service = new Service
                {
                    SiteName = "Service" + i,
                    Link = "http://service" + i + ".com",
                    WebServiceType = (WebServiceType)(i % 5),
                    PricingModel = (PricingModel)(i % 4)
                };

                serviceManager.AddService(service);

                for (int j = 0; j < 10; j++)
                {
                    UserProfile userProfile = new UserProfile
                    {
                        DateAdded = DateTime.Now.AddDays(-i),
                        Name = "User" + j,
                        Username = "username" + j,
                        Password = "password" + j,
                        SecurityQuestion1 = "Security Question 1",
                        SecurityAnswer1 = "Answer 1",
                        SecurityQuestion2 = "Security Question 2",
                        SecurityAnswer2 = "Answer 2",
                        RecoveryCodes = new string[] { "Code1", "Code2" },
                        EmailUsed = "user" + j + "@service" + i + ".com",
                        PhoneNumberUsed = "1234567890",
                        SubscriptionDetails = new SubscriptionDetails
                        {
                            SubscriptionPeriod = SubscriptionPeriod.Monthly,
                            PaymentSchedule = 1,
                            PaymentFee = 9.99m,
                            SubscriptionType = SubscriptionType.Basic
                        }
                    };

                    serviceManager.AddUserProfileToService("Service" + i, userProfile);
                }
            }

            // Measuring performance of sorting methods
            List<UserProfile> userProfiles = serviceManager.GetAllUserProfiles();

            // Bubble Sort
            TimeSpan bubbleSortTime = ExecutionTimeAnalysis.MeasureExecutionTime(delegate
            {
                SortingAlgorithms.BubbleSort(userProfiles.ToArray(), delegate (UserProfile x, UserProfile y) { return x.Name.CompareTo(y.Name); });
            });
            Console.WriteLine("Bubble Sort Time: " + bubbleSortTime.TotalMilliseconds + " ms");

            // Quick Sort
            TimeSpan quickSortTime = ExecutionTimeAnalysis.MeasureExecutionTime(delegate
            {
                var userProfilesArray = userProfiles.ToArray();
                SortingAlgorithms.QuickSort(userProfilesArray, 0, userProfilesArray.Length - 1, delegate (UserProfile x, UserProfile y) { return x.Name.CompareTo(y.Name); });
            });
            Console.WriteLine("Quick Sort Time: " + quickSortTime.TotalMilliseconds + " ms");

            // Merge Sort
            TimeSpan mergeSortTime = ExecutionTimeAnalysis.MeasureExecutionTime(delegate
            {
                SortingAlgorithms.MergeSort(userProfiles.ToArray(), delegate (UserProfile x, UserProfile y) { return x.Name.CompareTo(y.Name); });
            });
            Console.WriteLine("Merge Sort Time: " + mergeSortTime.TotalMilliseconds + " ms");


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
 
*/

