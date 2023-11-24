# EthersonGetContractAPI will get contract info from etherson website and store it DB base the contract address provided and API key. It will display the contracts in JSON format.

It has 3 endpoints
1. getContractInfoFromEtherson: Will get the Contract address and APIKey as input.
   It will validate the API Key and Contract address.
   It will get the contract information from the Etherson website and store all the list contracts in database.
2. getContractInfoFromDB: It get the input as ID and get the contract from DB base on the ID provide.
3. getAllContractInfoFromDB: It get all the contract from the DB and display it in JSON format.

API key: CPYIXBS59KH1IDK4963WTZYT2B1GM4X85X
Sample Contract address: 0x5c49576a7459b4168534bf712de60b42a525bc9d
URL for getContractInfoFromEtherson:  https://localhost:44371/api/EthersonContract/getContractInfoFromEtherson?contractAddress=0x5c49576a7459b4168534bf712de60b42a525bc9d&ApiKey=CPYIXBS59KH1IDK4963WTZYT2B1GM4X85X

To Acess the application:
1. Clone the repository and change the connection string.
2. Add migration to the DB for create database and table.
3. Run the API.


