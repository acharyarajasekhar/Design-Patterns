using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class CommandPattern
    {
        public static void ShowDemo()
        {
            Tv objTv = new Tv();

            int volume = -1;
            int channel = 50;

            Predicate<int> isPositiveValue =
            ip =>
            {
                bool isNegetive = ip < 0;
                if (isNegetive)
                    Console.WriteLine("Invalid input " + ip);
                return !isNegetive;
            };

            var volumeDownCommand = new Command<int>(isPositiveValue, objTv.SetVolume);
            if (volumeDownCommand.CanExecute(volume))
                volumeDownCommand.Execute(volume);

            var channelUpCommand = new Command<int>(isPositiveValue, objTv.SetChannel);
            if (channelUpCommand.CanExecute(channel))
                channelUpCommand.Execute(channel);
        }
    }

    class Tv
    {
        private int _channel;
        private int _volume;

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine("Channel set to " + _channel);
        }

        public void SetVolume(int volume)
        {
            _volume = volume;
            Console.WriteLine("Volume set to " + _volume);
        }
    }

    interface ICommand<Trequest>
    {
        bool CanExecute(Trequest req);
        void Execute(Trequest req);
    }

    class Command<TRequest> : ICommand<TRequest>
    {
        private Action<TRequest> _execute;
        private Predicate<TRequest> _canExecute;

        public Command(Predicate<TRequest> canExec, Action<TRequest> exec)
        {
            _canExecute = canExec;
            _execute = exec;
        }

        public bool CanExecute(TRequest req)
        {
            return _canExecute(req);
        }

        public void Execute(TRequest req)
        {
            _execute(req);
        }
    }
}