using InsuranceMS.dao;
using InsuranceMS.exception;
using InsuranceMS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMS.app
{
    internal class IMSApp
    {
        static readonly IPolicyService policyService = new PolicyService();
        static readonly IUserService userService = new UserService();
        public void Run()
        {
            PolicyService insuranceService = new PolicyService();
            User user = new User();
            bool loggedIn = false;

            while (!loggedIn)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        RegisterUser(userService);
                        break;
                    case 2:
                        loggedIn = LoginUser(userService);
                        break;
                    case 3:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }



            while (loggedIn)
            {
                // Implement menu for logged-in users here
                Console.WriteLine("1. Create Policy");
                Console.WriteLine("2. View Policies");
                Console.WriteLine("3. Update Policy");
                Console.WriteLine("4. Delete Policy");
                Console.WriteLine("5. Logout");
                Console.Write("Choose an option: ");
                int menuoption = int.Parse(Console.ReadLine());

                switch (menuoption)
                {
                    case 1:
                        // Create Policy logic
                        Console.WriteLine("Enter Policy Name : ");
                        string policyName = Console.ReadLine();
                        Console.WriteLine("Enter Policy Premium : ");
                        double policyPremium = double.Parse(Console.ReadLine());
                        Policy policy = new Policy
                        {
                            PolicyName = policyName,
                            Premium = policyPremium
                        };
                        bool created = insuranceService.CreatePolicy(policy);
                        Console.WriteLine("Policy created: " + created);
                        Console.WriteLine($"Policy Name : {policyName}\tPremium : {policyPremium}");
                        break;
                    case 2:
                        // View Policies logic
                        Console.WriteLine("All Policies:");
                        foreach (Policy policies in insuranceService.GetAllPolicies())
                        {
                            Console.WriteLine(policies);
                        }
                        break;
                    case 3:
                        // Update Policy logic
                        Console.WriteLine("Enter Policy ID : ");
                        int policyId = int.Parse(Console.ReadLine());
                        var existingPolicy = insuranceService.GetPolicyById(policyId);
                        if (existingPolicy != null)
                        {
                            Console.WriteLine("\nExisting Policy Details");
                            Console.WriteLine($"Policy ID : {existingPolicy.PolicyId}");
                            Console.WriteLine($"Policy Name : {existingPolicy.PolicyName}");
                            Console.WriteLine($"Policy Premium : {existingPolicy.Premium}");

                            Console.WriteLine("\nEnter updated policy Details");
                            Console.WriteLine("Enter Policy Name :");
                            string polName = Console.ReadLine();
                            Console.WriteLine("Enter Policy Premium :");
                            double premium = double.Parse(Console.ReadLine());

                            if(policyId == existingPolicy.PolicyId)
                            {
                                Policy updatedPolicy = new Policy
                                {
                                    PolicyName = polName,
                                    Premium = premium
                                };
                                insuranceService.UpdatePolicy(updatedPolicy);
                            }
                            Console.WriteLine("Policy Updated Successfully.");
                        }                        

                        else
                        {
                            Console.WriteLine("Policy not found for update");
                        }
                        break;
                    case 4:
                        // Delete Policy logic
                        try
                        {
                            Console.WriteLine("Enter Policy ID :");
                            int policyIdDel = int.Parse(Console.ReadLine());
                            insuranceService.DeletePolicy(policyIdDel);
                            Console.WriteLine("Policy removed successfully.");
                        }
                        catch(PolicyNotFoundException ex)
                        {
                            Console.WriteLine($"Policy Not Found : {ex.Message}");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Logging out...");
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
        static void RegisterUser(IUserService userService)
        {
            Console.Write("Enter username to register: ");
            string newUsername = Console.ReadLine();
            Console.Write("Enter password to register: ");
            string newPassword = Console.ReadLine();
            Console.Write("Enter your Role: ");
            string role = Console.ReadLine();

            if (userService.IsUsernameExists(newUsername))
            {
                Console.WriteLine("Username already exists. Please choose a different one.");
            }
            else
            {
                userService.RegisterUser(newUsername, newPassword, role);
                Console.WriteLine("User registered successfully!");
            }
        }

        static bool LoginUser(IUserService userService)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (userService.IsLoginValid(username, password))
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
                return false;
            }
        }
    }
}
