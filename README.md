# HotelBookingAppReact

##Diagrams and postman collections can be found in the Docs directory

##Steps to install: 
- Clone project
- Run 'npm install' command in the ClientApp directory
- Set the project solution
- Run the app

The application uses In Memory database so no database setup is needed.

##Testing the application:
- Get postman, import the provided postman collections from the Docs directory
- Run the app
- Log in using these credentials: username: 'admin@admin.com', password: 'Admin8*'
- The React app homepage will show the current bearer token for the logged in user
- Copy the bearer token and paste it in the parent directory of the postman collection in the Authorization section
- Test the endpoints present in the postman collection
