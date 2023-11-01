# DroneService
Application for processing drone service orders using a queue.

The Service Processing Application must use two Queue<T> data structures of a simple class which has the following five attributes: Client Name, Drone Model, Service Problem, Service Cost and Service Tag.
When a client delivers a drone to Icarus for attention the front desk staff will enter the details as required to populate the Drone class. The client will be able to select between a regular or express priority for the service of their drone. The express priority service will incur an additional 15% charge to the service cost. Once the priority has been selected the drone will be added to one of the two queues (regular or express). The client’s drone will be tagged and send to the service department for inspection and repair/service. Once the drone is repaired and returned to the front desk the Icarus staff will remove the details from the queue. This removal process will dequeue the appropriate data structure and add the details to the list of completed work. Finally, the client will be able to pay for the work and collect their drone; the staff on the front desk will then remove the item from the finished work list.
All user interactions must have full error trapping and feedback messaging which is displayed in a status strip at the bottom of the form. The need to use a message box for critical errors or general feedback is not necessary.

Tasks: 
Create two Queues of a simple class.
Create global list for finished items. Class Attributes: Client Name, Drone Model, Service Problem, Service Cost and Service Tag.
Create GUI that allows user to fill out the details for the class attributes. GUI also includes regular and express service queues and finished item list. Client Name, Drone Model, Service Problem and Service Cost in Textboxes. Service Tag as option between to radio buttons.
Create an add button to add data from relevant boxes into the respective queue (regular or express).	Radio button determines which queue data enters.
Create a display that displays data from the queues and list in list boxes.	
Create a mouse click method that displays the selected data from the queue list boxes in the relevant textboxes.
Add buttons that will dequeue the next item from their respective service queues and add them to the finished list.
Create a double click method that removes the double-clicked item from the finished list.
Create a method that clears all respective textboxes when an item is added or when needed. After clearing replace textbox instructions.

Program Behaviour:
•	Data can be added to a queue by filling out 4 textboxes and selecting the desired queue using a radio button.
•	Queue data will be displayed in the respective queue list box.
•	After an item in the queue is finished it will be moved to the list and displayed in the respective list box.
•	Double clicking finished items will remove them.
•	Clicking an item in the queue list boxes will display the information in the textboxes.
•	Textboxes will have instructions that will be cleared when the textbox is focused. These will be replaced after items in the textbox are cleared by the clear method.
