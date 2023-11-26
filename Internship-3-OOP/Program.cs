using Internship_3_OOP.enums;
using Internship_3_OOP.Telefonski_imenik;
using System.Collections.Generic;
using System.Text.RegularExpressions;

Dictionary<Contact, List<PhoneCall>> phoneBook = new Dictionary<Contact, List<PhoneCall>>();
bool status = true;
while(status == true)
{
    Menu();
    Console.WriteLine("Vas odabir: ");
    string choice = Console.ReadLine();
    switch(choice)
    {
        case "1":
            printPhoneBook(phoneBook);
            break;
        case "2":
            addNewContact(phoneBook);
            break;
        case "3":
            deleteContact(phoneBook);
            break;
        case "4":
            updateContactPreference(phoneBook);
            break;
        case "5":
            manageContact(phoneBook);
            break;
        case "6":
            displayAllCalls(phoneBook);
            break;
        case "7":
            status = false;
            break;
        default:
            Console.WriteLine("Izaberite jednu od ponudjenih opcija.");
            break;
    }
}

static void Menu()
{
    Console.WriteLine("------ TELEFONSKI IMENIK ------");
    Console.WriteLine("1. Ispis svih kontakata");
    Console.WriteLine("2. Dodavanje novih kontakata");
    Console.WriteLine("3. Brisanje kontakata");
    Console.WriteLine("4. Editiranje preference kontakta");
    Console.WriteLine("5. Upravljanje kontaktom");
    Console.WriteLine("6. Ispis svih poziva");
    Console.WriteLine("7. Izlaz");

    Console.Write("Odaberi opciju (1-6): ");
}
static void addPhoneCall(Contact contact, PhoneCall phoneCall, Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    if(!phoneBook.ContainsKey(contact))
        phoneBook[contact] = new List<PhoneCall>();
    phoneBook[contact].Add(phoneCall);
}

//Menu funkcije

static void printPhoneBook(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    if(phoneBook.Count == 0)
    {
        Console.WriteLine("Imenik je prazan");
        return;
    }

    foreach(var contact in phoneBook)
    {
        Console.WriteLine($"Kontakt: {contact.Key.FirstName} {contact.Key.LastName}, Broj: {contact.Key.PhoneNumber}, Preferenca: {contact.Key.Preference}");
        foreach(var call in contact.Value)
        {
            Console.WriteLine($"Poziv: Vrijeme - {call.TimeOfCall}, Status - {call.CallStatus}");
        }
        Console.WriteLine();
    }
}

static void addNewContact(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    Console.WriteLine("Unesite ime: ");
    string firstName = getString();
    Console.WriteLine("Unesite prezime: ");
    string lastName = getString();
    Console.WriteLine("Unesite broj mobitela: ");
    string phoneNumber = Console.ReadLine();
    if (phoneBook.Keys.Any(c => c.PhoneNumber == phoneNumber))
    {
        Console.WriteLine("Greska: Kontakt s ovim brojem vec postoji.");
        return;
    }
    Console.Write("Unesite preferencu (Favorite, Regular, Blocked): ");
    string preferenceString = Console.ReadLine();
    Enum.TryParse(preferenceString, out Preference preference);

    var newContact = new Contact(firstName, lastName, phoneNumber, preference);

    if (!phoneBook.ContainsKey(newContact))
    {
        phoneBook[newContact] = new List<PhoneCall>();
        Console.WriteLine("Novi kontakt dodan u imenik.");
    }
    else
    {
        Console.WriteLine("Kontakt već postoji u imeniku.");
    }
}

static void deleteContact(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    Console.WriteLine("Unesite ime i prezime kontakta kojeg zelite obrisati: ");
    string firstName = getString();
    string lastName = getString();

    Contact contactToDelete = null;

    foreach(var contact in phoneBook.Keys)
    {
        if(contact.FirstName.Equals(firstName) && contact.LastName.Equals(lastName))
        {
            contactToDelete = contact;
            break;
        }
    }

    if (contactToDelete != null)
    {
        phoneBook.Remove(contactToDelete);
        Console.WriteLine($"Kontakt {firstName} {lastName} obrisan iz imenika.");
    }
    else
    {
        Console.WriteLine("Kontakt ne postoji u imeniku.");
    }
}

static void updateContactPreference(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    Console.WriteLine("Unesite ime kontakta ciju preferencu zelite mijenjati: ");
    string firstName = getString();
    Console.WriteLine("Unesite prezime kontakta ciju preferencu zelite mijenjati: ");
    string lastName = getString();
    Contact contactToUpdate = null;
    foreach (var contact in phoneBook.Keys)
    {
        if (contact.FirstName.Equals(firstName) && contact.LastName.Equals(lastName))
        {
            contactToUpdate = contact;
            break;
        }
    }
    if (contactToUpdate != null)
    {
        Console.Write("Unesite novu preferencu (Favorite, Regular, Blocked): ");
        string newPreferenceString = Console.ReadLine();
        Enum.TryParse(newPreferenceString, out Preference newPreference);

        contactToUpdate.Preference = newPreference;
        Console.WriteLine($"Preferenca za kontakt {firstName} {lastName} editirana.");
    }
    else
    {
        Console.WriteLine("Kontakt ne postoji u imeniku.");
    }
}

