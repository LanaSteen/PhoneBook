namespace PhoneBook
{
    internal class Program
    {
        private static Dictionary<int, Contact> contacts;
        private const string FilePath = @"../../../Files/Contacts.txt"; 
        private static ContactManager contactManager;

        static void Main(string[] args)
        {
            contacts = Contact.LoadContacts(FilePath); 
            contactManager = new ContactManager(contacts, FilePath); 

            while (true)
            {
                Console.WriteLine("Phonebook Menu:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Search by ID");
                Console.WriteLine("3. Delete by ID");
                Console.WriteLine("4. Exit");
                Console.WriteLine("5. Display All Contacts");
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        contactManager.AddContact();
                        break;
                    case "2":
                        contactManager.SearchContact();
                        break;
                    case "3":
                        contactManager.DeleteContact();
                        break;
                    case "4":
                        return;
                    case "5":
                        contactManager.DisplayAllContacts();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

}
