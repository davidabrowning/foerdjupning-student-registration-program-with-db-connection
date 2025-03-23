# Student Registration Program with Database Connection
Author: David Browning (david.browning@studerande.yh.se, david.a.browning@gmail.com)\
Program: Systemutvecklare .NET 2025-2026\
Course: Databasutveckling

## Program info
Enkel konsolapplikation med EF Core databas där man kan registera studenter, uppdatera
studenter och lista ut alla studenter. Inmatad information om studenterna lagras i
databasen och finns kvar nästa gång programmet körs.

## Instruktioner
Skapa en enkel Student klass med:
- int StudentId 
- string FirstName 
- string LastName
- string City

Använd EF Core för att ansluta mot databasen. Se till att användaren genom menyval kan, välja:
- Registrera ny
- Ändra en student
- Lista alla studenter

Ha gärna föregående kurs innehåll i åttanke när du skapar applikationen, vilka delar här bör kanske hållas isär, gör
metoderna och klasserna lagom mycket etc.