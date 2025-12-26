The application represents functionality for the creation and management of water vending machines.

# Water Vending Machine — Lab 6 (Collections)

## Project Overview
This lab updates the existing Water Vending Machine application to use
**collections** for storing and managing objects.

---

## Object Storage
- All `WaterVendingMachine` objects are stored in a `List<WaterVendingMachine>`
- Object operations use built-in `List<T>` methods:
  - `Add`
  - `Remove`
  - `Find`
  - `FindAll`

---

## Program Structure
- Core logic is separated from console I/O
- Methods that manage the collection do **not** use:
  - `Console.ReadLine()`
  - `Console.WriteLine()`
- Console interaction is handled only in the UI layer

---

## Testing
- A new MSTest class is added to test **non-UI collection methods**
- Tests cover:
  - Adding objects
  - Removing objects
  - Searching objects
  - Handling invalid input
- All existing and new unit tests pass successfully

---

## Class Features
The `WaterVendingMachine` class keeps all functionality from previous labs:
- Water dispensing
- Cash handling
- Refill (full and partial)
- Cash withdrawal
- Address change
- Price-based water calculation
- Object counter and static logic

---

## Menu
1. Add object  
2. View all objects  
3. Find object  
4. Demonstrate behavior  
5. Delete object  
6. Demonstrate static methods  
0) Exit  

---

## Object Creation Options
- Create default
- Manually enter data
- Manually enter data partly
- Enter entity string
- Exit to main menu
