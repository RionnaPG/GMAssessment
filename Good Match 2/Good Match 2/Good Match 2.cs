// See https://aka.ms/new-console-template for more information

using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.Globalization;

var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
{
    HasHeaderRecord = false,
    Comment = '#',
    AllowComments = true,
    Delimiter = ";",
};

void removeDuplicates(ArrayList myList) //finds and removes duplicates from the arraylist
{ 
    
    for (int i=0;i<myList.Count;i++)
    {
        for (int j = i+1; j < myList.Count; j++)
        {
            if (myList[j].ToString()==myList[i].ToString())
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
        while (sl1.Length > 1)//while the length is greater than zero
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


Console.WriteLine("Enter in the file name with full path and csv file extention: ");
string fileName = Console.ReadLine();
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
    using (StreamWriter sw=File.CreateText(outputFile))// creates a new file
    {
        foreach (string i in female)
        {
            foreach (string j in male)
            {
                female2male = matchCalc(i.ToLower(), j.ToLower());// we first match the female name to the male name
                if (female2male >=80)// if the match is greater than or equal to 80%, we indicate that this is a good match
                {
                    sw.WriteLine(i+" matches "+j+" "+female2male+"%, Good Match");//sw.WriteLine() is used to write to the textfile
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
                    sw.WriteLine("The average match score between " + i + " and " + j + " is "+ avg+ "%, Good Match");
                }
                else
                {
                    sw.WriteLine("The average match score between " + i + " and " + j + " is " + avg + "%");
                }

                sw.WriteLine("");//leaves a line after each name combination for neatness
            }
        }

    }

    using (StreamReader sr=File.OpenText(outputFile)) //Reads and displays the output file to the console
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



