using ClassesManager.Services;
using ClassesManager.UIModels;

namespace ClassesManager.ConsoleApp
{
    internal class Program
    {
        static void ShowSubjectDetails(SubjectUIModel subject, StorageService storageService)
        {
            bool inSubMenu = true;
            while (inSubMenu)
            {
                Console.Clear();
                Console.WriteLine("Detailed info about subject: ");
                Console.WriteLine($"\n Title: {subject.Name}");
                Console.WriteLine($"\n ECTS: {subject.ECTS}");
                Console.WriteLine($"\n Field of knowledge: {subject.FieldOfKnowledge}");

                if (subject.Classes.Count == 0 && subject.Id.HasValue)
                {
                    var classesDBs = storageService.GetAllClasses(subject.Id.Value);
                    foreach ( var classDB in classesDBs )
                    {
                        subject.AddClass(new ClassesUIModel(classDB));
                    }
                }

                Console.WriteLine($"\n Duration of all classes: {subject.AllClassesDuration}");
                Console.WriteLine("\n Classes list");

                if (subject.Classes.Count == 0)
                {
                    Console.WriteLine("No classes for this sunject");
                }
                else
                {
                    for (int i = 0; i < subject.Classes.Count; i++)
                    {
                        var c = subject.Classes[i];
                        Console.WriteLine($"{i + 1}. {c.Date} - {c.ThemeOfClass} ({c.TypeOfClass})");
                    }
                }

                Console.WriteLine("\n0. Return to list");
                Console.Write("Choose number to see the details or choose 0 to return: ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    inSubMenu = false;
                    continue;
                }

                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= subject.Classes.Count)
                {
                    ShowClassDetails(subject.Classes[selectedIndex - 1]);
                }
                else
                {
                    Console.WriteLine("Wrong input, press any button");
                    Console.ReadKey();
                }
            }
        }

        static void ShowClassDetails(ClassesUIModel classModel)
        {
            Console.Clear();
            Console.WriteLine("\nClass Details");
            Console.WriteLine($"\nTheme: {classModel.ThemeOfClass}");
            Console.WriteLine($"\nType of class: {classModel.TypeOfClass}");
            Console.WriteLine($"\nDate: {classModel.Date}");
            Console.WriteLine($"\nTime: {classModel.StartTime} - {classModel.EndTime}");
            Console.WriteLine($"\nDuration: {classModel.ClassDuration} minutes");

            Console.WriteLine("\nPress any button to return");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            StorageService storageService = new StorageService();

            List<SubjectUIModel> subjectUIModels = new List<SubjectUIModel>();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("\nClasses Manager");

                if (subjectUIModels.Count == 0)
                {
                    var subjectDBs = storageService.GetAllSubjects();
                    foreach (var dbModel in subjectDBs)
                    {
                        subjectUIModels.Add(new SubjectUIModel(dbModel));
                    }
                }

                Console.WriteLine("Available subjects:");
                for (int i = 0; i < subjectUIModels.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {subjectUIModels[i].Name} (ECTS: {subjectUIModels[i].ECTS})");
                }
                Console.WriteLine("0. Return");
                Console.Write("\nChoose subject or 0 to return: ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    isRunning = false;
                    continue;
                }

                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= subjectUIModels.Count)
                {
                    var selectedSubject = subjectUIModels[selectedIndex - 1];
                    ShowSubjectDetails(selectedSubject, storageService);
                }
                else
                {
                    Console.WriteLine("Wrong input, press any button");
                    Console.ReadKey();
                }
            }
        }
    }
}
