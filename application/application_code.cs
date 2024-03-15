using System;

//application simulates a toy robot on a 5x5 unit square tabletop
namespace application
{
    class assignmentProgram
    {
        //section used as an entry point for the execution of a programme
        //this will be executed when the programme is run, coordinating and calling other functions
        static void Main()
        {
            toyRobot robot = new toyRobot();

            Console.WriteLine("                 WELCOME!                 ");
            Console.WriteLine("        TOY ROBOT SIMULATOR SYSTEM        \n");

            Console.WriteLine("note: to exit the system please write EXIT\n");
            string command;
            string overallcommand = "";

            //section used as a loop of asking the user to get input information until the user types EXIT to end the system
            do
            {
                //command used to ask for input information from the user by entering data
                //will be in the same line as the question
                Console.Write("INPUT: ");
                command = Console.ReadLine();

                //command used to check that the input information cannot be null or empty space
                if (string.IsNullOrWhiteSpace(command))
                {
                    Console.WriteLine("ERROR! PLEASE INPUT THE INFORMATION\n");
                }
                else if (command != "EXIT")
                {
                    //command used to check that the first input line should start with the word PLACE before processing other individual commands
                    if (command.StartsWith("PLACE"))
                    {
                        //command used to check that the first command has been entered into the correct format
                        if (parsefirstCommand(command, robot))
                        {
                            //section used as a loop for asking the user to get further input information until the user types REPORT to break the loop
                            do
                            {
                                command = Console.ReadLine();
                                
                                //command used to check that the input information cannot be null or empty space
                                if (string.IsNullOrWhiteSpace(command))
                                {
                                    Console.WriteLine("ERROR! PLEASE INPUT THE INFORMATION\n");
                                }
                                else
                                {
                                    //command used to check that the individual command should be only these four
                                    if (command == "MOVE" || command == "LEFT" || command == "RIGHT" || command == "REPORT")
                                    {
                                        //command used to add all input commands together into one variable by seperating with a specific character
                                        overallcommand += command + "/";

                                        //command used to check if the user has already entered the wording REPORT or not. if yes, process further logic.
                                        if (command == "REPORT")
                                        {
                                            //command used to send the whole command to the PROCESSCOMMAND function to work on further processes.
                                            //then restore the variable to be ready for the new input command
                                            robot.processCommand(overallcommand);
                                            overallcommand = "";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR! INVALID COMMAND\n");
                                    }
                                }
                            } while (command != "REPORT");
                        }
                        else
                        {
                            Console.WriteLine("ERROR! INVALID PLACE COMMAND FORMAT\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR! PLEASE INPUT THE CORRECT FORMAT\n");
                    }
                }
            } while (command != "EXIT");
        }

        //function used to deal with the format of the starting command
        private static bool parsefirstCommand(string placeCommand, toyRobot robot)
        {
            //section that used to remove the word PLACE and all space from the input information and
            //will split the string into an array by using commas
            //data that is stored in an array will be divided into 3 parts, which are X, Y, and direction
            string[] parameters = placeCommand.Replace("PLACE", "").Trim().Split(",");

            //command used to check the condition of a parameter where it should have only 3 parts
            //the first and second parameters should be able to be converted into INTEGERS
            //the third parameter should not be null or empty, and it should be only the specific wording
            if (parameters.Length == 3 && int.TryParse(parameters[0], out int convertX)
            && int.TryParse(parameters[1], out int convertY)
            && parameters[2] != null && parameters[2] != ""
            && (parameters[2] == "NORTH" || parameters[2] == "SOUTH" ||
                parameters[2] == "EAST" || parameters[2] == "WEST"))
            {
                robot.usedplaceCommand(convertX, convertY, parameters[2]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //section used as a method for processing commands and managing the position of a robot
    class toyRobot
    {
        private int x, y;
        private string direction;

        //function used to receive specific data from input information and update within the variable that will be used within the further process
        public void usedplaceCommand(int convertX, int convertY, string newDirection)
        {
            x = convertX;
            y = convertY;
            direction = newDirection;
        }

        //function used to deal with the whole input command
        public void processCommand(string command)
        {
            //command used to split the whole input by using a specific character and store it in an array
            string[] commands = command.Split("/", StringSplitOptions.RemoveEmptyEntries);

            //section used to loop through all the commands in the list

            foreach (var cmd in commands)
            {
                singleCommand(cmd);
            }
        }
        
        //function used to process a single command at a time from the total input information
        //sfter getting a match of commands, it will call each function to process a specific logic
        public void singleCommand(string command)
        {
            switch (command)
            {
                case "MOVE":
                    moveforward();
                    break;
                case "LEFT":
                    turnleft();
                    break;
                case "RIGHT":
                    turnright();
                    break;
                case "REPORT":
                    reportresult();
                    break;
            }
        }

        //function used to deal with the MOVE command, which will move the robot one unit forward in the current direction
        private void moveforward()
        {
            //section used to execute one variable among many alternatives
            //if the DIRECTION variable matches with any choice, the process will be executed

            //the command inside is used to check whether the robot can move or not. it can be moved only in a dimension of 5x5 units.
            switch (direction)
            {
                case "NORTH":
                    if(y < 4)
                        y++;
                    break;
                case "SOUTH":
                    if(y > 0)
                        y--;
                    break;
                case "EAST":
                    if(x < 4)
                        x++;
                    break;
                case "WEST":
                    if(x > 0)
                        x--;
                    break;
            }
        }

        //function used to deal with the LEFT command, which will rotate the robot 90 degrees to the left from the latest direction
        private void turnleft()
        {
            //section used to execute one variable among many alternatives
            //if the DIRECTION variable matches with any choice, the process will be executed

            //command inside is used to update the direction of the robot after turning
            switch (direction)
            {
                case "NORTH":
                    direction = "WEST";
                    break;
                case "SOUTH":
                    direction = "EAST";
                    break;
                case "EAST":
                    direction = "NORTH";
                    break;
                case "WEST":
                    direction = "SOUTH";
                    break;
            }
        }

        //function used to deal with the RIGHT command, which will rotate the robot 90 degrees to the right from the latest direction
        private void turnright()
        {
            switch (direction)
            {
                case "NORTH":
                    direction = "EAST";
                    break;
                case "SOUTH":
                    direction = "WEST";
                    break;
                case "EAST":
                    direction = "SOUTH";
                    break;
                case "WEST":
                    direction = "NORTH";
                    break;
            }
        }

        //function used to deal with the REPORT command and print the output of the location and direction of the robot
        private void reportresult()
        {
            Console.WriteLine($"OUTPUT: {x},{y},{direction} \n");
        }

    }

}