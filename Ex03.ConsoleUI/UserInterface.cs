using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        private const int k_NumberOfOptionsInMenu = 8;
        private GarageLogic.Garage garage;

        private static void showUserOptionsForFunctions()
        {
            string messageAskingAboutAllFunctions = string.Format(
                @"Please choose one of the options:
Press (1) for insert a new vehicle into the garage
Press (2) for display a list of license numbers currently in the garage
Press (3) for change a certain vehicle’s status 
Press (4) for inflate tires to maximum 
Press (5) refuel a fuel-based vehicle
Press (6) charge an electric-based vehicle 
Press (7) display vehicle information
Press (8) to exit");
            Console.WriteLine(messageAskingAboutAllFunctions);
        }

        private static string getLicenseNumber()
        {
            Console.WriteLine("Please enter your license number");
            string licenseNumberFromUser = Console.ReadLine();

            return licenseNumberFromUser;
        }

        private static string getVehicleOwnerName()
        {
            string vehicleOwnerNameFromUser;
            Console.WriteLine("Please enter the owner's name");
            vehicleOwnerNameFromUser = Console.ReadLine();

            return vehicleOwnerNameFromUser;
        }

        private static string getPhoneNumber()
        {
            Console.WriteLine("Please enter the owner's phone number");
            string phoneNumberFromUser = Console.ReadLine();

            return phoneNumberFromUser;
        }

        private static void getVehicleBasicProperties(out string o_VehicleType, out string o_ModelName)
        {
            bool v_IsPrintingOptionsToChooseFrom = true;

            printList(VehicleFactory.GetVehicleTypes(), "Choose a type of vehicle:", v_IsPrintingOptionsToChooseFrom);
            o_VehicleType = getUserOptionChoice(VehicleFactory.GetVehicleTypes().Count);
            Console.Clear();
            Console.WriteLine("Please enter the following properties of the vehicle:");
            List<string> inputsForVehicle = getValidInputListFromUser(Vehicle.BasicProperties());

            o_ModelName = inputsForVehicle[0];
        }

        private static string getQuantityOfFuelToAdd()
        {
            Console.WriteLine("Please enter the quantity of fuel (in liters) to refuel");

            return Console.ReadLine();
        }

        private static string getQuantityOfMinutesToAdd()
        {
            Console.WriteLine("Please enter number of minutes to charge");

            return Console.ReadLine();
        }

        private static void unMatchingEngineMsg(string i_EngineType, string i_LicenseNumber)
        {
            Console.WriteLine("The vehicle {0} doesn't have an {1} engine.", i_LicenseNumber, i_EngineType);
        }

        private static void exceptionMsg(Exception ex, string i_Msg)
        {
            Console.WriteLine(string.Format("{0}: {1}", ex.GetType(), i_Msg));
        }

        private static string getUserFilterChoice()
        {
            List<string> optionalStatusesForVehiclsInTheGarage = Garage.GetGrageStatuses();
            int numOfOptionsIncludingNoFilter = optionalStatusesForVehiclsInTheGarage.Count + 1;
            bool v_IsPrintingOptionsToChooseFrom = true;

            printList(optionalStatusesForVehiclsInTheGarage, "Choose a status to filter by:", v_IsPrintingOptionsToChooseFrom);
            Console.WriteLine(string.Format("Press ({0}) for printing all the license numbers without filter", numOfOptionsIncludingNoFilter));
            string userChoiceForFilter = getUserOptionChoice(numOfOptionsIncludingNoFilter);

            return userChoiceForFilter;
        }

        private static string getUserOptionChoice(int i_NumOfOptions)
        {
            string userChoice = Console.ReadLine();

            while (!isValidChoiceFromList(i_NumOfOptions, userChoice))
            {
                Console.WriteLine("Your choosing is not valid. please try again");
                userChoice = Console.ReadLine();
            }

            return userChoice;
        }

        private static bool isValidChoiceFromList(int i_NumOfOptions, string i_userChoice)
        {
            return !isNullOrHasWhiteSpaces(i_userChoice) && Garage.IsValidChoiceFromNumOfOptions(i_NumOfOptions, i_userChoice);
        }

        private static void printList(List<string> i_ListToPrint, string i_Title, bool i_IsPrintingOptionsToChooseFrom)
        {
            if (i_ListToPrint.Count != 0)
            {
                Console.WriteLine(i_Title);
                int optionCounter = 1;
                foreach (string line in i_ListToPrint)
                {
                    if (i_IsPrintingOptionsToChooseFrom)
                    {
                        Console.WriteLine("Press ({0}) for {1}", optionCounter, line);
                    }
                    else
                    {
                        Console.WriteLine("{0}. {1}", optionCounter, line);
                    }

                    optionCounter++;
                }
            }
            else
            {
                Console.WriteLine("There are no matching items");
            }
        }

        private static bool isNullOrHasWhiteSpaces(string i_StrToCheck)
        {
            bool valid = string.IsNullOrEmpty(i_StrToCheck);

            if (valid)
            {
                foreach (char charToCheck in i_StrToCheck)
                {
                    if (char.IsWhiteSpace(charToCheck))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        private static List<string> getValidInputListFromUser(List<string> i_PropertiesList)
        {
            List<string> validInputs = new List<string>(i_PropertiesList.Count);
            string currentPropertyFromUser;

            foreach (string property in i_PropertiesList)
            {
                string messageAskingForCurrentPropertyFromUser = string.Format(@"{0}:", property);
                Console.WriteLine(messageAskingForCurrentPropertyFromUser);
                currentPropertyFromUser = Console.ReadLine();

                while (isNullOrHasWhiteSpaces(currentPropertyFromUser))
                {
                    Console.WriteLine("Your input is not valid, please try again");
                    currentPropertyFromUser = Console.ReadLine();
                }

                validInputs.Add(currentPropertyFromUser);
            }

            return validInputs;
        }

        internal UserInterface()
        {
            garage = new GarageLogic.Garage();
        }

        internal void GarageVisit()
        {
            Console.WriteLine("Welcome to the garage!");
            eGarageFunctions userChoiceForFunction = 0;
            bool v_IsPrintingOptionsToChooseFrom = true;
            showUserOptionsForFunctions();
            userChoiceForFunction = getUserNumberChoosingForFunction();

            while (userChoiceForFunction != eGarageFunctions.ExitFromProgram)
            {
                string licenseNumber = string.Empty;

                try
                {
                    // The only case (except exit) that doesn't requires license number
                    if (userChoiceForFunction == eGarageFunctions.ShowListOfLicenseNumbersOfCurrentVehiclesInTheGarage)
                    {
                        string statusToFilterBy = getUserFilterChoice();
                        printList(garage.GetListOfLicenseNumbersOfCurrentVehiclesInTheGarage(statusToFilterBy), "The license numbers are:", !v_IsPrintingOptionsToChooseFrom);
                        goBackToFunctionsMenu();
                    }
                    else
                    {
                        licenseNumber = getLicenseNumber();
                        if (!garage.IsVehicleInTheGarage(licenseNumber) && userChoiceForFunction != eGarageFunctions.EnterNewVehicleToTheGarge)
                        {
                            Console.WriteLine("The vehicle {0} is not in the garage", licenseNumber);
                        }
                        else
                        {
                            switch (userChoiceForFunction)
                            {
                                case eGarageFunctions.EnterNewVehicleToTheGarge:
                                    {
                                        if (garage.IsVehicleInTheGarage(licenseNumber))
                                        {
                                            garage.SetStatusToExistedClient(licenseNumber);
                                            Console.WriteLine("The vehicle {0} is already in the garage. Changing status to in repair..", licenseNumber);
                                        }
                                        else
                                        {
                                            string vehicleOwnerName = getVehicleOwnerName();
                                            string vehicleOwnerPhoneNumber = getPhoneNumber();
                                            string vehicleType;
                                            string vehicleModelName;

                                            getVehicleBasicProperties(out vehicleType, out vehicleModelName);
                                            Vehicle vehicle = garage.VehiclsFactory.CreateVehicle(vehicleType, vehicleModelName, licenseNumber);
                                            setVehicleWheels(vehicle);
                                            setUniquePropertiesOfVehicleType(vehicle);
                                            garage.EnterNewVehicleToTheGarge(vehicleOwnerName, vehicleOwnerPhoneNumber, vehicle);
                                            Console.WriteLine("The vehicle was succeffuly entered to the garage");
                                        }

                                        break;
                                    }

                                case eGarageFunctions.ChangeStatusOfVehicleInTheGarage:
                                    {
                                        List<string> optionalStatusesForVehiclsInTheGarage = Garage.GetGrageStatuses();

                                        printList(optionalStatusesForVehiclsInTheGarage, "Choose the status you want to change to:", v_IsPrintingOptionsToChooseFrom);
                                        string theChoosingForTheNewStatus = getUserOptionChoice(optionalStatusesForVehiclsInTheGarage.Count);
                                        garage.ChangeStatusOfVehicleInTheGarage(licenseNumber, theChoosingForTheNewStatus);
                                        Console.WriteLine("The vehicle status was succeffuly changed");
                                        break;
                                    }

                                case eGarageFunctions.InflatingAirPressureToMaximum:
                                    {
                                        garage.InflatingAirPressureToMaximum(licenseNumber);
                                        Console.WriteLine("The air preesure was succesfully inflated to maximum");
                                        break;
                                    }

                                case eGarageFunctions.RefuelingGasVehicle:
                                    {
                                        if (garage.IsGasVehicle(licenseNumber))
                                        {
                                            List<string> possibleFuelTypesForGasVehicle = GasEngine.GetPossibleFuelTypes();

                                            printList(possibleFuelTypesForGasVehicle, "Choose the number of the desired fuel type:", v_IsPrintingOptionsToChooseFrom);
                                            string fuelTypeToSet = getUserOptionChoice(possibleFuelTypesForGasVehicle.Count);
                                            string quantityOfFuelToAdd = getQuantityOfFuelToAdd();
                                            garage.RefuelingGasVehicle(licenseNumber, quantityOfFuelToAdd, fuelTypeToSet);
                                            Console.WriteLine("The engine was succesfully refueled");
                                        }
                                        else
                                        {
                                            unMatchingEngineMsg("fuel", licenseNumber);
                                        }

                                        break;
                                    }

                                case eGarageFunctions.ChargeElectricVehicle:
                                    {
                                        if (garage.IsElectricVehicle(licenseNumber))
                                        {
                                            string quantityOfMinutesToCharge = getQuantityOfMinutesToAdd();
                                            garage.ChargeElectricVehicle(licenseNumber, quantityOfMinutesToCharge);
                                            Console.WriteLine("The engine was succesfully charged");
                                        }
                                        else
                                        {
                                            unMatchingEngineMsg("electric", licenseNumber);
                                        }

                                        break;
                                    }

                                case eGarageFunctions.ShowFullDataOfGarageClient:
                                    {
                                        Console.WriteLine(garage.GetFullDataOfGarageClient(licenseNumber));
                                        goBackToFunctionsMenu();
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch (FormatException ex)
                {
                    exceptionMsg(ex, ex.Message);
                }
                catch (ArgumentException ex)
                {
                    exceptionMsg(ex, ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    exceptionMsg(ex, ex.Message);
                }

                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                showUserOptionsForFunctions();
                userChoiceForFunction = getUserNumberChoosingForFunction();
            }

            Console.WriteLine("Bye! see you next time");
        }

        private void goBackToFunctionsMenu()
        {
            Console.WriteLine("Press enter in order to get back to the functions menu");
            Console.ReadLine();
        }

        private eGarageFunctions getUserNumberChoosingForFunction()
        {
            return (eGarageFunctions)Enum.Parse(typeof(eGarageFunctions), getUserOptionChoice(k_NumberOfOptionsInMenu));
        }

        private void setVehicleWheels(Vehicle io_Vehicle)
        {
            string wheelProducerName = string.Empty;
            float currentAirPreesure;
            List<string> inputsForWheels;

            inputsForWheels = getValidInputListFromUser(io_Vehicle.WheelProperties());
            wheelProducerName = inputsForWheels[0];
            currentAirPreesure = Ex03.GarageLogic.ParseUtils.Parse<float>(inputsForWheels[1], " is not a valid quantity of air");
            io_Vehicle.SetVehicleWheels(wheelProducerName, currentAirPreesure);
        }

        private void setUniquePropertiesOfVehicleType(Vehicle io_Vehicle)
        {
            List<string> vehicleProperties = io_Vehicle.UniqueProperties;
            io_Vehicle.UniqueProperties = getValidInputListFromUser(vehicleProperties);
        }
    }
}
