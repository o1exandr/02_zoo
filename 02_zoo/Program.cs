/*
 Завдання 2.
Визначити інтерфейси IWalk, IEat, ISleep. Реалізувати ці інтерфейси для класів різних тварин зоопарку(Bear, Parrot, …). 
Визначити  інтерфейс IWatch(спостерігати), який реалізувати для класів Відеокамера та класу  Працівника _Зоопарку. 
Створити клас Zoo, що міститиме  масиви(колекції List<>, ArrayList) тварин, робітників, відеокамер. 
Змоделювати роботу зоопарку:
•	Тварини можуть їсти, прогулюватися, тощо.
•	За цим слідкують відеокамери, та можуть слідкувати працівники.
•	Працівники можуть годувати тварин.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _02_zoo
{
    class Program
    {
        interface IEat
        {
            string KindEat { get; set; }
            double Kg { get; set; }
            void Eat();
        }

        interface ISleep
        {
            void Sleep();
        }

        interface IWolk
        {
            void Walk();
        }

        abstract class Animal : IEat, ISleep, IWolk
        {
            public string Name { get; set; }
            public string KindEat { get; set; }
            public double Kg { get; set; }

            public Animal(string name = "unknown animal", string kind = "some", double kg = 1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }

            virtual public void Eat()
            {
                Console.WriteLine("{0} {1} eat {2} - {3} kg", this.GetType().Name, Name, KindEat, Kg);
            }

            virtual public void Sleep()
            {
                Console.WriteLine("{0} {1} sleep", this.GetType().Name, Name);
            }

            virtual public void Walk()
            {
                Console.WriteLine("{0} {1} walk", this.GetType().Name, Name);
            }

            public override string ToString()
            {
                return $"{this.GetType().Name} {Name}";
            }
        }

        class Wolf : Animal
        {
            public Wolf(string name = "unknown wolf", string kind = "some", double kg = 1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }
        }

        class Bear : Animal
        {
            public Bear(string name = "unknown bear", string kind = "some", double kg = 1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }
        }

        class Parrot : Animal
        {
            public Parrot(string name = "unknown parrot", string kind = "some", double kg = 0.1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }
        }

        class Lion : Animal
        {
            public Lion(string name = "unknown lion", string kind = "some", double kg = 1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }
        }

        class Fox : Animal
        {
            public Fox(string name = "unknown fox", string kind = "some", double kg = 1)
            {
                Name = name;
                KindEat = kind;
                Kg = kg;
            }
        }

        interface IWatch
        {
            string Name { get; set; }
 
            void Watch();
            void AddAnimal(Animal animal);
        }

        abstract class Watchers : IWatch
        {
            public List<Animal> listAnimal = new List<Animal>();

            

            public string Name { get; set; }


            virtual public void Watch()
            {
                Random rand = new Random();
                Console.WriteLine("\n{0} watch by:", Name);
                foreach (var a in listAnimal)
                {
                    Thread.Sleep(100);
                    Console.Write("\t");
                    switch (rand.Next(1, 4))
                    {
                        case 1:
                            a.Eat();
                            break;
                        case 2:
                            a.Walk();
                            break;
                        case 3:
                            a.Sleep();
                            break;
                    }
                }
            }

            virtual public void AddAnimal(Animal animal)
            {
                listAnimal.Add(animal);
            }
        }

        class VideoCam : Watchers
        {
            public VideoCam(string name = "unknown webcam")
            {
                Name = name;
            }


        }

        class EmployeeZoo : Watchers
        {
            public EmployeeZoo(string name = "unknown employee")
            {
                Name = name;
            }

            public void FeedAnimal(Animal animal)
            {
                Console.WriteLine($"{Name} feed {animal.GetType()} {animal.Name} by {animal.KindEat}");
                animal.Eat();
            }

        }

        class Zoo
        {
            List<Animal> animals = new List<Animal>
            {
                new Wolf("Jack", "Meat", 4),
                new Bear("Teddy", "Honey", 3),
                new Parrot("Kesha", "Seed", 0.15),
                new Parrot("Gesha"),
                new Fox("Foxy", "Chiсken", 1.5),
                new Lion("Simba", "Meat", 7),
            };

            List<IWatch> watchers = new List<IWatch>
            {
                new VideoCam("VideoCam 1"),
                new VideoCam("VideoCam 2"),
                new EmployeeZoo("employee Will Smith")
            };

            void Generate()
            {
                for(int i = 0; i < watchers.Count(); i++)
                    for (int j = 0; j < (animals.Count() / watchers.Count()); j++)
                        watchers[i].AddAnimal(animals[j + i * animals.Count() / watchers.Count()]);
                foreach(var w in watchers)
                    w.Watch();

                var worker = from s in watchers
                               where s is EmployeeZoo
                               select s;
                //((EmployeeZoo)watchers[2]).FeedAnimal(animals[5]);
                //Console.WriteLine(worker.FirstOrDefault());
                //((EmployeeZoo)worker.FirstOrDefault()).FeedAnimal(animals[5]);
            }

            public void Show()
            {
                Generate();
            }
        }

        static void Main(string[] args)
        {
           
            Zoo z = new Zoo();
            z.Show();
               
            Console.ReadKey();
        }
    }
}
