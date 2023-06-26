while (true)
{
    Console.WriteLine("Enter a Reverse Polish Notation expression (or 'q' to quit):");
    string input = Console.ReadLine();

    // If the user enters 'q', break out of the loop
    if (input.ToLower() == "q")
    {
        break;
    }

    string[] tokens = input.Split(' ');

    Stack<double> stack = new Stack<double>();
    Queue<string> queue = new Queue<string>(tokens);

    while (queue.Count > 0)
    {
        string token = queue.Dequeue();

        if (double.TryParse(token, out double number))
        {
            stack.Push(number);
        }
        else if (token == "+" || token == "-" || token == "*" || token == "/")
        {
            if (stack.Count < 2)
            {
                Console.WriteLine("Error: Missing value.");
                continue;
            }

            double right = stack.Pop();
            double left = stack.Pop();
            double result;

            switch (token)
            {
                case "+":
                    result = left + right;
                    break;
                case "-":
                    result = left - right;
                    break;
                case "*":
                    result = left * right;
                    break;
                case "/":
                    if (right == 0)
                    {
                        Console.WriteLine("Error: Division by zero.");
                        continue;
                    }
                    result = left / right;
                    break;
                default:
                    Console.WriteLine("Error: Unknown operator.");
                    continue;
            }

            stack.Push(result);
        }
        else if (token == "=")
        {
            if (stack.Count != 1)
            {
                Console.WriteLine("Error: Ambiguous result.");
                continue;
            }

            Console.WriteLine($"Result: {stack.Pop()}");
            continue;
        }
        else
        {
            Console.WriteLine("Error: Unknown token.");
            continue;
        }
    }

    if (queue.Count == 0 && stack.Count != 1)
    {
        Console.WriteLine("Error: Missing '='.");
    }
}