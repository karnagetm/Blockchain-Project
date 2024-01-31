# Blockchain Assignment

This repository contains a simple implementation of a blockchain structure. The core functionality is encapsulated in the `Block` class, which represents a single block in the blockchain.

## Features

- Generation of a unique hash for each block using SHA256 encryption.
- Tracking of the block index within the chain.
- Recording of the timestamp for each block creation.

## Getting Started

Clone the repository to your local machine using: 

 https://github.com/your-username/BlockchainAssignment.git


## Usage

To use the `Block` class, create an instance by passing the index and the previous hash. For example:

```csharp
Block genesisBlock = new Block(0, "0");
Block nextBlock = new Block(genesisBlock);

 

