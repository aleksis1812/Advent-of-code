using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace advent
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

    class Monkey 
    {
        public int id;
        public long business;
        public char operation;
        public string operation_number;
        public int divisible;
        public int monkey_id_true;
        public int monkey_id_false;

        public List<ulong> items;

        public Monkey(int id, char operation, string operation_number, int divisible, int monkey_id_true, int monkey_id_false)
        {
            this.id = id;
            this.business = 0;
            this.operation = operation;
            this.operation_number = operation_number;
            this.divisible = divisible;
            this.monkey_id_false = monkey_id_false;
            this.monkey_id_true = monkey_id_true;
            this.items = new List<ulong>();
        }
    }

    public class HIllField
    {
        public int row;
        public int column;
        public int distance;

        public HIllField(int x, int y, int distance)
        {
            this.row = x;
            this.column = y;
            this.distance = distance;
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
            TreeHouse();
            Bridge();
            CathodeRayTube();
            MonkeyBusiness();
            HillClimbing();
            DistressSignal();
        }

        static void DistressSignal()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\indices.txt");
            List<int> indexes = new List<int>();

            for (int i = 0; i < lines.Length; i+= 3)
            {
                int start = 0;
                ArrayList firstList = LoadLists(ref start, lines[i]);
                start = 0;
                ArrayList secondList = LoadLists(ref start, lines[i + 1]);

                // comparing
                bool found = false;
                for(int j=0; j < firstList.Count; j++)
                {
                    if(firstList[j].GetType() ==  typeof(int))
                    {
                        if (secondList[j].GetType() == typeof(int))
                        {
                            if ((int)firstList[j] < (int)secondList[j])
                            {
                                found = true;
                                indexes.Add(i);
                                break;
                            }
                            else if ((int)firstList[j] > (int)secondList[j])
                            {
                                found = true;
                                break;
                            }

                            continue;
                        }

                        if (secondList[j].GetType() == typeof(ArrayList))
                        {
                            string is_correct = CompareIntegerAndList((int)firstList[j], (ArrayList)secondList[j]);

                            if (is_correct == "true")
                            {
                                found = true;
                                indexes.Add(i);
                                break;
                            }
                            else if (is_correct == "false")
                            {
                                found = true;
                                break;
                            }

                            continue;
                        }
                    }

                    if (firstList[j].GetType() == typeof(ArrayList))
                    {
                        if (secondList[j].GetType() == typeof(int))
                        {
                            string is_correct = CompareListAndInteger((ArrayList)firstList[j], (int)secondList[j]);

                            if (is_correct == "true")
                            {
                                found = true;
                                indexes.Add(i);
                                break;
                            }
                            else if (is_correct == "false")
                            {
                                found = true;
                                break;
                            }

                            continue;
                        }

                        if (secondList[j].GetType() == typeof(ArrayList))
                        {
                            string is_correct = CompareLists((ArrayList)firstList[j], (ArrayList)secondList[j]);

                            if (is_correct == "true")
                            {
                                found = true;
                                indexes.Add(i);
                                break;
                            }
                            else if (is_correct == "false")
                            {
                                found = true;
                                break;
                            }

                            continue;
                        }
                    }
                }

                if (!found && lines[i].Length < lines[i + 1].Length)
                {
                    indexes.Add(i);
                }
            }
        }

        static string CompareLists(ArrayList left, ArrayList right)
        {
            // TODO doesn't work if list contains empty list

            if (left.Count == 0 && right.Count != 0)
                return "true";

            if (left.Count != 0 && right.Count == 0)
                return "false";

            if (left.Count == 0 && right.Count == 0)
                return "";

            if (left[0].GetType() == typeof(ArrayList) && right[0].GetType() == typeof(ArrayList))
                return CompareLists((ArrayList)left[0], (ArrayList)right[0]);

            if (left[0].GetType() == typeof(ArrayList) && right[0].GetType() == typeof(int))
                return CompareListAndInteger((ArrayList)left[0], (int)right[0]);

            if (left[0].GetType() == typeof(int) && right[0].GetType() == typeof(ArrayList))
                return CompareIntegerAndList((int)left[0], (ArrayList)right[0]);

            int index = 0;

            // both contain integer(s) first
            while (index < left.Count && index < right.Count)
            {
                if (left[index].GetType() == typeof(ArrayList) && right[index].GetType() == typeof(ArrayList))
                    return CompareLists((ArrayList)left[index], (ArrayList)right[index]);

                if (left[index].GetType() == typeof(ArrayList) && right[index].GetType() == typeof(int))
                    return CompareListAndInteger((ArrayList)left[index], (int)right[index]);

                if (left[index].GetType() == typeof(int) && right[index].GetType() == typeof(ArrayList))
                    return CompareIntegerAndList((int)left[index], (ArrayList)right[index]);

                if ((int)left[index] < (int)right[index])
                    return "true";
                else if ((int)left[index] > (int)right[index])
                    return "false";
            }

            if (left.Count == right.Count)
                return "";
            else if (left.Count < right.Count)
                return "true";
            else
                return "false";

        }

        static string CompareIntegerAndList(int left, ArrayList right)
        {
            if (right.Count == 0)
                return "false";

            if (right[0].GetType() == typeof(ArrayList))
                return CompareIntegerAndList(left, (ArrayList)right[0]);

            if (left < (int)right[0])
                return "true";
            else if (left > (int)right[0])
                return "false";

            // left is shorter than right 
            if (right.Count > 1)
                return "true";

            return "";
        }

        static string CompareListAndInteger(ArrayList left, int right)
        {
            if (left.Count == 0)
                return "true";

            if (left[0].GetType() == typeof(ArrayList))
                return CompareListAndInteger((ArrayList)left[0], right);

            if ((int)left[0] < right)
                return "true";
            else if ((int)left[0] > right)
                return "false";

            // left is shorter than right 
            if (left.Count > 1)
                return "false";

            return "";
        }

        static ArrayList LoadLists(ref int start, string line)
        {
            ArrayList new_list = new ArrayList();
            // [[1],[2,3,4]]
            for (; start < line.Length;)
            {
                start++;

                if (line[start] == '[')
                    new_list.Add(LoadLists(ref start, line));
                else if (line[start] == ']')
                    return new_list;
                else if (line[start] == ',')
                    continue;
                else
                    new_list.Add(int.Parse(line[start].ToString()));
            }

            return new ArrayList();
        }

        static void HillClimbing()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\climbing.txt");

            char[,] matrix = new char[lines.Length, lines[0].Length];
            Tuple<int, int> start = new Tuple<int, int>(0, 0);
            Tuple<int, int> end = new Tuple<int, int>(0, 0);

            List<Tuple<int, int>> lowest_start_points = new List<Tuple<int, int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'S')
                        start = new Tuple<int, int>(i, j);

                    if (lines[i][j] == 'E')
                        end = new Tuple<int, int>(i,j);

                    if (lines[i][j] == 'a')
                        lowest_start_points.Add(new Tuple<int, int>(i,j));

                    matrix[i, j] = lines[i][j];
                }
            }

            int shortest_path = BFS(ref matrix, start, end);

            Console.WriteLine("Shortest path pt1 = " + shortest_path);

            foreach(Tuple<int,int> start_point in lowest_start_points)
            {
                int new_shortest_path = BFS(ref matrix, start_point, end);
                if (new_shortest_path < shortest_path && new_shortest_path > 0)
                    shortest_path = new_shortest_path;
            }

            Console.WriteLine("Shortest path pt2 = " + shortest_path);
        }

        static int BFS(ref char[,] grid, Tuple<int, int> start, Tuple<int, int> end)
        {
            HIllField source = new HIllField(start.Item1, start.Item2, 0);
            grid[start.Item1, start.Item2] = 'a';
            grid[end.Item1, end.Item2] = 'z';

            int N = grid.GetLength(0);
            int M = grid.GetLength(1);

            bool[,] visited = new bool[N, M];
            
            // applying BFS on matrix cells starting from source
            Queue<HIllField> queue = new Queue<HIllField>();
            queue.Enqueue(source);

            visited[source.row, source.column] = true;

            while (queue.Count > 0)
            {
                HIllField p = queue.Peek();
                queue.Dequeue();

                // Destination found;
                if (p.row == end.Item1 && p.column == end.Item2)
                {
                    return p.distance;
                }

                // moving up
                if (p.row - 1 >= 0 && visited[p.row - 1, p.column] == false && (grid[p.row - 1, p.column] - grid[p.row, p.column]) <= 1)
                {
                    queue.Enqueue(new HIllField(p.row - 1, p.column, p.distance + 1));
                    visited[p.row - 1, p.column] = true;
                }

                // moving down
                if (p.row + 1 < N && visited[p.row + 1, p.column] == false && (grid[p.row + 1, p.column] - grid[p.row, p.column]) <= 1)
                {
                    queue.Enqueue(new HIllField(p.row + 1, p.column, p.distance + 1));
                    visited[p.row + 1, p.column] = true;
                }

                // moving left
                if (p.column - 1 >= 0 && visited[p.row, p.column - 1] == false && (grid[p.row, p.column - 1] - grid[p.row, p.column]) <= 1)
                {
                    queue.Enqueue(new HIllField(p.row, p.column - 1, p.distance + 1));
                    visited[p.row, p.column - 1] = true;
                }

                // moving right
                if (p.column + 1 < M && visited[p.row, p.column + 1] == false && (grid[p.row, p.column + 1] - grid[p.row, p.column]) <= 1)
                {
                    queue.Enqueue(new HIllField(p.row, p.column + 1, p.distance + 1));
                    visited[p.row, p.column + 1] = true;
                }
            }

            return -1;
        }

        static void MonkeyBusiness()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\monkeys.txt");

            List<Monkey> monkeys = new List<Monkey>();
            ulong total_module = 1;

            for(int i = 0; i <= lines.Length; i += 7)
            {
                int id = int.Parse(lines[i].Trim().Split(" ")[1].Split(":")[0]);
                List<ulong> items = lines[i + 1].Trim().Split(":")[1]
                                                      .Split(",")
                                                      .Select(e => e.Trim())
                                                      .Select(e => ulong.Parse(e))
                                                      .ToList();
                char operation = char.Parse(lines[i + 2].Trim().Split("old ")[1].Split(" ")[0]);

                string operation_number = lines[i + 2].Trim().Split("old ")[1].Split(" ")[1];
                int divisible = int.Parse(lines[i + 3].Trim().Split("by ")[1]);
                int monkey_id_true = int.Parse(lines[i + 4].Trim().Split("monkey ")[1]);
                int monkey_id_false = int.Parse(lines[i + 5].Trim().Split("monkey ")[1]);

                Monkey monkey = new Monkey(id, operation, operation_number, divisible, monkey_id_true, monkey_id_false);
                monkey.items = items;

                monkeys.Add(monkey);

                total_module *= (ulong)divisible;
            }

            for (int i = 0; i < 10000; i++)
            {
                for(int j = 0; j < monkeys.Count; j++)
                {
                    Monkey current = monkeys[j];

                    foreach(ulong item in current.items)
                    {
                        monkeys[j].business++;

                        ulong new_item = item;
                        ulong second_number = 0;

                        if (current.operation_number == "old")
                            second_number = item;
                        else
                            second_number = ulong.Parse(current.operation_number);

                        new_item = new_item % total_module;

                        switch (current.operation)
                        {
                            case '+':
                                new_item += second_number;
                                break;
                            case '-':
                                new_item -= second_number;
                                break;
                            case '/':
                                new_item /= second_number;
                                break;
                            case '*':
                                new_item *= second_number;
                                break;
                            default:
                                Console.WriteLine("Error!");
                                break;
                        }

                        // new_item = (int)Math.Floor((decimal)new_item / 3);

                        new_item = new_item % total_module;

                        if (new_item % (ulong)current.divisible == 0)
                            monkeys[current.monkey_id_true].items.Add(new_item);
                        else
                            monkeys[current.monkey_id_false].items.Add(new_item);

                    }

                    monkeys[j].items = new List<ulong>();
                }
            }

            long max_business_1 = monkeys.Max(e => e.business);
            monkeys.Remove(monkeys.First(e => e.business == max_business_1));
            long max_business_2 = monkeys.Max(e => e.business);

            Console.WriteLine("Monkey business = " + max_business_1 * max_business_2);
        }

        static void CathodeRayTube() 
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\cycles.txt");
            int cycles=0, previousCycle=0, x=1, signalStrength=0;
            bool first = true;
            List<bool> screen = new List<bool>();

            for (int i = 0; i < 240; i++)
                screen.Add(false);

            foreach (string line in lines)
            {
                string[] parts = line.Split(" ");
                string operation = parts[0];

                switch (operation)
                {
                    case "noop":

                        if (Math.Abs(x - (cycles % 40)) <= 1)
                            screen[cycles] = true;

                        cycles++;

                        if (first && cycles == 20)
                        {
                            first = false;
                            previousCycle = cycles;
                            signalStrength += cycles * x;
                            continue;
                        }

                        if (cycles - previousCycle == 40)
                        {
                            previousCycle = cycles;
                            signalStrength += cycles * x;
                        }

                        break;
                    case "addx":
                        int value = int.Parse(parts[1]);
                        int i = 0;
                        do
                        {
                            if (Math.Abs(x - (cycles % 40)) <= 1)
                                screen[cycles] = true;

                            cycles++;
                            i++;

                            if (first && cycles == 20)
                            {
                                first = false;
                                previousCycle = cycles;
                                signalStrength += cycles * x;
                                continue;
                            }

                            if (cycles - previousCycle == 40)
                            {
                                previousCycle = cycles;
                                signalStrength += cycles * x;
                            }

                        } while (i != 2);

                        x += value;

                        break;
                }
            }
            Console.WriteLine("Signal strength " + signalStrength);

            for (int i = 0; i < screen.Count; i++)
            {
                if (i % 40 == 0)
                {
                    Console.WriteLine();
                }

                if (screen[i])
                    Console.Write("#");
                else
                    Console.Write(".");
            }
            Console.WriteLine();
        }

        static void Bridge()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\bridge.txt");
            int left = 0, right = 0, up = 0, down = 0;
            foreach(string line in lines)
            {
                string[] parts = line.Split(" ");
                int steps = int.Parse(parts[1]);

                switch (parts[0])
                {
                    case "R":
                        right += steps;
                        break;
                    case "L":
                        left += steps;
                        break;
                    case "U":
                        up += steps;
                        break;
                    case "D":
                        down += steps;
                        break;
                }
            }

            bool[,] matrix = new bool[right + left, up + down];
            bool[,] matrix_knots = new bool[right + left, up + down];

            Tuple<int, int> head = new Tuple<int, int>((right + left) / 2, (up + down) / 2);

            Tuple<int, int> tail = new Tuple<int, int>((right + left) / 2, (up + down) / 2);

            List<Tuple<int, int>> knots = new List<Tuple<int, int>>();

            for (int i = 0; i < 10; i++) {
                knots.Add(new Tuple<int, int>((right + left) / 2, (up + down) / 2));
            }

            matrix[tail.Item1, tail.Item2] = true;
            matrix_knots[tail.Item1, tail.Item2] = true;

            foreach (string line in lines)
            {
                string[] parts = line.Split(" ");
                int steps = int.Parse(parts[1]);

                switch (parts[0])
                {
                    case "R":
                        for(int i = 0; i < steps; i++)
                        {
                            //head = new Tuple<int, int>(head.Item1 + 1, head.Item2);

                            //if (Math.Abs(head.Item1 - tail.Item1) <= 1 && Math.Abs(head.Item2 - tail.Item2) <= 1)
                            //    continue;

                            //if ((head.Item2 == tail.Item2))
                            //{
                            //    tail = new Tuple<int, int>(tail.Item1 + 1, tail.Item2);
                            //    matrix[tail.Item1, tail.Item2] = true;
                            //    continue;
                            //}

                            //tail = new Tuple<int, int>(tail.Item1 + 1, head.Item2);

                            //matrix[tail.Item1, tail.Item2] = true;

                            MakeMove(ref knots, ref matrix_knots,1,0);
                        }
                        break;
                    case "L":
                        for (int i = 0; i < steps; i++)
                        {
                            //head = new Tuple<int, int>(head.Item1 - 1, head.Item2);

                            //if (Math.Abs(head.Item1 - tail.Item1) <= 1 && Math.Abs(head.Item2 - tail.Item2) <= 1)
                            //    continue;

                            //if ((head.Item2 == tail.Item2))
                            //{
                            //    tail = new Tuple<int, int>(tail.Item1 - 1, tail.Item2);
                            //    matrix[tail.Item1, tail.Item2] = true;
                            //    continue;
                            //}

                            //tail = new Tuple<int, int>(tail.Item1 - 1, head.Item2);

                            //matrix[tail.Item1, tail.Item2] = true;

                            MakeMove(ref knots, ref matrix_knots,-1,0);
                        }
                        break;
                    case "U":
                        for (int i = 0; i < steps; i++)
                        {
                            //head = new Tuple<int, int>(head.Item1, head.Item2 - 1);

                            //if (Math.Abs(head.Item1 - tail.Item1) <= 1 && Math.Abs(head.Item2 - tail.Item2) <= 1)
                            //    continue;

                            //if ((head.Item1 == tail.Item1))
                            //{
                            //    tail = new Tuple<int, int>(tail.Item1, tail.Item2 - 1);
                            //    matrix[tail.Item1, tail.Item2] = true;
                            //    continue;
                            //}

                            //tail = new Tuple<int, int>(head.Item1, tail.Item2 - 1);

                            //matrix[tail.Item1, tail.Item2] = true;

                            MakeMove(ref knots, ref matrix_knots,0,-1);
                        }
                        break;
                    case "D":
                        for (int i = 0; i < steps; i++)
                        {
                            //head = new Tuple<int, int>(head.Item1, head.Item2 + 1);

                            //if (Math.Abs(head.Item1 - tail.Item1) <= 1 && Math.Abs(head.Item2 - tail.Item2) <= 1)
                            //    continue;

                            //if ((head.Item1 == tail.Item1))
                            //{
                            //    tail = new Tuple<int, int>(tail.Item1, tail.Item2 + 1);
                            //    matrix[tail.Item1, tail.Item2] = true;
                            //    continue;
                            //}

                            //tail = new Tuple<int, int>(head.Item1, tail.Item2 + 1);

                            //matrix[tail.Item1, tail.Item2] = true;

                            MakeMove(ref knots, ref matrix_knots,0,1);
                        }
                        break;
                }
            }

            int counter = 0;
            int counter_knot_9 = 0;

            foreach(bool field in matrix)
            {
                if (field)
                    counter++;
            }

            foreach(bool field in matrix_knots) 
            {
                if (field)
                    counter_knot_9++;
            }

            Console.WriteLine("Fields stepped on = " + counter);
            Console.WriteLine("Knot 9 step on  = " + counter_knot_9 + " fields.");
        }

        static void MakeMove(ref List<Tuple<int,int>> knots, ref bool[,] matrix_knots, int dir1, int dir2) 
        {
            knots[0] = new Tuple<int, int>(knots[0].Item1 + dir1, knots[0].Item2 + dir2);
            int direction1, direction2;

            for (int j = 1; j < knots.Count; j++)
            {
                if (Math.Abs(knots[j - 1].Item1 - knots[j].Item1) <= 1 && Math.Abs(knots[j - 1].Item2 - knots[j].Item2) <= 1)
                    continue;

                if ((knots[j - 1].Item1 == knots[j].Item1))
                {
                    if (knots[j - 1].Item2 > knots[j].Item2)
                        direction2 = 1;
                    else
                        direction2 = -1;

                    knots[j] = new Tuple<int, int>(knots[j].Item1, knots[j].Item2 + direction2);
                    if (j == knots.Count - 1)
                        matrix_knots[knots[j].Item1, knots[j].Item2] = true;

                    continue;
                }

                if (knots[j - 1].Item2 == knots[j].Item2)
                {
                    if (knots[j - 1].Item1 > knots[j].Item1)
                        direction1 = 1;
                    else
                        direction1 = -1;

                    knots[j] = new Tuple<int, int>(knots[j].Item1 + direction1, knots[j].Item2);

                    if (j == knots.Count - 1)
                        matrix_knots[knots[j].Item1, knots[j].Item2] = true;

                    continue;
                }

                if (knots[j - 1].Item1 > knots[j].Item1)
                    direction1 = 1;
                else
                    direction1 = -1;

                if (knots[j - 1].Item2 > knots[j].Item2)
                    direction2 = 1;
                else
                    direction2 = -1;
                knots[j] = new Tuple<int, int>(knots[j].Item1 + direction1, knots[j].Item2 + direction2);

                if (j == knots.Count - 1)
                    matrix_knots[knots[j].Item1, knots[j].Item2] = true;
            }
        }

        static void TreeHouse()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\treetop.txt");
            int[,] trees = new int[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    trees[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            int visible_trees_count = 0;
            List<int> scenic_scores = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    // border tree
                    if (i == 0 || j == 0 || i == lines[0].Length - 1 || j == lines.Length - 1)
                    {
                        visible_trees_count++;
                        continue;
                    }

                    int tree = trees[i, j];
                    List<int> row_left = GetRowLeft(trees, i, j);
                    List<int> row_right = GetRowRight(trees, i, j);

                    List<int> column_top = GetColumnTop(trees, j, i);
                    List<int> column_bottom = GetColumnBottom(trees, j, i);

                    if (row_left.All(e => e < tree) || row_right.All(e => e < tree) || column_bottom.All(e => e < tree) || column_top.All(e => e < tree))
                    {
                        visible_trees_count++;

                        int scenic_score = 1;
                        int score_counter = 0;

                        for (int k = row_left.Count - 1; k >= 0; k--)
                        {
                            if (row_left[k] < tree)
                                score_counter++;
                            else
                            {
                                score_counter++;
                                break;
                            }
                        }

                        scenic_score *= score_counter;
                        score_counter = 0;

                        for (int k = column_top.Count - 1; k >= 0; k--)
                        {
                            if (column_top[k] < tree)
                                score_counter++;
                            else
                            {
                                score_counter++;
                                break;
                            }
                        }

                        scenic_score *= score_counter;
                        score_counter = 0;

                        for (int k = 0; k < row_right.Count; k++)
                        {
                            if (row_right[k] < tree)
                                score_counter++;
                            else
                            {
                                score_counter++;
                                break;
                            }
                        }

                        scenic_score *= score_counter;
                        score_counter = 0;

                        for (int k = 0; k < column_bottom.Count; k++)
                        {
                            if (column_bottom[k] < tree)
                                score_counter++;
                            else
                            {
                                score_counter++;
                                break;
                            }
                        }

                        scenic_score *= score_counter;

                        scenic_scores.Add(scenic_score);
                    }
                }
            }

            Console.WriteLine("Visible trees = " + visible_trees_count);
            Console.WriteLine("Highest scenic score = " + scenic_scores.Max());
        }

        static List<int> GetColumnTop(int[,] matrix, int columnNumber, int rowNumber)
        {
            return Enumerable.Range(0, rowNumber)
                    .Select(x => matrix[x, columnNumber])
                    .ToList();
        }

        static List<int> GetColumnBottom(int[,] matrix, int columnNumber, int rowNumber)
        {
            return Enumerable.Range(rowNumber + 1, matrix.GetLength(0) - rowNumber - 1)
                    .Select(x => matrix[x, columnNumber])
                    .ToList();
        }

        static List<int> GetRowLeft(int[,] matrix, int rowNumber, int columnNumber)
        {
            return Enumerable.Range(0, columnNumber)
                    .Select(x => matrix[rowNumber, x])
                    .ToList();
        }

        static List<int> GetRowRight(int[,] matrix, int rowNumber, int columnNumber)
        {
            return Enumerable.Range(columnNumber + 1, matrix.GetLength(1) - columnNumber - 1)
                    .Select(x => matrix[rowNumber, x])
                    .ToList();
        }

        static void Space()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Marble-IT\Desktop\space.txt");

            SpaceElement current = new SpaceElement();
            current.name = "/";

            foreach (string line in lines)
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
                            foreach (SpaceElement child in current.children)
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

            for (int i = 0; i < lines.Length; i += 3)
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

                        if (item1 == item2 && item1 != 0)
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