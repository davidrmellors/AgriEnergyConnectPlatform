# AgriEnergyConnectPlatform

## Overview

AgriEnergyConnectPlatform is a prototype web application developed as part of the PROG7311 university module project. The platform aims to bridge the gap between the agricultural sector and green energy technology providers, promoting sustainable farming practices and the adoption of renewable energy solutions.

## Table of Contents

- [Project Purpose](#project-purpose)
- [Installation](#installation)
- [Development Environment Setup](#development-environment-setup)
- [Building and Running the Prototype](#building-and-running-the-prototype)
- [System Functionalities and User Roles](#system-functionalities-and-user-roles)
- [Contributing](#contributing)
- [License](#license)

## Project Purpose

The primary objective of this project is to create a digital ecosystem where farmers, green energy experts, and enthusiasts can collaborate, share resources, and innovate in the realms of sustainable agriculture and renewable energy. This initiative is in response to the growing need for sustainable agricultural practices and the integration of green energy solutions in South Africa.

## Installation

To set up the AgriEnergyConnectPlatform on your local machine, follow these steps.

### Prerequisites

Ensure you have the following installed:

- [Visual Studio 2019 or later](https://visualstudio.microsoft.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition is recommended)
- [Git](https://git-scm.com/)

## Development Environment Setup

1. **Clone the Repository**

    ```sh
    git clone https://github.com/davidrmellors/AgriEnergyConnectPlatform.git
    cd AgriEnergyConnectPlatform
    ```

2. **Open the Project in Visual Studio**

    Open Visual Studio and select "Open a project or solution." Navigate to the cloned repository folder and open the `.sln` file.

3. **Restore NuGet Packages**

    In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Manage NuGet Packages for Solution...` and restore the required packages.


## Building and Running the Prototype

1. **Build the Solution**

    In Visual Studio, build the solution by clicking `Build` > `Build Solution` or pressing `Ctrl+Shift+B`.

2. **Run the Application**

    Set the `AgriEnergyConnectPlatform` project as the startup project. Run the application by clicking `Debug` > `Start Debugging` or pressing `F5`. The application should launch in your default web browser.

## System Functionalities and User Roles

### User Roles

1. **Admin**

    - Manage employee and farmer accounts (create, delete)
    - View and filter farmer products
	- Access personal profile and update information

2. **Employee**

    - Create new farmer accounts
	- View and filter farmer products
	- Access personal profile and update information
	
3. **Farmer**

    - Access personal profile and update information
    - View AgriConnect Marketplace and filter products
    - Manage new personal products (create, update, delete)

### Key Functionalities

- **Sustainable Farming Hub**: Provides resources for best practices in sustainable farming, including organic farming techniques, water conservation methods, and soil health maintenance. Includes interactive forums and discussion boards for farmers to seek advice and share experiences.
- **Green Energy Marketplace**: Offers a marketplace for green energy solutions tailored to agricultural needs, such as solar-powered irrigation systems, wind turbines for farms, and biogas energy solutions. Features for comparing products, reviewing technologies, and connecting with green tech providers.
- **Project Collaboration and Funding Opportunities**: Enables farmers and energy experts to propose and collaborate on joint projects, and provides information on grants, subsidies, and funding opportunities for green initiatives in agriculture.
- **Educational and Training Resources**: Provides online courses, webinars, and workshops on integrating green energy technologies in agriculture.
- **User Profile Management**: Allows users to update their personal information and change passwords.

## Contributing

We welcome contributions from the community. Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature-name`).
5. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.