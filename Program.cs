using System;

namespace Exercise10_ATM
{
    class Program
    {

        static void Main(string[] args)
        {

            //USER INTERFACE

            double bankAcct = 575.32;
            int savedPin = 1234;



            string userResponse = "y";

            while (userResponse == "y")
            {
                Console.WriteLine("Please enter your pin.");
                int userPin = int.Parse(Console.ReadLine().Trim());

                bool canHaveAccess = HasCorrectPin(userPin, savedPin);


                if (canHaveAccess)
                {
                    bool flag = true;
                    while (flag)
                    {
                        Console.WriteLine("Welcome to the ATM!");


                        Console.WriteLine("Enter your choice to select your menu option");

                        // 1 - Balance inquiry
                        Console.WriteLine("Enter (1) for Balance Inquiry");

                        // 2 - Quick withdrawal 
                        Console.WriteLine("Enter (2) for Quick Withdrawal");

                        // 3 - Cash withdrawal 
                        Console.WriteLine("Enter (3) for Cash Withdrawal");

                        // 4 - Cash deposit 
                        Console.WriteLine("Enter (4) for Cash Deposit");

                        int menuSelection = int.Parse(Console.ReadLine());


                        switch (menuSelection)
                        {
                            case 1:
                                Console.WriteLine(Receipt(bankAcct));
                                flag = false;
                                break;
                            case 2:

                                if (bankAcct < 20)
                                {
                                    Console.WriteLine("Insufficient Funds");
                                    flag = false;
                                }
                                else
                                {
                                    bankAcct = QuickWithdrawal(bankAcct);
                                    
                                }

                                Console.WriteLine(Receipt(bankAcct));

                                break;
                            case 3:
                                bool isValidWithdrawAmount = false;
                                while (!isValidWithdrawAmount)
                                {
                                    Console.WriteLine("How much money would you like to withdrawal?");
                                    double amountToWithdraw = double.Parse(Console.ReadLine().Trim());

                                    if (DoesHaveSufficientFunds(bankAcct, amountToWithdraw) && ValidateIsMultipleOfFive(amountToWithdraw))
                                    {
                                        bankAcct = CashWithdrawal(amountToWithdraw, bankAcct);
                                        Console.WriteLine(Receipt(bankAcct));
                                        isValidWithdrawAmount = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid entry");
                                    } 
                                }
                                break;
                            case 4:
                                bool isValidDepositAmount = false;
                                while (!isValidDepositAmount)
                                {
                                    Console.WriteLine("How much money are you depositing?");
                                    double userDeposit = double.Parse(Console.ReadLine().Trim());

                                    Console.WriteLine("Are you depositing a [1]cash or [2]check?");
                                    string cashOrCheckChoice = Console.ReadLine();

                                    switch (cashOrCheckChoice)
                                    {
                                        case "1":
                                            if (IsWholeValue(userDeposit))
                                            {
                                                bankAcct = Deposit(userDeposit, bankAcct);
                                                isValidDepositAmount = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Must insert dollar bills only");
                                            }

                                            Console.WriteLine(Receipt(bankAcct));

                                            break;


                                        case "2":

                                            bool isValidPin = false;
                                            while (!isValidPin)
                                            {
                                                Console.WriteLine("Enter pin number to sign check.");
                                                int pinInput = int.Parse(Console.ReadLine());

                                                if (HasCorrectPin(pinInput, savedPin))
                                                {
                                                    bankAcct = Deposit(userDeposit, bankAcct);
                                                    isValidPin = true;
                                                    isValidDepositAmount = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid Pin");
                                                } 
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Invalid choice");
                                            break;

                                    }

                                }

                                Console.WriteLine(Receipt(bankAcct));

                                break;

                            default:

                                Console.WriteLine("Invalid Choice");

                                continue;
                        } 
                    }


                    
                }
                else
                {
                    Console.WriteLine("Invalid Pin");
                }

                Console.WriteLine("Would you like to perform another transaction, yes or no?");
                userResponse = Console.ReadLine();

                if (userResponse == "no" || userResponse == "n")
                {
                    Console.WriteLine("Thank you for stopping in!");
                    Console.WriteLine("Have a nice day!");
                    break;
                }
            }


            Console.ReadLine();
        }






        public static bool DoesHaveSufficientFunds(double balance, double amount)
        {
            if (amount > balance)
            {
                return false;
            }

            return true;
        }

        public static bool ValidateIsMultipleOfFive(double amount)
        {
            if (amount % 5 == 0)
            {
                return true;
            }

            return false;
        }

        public static bool IsWholeValue(double amount)
        {
            if (amount % 1 == 0)
            {
                return true;
            }

            return false;
        }


        public static bool HasCorrectPin(int userPin, int savedPin)
        {
            if (userPin == savedPin)
            {
                return true;

            }

            return false;
        }


        public static double QuickWithdrawal(double bankAccountAmount)
        {
            return bankAccountAmount - 20;
        }

        public static double CashWithdrawal(double withdrawAmount, double bankAccountAmount)
        {

            bankAccountAmount -= withdrawAmount;

            return bankAccountAmount;

        }


        public static double Deposit(double userDeposit, double bankAccountAmount)
        {
            return bankAccountAmount + userDeposit;
        }


        public static string Receipt(double bankAccountAmount)
        {
            return $"Your balance is: ${bankAccountAmount}.";

        }

    }
}

