# Chat Room Project


## Documentation

## Todo list:
1. Login pop-up window
   * A window should pop up to prompt the user to input a user name.
   * Error checking is needed, i.e. a user can not user a blank name.
   * The login button should be disabled after the inital connection to the server and the logout button should become enabled.
     * If time allows there should be checking to see if a username is already in use and alert the user to that.
   * ...
2. Update each connected Client with the message sent to the server.
   * Currently only the client that sent the message can see the message. The server recieves all of the messages. The log window for the server correctly shows the messages recieved from each Client. The server needs to echo the message to ALL of the clients.
   * ...
3. Show less information on Client chat windows.
   * Currently the message recieved from the server contains information that the user doesn't care about. The Client should only see _username>message_ 
   * ...
4. Update Client User List
   * The list of users logged in to the chat room needs to update everytime a user logins into the server.
   * This could maybe implemented by the server sending a message to the clients that tells them to update their lists AND NOT print it out to the chat text window.
   * ...
5. Private Chat
   * No work has been done on this as of yet.
   * Request Private chat and Logout Private Chat buttons need to be implemented. 
   * The user should click on a name in the current user list and click the request button.
     * It is the servers responsibility to know a number of things:
       * If the private chat room is already in use
       * If both clients requested eachother to chat with
     * The server will need to keep track of WHO requests WHO to chat with. If A requests to chat with B, but B requests to chat with C and C requests to chat with A then there is a deadlock condition. All three clients are stuck waiting to chat privately with the other. In this situation there could be a timer that will cancel private chat requests after a certain duration of time with no response from the other client. 
   * ...
6. _Add more as needed_
 
