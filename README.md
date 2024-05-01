# Blockchain Simulator Personal Assignment Application

This repository houses an educational blockchain application that demonstrates the key components and functionalities of a distributed ledger system. The primary classes—`Block`, `Blockchain`, `Transaction`, and `Wallet`—collectively simulate the critical operations of a blockchain.

## Release Notes

### v1.7/5.5 (Updated New Features) (FINAL VERSION)
- **Block Reward Halving Mechanism**: Implemented a reward halving feature where the mining reward is reduced by half every `halvingInterval` number of blocks, reflecting the scarcity and incentivizing early mining participation.
  - This feature emulates the economic model of deflationary assets and is pivotal for maintaining the long-term value of the blockchain's currency.
- **Dynamic Reward Calculation**: The reward for mining new blocks now dynamically adjusts based on the total number of blocks mined in the blockchain, fostering a realistic mining environment.

### v1.6/5.4 (New Features)
- **Alternative Mining Preference Settings**: Added the ability for miners to choose their preferred transaction selection strategy when mining a new block. Options include:
  - **Greedy**: Prioritizes transactions with the highest fees.
  - **Altruistic**: Selects transactions based on the longest waiting time.
  - **Random**: Randomly picks transactions from the pool.
  - **Address Preference**: Prefers transactions involving the miner's own address.
- **UI Integration**: Updated the user interface to allow miners to easily select their mining preference.
- **Dynamic Transaction Handling**: Enhanced the system's transaction handling logic to accommodate different mining preferences.

(Include previous release notes here...)

### Upcoming Features:
- **Smart Contracts**: Introducing the capability to deploy and execute smart contracts within the blockchain.
- **Proof of Stake**: A transition to the Proof of Stake consensus algorithm for energy-efficient block validation.

## Practical Use
This application serves as a practical tool for exploring blockchain mechanics. It is an invaluable educational asset for students and enthusiasts aiming to understand the inner workings of decentralized ledger systems, cryptographic security, and consensus algorithms in blockchain networks.

## Motivation
The intent behind this project is to clarify the complexities of blockchain technology for educational purposes. It breaks down intricate blockchain concepts into digestible code segments, facilitating a deeper comprehension of the technology's foundational aspects.

## Usage
Click generate new wallet to have your keys ready, To generate new blocks and interact with the blockchain, instantiate the `Block` class and engage with the application's methods:
```csharp
Block genesisBlock = new Block();
Block nextBlock = new Block(genesisBlock, transactionList, minerAddress, difficulty);


 

