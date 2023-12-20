namespace AdventOfCode
{
    enum Pulse
    { 
        Low = 0,
        High = 1
    }

    class Module
    {
        private readonly string _id;

        public string Name { get; set; }
        public bool State { get; set; } = false;
        public Dictionary<string, Pulse> Inputs { get; set; }
        public List<string> Outputs { get; set; }

        public bool IsFlipFlop { get { return _id[0] == '%'; } }
        public bool IsConjunction { get { return _id[0] == '&'; } }

        public Module(string line)
        {
            var split = line.Split(' ');
            _id = split[0];
            Name = split[0].Trim('%', '&');
            Outputs = split[2..].Select(s => s.Trim(',')).ToList();

            if (IsConjunction)
                Inputs = new();
        }
    }

    public class Day20
    {
        static readonly Queue<(string, Pulse)> _queue = new();
        static readonly Dictionary<string, Module> _modules = new();

        static int _highPulses = 0;
        static int _lowPulses = 0;

        public static void Solve()
        {
            foreach(var line in File.ReadAllLines("input.txt"))
            {
                var module = new Module(line);
                _modules.Add(module.Name, module);
            }

            foreach(var module in _modules.Values.Where(m => m.IsFlipFlop))
            {
                foreach (var output in module.Outputs.Where(o => _modules[o].IsConjunction))
                {
                    _modules[output].Inputs.Add(module.Name, Pulse.Low);
                }
            }

            foreach (int i in Enumerable.Range(0, 1000))
            {
                _queue.Enqueue(("broadcaster", Pulse.Low));
                PressButton();
            }

            Console.WriteLine($"Low: {_lowPulses} High: {_highPulses}");
            Console.WriteLine($"Result: {_lowPulses * _highPulses}");
        }

        private static void PressButton()
        {
            // Button press
            _lowPulses++;

            do
            {
                (var name, var pulse) = _queue.Dequeue();

                var module = _modules[name];

                var tempQueue = new List<(string, Pulse)>();

                foreach (var output in module.Outputs)
                {
                    if (pulse == Pulse.Low)
                        _lowPulses++;
                    if (pulse == Pulse.High)
                        _highPulses++;

                    // output
                    if (!_modules.ContainsKey(output))
                        continue;

                    var receiver = _modules[output];

                    //Console.WriteLine($"{module.Name} -{pulse}-> {receiver.Name}");
                    
                    if (receiver.IsFlipFlop && pulse == Pulse.Low)
                    {
                        var nextPulse = receiver.State == false ? Pulse.High : Pulse.Low;
                        receiver.State = !receiver.State;
                        tempQueue.Add((receiver.Name, nextPulse));
                    }
                    else if (receiver.IsConjunction)
                    {
                        receiver.Inputs[module.Name] = pulse;
                        var nextPulse = receiver.Inputs.Values.All(p => p == Pulse.High) ? Pulse.Low : Pulse.High;
                        tempQueue.Add((receiver.Name, nextPulse));
                    }
                }

                tempQueue.ForEach(_queue.Enqueue);
            }
            while (_queue.Any());
        }
    }
}
