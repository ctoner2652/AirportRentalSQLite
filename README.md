# AirportRentalSQLite

**AirportRentalSQLite** is a lightweight application that demonstrates how to manage an airport rental service using SQLite as the backend database. It covers basic CRUD operations and provides a clear, easy-to-follow structure—perfect for anyone looking to integrate SQLite into their projects.

---

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Code Overview](#code-overview)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

---

## Features

- **SQLite Integration:** Seamlessly connect and interact with an SQLite database.
- **CRUD Operations:** Easily create, read, update, and delete rental records.
- **Simple and Modular:** Well-organized code that's straightforward to understand and extend.
- **Error Handling:** Basic error management to ensure smooth database transactions.

---

## Getting Started

Follow these instructions to set up and run the project locally.

### Prerequisites

- **.NET SDK:** Download the latest version from the [official .NET site](https://dotnet.microsoft.com/download) if this is a .NET application.
- **SQLite:** While the project manages SQLite integration, installing SQLite separately can help you explore and manage the database directly.
- **Code Editor:** Visual Studio, VS Code, or any editor of your choice.
- **DatabaseFile:** The repo currently uses a connection string based on usersecrets that will need to be updated to push to your local database file

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/ctoner2652/AirportRentalSQLite.git

2. **Navigate to the Project Directory**

   ```bash
   cd AirportRentalSQLite

3. **Build the Project**

   For .NET projects, build the project using:

   ```bash
   dotnet build

---

## Usage

After building the project, run it with:
```bash
dotnet run
```
Follow the on-screen instructions to interact with the application. This will demonstrate how to manage rental data using an SQLite database, highlighting the CRUD operations in action.

## Code Overview

The project is structured to be both educational and practical. Here are some key aspects:

- **Database Connection:**  
  The code establishes and manages a connection to the SQLite database, demonstrating best practices in connection handling.

- **CRUD Operations:**  
  Well-defined functions allow you to create, read, update, and delete rental records in a concise manner.

- **Error Handling:**  
  Basic error handling is included to ensure reliable database transactions and smooth execution.

---

## Contributing

Contributions are always welcome! To contribute:

1. **Fork the Repository**
2. **Create a Feature Branch**

   ```bash
   git checkout -b feature/your-feature-name

3. **Commit Your Changes**

   ```bash
   git commit -m "Add some feature"

4. **Push to Your Branch**

   ```bash
   git push origin feature/your-feature-name

5. **Open a Pull Request**

In my opinion, the simplicity and clarity of this project make it an excellent starting point for developers looking to work with SQLite in real-world applications.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

## Acknowledgements

- **SQLite:** For being a reliable, lightweight database solution.
- **.NET Community:** For providing the tools and frameworks that make projects like this possible.
- **Open Source Contributors:** Thanks to everyone who shares their work and helps improve these learning resources.

---

Happy coding!