static void manageContact(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    Console.WriteLine("Unesite ime kontakta za upravljanje: ");
    string firstName = getString();
    Console.WriteLine("Unesite prezime kontakta za upravljanje: ");
    string lastName = getString();
    Contact contactToManage = null;
    foreach(var contact in phoneBook.Keys)
    {
        if(contact.FirstName.Equals(firstName) && contact.LastName.Equals(lastName))
        {
            contactToManage = contact;
            break;
        }
    }
    bool status = true;
    while(status)
    {
        Submenu();
            Console.WriteLine("Vas odabir: ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    printAllCalls(contactToManage);
                    break;
                case "2":
                    createNewCall(contactToManage, phoneBook);
                    break;
                case "3":
                    status = false;
                    break;
                default:
                    Console.WriteLine("Nije jedna od opcija. Pokusajte ponovno.");
                    break;
            }
    }
    
}


static void displayAllCalls(Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    if (phoneBook.Count == 0)
    {
        Console.WriteLine("Telefonski imenik je prazan.");
        return;
    }

    foreach (var contact in phoneBook.Keys)
    {
        List<PhoneCall> calls = phoneBook[contact];

        Console.WriteLine($"Pozivi za {contact.FirstName} {contact.LastName} ({contact.PhoneNumber}):");

        if (calls.Count == 0)
        {
            Console.WriteLine("Nema uspostavljenih poziva.");
        }
        else
        {
            foreach (var call in calls)
            {
                Console.WriteLine($"Vrijeme uspostave: {call.TimeOfCall}, Status: {call.CallStatus}, Trajanje: {call.Duration} sekundi");
            }
        }
        Console.WriteLine();
    }
}
//Submenu

static void Submenu()
{
    Console.WriteLine("1 - Ispis svih poziva, poredan od najnovijeg prema najstarijem");
    Console.WriteLine("2 - Kreiranje novog poziva");
    Console.WriteLine("3 - Izlaz iz podmenua");
}

static void printAllCalls(Contact contactToManage)
{
    if(contactToManage.PhoneCalls.Count == 0)
    {
        Console.WriteLine("Nema uspostavljenih poziva s ovim kontaktom");
        return;
    }
    var sortedCalls = contactToManage.PhoneCalls.OrderByDescending(c => c.TimeOfCall);
    foreach (var phoneCall in sortedCalls)
    {
        Console.WriteLine($"Vrijeme uspostave poziva: {phoneCall.TimeOfCall}, Status: {phoneCall.CallStatus}");
    }

}

static void createNewCall(Contact contactToManage, Dictionary<Contact, List<PhoneCall>> phoneBook)
{
    if (contactToManage.Preference == Preference.Blocked)
    {
        Console.WriteLine("Greska: Nemoguca uspostava poziva sa blokiranim kontaktom.");
        return;
    }

    Console.Write("Unesite status poziva (Ongoing, Missed, Ended): ");
    if (Enum.TryParse(Console.ReadLine(), out CallStatus status))
    {
        Random random = new Random();
        int duration = random.Next(1, 21);

        PhoneCall newPhoneCall = new PhoneCall(DateTime.Now, status, duration);
        contactToManage.AddCall(newPhoneCall);

        Console.WriteLine("Novi poziv je uspjesno dodan.");

        
        var ongoingCalls = contactToManage.PhoneCalls.Where(c => c.CallStatus == CallStatus.Ongoing).ToList();

        if (ongoingCalls.Any())
        {
            
            PhoneCall latestOngoingCall = ongoingCalls.OrderByDescending(c => c.TimeOfCall).First();

            CallStatus responseStatus = GetRandomResponseStatus();
            int responseDuration = random.Next(1, 21);

            PhoneCall responseCall = new PhoneCall(DateTime.Now, responseStatus, responseDuration);
            contactToManage.AddCall(responseCall);

            Console.WriteLine($"Odgovoreno na dolazeci poziv sa statusom: {responseStatus} i vremenom trajanja: {responseDuration} sekundi.");
            phoneBook[contactToManage] = contactToManage.PhoneCalls;
        }
    }
    else
    {
        Console.WriteLine("Nepostojeci status poziva.");
    }
}




static CallStatus GetRandomResponseStatus()
{
    Random random = new Random();
    Array values = Enum.GetValues(typeof(CallStatus));
    return (CallStatus)values.GetValue(random.Next(values.Length));
}
// Provjera

static string getString()
{

    while (true)
    {
        string input = Console.ReadLine();
        if (checkIfString(input))
            return input;
        else
        {
            Console.WriteLine("Pogresno unesen string. Pokusajte ponovno.");
        }
    }
}

static string getPhoneNumber()
{
    while(true)
    {
        string input = Console.ReadLine();
        if (isPhoneNumber(input))
            return input;
        else
        {
            Console.WriteLine("Pogresno unesen broj mobitela. Unesite 10 znamenki: ");
        }
    }
}


static bool checkIfString(string value)
{
    foreach (char c in value)
    {
        if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            return false;
    }

    return true;
}

static bool isPhoneNumber(string phoneNumber)
{
    // standardni hr broj od 10 znamenki
    return phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
}