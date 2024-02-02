# Blockchain Assignment Application

This repository contains a simple yet functional implementation of a blockchain system designed for educational purposes. The core classes—`Block`, `Blockchain`, `Transaction`, and `Wallet`—work together to simulate the fundamental operations of a blockchain.

## Features

- **SHA256 Encryption**: Employs SHA256 hashing for securing blocks.
- **Immutable Ledger**: Tracks the sequence of blocks with an incremental index and timestamps.
- **Public/Private Key Generation**: Facilitates secure transactions with cryptographic key pairs.
- **Transaction Verification**: Incorporates signature validation to authenticate transactions.
- **Mining Simulation**: Demonstrates the process of mining new blocks with nonce and difficulty levels.
- **Transaction Pool**: Manages a pool of pending transactions ready to be included in the next block.

## Practical Use

This application provides a hands-on experience with blockchain technology. It's a valuable educational resource for understanding decentralized ledger functionality, cryptographic principles, and the consensus process in blockchain networks.

## Motivation

The project was developed to demystify the technicalities of blockchain operations for students, developers, and blockchain enthusiasts. It strips down complex concepts into manageable code, making it easier to grasp the underlying principles of blockchain technology.

## Getting Started

Clone the repository to your local machine:


## Usage

Instantiate the `Block` class to create new blocks:

```csharp
Block genesisBlock = new Block(0, "0");
Block nextBlock = new Block(genesisBlock);

 

 

