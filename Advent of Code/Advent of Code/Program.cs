using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class SpaceElement
    {
        public string name;
        public int size;
        public bool file;

        public SpaceElement parent;
        public List<SpaceElement> children;

        public SpaceElement()
        {
            name = "";
            size = 0;
            file = false;
            parent = null;
            children = new List<SpaceElement>();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Calories();
            RockPaperScissors();
            Rucksack();
            CampCleanup();
            Stacks();
            Trouble();
            Space();
        }

        static void Space()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\space.txt");
            
            SpaceElement current = new SpaceElement();
            current.name = "/";
            
            foreach(string line in lines)
            {
                string[] parts = line.Split(' ');

                // command
                if (parts[0] == "$")
                {
                    if (parts[1] == "cd")
                    {
                        if (parts[2] == "/")
                        {
                            while (current.parent != null)
                                current = current.parent;
                        }
                        else if (parts[2] == "..")
                        {
                            current = current.parent;
                        }
                        else
                        {
                            foreach(SpaceElement child in current.children)
                            {
                                if (child.name == parts[2])
                                {
                                    current = child;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (parts[1] == "ls")
                {
                    continue;
                }
                else
                {
                    if (parts[0] == "dir")
                    {
                        bool exists = false;
                        foreach (SpaceElement child in current.children)
                        {
                            if (child.name == parts[1])
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            SpaceElement dir = new SpaceElement();
                            dir.name = parts[1];
                            dir.parent = current;
                            dir.file = false;

                            current.children.Add(dir);
                        }
                    }
                    else
                    {
                        bool exists = false;
                        foreach (SpaceElement child in current.children)
                        {
                            if (child.name == parts[1])
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            SpaceElement file = new SpaceElement();
                            file.name = parts[1];
                            file.parent = current;
                            file.file = true;
                            file.size = int.Parse(parts[0]);

                            current.size += file.size;

                            current.children.Add(file);
                        }
                    }
                }
            }

            while (current.parent != null)
                current = current.parent;

            int total_space = 0;

            LoadDirSpace(current);
            GetRepositories(current, ref total_space);

            Console.WriteLine("Total space = " + total_space);

            while (current.parent != null)
                current = current.parent;

            int free_space = 70000000 - current.size;

            int needed = 30000000 - free_space;

            List<SpaceElement> sorted = new List<SpaceElement>();            
            SortDirs(current, ref sorted);
            sorted = sorted.OrderBy(e => e.size).ToList();
            int min_size = sorted.FindAll(e => e.size >= needed).OrderBy(e => e.size).First().size;
            Console.WriteLine("Min size = " + min_size);
        }

        static void SortDirs(SpaceElement element, ref List<SpaceElement> sorted)
        {
            if (!element.file)
                sorted.Add(element);

            if (element.children.Count == 0 || !element.children.Exists(e => e.file == false))
                return;

            foreach (SpaceElement child in element.children)
            {
                if (!child.file)
                    SortDirs(child, ref sorted);
            }
        }

        static void LoadDirSpace(SpaceElement element)
        {
            foreach (SpaceElement child in element.children)
            {
                if (!child.file)
                    LoadDirSpace(child);
            }

            if (!element.file && element.parent != null)
                element.parent.size += element.size;

            if (element.children.Count == 0 || !element.children.Exists(e => e.file == false))
                return;

        }

        static void GetRepositories(SpaceElement element, ref int total_space)
        {
            if (element.size <= 100000)
                total_space += element.size;

            if (element.children.Count == 0 || !element.children.Exists(e => e.file == false))
                return;

            foreach (SpaceElement child in element.children)
            {
                if (!child.file)
                    GetRepositories(child, ref total_space);
            }
        }

        static void Trouble()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\trouble.txt");
            string line = lines[0];
            bool found = false;
            int counter = 0;
            Queue<char> marker = new Queue<char>();

            do
            {
                marker.Enqueue(line[counter]);

                if (counter >= 13)
                {
                    int groups_count = marker.GroupBy(x => x).Count();
                    if (groups_count == 14)
                    {
                        found = true;
                        Console.WriteLine(counter + 1);
                    }
                    marker.Dequeue();
                }

                counter++;
                
            } while (!found);
        }

        static void Stacks()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\stacks.txt");
            List<Stack> reverseStacks = new List<Stack>();
            List<Stack> stacks = new List<Stack>();
            int stacks_count = 0;

            foreach (string line in lines)
            {
                if (line.Length > 2 && int.TryParse(line[1].ToString(), out int number))
                {
                    int.TryParse(line[line.Length - 2].ToString(), out stacks_count);
                    
                    for (int i = 0; i < stacks_count; i++)
                    {
                        reverseStacks.Add(new Stack());
                        stacks.Add(new Stack());
                    }

                    break;
                }
            }

            foreach (string line in lines)
            {
                // loading stacks
                if (line.Length > 2 && line.Contains('['))
                {
                    for (int i = 0; i < line.Length; i += 4)
                    {
                        if (line[i + 1] >= 'A' && line[i + 1] <= 'Z')
                        {
                            if (reverseStacks[i / 4] == null)
                                reverseStacks[i / 4] = new Stack();

                            reverseStacks[i / 4].Push(line[i + 1]);
                        }
                    }

                }

                // reversing stacks
                if (line.Length > 2 && int.TryParse(line[1].ToString(), out int number))
                {
                    int counter = 0;
                    foreach (Stack reverse_stack in reverseStacks)
                    {
                        while (reverse_stack.Count > 0)
                            stacks[counter].Push(reverse_stack.Pop());
                        counter++;
                    }
                }

                // moving objects
                if (line.Length > 4 && line[0] == 'm')
                {
                    int amount = int.Parse(line.Split(' ')[1]);
                    int from = int.Parse(line.Split(' ')[3]);
                    int to = int.Parse(line.Split(' ')[5]);

                    Stack temp = new Stack();
                    
                    for (int i = 0; i < amount; i++)
                        temp.Push(stacks[from - 1].Pop());

                    for (int i = 0; i < amount; i++)
                        stacks[to - 1].Push(temp.Pop());
                }
            }

            Console.WriteLine("Letters = ");

            for (int i = 0; i < stacks_count; i++)
            {
                Console.Write(stacks[i].Peek());
            }
            Console.WriteLine();
        }

        static void CampCleanup()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\cleanup.txt");
            int total_containing = 0;
            int total_overlap = 0;

            foreach (string line in lines)
            {
                string first_elf = line.Split(',')[0];
                string second_elf = line.Split(',')[1];

                int a = int.Parse(first_elf.Split('-')[0]);
                int b = int.Parse(first_elf.Split('-')[1]);
                int c = int.Parse(second_elf.Split('-')[0]);
                int d = int.Parse(second_elf.Split('-')[1]);

                if ((a <= c && b >= d) || (a >= c && b <= d))
                    total_containing++;

                if ((a <= c && b >= c) || (a <= d && b >= d) || (c <= a && d >= a) || (c <= b && d >= b))
                    total_overlap++;
            }

            Console.WriteLine("Total containing = " + total_containing);
            Console.WriteLine("Total overlap = " + total_overlap);
        }

        static void Rucksack()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\rucksack.txt");
            char[] differences = new char[lines.Length];
            int counter = 0;
            int total_score = 0;
            int total_badges = 0;

            /*
            foreach(string line in lines)
            {
                bool found = false;

                for (int i = 0; i < line.Length/2; i++)
                {
                    char item = line[i];

                    for (int j = line.Length - 1; j >= line.Length/2; j--)
                    {
                        if (item == line[j])
                        {
                            differences[counter++] = item;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                        break;
                }
            }
            */

            for (int i = 0; i < lines.Length; i+=3)
            {
                bool found = false;
                string line1 = lines[i];
                string line2 = lines[i + 1];
                string line3 = lines[i + 2];

                for (int j = 0; j < line1.Length; j++)
                {
                    char item1 = line1[j];

                    for (int k = 0; k < line2.Length; k++)
                    {
                        char item2 = line2[k];

                        if (item1 == item2 && item1 !=0)
                        {
                            for (int l = 0; l < line3.Length; l++)
                            {
                                char item3 = line3[l];

                                if (item1 == item3)
                                {
                                    differences[counter++] = item1;
                                    found = true;
                                    break;
                                }
                            }
                        }

                        if (found)
                            break;
                    }

                    if (found)
                        break;
                }
            }

            foreach (char item in differences)
            {
                if (char.IsLower(item))
                {
                    int priority = (int)item - 96;
                    total_score += priority;
                }
                else if (char.IsUpper(item))
                {
                    int priority = (int)item - 38;
                    total_score += priority;
                }
            }

            Console.WriteLine("Total score = " + total_score);

        }

        static void RockPaperScissors()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\rockpaperscissors.txt");
            int totalScore = 0;
            int totalScore2 = 0;

            foreach (string line in lines)
            {
                char first = line[0];
                char second = line[2];

                switch (second)
                {
                    case 'X':
                        totalScore += 1;
                        switch (first)
                        {
                            case 'A':
                                totalScore += 3;
                                totalScore2 += 3;
                                break;
                            case 'B':
                                totalScore2 += 1;
                                break;
                            case 'C':
                                totalScore += 6;
                                totalScore2 += 2;
                                break;
                        }

                        break;
                    case 'Y':
                        totalScore += 2;
                        totalScore2 += 3;
                        switch (first)
                        {
                            case 'A':
                                totalScore += 6;
                                totalScore2 += 1;
                                break;
                            case 'B':
                                totalScore += 3;
                                totalScore2 += 2;
                                break;
                            case 'C':
                                totalScore2 += 3;
                                break;
                        }
                        break;
                    case 'Z':
                        totalScore += 3;
                        totalScore2 += 6;
                        switch (first)
                        {
                            case 'A':
                                totalScore2 += 2;
                                break;
                            case 'B':
                                totalScore += 6;
                                totalScore2 += 3;
                                break;
                            case 'C':
                                totalScore += 3;
                                totalScore2 += 1;
                                break;
                        }
                        break;
                }


            }
            Console.WriteLine("Total score 1 = " + totalScore);
            Console.WriteLine("Total score 2 = " + totalScore2);
        }

        static int Calories()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\calories.txt");
            int[] calories = new int[lines.Length];
            calories[0] = 0;
            int counter = 0;

            foreach (string line in lines)
            {
                if (line == "")
                {
                    counter++;
                    calories[counter] = 0;
                    continue;
                }

                calories[counter] += int.Parse(line);

            }
            int max1 = calories.Max();
            int index1 = calories.ToList().IndexOf(max1);
            calories[index1] = 0;

            int max2 = calories.Max();
            int index2 = calories.ToList().IndexOf(max2);
            calories[index2] = 0;

            int max3 = calories.Max();
            int index3 = calories.ToList().IndexOf(max3);

            int total = max1 + max2 + max3;
            Console.WriteLine("Max calories = " + total);

            return total;
        }
    }
}
