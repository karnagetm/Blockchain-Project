# Blockchain Personal Assignment Application:

This repository houses an educational blockchain application that demonstrates the key components and functionalities of a distributed ledger system. The primary classes—`Block`, `Blockchain`, `Transaction`, and `Wallet`—collectively simulate the critical operations of a blockchain.

## Release Notes:

### v1.6/5.4 (New Features)
- **Alternative Mining Preference Settings**: Added the ability for miners to choose their preferred transaction selection strategy when mining a new block. Options include:
  - **Greedy**: Prioritizes transactions with the highest fees.
  - **Altruistic**: Selects transactions based on the longest waiting time.
  - **Random**: Randomly picks transactions from the pool.
  - **Address Preference**: Prefers transactions involving the miner's own address.
- **UI Integration**: Updated the user interface to allow miners to easily select their mining preference.
- **Dynamic Transaction Handling**: Enhanced the system's transaction handling logic to accommodate different mining preferences.

### v1.5/5.3 (New Features)
- **Double-Spending Prevention**: Implemented checks to prevent double-spending within the transaction pool and upon block confirmation.
- **Merkle Root Computation**: Integrated the calculation of the Merkle root for efficient transaction verification and enhanced block integrity.
- **Transaction Validation Rules**: Enforced transaction rules to verify the authenticity and sufficiency of funds before acceptance into the transaction pool.
- **Network Consensus Enforcement**: Codified the rules for network consensus, including proof-of-work requirements and chain validity.

### v1.4 (Previous Features)
- **Multi-Threaded Mining**: Utilizes multiple CPU cores for concurrent mining operations, significantly optimizing block discovery times.
- **Mining Duration Metric**: Records and reports the time taken to mine blocks, providing insight into mining efficiency.

### v1.3 (Previous Features)
- **SHA256 Encryption**: Secures block contents with SHA256 hashing.
- **Immutable Ledger**: Ensures a tamper-evident sequence of blocks using an incremental index and precise timestamps.
- **Public/Private Key Generation**: Generates cryptographic key pairs for transaction security.
- **Transaction Verification**: Validates transactions by verifying digital signatures.
- **Mining Simulation**: Illustrates mining with nonce and difficulty adjustments.
- **Transaction Pool**: Manages pending transactions awaiting block inclusion.

### Upcoming:
- Further enhancements to security measures and network efficiency.
- Advanced features for smart contract functionality.

## Practical Use:
This application serves as a practical tool for exploring blockchain mechanics. It is an invaluable educational asset for students and enthusiasts aiming to understand the inner workings of decentralized ledger systems, cryptographic security, and consensus algorithms in blockchain networks.

## Motivation:
The intent behind this project is to clarify the complexities of blockchain technology for educational purposes. It breaks down intricate blockchain concepts into digestible code segments, facilitating a deeper comprehension of the technology's foundational aspects.

## Usage:
To generate new blocks and interact with the blockchain, instantiate the `Block` class and engage with the application's methods:
```csharp
Block genesisBlock = new Block();
Block nextBlock = new Block(genesisBlock, transactionList, minerAddress, difficulty);


 

