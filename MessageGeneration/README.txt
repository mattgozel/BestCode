How to use the program:

Option 1 - The program is hosted in Azure. Simply navigate to this link and use the program in your browser: https://messagegenerationui.azurewebsites.net/
Option 2 - Grab the project from GitHub and run in Visual Studio: https://github.com/mattgozel/BestCode/tree/master/MessageGeneration


Design decisions:

The project is structured into 4 different layers: Data, Models, Tests and UI.

The data layer is structured using factory classes that pick the repositories used based on an AppSettings key. I used interfaces and inheritance when creating my repositories.
I did this so that my code is easily reused and can be refactored easily in the future. I have my code neatly organized into classes and I adhered to the single responsibility principle in my method creation.

The model layer contains models for objects based on the data contained in the JSON files.

The test layer contains tests for all of the logic in my data layer.

The UI is a basic web application (I just used HTML, CSS and MVC 5 Razor Syntax), but works as intended.


Language:

I picked C# because I love C#, I am the most comfortable with it, and I knew it had the perfect functionality to create this program. More over, I knew I was going to host this program in Azure, which made C# a natural choice.


Correctness of program:
I have unit tests to check my logic, and tested the front end and output as well. The program is working as intended as far as I can tell. At first, the check in and check out times were throwing me for a loop,
in converting from ticks to an actual DateTime, the check in and check out times were identical in terms of date and time down to the second. But I set up breakpoints in my code, so I could step through it line by line and
make sure that the tick values were populating correctly everywhere, which they were. To double check, I edited a JSON file and made the tick count much, much bigger for a test check out, and the DateTime in my program updated accordingly.
All was well and everything was actually working as intended.

To do:
1. Improve the UI. Since a UI was not required for this assignment, I just put together a very basic one so y'all could actually use my program. If this was an actual application for users, I would make the UI much prettier.
2. Update the DateTime logic. Right now, timezone is not taken into account when determining if the 'morning', 'afternoon' and 'evening' greetings are used. Since in the JSON data provided, all time zones are within 2 hours of each other,
I decided it wasn't a priority to have that functionality immediately. It is something I would implement in the future, especially if the application was to be used internationally.
3. Update the logic for locating the JSON files. Right now, I have the JSON files saved in two places so they can be accessed by the unit test and data layer projects. That is something I want to clean up in the future so that
multiple copies don't need to be updated.