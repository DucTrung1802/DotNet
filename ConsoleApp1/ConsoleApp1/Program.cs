class Program
{
    public static void Main()
    {

        // Create a new dictionary  
        // of strings, with string keys. 
        Dictionary<string, string> my_dictionary =
           new Dictionary<string, string>();

        // Adding key/value pairs in my_dictionary  
        my_dictionary.Add("Australia", "Canberra");
        my_dictionary.Add("Belgium", "Brussels");
        my_dictionary.Add("Netherlands", "Amsterdam");
        my_dictionary.Add("China", "Beijing");
        my_dictionary.Add("Russia", "Moscow");
        my_dictionary.Add("India", "New Delhi");

        foreach (var item in my_dictionary.Keys)
        {
            Console.WriteLine(item);
        }
    }
}