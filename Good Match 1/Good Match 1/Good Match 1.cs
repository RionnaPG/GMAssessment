// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

bool isValid(string name)//method to check if a string contains alphabets only
{
    bool isValid = false;
    for (int i = 0; i < name.Length; i++)
    {
        if (Regex.IsMatch(name, @"^[a-z]+$"))//uses regular expression to check that the given contains only alphabets
        {
            isValid = true;// Returns true only if the string contains alphabets only, otherwise the method returns false
        }
    }

    return isValid;

}

void matchCalc(string name1, string name2)
{

    string line = (name1 + "matches" + name2).ToLower();// create a string containing the word "matches" with no spaces between the word "matches" and the 2 names provided
    string sl1 = "";
    string line2 = line;//creates a copy of the variable line
    string sCounter = "";
    int cNumber;
    int counter = 0;

    for (int i = 0; i < line.Length; i++)
    {
        for (int j = 0; j < line2.Length; j++)
        {//Loops through the string
            if (line[i] == line2[j])
            {
                counter++;//counts the number of times a character appears
                line2 = line2.Remove(j, 1);//removes the character from the string so that the character won't be counted multiple times

            }

        }

        if (counter >= 1) { sCounter += counter.ToString(); }//adds the the count for each character to a string 

        counter = 0;

    }

    sl1 = sCounter;
    sCounter = "";


    while ((Int32.TryParse(sl1, out cNumber) == false) || (Int32.TryParse(sl1, out cNumber) == true && (cNumber < 10 || cNumber > 100)))//loops until the string containing the count only has 2 digits
    {
        //Console.WriteLine(sl1);
        while (sl1.Length > 1)//while the length is greater than one
        {
            counter = (Int32.Parse(sl1.Substring(0, 1)) + (Int32.Parse(sl1.Substring(sl1.Length - 1, 1))));//adds current the first and last value of the string 

            sCounter += counter.ToString();//into a new string
            sl1 = sl1.Remove(0, 1);//removes the current first value
            sl1 = sl1.Remove(sl1.Length - 1, 1);//removes the current last value
        }
        if (sl1.Length == 1)//if the string contains an odd number of values 1 value will be left
        {
            sCounter += sl1;//this value is added to the end of the new string
        }
        sl1 = sCounter;// the new string the overwrites the original string
        sCounter = "";


    }

    cNumber = Int32.Parse(sl1);
    if (cNumber >= 80)//if the resulting number is greater than or equal to 80 , we indicate that this match is a "Good match"
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + sl1 + "%, good match!");
    }
    else
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + sl1 + "%");//otherwise we just indicate the resulting match score
    }


}

Console.WriteLine("Enter in the name of the first person: ");
string name1 = (Console.ReadLine().ToLower());//Gets the first persons name
while (isValid(name1) == false)//if the string contains character that are not a part of the alphabet
{
    Console.WriteLine("Re-enter in the first name (Alphabets only): ");//we ask a user to re-enter the name
    name1 = (Console.ReadLine().ToLower());//until the string contains only alphabets. This is also done when entering the name of the second person
}

Console.WriteLine("Enter in the name of the second person: ");
string name2 = Console.ReadLine().ToLower();
while (isValid(name2) == false)
{
    Console.WriteLine("Re-enter in the Second name (Alphabets only): ");
    name2 = (Console.ReadLine().ToLower());
}

matchCalc(name1, name2);//calculates and outputs the match score between 2 given names
