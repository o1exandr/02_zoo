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

        abstract class Watcher : IWatch
        {
            public List<Animal> listAnimal = new List<Animal>();

            public string Name { get; set; }

            virtual public void Watch()
            {
                Random rand = new Random();
                Console.WriteLine("\n{0} watch by:", Name);
                foreach (var a in listAnimal)
                {
                    // рандомно виводимо дії тварин
                    // можна зробити на зчитування поточного часу (чи введення годин користувачем) і присвоювати дії згідно годин,
                    // наприклад о 17 всі їдять, о 18 гуляють, а о 19 вже сплять абощо
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

        class VideoCam : Watcher
        {
            public VideoCam(string name = "unknown webcam")
            {
                Name = name;
            }
        }

        class EmployeeZoo : Watcher
        {
            public EmployeeZoo(string name = "unknown employee")
            {
                Name = name;
            }

            public void FeedAnimal(Animal animal)
            {
                Console.WriteLine($"\n{Name} feed {animal.GetType().Name} {animal.Name} by {animal.KindEat}");
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
                new Lion("Mufasa", "Meat", 8),
                new Lion("Sarabi", "Meat", 6.5),
                new Wolf("Wolfy", "Meat", 4.5),
            };

            List<Watcher> watchers = new List<Watcher>
            {
                new VideoCam("VideoCam 1"),
                new EmployeeZoo("Employee Will Smith"),
                new VideoCam("VideoCam 2"),
                new VideoCam("VideoCam 3"),
            };

            void Generate()
            {
                // привязуємо усіх звірів до наглядачів
                int cnt = 0;
                while (cnt <= animals.Count - 1)
                {
                    for (int i = 0; i < watchers.Count(); i++)
                    {
                        watchers[i].AddAnimal(animals[cnt++]);
                        if (cnt >= animals.Count)
                            break;
                    }
                }

                // виводимо хто з наглядачів за ким із звірів дивиться, і що звірі роблять в даний момент
                foreach(var w in watchers)
                    w.Watch();

                // шукаємо першого наглядача працівника
                var worker = from s in watchers
                               where s is EmployeeZoo
                               select s;
                
                // якщо такий є, то вказуємо йому нагодувати рандомну тваринку
                if (worker.FirstOrDefault() != null)
                {
                    Random rand = new Random();
                    ((EmployeeZoo)worker.FirstOrDefault()).FeedAnimal(animals[rand.Next(0, animals.Count())]);
                }
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
