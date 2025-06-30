using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace TestSnackSnap
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestUserRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost/SnackSnap-/registration.php");
            driver.Manage().Window.Maximize();

     
            driver.FindElement(By.Name("firstname")).SendKeys("shagor");
            driver.FindElement(By.Name("lastname")).SendKeys("miah");
            driver.FindElement(By.Name("email")).SendKeys("shagor.miah" + new Random().Next(1000, 9999) + "@example.com"); // Unique email
            driver.FindElement(By.Name("phone")).SendKeys("01712345678");
            driver.FindElement(By.Name("password")).SendKeys("shagor@1234");
            driver.FindElement(By.Name("cpassword")).SendKeys("shagor@1234");

            // Submit the form
            driver.FindElement(By.Name("submit")).Click();

            Thread.Sleep(3000); // wait to see result (replace with WebDriverWait in real tests)

            // You could now assert something like success message or URL redirect
            // e.g., Assert.IsTrue(driver.Url.Contains("success.php"));

            driver.Quit();
        }


        [Test]
        public void TestUserLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            // Test with empty username and password
            PerformLogin(driver, "", "");
            Thread.Sleep(2000);

            // Test with empty password
            PerformLogin(driver, "Nahid Hasan Nobil", "");
            Thread.Sleep(2000);

            // Test with empty username
            PerformLogin(driver, "", "Nobil@1234");
            Thread.Sleep(2000);

            // Test with incorrect credentials
            PerformLogin(driver, "Wrong User", "WrongPass");
            Thread.Sleep(2000);

            // Test with correct credentials
            PerformLogin(driver, "Nahid Hasan Nobil", "Nobil@1234");
            Thread.Sleep(3000);

            driver.Quit();
        }

        [Test]
        public void TestAddRestaurant()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/SnackSnap-/admin/add_restaurant.php");

            Thread.Sleep(2000); // Wait for page to fully load

            try
            {
                // Fill in restaurant details
                driver.FindElement(By.Name("c_name")).SendKeys("1"); // Assuming "1" is a valid category ID
                driver.FindElement(By.Name("res_name")).SendKeys("Selenium Bistro " + new Random().Next(100, 999));
                driver.FindElement(By.Name("email")).SendKeys("selenium@example.com");
                driver.FindElement(By.Name("phone")).SendKeys("01712345678");
                driver.FindElement(By.Name("url")).SendKeys("http://selenium-bistro.com");
                driver.FindElement(By.Name("o_hr")).SendKeys("9:00 AM");
                driver.FindElement(By.Name("c_hr")).SendKeys("10:00 PM");
                driver.FindElement(By.Name("o_days")).SendKeys("Mon-Sun");
                driver.FindElement(By.Name("address")).SendKeys("123 Test Street, Selenium City");

                // Submit the form
                driver.FindElement(By.Name("submit")).Click();

                Thread.Sleep(3000); // Wait for response

                // Assert success alert is shown
                Assert.IsTrue(driver.PageSource.Contains("New Restaurant Added Successfully"), "Restaurant may not have been added.");
            }
            catch (Exception ex)
            {
                Assert.Fail("TestAddRestaurant failed: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }

        [Test]
        public void TestAddCategory()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/SnackSnap-/admin/add_category.php");

            Thread.Sleep(2000); // Wait for page to load

            try
            {
                // Generate a unique category name to avoid duplicates
                string uniqueCategory = "Selenium Category " + new Random().Next(1000, 9999);

                // Fill the category name
                driver.FindElement(By.Name("c_name")).SendKeys(uniqueCategory);

                // Submit the form
                driver.FindElement(By.Name("submit")).Click();

                Thread.Sleep(2000); // Wait for result

                // Assert success message
                Assert.IsTrue(driver.PageSource.Contains("New Category Added Successfully"), "Category may not have been added.");
            }
            catch (Exception ex)
            {
                Assert.Fail("TestAddCategory failed: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }


        [Test]
        public void TestAddMenuItem()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/SnackSnap-/admin/add_menu.php");

            Thread.Sleep(2000); // Wait for page load

            try
            {
                // Fill in the form with valid data
                driver.FindElement(By.Name("d_name")).SendKeys("Selenium Special Burger");
                driver.FindElement(By.Name("about")).SendKeys("Delicious and automated test-created burger.");
                driver.FindElement(By.Name("price")).SendKeys("250");

                // Assuming 'res_name' is a dropdown or input field for restaurant ID
                driver.FindElement(By.Name("res_name")).SendKeys("1"); // Use a valid restaurant ID


                // Submit the form
                driver.FindElement(By.Name("submit")).Click();

                Thread.Sleep(3000); // Wait for response

                // Assert that success message is shown
                Assert.IsTrue(driver.PageSource.Contains("New Dish Added Successfully"), "Menu item may not have been added.");
            }
            catch (Exception ex)
            {
                Assert.Fail("TestAddMenuItem failed: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }


        private void PerformLogin(IWebDriver driver, string username, string password)
        {
            // Navigate to login page
            driver.Navigate().GoToUrl("http://localhost/SnackSnap-/login.php");

            // Find input fields by their "name" attributes
            IWebElement usernameInput = driver.FindElement(By.Name("username"));
            IWebElement passwordInput = driver.FindElement(By.Name("password"));

            // Fill in credentials
            usernameInput.Clear();
            usernameInput.SendKeys(username);

            passwordInput.Clear();
            passwordInput.SendKeys(password);

            // Click the login button (now named "submit")
            IWebElement loginButton = driver.FindElement(By.Name("submit"));
            loginButton.Click();
        }

    }
}
