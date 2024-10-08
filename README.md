# Indexer/Search Engine Project

This project is a simple yet effective indexer/search engine that allows users to search through various types of documents, such as text files, PDFs, and others, using an efficient algorithmic approach. It supports recursive folder indexing and implements well-known algorithms like TF-IDF for document indexing and Cosine Similarity for determining document similarity.

**Developers:**
- Daniel Marin
- Jennifer Vicentes

## Table of Contents

- [Introduction](#introduction)
- [Instructions on how to use it](#instructions-on-how-to-use-it)
- [Using the GUI](#using-the-gui)
- [Features](#features)
- [Installation](#installation)
- [Using the CLI](#using-the-cli)
- [Code Structure](#code-structure)
- [License](#license)

## Introduction

The Indexer/Search Engine is designed to help users efficiently retrieve relevant documents based on a given query. It mimics the core principles behind search engines and highlights the importance of data indexing and efficient retrieval.

## Instructions on how to use it

1. **Clone the repository** to your local machine.
2. **Install the required dependencies** using the package manager of your choice (e.g., NuGet for .NET projects).
3. **Run the application** using the provided command line interface or graphical user interface.

## Using the GUI

To use the Graphical User Interface (GUI) of the indexer/search engine:

1. **Launch the Application**: Start the application, and the GUI will load in your web browser.

2. **Select Vectorization Method**:
   - In the GUI, you will see a dropdown menu labeled "Choose Vectorization Method."
   - Select either "TFIDF" or "Vectorizer" based on your preference for indexing.

3. **Choose Distance Metric**:
   - Below the vectorization method, there is another dropdown labeled "Choose Distance Metric."
   - Select either "Cosine" or "Euclidean" to determine how document similarity will be calculated.

4. **Enter Search Query**:
   - In the input box labeled "Enter your search query," type the keywords or phrases you want to search for in the indexed documents.

5. **Perform Search**:
   - Click the "Search" button to initiate the search process.
   - The application will process your query and display the results below.

6. **View Search Results**:
   - If there are matching documents, they will be listed as clickable links.
   - Click on any document title to view its full content.

7. **Handle No Results**:
   - If no results are found, an appropriate message will be displayed indicating that no documents matched your query.

## Features

- Recursive folder indexing
- Support for multiple document types (TXT, CSV, XML, JSON, HTML, PDF)
- TF-IDF and Cosine Similarity algorithms for indexing and searching
- Command Line Interface (CLI) and Graphical User Interface (GUI)

## Installation

To install the project, follow these steps:

1. Ensure you have the .NET SDK installed on your machine.
2. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
3. Navigate to the project directory and restore the dependencies:
   ```bash
   cd <project-directory>
   dotnet restore
   ```

## Using the CLI

To use the indexer/search engine:

1. **Index a folder**:
   - Use the command `index -f <folder_name> -t <type> -dis <distance>` in the CLI.
   - Select the folder containing the documents you want to index.

2. **Load an index**:
   - Use the command `load -p <index_file_name>` to load a previously saved index.

3. **Search for documents**:
   - Use the command `search -q <query> -k <k>` to search for documents matching your query.

## Code Structure

The project is structured into several components:

- **Classes**: Contains the core logic for indexing and searching documents.
- **Components**: Contains the Blazor components for the user interface.
- **UnitTest**: Contains unit tests for validating the functionality of the classes.

## License

N/A