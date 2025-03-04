using System;
using System.Collections.Generic;

namespace WebServiceManagementSystem
{
    public class ServiceManager
    {
        private LinkedList<Service> services;

        public ServiceManager()
        {
            services = new LinkedList<Service>();
        }

        public void AddService(Service service)
        {
            services.Add(service);
        }

        public void RemoveService(Service service)
        {
            services.Delete(service);
        }

        public void AddUserProfileToService(string serviceName, UserProfile userProfile)
        {
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.SiteName == serviceName)
                {
                    current.Data.UserProfiles.Add(userProfile);
                    break;
                }
                current = current.Next;
            }
        }

        public void RemoveUserProfileFromService(string serviceName, UserProfile userProfile)
        {
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.SiteName == serviceName)
                {
                    current.Data.UserProfiles.Delete(userProfile);
                    break;
                }
                current = current.Next;
            }
        }

        public void PrintAllServices()
        {
            services.PrintAll();
        }

        public void PrintAllUserProfiles(string serviceName)
        {
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.SiteName == serviceName)
                {
                    current.Data.UserProfiles.PrintAll();
                    break;
                }
                current = current.Next;
            }
        }

        public Node<Service> GetServiceNode(string serviceName)
        {
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.SiteName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public Node<Service> GetHead()
        {
            return services.Head;
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            List<UserProfile> allUserProfiles = new List<UserProfile>();
            var current = services.Head;
            while (current != null)
            {
                var userProfileNode = current.Data.UserProfiles.Head;
                while (userProfileNode != null)
                {
                    allUserProfiles.Add(userProfileNode.Data);
                    userProfileNode = userProfileNode.Next;
                }
                current = current.Next;
            }
            return allUserProfiles;
        }

        public List<UserProfile> GetUserProfilesByCategory(WebServiceType category)
        {
            List<UserProfile> userProfilesByCategory = new List<UserProfile>();
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.WebServiceType == category)
                {
                    var userProfileNode = current.Data.UserProfiles.Head;
                    while (userProfileNode != null)
                    {
                        userProfilesByCategory.Add(userProfileNode.Data);
                        userProfileNode = userProfileNode.Next;
                    }
                }
                current = current.Next;
            }
            return userProfilesByCategory;
        }

        public List<UserProfile> GetUserProfilesByPricingModel(PricingModel pricingModel)
        {
            List<UserProfile> userProfilesByPricingModel = new List<UserProfile>();
            var current = services.Head;
            while (current != null)
            {
                if (current.Data.PricingModel == pricingModel)
                {
                    var userProfileNode = current.Data.UserProfiles.Head;
                    while (userProfileNode != null)
                    {
                        userProfilesByPricingModel.Add(userProfileNode.Data);
                        userProfileNode = userProfileNode.Next;
                    }
                }
                current = current.Next;
            }
            return userProfilesByPricingModel;
        }

        public void RemoveUserProfile(UserProfile userProfile)
        {
            var current = services.Head;
            while (current != null)
            {
                var userProfileNode = current.Data.UserProfiles.Head;
                while (userProfileNode != null)
                {
                    if (userProfileNode.Data == userProfile)
                    {
                        current.Data.UserProfiles.Delete(userProfileNode.Data);
                        return;
                    }
                    userProfileNode = userProfileNode.Next;
                }
                current = current.Next;
            }
        }
    }
}