using NUnit.Framework;
using OpenQA.Selenium;
using Orasi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orasi.Selenium.Core.PageClass
{
    public class WebTable : Element
    {
        private Tuple<locatorType, string> element;
        IWebElement webTable = null;

        public WebTable(Tuple<locatorType, string> element, IWebDriver driver)
        {
            this.element = element;
            this.driver = driver;
            this.webTable = findElement(this.element);
        }

        /// <summary>
        ///     Attempts to locate the number of rows in a given webtable
        /// </summary>
        /// <returns>Integer number of rows in the table</returns>
        public int getRowCount()
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the driver to the initial implicit wait time
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            return rowCollection.Count;
        }

        /// <summary>
        ///     Attempts to locate the number of columns for a particular row
        /// </summary>
        /// <param name="row">Row within the webtable to count the columns</param>
        /// <returns>Number of columns in the row</returns>
        public int getColumnCount(int row)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));
            Boolean rowFound = false;

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the driver to the initial implicit wait time
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1;
            int columnCount = 0;
            string XPath = null;

            //Iterate through each row until the desired row is reached or there are no more rows to iterate through
            foreach (IWebElement rowElement in rowCollection)
            {
                //If the row number is found...
                if (row == currentRow)
                {
                    rowFound = true;
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));

                    //Use different, known xpaths, to find the columns in the row
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }
                    else
                    {
                        throw new NoSuchElementException("No child element with the HTML tag \"th\" or \"td\" were found for the parent webtable [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]");
                    }
                    //Reset the driver to the initial implicit wait time
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));
                    //Grab the number of columns in the row
                    columnCount = rowElement.FindElements(By.XPath(XPath)).Count;
                    break;
                }
                else
                {
                    currentRow++;
                }
            }
            //Ensure the desired row was found
            Assert.AreEqual(rowFound, true, "The expected row [" + row.ToString() + "] was not found. The number of rows found for webtable [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>] is [" + rowCollection.Count.ToString() + "].");

            return columnCount;
        }

        /// <summary>
        ///     Attempts to locate the number of columns for a particular row
        /// </summary>
        /// <param name="row">Row within the webtable to count the columns</param>
        /// <returns>Number of columns in the row</returns>
        public int getColumnCount(string row)
        {
            return getColumnCount(int.Parse(row));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row, determined 
        ///		by the parameter 'row', is found. For this row, all columns are then 
        ///		iterated through until the desired column, determined by the parameter 
        ///		'column', is found.
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        /// <returns>The cell in the web table element defined by the row and column values</returns>
        public IWebElement getCell(int row, int column)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the driver to the initial implicit wait time
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));
            IWebElement elementCell = null;

            int currentRow = 1, currentColumn = 1;
            string XPath = null;
            Boolean found = false;
            IList<IWebElement> columnCollection = null;

            //Iterate through each row...
            foreach (IWebElement rowElement in rowCollection)
            {
                //If the current row is not the desired row...
                if (row != currentRow)
                {
                    currentRow++;
                }
                //If the current row is the desired row...
                else
                {
                    //Set the driver to not wait if the webtable is not immediately present
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                    //Use different, known xpaths, to find the columns in the row
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }
                    else
                    {
                        throw new NoSuchElementException("No child element with the HTML tag \"th\" or \"td\" were found for the parent webtable [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]");
                    }

                    //Reset the driver to the initial implicit wait time
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                    //Grab all cells in the row
                    columnCollection = rowElement.FindElements(By.XPath(XPath));
                    foreach (IWebElement cell in columnCollection)
                    {
                        //If the current column is not the desired column...
                        if (column != currentColumn)
                        {
                            currentColumn++;
                        }
                        //If the current column in the desired column
                        else
                        {
                            elementCell = cell;
                            found = true;
                            break;
                        }
                    }
                    if (found) { break; }
                }
            }
            //Ensure the cell is found
            Assert.IsTrue(found, "No cell was found for row [" + row.ToString() + "] and column [" + column.ToString() + "]. The column count for row [" + row.ToString() + "] is [" + columnCollection.Count.ToString() + "]");

            return elementCell;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row, determined 
        ///		by the parameter 'row', is found. For this row, all columns are then 
        ///		iterated through until the desired column, determined by the parameter 
        ///		'column', is found.
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        /// <returns>The cell in the web table element defined by the row and column values</returns>
        public IWebElement getCell(string row, string column)
        {
            return getCell(int.Parse(row), int.Parse(column));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        /// 	tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        /// 	All rows are then iterated through until the desired row, determined 
        /// 	by the parameter 'row', is found. For this row, all columns are then 
        /// 	iterated through until the desired column, determined by the parameter 
        /// 	'column', is found. The cell found by the row/column indices is then 
        /// 	clicked
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        public void clickCell(int row, int column)
        {
            IWebElement cell = null;
            cell = getCell(row, column);
            cell.Click();
            Assert.NotNull(cell, "No cell was found for row [" + row.ToString() + "] and column [" + column.ToString() + "]");
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        /// 	tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        /// 	All rows are then iterated through until the desired row, determined 
        /// 	by the parameter 'row', is found. For this row, all columns are then 
        /// 	iterated through until the desired column, determined by the parameter 
        /// 	'column', is found. The cell found by the row/column indices is then 
        /// 	clicked
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        public void clickCell(string row, string column)
        {
            clickCell(int.Parse(row), int.Parse(column));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row, determined 
        ///		by the parameter 'row', is found. For this row, all columns are then 
        ///		iterated through until the desired column, determined by the parameter 
        ///		'column', is found.
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        /// <returns>string contents of the cell</returns>
        public string getCellData(int row, int column)
        {
            IWebElement cell = null;
            cell = getCell(row, column);
            Assert.NotNull(cell, "No cell was found for row [" + row.ToString() + "] and column [" + column.ToString() + "]");
            string cellData = cell.Text;
            return cellData;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row, determined 
        ///		by the parameter 'row', is found. For this row, all columns are then 
        ///		iterated through until the desired column, determined by the parameter 
        ///		'column', is found.
        /// </summary>
        /// <param name="row">Web table row which contains the cell</param>
        /// <param name="column">Web table column which contains the cell</param>
        /// <returns>string contents of the cell</returns>
        public string getCellData(string row, string column)
        {
            return getCellData(int.Parse(row), int.Parse(column));
        }

        /// <summary>
        ///      Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the cell with the desired text, determined by the parameter, is 
        ///		found.
        /// </summary>
        /// <param name="text">Text to search for in the web table cells</param>
        /// <returns>Integer number of the row containing the text</returns>
        public int getRowWithCellText(string text)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the driver to the initial implicit wait time
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, rowFound = 0;
            string XPath = null;
            Boolean found = false;

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Ensure the current row is within the bounds of the actual number of rows in the table
                if (currentRow <= rowCollection.Count)
                {
                    //Set the driver to not wait if the webtable is not immediately present
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                    //Determine which HTML tag denotes columns
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }
                    //Reset the web driver implicit timeout
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                    //Grab all of the cells in the row
                    IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                    //Iterate through each cell
                    foreach (IWebElement cell in columnCollection)
                    {
                        //Ensure the current row is within the bounds of the actual number of cells in the row
                        if (currentColumn <= columnCollection.Count)
                        {
                            //Determine if the current cell contains the desired text
                            if (cell.Text.Trim().Equals(text))
                            {
                                rowFound = currentRow;
                                found = true;
                                break;
                            }
                            currentColumn++;
                        }
                    }
                    if (found) { break; }
                    currentRow++;
                    currentColumn = 1;
                }
            }
            //Ensure the cell with the desired text is found
            Assert.AreEqual(found, true, "No cell was found containing the text [" + text + "].");
            return rowFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the desired cell is located. The cell text is then validated 
        ///		against the parameter 'text'
        /// </summary>
        /// <param name="text">Text to be searched for</param>
        /// <param name="columnPosition">Column position where the text is expected to be found</param>
        /// <returns>Integer number of the row containing the text</returns>
        public int getRowWithCellText(string text, int columnPosition)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the web driver implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, rowFound = 0;
            string XPath = null;
            Boolean found = false;

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Ensure the current row is within the bounds of the actual number of rows in the web table
                if (currentRow <= rowCollection.Count)
                {
                    //Set the driver to not wait if the webtable is not immediately present
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                    //Determine which HTML tag denotes columns
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }
                    //Reset the web driver implicit wait	        	
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                    //Grab the cells in the current row
                    IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                    //Iterate through each cell
                    foreach (IWebElement cell in columnCollection)
                    {
                        //If the current cell is the user-defined column
                        if (currentColumn == columnPosition)
                        {
                            //Determine if the cell text matches that which is expected
                            if (cell.Text.Trim().Equals(text))
                            {
                                rowFound = currentRow;
                                found = true;
                                break;
                            }
                        }
                        else
                        {
                            currentColumn++;
                        }
                    }
                    if (found) { break; }
                    currentRow++;
                    currentColumn = 1;
                }
            }
            Assert.AreEqual(found, true, "No cell in column [" + columnPosition.ToString() + "] was found to contain the text [" + text + "].");
            return rowFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the desired cell is located. The cell text is then validated 
        ///		against the parameter 'text'
        /// </summary>
        /// <param name="text">Text to be searched for</param>
        /// <param name="columnPosition">Column position where the text is expected to be found</param>
        /// <returns>Integer number of the row containing the text</returns>
        public int getRowWithCellText(string text, string columnPosition)
        {
            return getRowWithCellText(text, int.Parse(columnPosition));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the desired cell is located. The cell text is then validated 
        ///		against the parameter 'text'
        /// </summary>
        /// <param name="text">Text for which to search</param>
        /// <param name="columnPosition">Column position where the text is expected to be found</param>
        /// <param name="startRow">Row number to start searching</param>
        /// <returns>Integer number of the row containing the text</returns>
        public int getRowWithCellText(string text, int columnPosition, int startRow)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the web driver implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, rowFound = 0;
            string XPath = null;
            Boolean found = false;

            //Ensure the start row is less than or equal to the total number of rows in the web table
            Assert.LessOrEqual(startRow, rowCollection.Count, "The start row [" + startRow.ToString() + "] is greater than the total number of rows in the web table [" + rowCollection.Count.ToString() + "].");

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Skip any row number that is less than the start row
                if (startRow > currentRow)
                {
                    currentRow++;
                    //If the start row is found...
                }
                else
                {
                    //Ensure the current row is less than the actual number of rows in the web table
                    if (currentRow <= rowCollection.Count)
                    {
                        //Set the driver to not wait if the webtable is not immediately present
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                        //Determine which HTML tag denotes columns
                        if (rowElement.FindElements(By.XPath("th")).Count != 0)
                        {
                            XPath = "th";
                        }
                        else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                        {
                            XPath = "td";
                        }
                        //Reset the web driver implicit wait
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                        //Grab the list of cells in the current row
                        IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                        //Iterate through each cell
                        foreach (IWebElement cell in columnCollection)
                        {
                            //If the current cell is the user-defined column
                            if (currentColumn == columnPosition)
                            {
                                //Determine if the cell text matches that which is expected
                                if (cell.Text.Trim().Equals(text))
                                {
                                    rowFound = currentRow;
                                    found = true;
                                    break;
                                }
                            }
                            else
                            {
                                currentColumn++;
                            }
                        }
                        if (found) { break; }
                        currentRow++;
                        currentColumn = 1;
                    }
                }
            }
            Assert.AreEqual(found, true, "No cell in column [" + columnPosition.ToString() + "] was found to contain the text [" + text + "].");
            return rowFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the desired cell is located. The cell text is then validated 
        ///		against the parameter 'text'
        /// </summary>
        /// <param name="text">Text for which to search</param>
        /// <param name="columnPosition">Column position where the text is expected to be found</param>
        /// <param name="startRow">Row number to start searching</param>
        /// <returns>Integer number of the row containing the text</returns>
        public int getRowWithCellText(string text, string columnPosition, string startRow)
        {
            return getRowWithCellText(text, int.Parse(columnPosition), int.Parse(startRow));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through as well as each column for each row 
        ///		until the desired cell is located. The cell text is then validated 
        ///		against the parameter 'text'
        /// </summary>
        /// <param name="text">Text for which to search</param>
        /// <returns>Integer number of the column containing the text</returns>
        public int getColumnWithCellText(string text)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the web driver implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, columnFound = 0;
            string XPath = null;
            Boolean found = false;

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Ensure the current row is within the bounds of the actual number of rows in the web table
                if (currentRow <= rowCollection.Count)
                {
                    //Set the driver to not wait if the webtable is not immediately present
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                    //Determine which HTML tag denotes columns
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }
                    //Reset the web driver implicit wait
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                    //Grab all of the cells in the current row
                    IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                    foreach (IWebElement cell in columnCollection)
                    {
                        //Ensure the current cell is within the bounds of the actual number of columns in the row
                        if (currentColumn <= columnCollection.Count)
                        {
                            //Determine if the cell text matches that which is expected
                            if (cell.Text.Trim().Equals(text))
                            {
                                columnFound = currentColumn;
                                found = true;
                                break;
                            }
                            currentColumn++;
                        }
                    }
                    if (found) { break; }
                    currentRow++;
                    currentColumn = 1;
                }
            }
            Assert.AreEqual(found, true, "No cell in any column was found to have the text [" + text + "].");
            return columnFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row is found, 
        ///		then all columns are iterated through until the desired text is found.
        /// </summary>
        /// <param name="text">Text for which to search</param>
        /// <param name="rowPosition">Row number where the text is expected to be found</param>
        /// <returns>Integer number of the column containing the text</returns>
        public int getColumnWithCellText(string text, int rowPosition)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the web driver implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, columnFound = 0;
            string XPath = null;
            Boolean found = false;

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Skip any row number less than the expected row
                if (rowPosition > currentRow)
                {
                    currentRow++;
                    //If the expected row is found
                }
                else
                {
                    //Ensure the current row is within the bounds of the actual number of rows in the web table
                    if (currentRow <= rowCollection.Count)
                    {
                        //Set the driver to not wait if the webtable is not immediately present
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                        //Determine which HTML tag denotes columns
                        if (rowElement.FindElements(By.XPath("th")).Count != 0)
                        {
                            XPath = "th";
                        }
                        else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                        {
                            XPath = "td";
                        }
                        //Reset the web driver implicit wait
                        driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                        //Grab all of the cells for the current row
                        IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                        //Iterate through each cell
                        foreach (IWebElement cell in columnCollection)
                        {
                            //Ensure the current cell number is within the bounds of the actual number of columns
                            if (currentColumn <= columnCollection.Count)
                            {
                                //Determine if the current cell text is that which is expected
                                if (cell.Text.Trim().Equals(text))
                                {
                                    columnFound = currentColumn;
                                    found = true;
                                    break;
                                }
                                currentColumn++;
                            }
                        }
                        if (found) { break; }
                        currentRow++;
                        currentColumn = 1;
                    }
                }
            }
            Assert.AreEqual(found, true, "No cell in row [" + rowPosition.ToString() + "] was found to have the text [" + text + "].");
            return columnFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through until the desired row is found, 
        ///		then all columns are iterated through until the desired text is found.
        /// </summary>
        /// <param name="text">Text for which to search</param>
        /// <param name="rowPosition">Row number where the text is expected to be found</param>
        /// <returns>Integer number of the column containing the text</returns>
        public int getColumnWithCellText(string text, string rowPosition)
        {
            return getColumnWithCellText(text, int.Parse(rowPosition));
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through and a particular column, determined 
        ///		by the 'columnPosition' parameter, is grabbed and the text is validate 
        ///		against the expected text defined by the parameter 'text'.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="columnPosition"></param>
        /// <returns>Integer number of the column containing the text</returns>
        public int getRowThatContainsCellText(string text, int columnPosition)
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the web driver implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            int currentRow = 1, currentColumn = 1, rowFound = 0;
            string XPath = null;
            Boolean found = false;

            //Iterate through each row
            foreach (IWebElement rowElement in rowCollection)
            {
                //Ensure the current row is within the bounds of the actual number of rows in the web table
                if (currentRow <= rowCollection.Count)
                {
                    //Set the driver to not wait if the webtable is not immediately present
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));
                    //Determine which HTML tag denotes columns
                    if (rowElement.FindElements(By.XPath("th")).Count != 0)
                    {
                        XPath = "th";
                    }
                    else if (rowElement.FindElements(By.XPath("td")).Count != 0)
                    {
                        XPath = "td";
                    }

                    //Reset the web driver implicit wait
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

                    //Grab all the cells in the current row
                    IList<IWebElement> columnCollection = rowElement.FindElements(By.XPath(XPath));
                    foreach (IWebElement cell in columnCollection)
                    {
                        //If the current cell number equals the user-defined column number
                        if (currentColumn == columnPosition)
                        {
                            //Determine if the cell text matches that which is expected
                            if (cell.Text.Trim().Contains(text))
                            {
                                rowFound = currentRow;
                                found = true;
                                break;
                            }
                        }
                        else
                        {
                            currentColumn++;
                        }
                    }
                    if (found) { break; }
                    currentRow++;
                    currentColumn = 1;
                }
            }
            return rowFound;
        }

        /// <summary>
        ///     Attempts to locate the number of child elements with the HTML 
        ///		tag "tr" using XPath. If none are found, the XPath "tbody/tr" is used. 
        ///		All rows are then iterated through and a particular column, determined 
        ///		by the 'columnPosition' parameter, is grabbed and the text is validate 
        ///		against the expected text defined by the parameter 'text'.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="columnPosition"></param>
        /// <returns>Integer number of the column containing the text</returns>
        public int getRowThatContainsCellText(string text, string columnPosition)
        {
            return getRowThatContainsCellText(text, int.Parse(columnPosition));
        }

        /// <summary>
        ///     Cycle through each cell and highlight them
        /// </summary>
        /// <returns>Integer number of rows in the table</returns>
        public void highlightEachCell()
        {
            //Set the driver to not wait if the webtable is not immediately present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            //Define a collection of rows using the "tr" xpath
            IList<IWebElement> rowCollection = this.webTable.FindElements(By.XPath("tr"));
            //Define a collection of cells for each row
            IList<IWebElement> cellCollection = null; ;
            string XPath = null;

            //If no rows are found, use the "tbody/tr" xpath
            if (rowCollection.Count == 0)
            {
                rowCollection = this.webTable.FindElements(By.XPath("tbody/tr"));
            }
            //Reset the driver to the initial implicit wait time
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            //Iterate through each row
            foreach (IWebElement row in rowCollection)
            {
                //Set the driver to not wait if the webtable is not immediately present
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(0));

                //Use different, known xpaths, to find the columns in the row
                if (row.FindElements(By.XPath("th")).Count != 0)
                {
                    XPath = "th";
                }
                else if (row.FindElements(By.XPath("td")).Count != 0)
                {
                    XPath = "td";
                }
                else
                {
                    throw new NoSuchElementException("No child element with the HTML tag \"th\" or \"td\" were found for the parent webtable [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]");
                }

                //Grab all of the cells in the row
                cellCollection = row.FindElements(By.XPath(XPath));

                //Iterate through each ell and highlight it
                foreach (IWebElement cell in cellCollection)
                {
                    highlight(cell);
                }

                //Reset the driver to the initial implicit wait time
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));
            }
        }
    }
}
