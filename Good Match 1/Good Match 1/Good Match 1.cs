// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

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

int matchCalc(string name1, string name2)
{

    string line = (name1 + "matches" + name2).ToLower();
    string sl1 = "";
    string line2 = line;
    string sCounter = "";
    int cNumber;
    int counter = 0;

    for (int i = 0; i < line.Length; i++)
    {
        for (int j = 0; j < line2.Length; j++)
        {
            if (line[i] == line2[j])
            {
                counter++;
                line2 = line2.Remove(j, 1);

            }

        }

        if (counter >= 1) { sCounter += counter.ToString(); }

        counter = 0;

    }

    sl1 = sCounter;
    sCounter = "";


    while ((Int32.TryParse(sl1, out cNumber) == false) || (Int32.TryParse(sl1, out cNumber) == true && (cNumber < 10 || cNumber > 100)))
    {
        //Console.WriteLine(sl1);
        while (sl1.Length > 1)
        {
            counter = (Int32.Parse(sl1.Substring(0, 1)) + (Int32.Parse(sl1.Substring(sl1.Length - 1, 1))));

            sCounter += counter.ToString();
            sl1 = sl1.Remove(0, 1);
            sl1 = sl1.Remove(sl1.Length - 1, 1);
        }
        if (sl1.Length == 1)
        {
            sCounter += sl1;
        }
        sl1 = sCounter;
        sCounter = "";


    }

    cNumber = Int32.Parse(sl1);
    
    return cNumber;

}

Console.WriteLine("Enter in the name of the first person : ");
string name1 = (Console.ReadLine());
while (isValid(name1) == false)
{
    Console.WriteLine("Re-enter in the first name (Alphabets only): ");
    name1 = (Console.ReadLine());
}

Console.WriteLine("Enter in the name of the second person : ");
string name2 = Console.ReadLine();
while (isValid(name2) == false)
{
    Console.WriteLine("Re-enter in the Second name (Alphabets only): ");
    name2 = (Console.ReadLine());
}
int s1,s2;

s1=matchCalc(name1, name2);
if (s1 >= 80)
{
    Console.WriteLine(name1 + " matches " + name2 + " " + s1 + "%, good match!");
}
else
{
    Console.WriteLine(name1 + " matches " + name2 + " " + s1 + "%");
}

s2=matchCalc(name2, name1);
if (s2 >= 80)
{
    Console.WriteLine(name2 + " matches " + name1 + " " + s2 + "%, good match!");
}
else
{
    Console.WriteLine(name2 + " matches " + name1+ " " + s2 + "%");
}

Console.WriteLine("The Average match score between "+name1+" and "+name2+" is "+((s1+s2)/2)+"%");
