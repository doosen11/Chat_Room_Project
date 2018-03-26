# Chat Room Project


## Documentation
1. **Server Usage**
  * *Starting the Server* 
    * Starting the server is a simple task, simply launch the program and everything else is automated.
  
  * *Tracking Users*
    * Connected users will appear in the right-hand window of the server. This continually updates to show the correct user listing.
    * Once a user is connected, the server will provide an notification in the log.
   
  * *
  
2. **Client Usage**
  * *User Status*
    * The client program provides a windows which shows all currently connected users.
    
  * *The Buttons*
    * *Login*
        * The login button provides a way for the user to connect to the server. Once this button is pressed, a new window appears which prompts the user to enter a username and connect to the server. Once a name is entered, the programs automatically attempts to connect to the server and login using that name.
    * *Logout*
        * The logout button provides a method for the user to disconnect from the server.
    * *Request Private Chat*
        * This button allows for a user to request a private chat with a user of their choice on the server.
    * *End Private Chat*
        * Ends the private chat.
     
  
  * *Public chat*
    * The text input at the bottom of the window allows for the user to output chat that everyone connected to the server can see. Once connected, all a user has to do is enter their chat into the textbox, and then click "Send".
       
        


## Todo list:
1. Login pop-up window
   * A window should pop up to prompt the user to input a user name.
   * Error checking is needed, i.e. a user can not user a blank name.
   * The login button should be disabled after the inital connection to the server and the logout button should become enabled.
     * If time allows there should be checking to see if a username is already in use and alert the user to that.
   * Work Done:
   
         Currently the username is parsed from the input text field
2. Update each connected Client with the message sent to the server.
   * Currently only the client that sent the message can see the message. The server recieves all of the messages. The log window for the server correctly shows the messages recieved from each Client. The server needs to echo the message to ALL of the clients.
   * Work Done:
          
          None.
3. Show less information on Client chat windows.
   * Currently the message recieved from the server contains information that the user doesn't care about. The Client should only see _username>message_ 
   * Work Done:
   
         None.
4. ~~Update Client User List~~
   * ~~The list of users logged in to the chat room needs to update everytime a user logins into the server.~~
   * ~~This could maybe implemented by the server sending a message to the clients that tells them to update their lists AND NOT print it out to the chat text window.~~
   * Work Done:
          
         This is working now.
5. Private Chat
   * Request Private chat and Logout Private Chat buttons need to be implemented. 
   * The user should click on a name in the current user list and click the request button.
     * It is the servers responsibility to know a number of things:
       * If the private chat room is already in use
       * If both clients requested eachother to chat with
     * The server will need to keep track of WHO requests WHO to chat with. If A requests to chat with B, but B requests to chat with C and C requests to chat with A then there is a deadlock condition. All three clients are stuck waiting to chat privately with the other. In this situation there could be a timer that will cancel private chat requests after a certain duration of time with no response from the other client. 
   * Work Done: Garrett is working on it.
   
         None.
6. Error Handeling!
   * Error handeling should be implemented to things don't break.
     * The server should not crash when a user closes their Client window instead of sending a logout request. Is there a way that the logout function can be tied to the close button? Something to look into...
     * Duplicate usernames
   * Work Done:
   
         None.
7. _Add more as needed_
 
