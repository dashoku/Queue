using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private class Person
        {
            public int floor;
            public bool priority;
        }

        private class Lift
        {
            public int number = 0;
            public int currentFloor = 0;
            public bool isMovingUp = true;
            public bool isMovingDown = true;
            public bool isFull = true;
            public bool isEmpty = true;
            public int timeInOperation = 0;
            public int numPersons = 0;
            public TimeSpan WorkingTime;
            public void Clear()
            {
                currentFloor = 1;
                isMovingUp = false;
                isMovingDown = false;
                isFull = false;
                isEmpty = true;
                timeInOperation = 0;
                numPersons = 0;
            }
        }
        Queue<Person> Persons = new Queue<Person>();
        Queue<Person> Persons_Extra = new Queue<Person>();
        int CountOfPeople = 0;
        int MaxFloor(Queue<Person> FPersons)
        {
            int[] arr_maxfloor = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (FPersons.Count != 0)
                {
                    Person pp = FPersons.Dequeue();
                    arr_maxfloor[i] = pp.floor;
                    CountOfPeople++;
                }
                else
                {
                    arr_maxfloor[i] = 0;
                }
            }
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                if (arr_maxfloor[i] > max)
                    max = arr_maxfloor[i];
            }
            return max;
        }

        private void LiftOutput(Lift Out)
        {
            listBox1.Items.Add("Lift " + Out.number + ":");
            listBox1.Items.Add("Time in operation: " + Out.WorkingTime.ToString(@"hh\:mm\:ss"));
            listBox1.Items.Add("Count of people:" + Out.numPersons);
        }
        
        private int numSpecial;
        private int numRegular;
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            numSpecial = int.Parse(txtNumSpecial.Text);
            numRegular = int.Parse(txtNumRegular.Text);
            Persons.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            Random random = new Random();

            for (int i = 0; i < numSpecial; i++)
            {
                Person p = new Person();
                Persons.Enqueue(new Person() { floor = random.Next(1, 45), priority = true });
            }

            for (int i = 0; i < numRegular; i++)
            {
                Person p = new Person();
                Persons.Enqueue(new Person() { floor = random.Next(1, 45), priority = false});
            }

            foreach (Person person in Persons)
            {
                listBox2.Items.Add("Person: Floor - " + person.floor + " Priority - " + person.priority);
            }

            Persons_Extra = new Queue<Person>(Persons.Reverse());
            Lift Lift_1 = new Lift();
            Lift_1.number = 1;
            
            Persons_Extra = new Queue<Person>(Persons.Reverse());
            Lift Lift_2 = new Lift();
            Lift_2.number = 2;
            
            Persons_Extra = new Queue<Person>(Persons.Reverse());
            Lift Lift_3 = new Lift();
            Lift_3.number = 3;
            
            Persons_Extra = new Queue<Person>(Persons.Reverse());
            Lift Lift_4 = new Lift();
            Lift_4.number = 4;

            Persons_Extra = new Queue<Person>(Persons.Reverse());
            Lift Lift_All = new Lift();
            Lift_All.number = 5;

            Lift_1.Clear();
            Lift_2.Clear();
            Lift_3.Clear();
            Lift_4.Clear();

            CountOfPeople = 0;
            
            for (int i = 0; i < Persons_Extra.Count;)
            {
                if (Lift_1.isEmpty != false)
                {
                    int fmax = MaxFloor(Persons_Extra);
                    Lift_1.timeInOperation += fmax * 60 - fmax * 30;
                    Lift_1.numPersons += CountOfPeople;
                    CountOfPeople = 0;
                    Lift_1.WorkingTime = TimeSpan.FromSeconds(Lift_1.timeInOperation);
                    if (Lift_1.timeInOperation > 86400)
                    {
                        Lift_1.isEmpty = false;
                    }
                }
                LiftOutput(Lift_1);
                if (Lift_2.isEmpty != false)
                {
                    int fmax = MaxFloor(Persons_Extra);
                    Lift_2.timeInOperation += fmax * 60 - fmax * 30;
                    Lift_2.numPersons += CountOfPeople;
                    CountOfPeople = 0;
                    Lift_2.WorkingTime = TimeSpan.FromSeconds(Lift_2.timeInOperation);
                    if (Lift_2.timeInOperation > 86400)
                    {
                        Lift_2.isEmpty = false;
                    }
                }
                LiftOutput(Lift_2);
                if (Lift_3.isEmpty != false)
                {
                    int fmax = MaxFloor(Persons_Extra);
                    Lift_3.timeInOperation += fmax * 60 - fmax * 30;
                    Lift_3.numPersons += CountOfPeople;
                    CountOfPeople = 0;
                    Lift_3.WorkingTime = TimeSpan.FromSeconds(Lift_3.timeInOperation);
                    if (Lift_3.timeInOperation > 86400)
                    {
                        Lift_3.isEmpty = false;
                    }
                }
                LiftOutput(Lift_3);
                if (Lift_4.isEmpty != false)
                {
                    int fmax = MaxFloor(Persons_Extra);
                    Lift_4.timeInOperation += fmax * 60 - fmax * 30;
                    Lift_4.numPersons += CountOfPeople;
                    CountOfPeople = 0;
                    Lift_4.WorkingTime = TimeSpan.FromSeconds(Lift_4.timeInOperation);
                    if (Lift_4.WorkingTime.Hours > 11 && Lift_4.WorkingTime.Minutes > 0)
                    {
                        if (Persons_Extra.Peek().priority == true)
                        {
                            while (Persons_Extra.Peek().priority == true && Lift_1.timeInOperation < 86400)
                            {
                                fmax = MaxFloor(Persons_Extra);
                                Lift_4.timeInOperation += fmax * 60 - fmax * 30;
                                Lift_4.numPersons += CountOfPeople;
                                Lift_4.WorkingTime = TimeSpan.FromSeconds(Lift_4.timeInOperation);
                            }
                            Lift_4.isEmpty = false;
                        }
                        else
                            Lift_4.isEmpty = false;
                    }
                }
                LiftOutput(Lift_4);
            }
            Lift_All.timeInOperation = Lift_1.timeInOperation + Lift_2.timeInOperation + Lift_3.timeInOperation + Lift_4.timeInOperation;
            Lift_All.WorkingTime = TimeSpan.FromSeconds(Lift_All.timeInOperation);
            Lift_All.numPersons = Lift_1.numPersons + Lift_2.numPersons + Lift_3.numPersons + Lift_4.numPersons;
            listBox1.Items.Add("Lift_Info:");
            LiftOutput(Lift_All);

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }