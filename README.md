# 🎮 Connect Four Game in C#

## 📌 Project Overview
This is a two-player **Connect Four** game developed in C# as a console application. The game allows two players to take turns dropping discs into a 7-column, 6-row grid. The goal is to connect four of your discs in a line — horizontally, vertically, or diagonally.

This project was created for the **SODV1202: Introduction to Object-Oriented Programming** final assignment.

---

## 🚀 How to Run the Game

### 🔧 Requirements
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- Visual Studio or Visual Studio Code

👨‍💻 Features
Turn-based 2-player gameplay (Player 1 = X, Player 2 = O)

Real-time console board rendering

Input validation and error handling

Win detection (horizontal, vertical, and diagonal)

Draw detection if the board fills up

🧱 Object-Oriented Programming (OOP) Concepts Used
✅ Encapsulation
The Game class contains all game logic, including the board, player turns, and input handling.

Fields like board, currentPlayer, and player names are kept private.

✅ Abstraction
Methods like MakeMove(), CheckWinner(), PrintBoard(), and SwitchPlayer() encapsulate specific tasks and hide complexity.

✅ Enum Usage
CellState enum is used to represent each board cell state (Empty, Player1, Player2), improving code readability.

🧠 Challenges Faced
Implementing all win condition checks, especially diagonals.

Ensuring proper input validation for column entries and full columns.

Designing a clean and readable structure using OOP principles.

🔮 Future Improvements
Add a graphical user interface (GUI) using Windows Forms or WPF.

Implement AI for single-player mode.

Add sound effects or animations for moves.

Track and display player scores over multiple rounds.

👤 Author
Name: Rahul Mahyavanshi
Course: SODV1202 - Introduction to Object-Oriented Programming
Instructor: Mahbub Murshed

