// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

bool isValid(string name)
{
    bool isValid = false;
    for (int i = 0; i < name.Length; i++)
    {
        if (Regex.IsMatch(name, @"^[a-z]+$"))
        {
            isValid = true;
        }
    }

    return isValid;

}

void matchCalc(string name1, string name2)
{

    string line = name1 + "matches" + name2;
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
    if (cNumber >= 80)
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + sl1 + "%, good match!");
    }
    else
    {
        Console.WriteLine(name1 + " matches " + name2 + " " + sl1 + "%");
    }


}

Console.WriteLine("Enter in the first name: ");
string name1 = (Console.ReadLine().ToLower());
while (isValid(name1) == false)
{
    Console.WriteLine("Re-enter in the first name (Alphabets only): ");
    name1 = (Console.ReadLine().ToLower());
}

Console.WriteLine("Enter in the Second name: ");
string name2 = Console.ReadLine().ToLower();
while (isValid(name2) == false)
{
    Console.WriteLine("Re-enter in the Second name (Alphabets only): ");
    name2 = (Console.ReadLine().ToLower());
}

matchCalc(name1, name2);
