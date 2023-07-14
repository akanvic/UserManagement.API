#Instructions to run the Application

Install SqlLiteDBBrowser or any other SQLLite manager of your choice.
Open the SqlLite db file rooted in the EventMgt.API web folder.
In Visual studio open package manager console and run this command 'update-database'.
Ensure the default project on the package manager console is set to UserMgt.Core.

#Design Decisons

With regards to the application architecture I used the Clean architecture to ensure Separation of Concerns, Testability, Maintainability and Scalability,
Readability and Understandability etc.

I followed principles regarding API security which are in regards
- Returning only the neccessary information in the API responses.
- Avoid using GET HTTP method in API's that are dealing with sensitive application data or API's that modify the applications's data rather i Used a POST method.
- Securing the API with Bearer tokens.
- Password hashing.
