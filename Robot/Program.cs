using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Robot
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot();
            Invoker invoker = new Invoker();
            MoveForwardCommand command = new MoveForwardCommand(robot, 5);
            invoker.SetCommand(command);
            invoker.SetCommand(new RotateCommand(robot, 30));
            invoker.SetCommand(new HandUpCommand(robot, 10));
            invoker.Run();
            invoker.Cancel(5);
            Read();
        }
    }
    class Robot
    {
        public void MoveForward(int step)
        {
            if (step > 0)
            {
                WriteLine($"Робот сделал {step} шагов вперед");
            }
            else
            {
                WriteLine($"Робот сделал {-1 * step} шагов назад");
            }
        }

        public void Rotate(int degree)
        {
            if (degree > 0)
            {
                WriteLine($"Робот движется на заданый  угол {degree} градусов вправо");
            }
            else
            {
                WriteLine($"Робот движется на заданый угол {-1 * degree} градусов влево");
            }
        }

        public void HandUp(int cm)
        {
            if (cm > 0)
            {
                WriteLine($"Робот поднимает руку {cm} см вверх");
            }
            else
            {
                WriteLine($"Робот поднимает руку  {-1 * cm} см вниз");
            }
        }
    }

    interface ICommand
    {
        void Execute();
        void Undo();
    }
    class MoveForwardCommand : ICommand
    {
        Robot receiver;
        private int step;
        public MoveForwardCommand (Robot robot, int step)
        {
            this.receiver = robot;
            this.step = step;
        }
        public void Execute()
        {
            receiver.MoveForward(step);
        }
        public void Undo()
        {
            receiver.MoveForward(-1 * step);
        }
    }
    class RotateCommand : ICommand
    {
        Robot receiver;
        private int degree;
        public RotateCommand(Robot robot, int degree)
        {
            this.receiver = robot;
            this.degree = degree;
        }
        public void Execute()
        {
            receiver.Rotate(degree);
        }

        public void Undo()
        {
            receiver.Rotate(-1 * degree);
        }
    }
    class HandUpCommand : ICommand
    {
        Robot receiver;
        int cm;
        public HandUpCommand(Robot robot, int cm)
        {
            this.receiver = robot;
            this.cm = cm;
        }
        public void Execute()
        {
            receiver.HandUp(cm);
        }

        public void Undo()
        {
            receiver.HandUp(-1 * cm);
        }
    }

    class Invoker
    {
        Queue<ICommand> commandQueue = new Queue<ICommand>();
        Stack<ICommand> commandStack = new Stack<ICommand>();
        public void SetCommand(ICommand command)
        {

            commandQueue.Enqueue(command);

        }
        public void Run()
        {
            while (commandQueue.Count > 0)
            {
                ICommand command = commandQueue.Dequeue();
                command.Execute();
                commandStack.Push(command);
            }
        }

        public void Cancel(int number)
        {
            while(commandStack.Count>0 && number>0 )
            {
                ICommand command = commandStack.Pop();               
                command.Undo();
                number--;
            }
        }
    }

    }

