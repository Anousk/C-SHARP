/*Color code:
    - \u001b[32m : Green
    - \u001b[35m : Magenta
    - \u001b[43m : Yellow Background
    - \u001b[0m : Reset color
        
        - https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html*/

string[][] panel = new string[][] {new string[] {" \u001b[35m0 ", "\u001b[43m \u001b[0m", " \u001b[35m1 ", "\u001b[43m \u001b[0m", " \u001b[35m2 "},
                                   new string[] {" \u001b[35m3 ", "\u001b[43m \u001b[0m", " \u001b[35m4 ", "\u001b[43m \u001b[0m", " \u001b[35m5 "},
                                   new string[] {" \u001b[35m6 ", "\u001b[43m \u001b[0m", " \u001b[35m7 ", "\u001b[43m \u001b[0m", " \u001b[35m8 "}};

string panelSeparator = "\n\u001b[43m           \u001b[0m\n";

bool game = true;

int state = 0;

string[] allowedInputs = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "?"};


int Getinput(string player)
{
    string userInput = "";
    bool inputting = true;
    
    
    while (inputting)
    {
        Console.Write("What do you want to play (? -> Tutorial) (" + player + ") : ");
        userInput = Console.ReadLine();

        if (userInput == "?")
        {
            Console.Write("\n\nThis game is a simple TikTakToe you need to align 3 of your symbols in a row to win." +
                            "\nTo use your symbol you need to input the number of the box you want to play.\n\n");
        }
        else if (!allowedInputs.Any(userInput.Contains))
        {
            Console.WriteLine("\n\n\n\u001b[31mERROR: Invalid input\u001b[0m\n\n\n");
        }
        else
        {
            inputting = false;
        }
    }

    return int.Parse(userInput);
}

string Parseinputtoarray(string player, int input)
{
    if (input >= 0 && input <= 2)
    {
        panel[0][input * 2] = "\u001b[32m " + player + " \u001b[0m";
    }
    else if (input >= 3 && input <= 5)
    {
        panel[1][(input - 3) * 2] = "\u001b[32m " + player + " \u001b[0m";
    }
    else if (input >= 6 && input <= 8)
    {
        panel[2][(input - 6) * 2] = "\u001b[32m " + player + " \u001b[0m";
    }
    else
    {
        Console.WriteLine("Invalid input");
    }

    return null;
}

bool Checkforwin()
{
    bool returning = true;
    
    for (int line = 0; line < 3; line++)
    {
        if (panel[line][0] == panel[line][2] && panel[line][2] == panel[line][4])
        {
            returning = false;
        }
    } //Checking Wins in a line
    
    for (int column = 0; column < 5; column += 2)
    {
        if (panel[0][column] == panel[1][column] && panel[1][column] == panel[2][column])
        {
            returning = false;
        }
    } //Checking Wins in a column

    if ((panel[0][0] == panel[1][2] && panel[1][2] == panel[2][4]) || (panel[0][4] == panel[1][2] && panel[1][2] == panel[2][0]))
    {
        returning = false;
    } //Checking Wins in a diagonal
    
    return returning;
}



//Main Game Loop
while (game)
{
    for (int i = 0; i < 3; i++)
    {
        Array.ForEach(panel[i], Console.Write);

        if (i != 2)
        {
            Console.Write(panelSeparator);
        }
    }
    Console.WriteLine("\n\u001b[0m"); //Prints out the panel

    //Registering players input and sending it to the panel [State : 0 = player(X), 1 = player(Z)]
    if (state == 0)
    {
        Parseinputtoarray("X", Getinput("X"));
        state = 1;
    }
    else if (state == 1)
    {
        Parseinputtoarray("Z", Getinput("Z"));
        state = 0;
    }
    
    
    game = Checkforwin();
    if (!game)
    {
        for (int i = 0; i < 3; i++)
        {
            Array.ForEach(panel[i], Console.Write);

            if (i != 2)
            {
                Console.Write(panelSeparator);
            }
        }
        Console.WriteLine("\n\u001b[0m"); 
        
        Console.WriteLine("  You won!");
    } //Prints out the end panel
}