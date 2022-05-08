// See https://aka.ms/new-console-template for more information

using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
{
    HasHeaderRecord = false,
    Comment = '#',
    AllowComments = true,
    Delimiter = ";",
};



bool isValid(string name)//method to check if a string contains alphabets only
{
    bool isValid = false;
    for (int i = 0; i < name.Length; i++)
    {
        if (Regex.IsMatch(name, @"^[A-Za-z]+$"))//uses regular expression to check that the given contains only alphabets
        {
            isValid = true;// Returns true only if the string contains alphabets only, otherwise the method returns false
        }
    }

    return isValid;

}

void removeDuplicates(ArrayList myList) //finds and removes duplicates from the arraylist
{

    for (int i = 0; i < myList.Count; i++)
    {
        for (int j = i + 1; j < myList.Count; j++)
        {
            if (myList[j].ToString() == myList[i].ToString())
            {
                myList.Remove(myList[j]);

            }
        }
    }
}

int matchCalc(string name1, string name2)
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
        {
            if (line[i] == line2[j])//Loops through the string
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

    return Int32.Parse(sl1);

}

int avgMatch(int num1, int num2)
{
    return ((num1 + num2) / 2);//returns the average between 2 values
}

Console.WriteLine("Welcome. Please select an option from the ones entered below:");
Console.WriteLine("1: Get data from user");
Console.WriteLine("2: Get data from user");
string option = Console.ReadLine();
while (!(Regex.IsMatch(option, @"^[1-2]+$")))
{
    Console.WriteLine("Please select an option from the options provided above");
    option = Console.ReadLine();

}

if (option == "1")
{
    Console.WriteLine("Enter in the name of the first person: ");
    string name1 = (Console.ReadLine());//Gets the first persons name
    while (isValid(name1) == false)//if the string contains character that are not a part of the alphabet
    {
        Console.WriteLine("Re-enter in the first name (Alphabets only): ");//we ask a user to re-enter the name
        name1 = (Console.ReadLine());//until the string contains only alphabets. This is also done when entering the name of the second person
    }

    Console.WriteLine("Enter in the name of the second person: ");
    string name2 = Console.ReadLine();
    while (isValid(name2) == false)
    {
        Console.WriteLine("Re-enter in the Second name (Alphabets only): ");
        name2 = (Console.ReadLine());
    }

    int s1, s2;

    s1 = matchCalc(name1, name2);
    if (s1 >= 80)
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + s1 + "%, good match!");
    }
    else
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + s1 + "%");
    }

    s2 = matchCalc(name2, name1);
    if (s2 >= 80)
    {
        Console.WriteLine(name2 + " matches " + name1 + " " + s2 + "%, good match!");
    }
    else
    {
        Console.WriteLine(name2 + " matches " + name1 + " " + s2 + "%");
    }

    Console.WriteLine("The Average match score between " + name1 + " and " + name2 + " is " + ((s1 + s2) / 2) + "%");

}
if (option == "2")
{

    Console.WriteLine("Enter in the file name with full path and csv file extention: ");
    string fileName = Console.ReadLine();


    while (!(File.Exists(fileName)))
    {
        Console.WriteLine("This file does not exist");
        Console.WriteLine("Please RE-enter the file name with full path and csv file extention: ");
        fileName = Console.ReadLine();

    }
    if (File.Exists(fileName))
    {
        using var streamReader = File.OpenText(fileName);
        using var csvReader = new CsvReader(streamReader, csvConfig);

        ArrayList female = new ArrayList();
        ArrayList male = new ArrayList();
        int female2male, male2female, avg;

        while (csvReader.Read()) //Reads each line in the csv file
        {
            //Console.WriteLine(csvReader.GetField(0));
            if ((csvReader.GetField(1).ToLower() == "m"))// if the letter after the semi-colon is "m", the name is added to the arraylist that will contain the firstname of males
            {
                male.Add(csvReader.GetField(0));

            }
            if ((csvReader.GetField(1).ToLower() == "f"))// if the letter after the semi-colon is "f", the name is added to the arraylist that will contain the firstname of females
            {
                female.Add(csvReader.GetField(0));

            }

        }

        Console.WriteLine("");//For neatness

        /*foreach (string i in male)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        foreach (string i in female)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        */

        removeDuplicates(male);//removes duplicate names from the male arraylist
        removeDuplicates(female);//removes duplicate names from the female arraylist



        string outputFile = @"Output.txt";
        try
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);//Deletes the file if it already exist

            }
            using (StreamWriter sw = File.CreateText(outputFile))// creates a new file
            {
                foreach (string i in female)
                {
                    foreach (string j in male)
                    {
                        female2male = matchCalc(i.ToLower(), j.ToLower());// we first match the female name to the male name
                        if (female2male >= 80)// if the match is greater than or equal to 80%, we indicate that this is a good match
                        {
                            sw.WriteLine(i + " matches " + j + " " + female2male + "%, Good Match");//sw.WriteLine() is used to write to the textfile
                        }
                        else
                        {
                            sw.WriteLine(i + " matches " + j + " " + female2male + "%");
                        }

                        male2female = matchCalc(j.ToLower(), i.ToLower());// here we match the male name to the female name
                        if (male2female >= 80)// if the match is greater than or equal to 80%, we indicate that this is a good match
                        {
                            sw.WriteLine(j + " matches " + i + " " + male2female + "%, Good Match");
                        }
                        else
                        {
                            sw.WriteLine(j + " matches " + i + " " + male2female + "%");
                        }

                        avg = avgMatch(female2male, male2female);// calculates the average match score between the female name and the male name
                        if (avg >= 80)
                        {
                            sw.WriteLine("The average match score between " + i + " and " + j + " is " + avg + "%, Good Match");
                        }
                        else
                        {
                            sw.WriteLine("The average match score between " + i + " and " + j + " is " + avg + "%");
                        }

                        sw.WriteLine("");//leaves a line after each name combination for neatness
                    }
                }

            }

            using (StreamReader sr = File.OpenText(outputFile)) //Reads and displays the output file to the console
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.ToString());
        }

    }



}
