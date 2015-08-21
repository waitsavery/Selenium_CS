using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Orasi.Utilities
{
    /// <summary>
    ///     Contains members and methods to interact with the properties file
    /// </summary>
    public class Properties
    {
        //Define a dictionary to hold all property files
        private Dictionary<string, string> list;
        //Property file location
        private string filename = @"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CSharp\mySelenium\Orasi.Utilities\Credentials_URL.properties";

        /// <summary>
        ///     Constructor that loads a predefined properties file
        /// </summary>
        public Properties()
        {
            reload();
        }

        /// <summary>
        ///     Overloaded constructor that loads a user-defined file
        /// </summary>
        /// <param name="file">Fully qualified path to the properties file</param>
        public Properties(string file)
        {
            this.filename = file;
            reload(this.filename);
        }

        /// <summary>
        ///     Retrieves a dictionary value for a given field.
        ///     Able to handle multiple fields with the same name by passing a definition value
        /// </summary>
        /// <param name="field">Property file field name</param>
        /// <param name="defValue">Property file definition number; handles multiple definitions with the same field</param>
        /// <returns>Desired property</returns>
        public string get(string field, string defValue)
        {
            return (get(field) == null) ? (defValue) : (get(field));
        }
        /// <summary>
        ///     If the dictionary list contains the field, then the desired value is returned
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Desired property</returns>
        public string get(string field)
        {
            return (list.ContainsKey(field)) ? (list[field]) : (null);
        }

        /// <summary>
        ///     Adds items to a dictionary list
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void set(string field, Object value)
        {
            //If the field does not exist in the dictionary, then add it.
            //Else redefine the value in the dictionary for the given field
            if (!list.ContainsKey(field))
                list.Add(field, value.ToString());
            else
                list[field] = value.ToString();
        }

        /// <summary>
        ///     Saves the properties file for the given filename
        /// </summary>
        public void Save()
        {
            Save(this.filename);
        }

        /// <summary>
        ///     Determines if the property file exists.
        ///     If not, it creates the property file.
        ///     The dictionary list is written to the file and then the file stream is closed, thereby saving the property file.
        /// </summary>
        /// <param name="filename">Name of the file to save</param>
        public void Save(string filename)
        {
            this.filename = filename;

            //If the file doesnt exist, create it
            if (!System.IO.File.Exists(filename))
                System.IO.File.Create(filename);

            //Create a new StreamWriter
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);

            //Iterate through each key/value pair in the dictionary and write them to the properties file
            foreach (string prop in list.Keys.ToArray())
                if (!string.IsNullOrWhiteSpace(list[prop]))
                    file.WriteLine(prop + "=" + list[prop]);

            //Close the file stream
            file.Close();
        }

        /// <summary>
        ///     Loads the property file
        /// </summary>
        public void reload()
        {
            reload(this.filename);
        }

        /// <summary>
        ///     Creates a dictionary list and invokes a method to readin the file
        /// </summary>
        /// <param name="filename">Property file to load</param>
        public void reload(string filename)
        {
            this.filename = filename;
            //Instantiate a dictionary object with string key/value pairs
            list = new Dictionary<string, string>();

            //If the file exists then load the file, else throw an exception
            if (System.IO.File.Exists(filename))
                loadFromFile(filename);
            else
                throw new FileNotFoundException("The file [{0}] was not found to exist.", this.filename);
        }

        /// <summary>
        ///     Reads each line from the property file and stores it in a local list
        /// </summary>
        /// <param name="file">File from which to read</param>
        private void loadFromFile(string file)
        {
            //Iterate through each line of the property file
            foreach (string line in System.IO.File.ReadAllLines(file))
            {
                //Ignore lines starting with certain special characters
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    //For each line, find the index of the equals mark
                    int index = line.IndexOf('=');
                    //For each line, carve out the key
                    string key = line.Substring(0, index).Trim();
                    //For each line, carve out the value
                    string value = line.Substring(index + 1).Trim();

                    //If the value starts and ends with certain special characters, 
                    //then trim off the special charaters
                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    //Add the key/value pair to the dictionary
                    try
                    {
                        //ignore dublicates
                        list.Add(key, value);
                    }
                    catch { }
                }
            }
        }
    }
}
